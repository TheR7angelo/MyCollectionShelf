using System;
using System.Drawing;
using System.Windows;
using System.Windows.Threading;
using OpenCvSharp;
using OpenCvSharp.WpfExtensions;
using ZXing;
using ZXing.Common;
using ZXing.Windows.Compatibility;

namespace MyCollectionShelf.Camera.Ui;

public partial class Camera : IDisposable
{
    public static readonly DependencyProperty FpsProperty = DependencyProperty.Register(nameof(Fps), typeof(double),
        typeof(Camera), new PropertyMetadata(30.0));

    public static readonly DependencyProperty EnableBarCodeReaderProperty =
        DependencyProperty.Register(nameof(EnableBarCodeReader), typeof(bool), typeof(Camera),
            new PropertyMetadata(default(bool), PropertyEnableBarCodeReaderChanged));

    private static void PropertyEnableBarCodeReaderChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var sender = d as Camera;
        sender!.EnableBarCodeReader = (bool)e.NewValue;
    }

    public static readonly DependencyProperty BarcodeOptionsProperty =
        DependencyProperty.Register(nameof(BarcodeOptions), typeof(DecodingOptions), typeof(Camera),
            new PropertyMetadata(new DecodingOptions { TryHarder = true }, PropertyDecodingOptions_Changed));

    private static void PropertyDecodingOptions_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var sender = d as Camera;
        sender!.BarcodeReader.Options = (DecodingOptions)e.NewValue;
    }

    private DispatcherTimer Timer { get; } = new();
    private VideoCapture? Capture { get; set; }

    public double Fps
    {
        get => (double)GetValue(FpsProperty);
        set => SetValue(FpsProperty, value);
    }

    public bool EnableBarCodeReader
    {
        get => (bool)GetValue(EnableBarCodeReaderProperty);
        set => SetValue(EnableBarCodeReaderProperty, value);
    }

    public DecodingOptions BarcodeOptions
    {
        get => (DecodingOptions)GetValue(BarcodeOptionsProperty);
        set => SetValue(BarcodeOptionsProperty, value);
    }

    private BarcodeReader<Bitmap> BarcodeReader { get; }

    public delegate void ResultFoundHandler(Camera sender, Result result);

    public event ResultFoundHandler? ResultFound;
    
    public Camera()
    {
        BarcodeReader = new BarcodeReader
        {
            Options = BarcodeOptions
        };
        
        InitializeComponent();

        Timer.Tick += TimerOnTick;
    }

    public void StartCamera(double fps, Object.Structures.VideoCapture videoCapture)
    {
        Capture = new VideoCapture(videoCapture.Index);
        Fps = fps;

        StartCapture();
    }

    public void StartCamera(Object.Structures.VideoCapture videoCapture)
    {
        Capture = new VideoCapture(videoCapture.Index);
        StartCapture();
    }

    private void StartCapture()
    {
        var maxFps = Capture!.Fps;
        if (Fps > maxFps)
        {
            Fps = maxFps;
            Console.WriteLine($"Warning max fps camera is {maxFps}");
        }

        Timer.Interval = TimeSpan.FromMilliseconds(1 / Fps);
        Timer.Start();
    }

    private void TimerOnTick(object? sender, EventArgs e)
    {
        var frame = new Mat();
        Capture?.Read(frame);

        if (frame.Empty()) return;

        var bitmapSource = frame.ToBitmapSource();
        Image.Source = bitmapSource;

        if (!EnableBarCodeReader) return;
        
        var result = BarcodeReader.Decode(bitmapSource);

        if (result is not null)
        {
            ResultFound?.Invoke(this, result);
        }
    }

    public void Dispose()
    {
        Timer.Stop();
        Capture?.Dispose();
    }
}