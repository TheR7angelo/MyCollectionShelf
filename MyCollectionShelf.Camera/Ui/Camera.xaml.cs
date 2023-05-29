using System;
using System.Windows;
using System.Windows.Threading;
using MyCollectionShelf.Camera.Object.Structures;
using OpenCvSharp;
using OpenCvSharp.WpfExtensions;

namespace MyCollectionShelf.Camera.Ui;

public partial class Camera : IDisposable
{
    public static readonly DependencyProperty FpsProperty = DependencyProperty.Register(nameof(Fps), typeof(double),
        typeof(Camera), new PropertyMetadata(30.0));

    private DispatcherTimer Timer { get; } = new();
    private VideoCapture? Capture { get; set; }

    public double Fps
    {
        get => (double)GetValue(FpsProperty);
        set => SetValue(FpsProperty, value);
    }

    public Camera()
    {
        InitializeComponent();
        Timer.Tick += TimerOnTick;
    }

    public void StartCamera(double fps, SVideoCaptureEnum videoCaptureEnum)
    {
        Capture = new VideoCapture(videoCaptureEnum.Index);
        Fps = fps;
        
        StartCapture();
    }
    
    public void StartCamera(SVideoCaptureEnum videoCaptureEnum)
    {
        Capture = new VideoCapture(videoCaptureEnum.Index);
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

        var bitmapSource = BitmapSourceConverter.ToBitmapSource(frame);
        Image.Source = bitmapSource;
    }

    public void Dispose()
    {
        Timer.Stop();
        Capture?.Dispose();
    }
}