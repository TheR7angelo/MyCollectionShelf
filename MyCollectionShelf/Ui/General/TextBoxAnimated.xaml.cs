using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace MyCollectionShelf.Ui.General;

public partial class TextBoxAnimated
{
    private static bool _doneAnimation;
    
    public static readonly DependencyProperty TextBoxTextProperty = DependencyProperty.Register(nameof(TextBoxText),
        typeof(string), typeof(TextBoxAnimated), new PropertyMetadata(default(string)));

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

    private void TextBoxBase_OnTextChanged(object sender, TextChangedEventArgs e)
    {
        var fadeIn = new DoubleAnimation(0, 1, TextBoxAnimatedAnimationDuration);
        var fadeOut = new DoubleAnimation(1, 0, TextBoxAnimatedAnimationDuration);

        if (string.IsNullOrEmpty(TextBoxText))
        {
            LabelWatermark.BeginAnimation(OpacityProperty, fadeIn);
            LabelWatermarkTitle.BeginAnimation(OpacityProperty, fadeOut);
            _doneAnimation = false;
            LabelWatermarkTitle.Visibility = Visibility.Collapsed;
        }
        else if (!_doneAnimation)
        {
            LabelWatermark.BeginAnimation(OpacityProperty, fadeOut);
            LabelWatermarkTitle.BeginAnimation(OpacityProperty, fadeIn);
            _doneAnimation = true;
            LabelWatermarkTitle.Visibility = Visibility.Visible;
        }
    }
}