using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace MyCollectionShelf.Wpf.Ui.Common.UserControls;

public partial class TextBoxAnimated
{
    private static bool _doneAnimation;

    public static readonly DependencyProperty TextBoxTextProperty = DependencyProperty.Register(nameof(TextBoxText),
        typeof(string), typeof(TextBoxAnimated),
        new PropertyMetadata(default(string), Property_TextBoxTextProperty_ChangedCallback));

    private static void Property_TextBoxTextProperty_ChangedCallback(DependencyObject d,
        DependencyPropertyChangedEventArgs e)
    {
        var sender = (TextBoxAnimated)d;
        sender.Update();
    }

    public static readonly DependencyProperty WaterMarkForegroundColorProperty = DependencyProperty.Register(
        nameof(WaterMarkForegroundColor), typeof(Brush), typeof(TextBoxAnimated),
        new PropertyMetadata(Brushes.Gray));

    public static readonly DependencyProperty WaterMarkContentProperty =
        DependencyProperty.Register(nameof(WaterMarkContent), typeof(string), typeof(TextBoxAnimated),
            new PropertyMetadata(default(string)));

    public static readonly DependencyProperty WaterMarkTitleForegroundColorProperty =
        DependencyProperty.Register(nameof(WaterMarkTitleForegroundColor), typeof(Brush),
            typeof(TextBoxAnimated), new PropertyMetadata(Brushes.Gray));

    public static readonly DependencyProperty WaterMarkTitleFontSizeProperty =
        DependencyProperty.Register(nameof(WaterMarkTitleFontSize), typeof(double), typeof(TextBoxAnimated),
            new PropertyMetadata(10.0));

    public static readonly DependencyProperty TextBoxAnimatedThicknessProperty = DependencyProperty.Register(
        nameof(TextBoxAnimatedThickness),
        typeof(Thickness), typeof(TextBoxAnimated), new PropertyMetadata(new Thickness(1)));

    public static readonly DependencyProperty TextBoxAnimatedBorderBrushProperty =
        DependencyProperty.Register(nameof(TextBoxAnimatedBorderBrush), typeof(Brush), typeof(TextBoxAnimated),
            new PropertyMetadata(default(Brush)));

    public static readonly DependencyProperty TextBoxAnimatedCornerRadiusProperty =
        DependencyProperty.Register(nameof(TextBoxAnimatedCornerRadius), typeof(CornerRadius), typeof(TextBoxAnimated),
            new PropertyMetadata(default(CornerRadius)));

    public static readonly DependencyProperty TextBoxAnimatedAnimationDurationProperty =
        DependencyProperty.Register(nameof(TextBoxAnimatedAnimationDuration), typeof(Duration), typeof(TextBoxAnimated),
            new PropertyMetadata(new Duration(TimeSpan.FromMilliseconds(750))));

    public static readonly DependencyProperty TextBoxAnimatedAcceptsReturnProperty =
        DependencyProperty.Register(nameof(TextBoxAnimatedAcceptsReturn), typeof(bool), typeof(TextBoxAnimated),
            new PropertyMetadata(default(bool)));

    public static readonly DependencyProperty TextBoxAnimatedAcceptsTabProperty =
        DependencyProperty.Register(nameof(TextBoxAnimatedAcceptsTab), typeof(bool), typeof(TextBoxAnimated),
            new PropertyMetadata(default(bool)));

    public TextBoxAnimated()
    {
        InitializeComponent();
    }

    public string TextBoxText
    {
        get => (string)GetValue(TextBoxTextProperty);
        set => SetValue(TextBoxTextProperty, value);
    }

    public Brush WaterMarkForegroundColor
    {
        get => (Brush)GetValue(WaterMarkForegroundColorProperty);
        set => SetValue(WaterMarkForegroundColorProperty, value);
    }

    public string WaterMarkContent
    {
        get => (string)GetValue(WaterMarkContentProperty);
        set => SetValue(WaterMarkContentProperty, value);
    }

    public Brush WaterMarkTitleForegroundColor
    {
        get => (Brush)GetValue(WaterMarkTitleForegroundColorProperty);
        set => SetValue(WaterMarkTitleForegroundColorProperty, value);
    }

    public double WaterMarkTitleFontSize
    {
        get => (double)GetValue(WaterMarkTitleFontSizeProperty);
        set => SetValue(WaterMarkTitleFontSizeProperty, value);
    }

    public Thickness TextBoxAnimatedThickness
    {
        get => (Thickness)GetValue(TextBoxAnimatedThicknessProperty);
        set => SetValue(TextBoxAnimatedThicknessProperty, value);
    }

    public Brush TextBoxAnimatedBorderBrush
    {
        get => (Brush)GetValue(TextBoxAnimatedBorderBrushProperty);
        set => SetValue(TextBoxAnimatedBorderBrushProperty, value);
    }

    public CornerRadius TextBoxAnimatedCornerRadius
    {
        get => (CornerRadius)GetValue(TextBoxAnimatedCornerRadiusProperty);
        set => SetValue(TextBoxAnimatedCornerRadiusProperty, value);
    }

    public Duration TextBoxAnimatedAnimationDuration
    {
        get => (Duration)GetValue(TextBoxAnimatedAnimationDurationProperty);
        set => SetValue(TextBoxAnimatedAnimationDurationProperty, value);
    }

    public bool TextBoxAnimatedAcceptsReturn
    {
        get => (bool)GetValue(TextBoxAnimatedAcceptsReturnProperty);
        set => SetValue(TextBoxAnimatedAcceptsReturnProperty, value);
    }

    public bool TextBoxAnimatedAcceptsTab
    {
        get => (bool)GetValue(TextBoxAnimatedAcceptsTabProperty);
        set => SetValue(TextBoxAnimatedAcceptsTabProperty, value);
    }

    private void TextBoxBase_OnTextChanged(object sender, TextChangedEventArgs e)
        => Update();

    private void Update()
    {
        var fadeIn = new DoubleAnimation(0, 1, TextBoxAnimatedAnimationDuration);
        var fadeOut = new DoubleAnimation(1, 0, TextBoxAnimatedAnimationDuration);

        if (string.IsNullOrEmpty(TextBoxText))
        {
            LabelWatermark.BeginAnimation(OpacityProperty, fadeIn);
            LabelWatermarkTitle.BeginAnimation(OpacityProperty, fadeOut);
            _doneAnimation = true;
            LabelWatermarkTitle.Visibility = Visibility.Collapsed;
        }
        else if (_doneAnimation || (!_doneAnimation && TextBoxText.Length == 1))
        {
            LabelWatermark.BeginAnimation(OpacityProperty, fadeOut);
            LabelWatermarkTitle.BeginAnimation(OpacityProperty, fadeIn);
            _doneAnimation = false;
            LabelWatermarkTitle.Visibility = Visibility.Visible;
        }
    }
}