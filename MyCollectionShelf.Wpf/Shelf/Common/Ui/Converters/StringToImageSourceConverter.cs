using System;
using System.Globalization;
using System.IO;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace MyCollectionShelf.Wpf.Shelf.Common.Ui.Converters;

public class StringToImageSourceConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is not string str) return DependencyProperty.UnsetValue;
        if (string.IsNullOrEmpty(str)) return DependencyProperty.UnsetValue;
        
        try
        {
            using var fileStream = new FileStream(str, FileMode.Open, FileAccess.Read);
            
            var bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();
            bitmapImage.StreamSource = fileStream;
            bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
            bitmapImage.EndInit();

            return bitmapImage;
        }
        catch (Exception ex)
        {
            // Gérer l'exception (par exemple, journaliser l'erreur)
            Console.WriteLine($"Erreur lors de la conversion de l'URI en ImageSource : {ex.Message}");
        }

        return DependencyProperty.UnsetValue;
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotSupportedException();
    }
}