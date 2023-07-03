using System;
using System.Windows.Data;

namespace MyCollectionShelf.Wpf.Shelf.Common.Converters;

[ValueConversion(typeof(bool), typeof(bool))]
public class InverseBooleanConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, System.Globalization.CultureInfo culture)
    {
        // if (targetType == typeof(bool)) throw new InvalidOperationException("The target must be a boolean");

        var b = value is not null && (bool)value;

        return !b;
    }

    public object ConvertBack(object? value, Type targetType, object? parameter,
        System.Globalization.CultureInfo culture)
    {
        throw new NotSupportedException();
    }
}