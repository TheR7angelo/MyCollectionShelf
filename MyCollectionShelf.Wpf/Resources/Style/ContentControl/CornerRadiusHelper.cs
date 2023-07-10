using System.Windows;
using System.Windows.Controls;

namespace MyCollectionShelf.Wpf.Resources.Style.ContentControl;

public static class CornerRadiusHelper
{
    public static readonly DependencyProperty CornerRadiusProperty =
        DependencyProperty.RegisterAttached("CornerRadius", typeof(CornerRadius), typeof(CornerRadiusHelper), new FrameworkPropertyMetadata(default(CornerRadius), FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.SubPropertiesDoNotAffectRender, OnCornerRadiusPropertyChanged));

    public static CornerRadius GetCornerRadius(DependencyObject obj)
    {
        return (CornerRadius)obj.GetValue(CornerRadiusProperty);
    }

    public static void SetCornerRadius(DependencyObject obj, CornerRadius value)
    {
        obj.SetValue(CornerRadiusProperty, value);
    }

    private static void OnCornerRadiusPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        // This method is called when the CornerRadius property is changed
        if (d is Border border)
        {
            border.CornerRadius = (CornerRadius)e.NewValue;
        }
    }
}