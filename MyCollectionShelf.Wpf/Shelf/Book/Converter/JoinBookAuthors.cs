using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Data;
using MyCollectionShelf.Sql.Object.Book.Class.Table;

namespace MyCollectionShelf.Wpf.Shelf.Book.Converter;

public class JoinBookAuthors : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is null) return string.Empty;

        var values = (IEnumerable<BookAuthor>)value;
        var results = string.Join("; ", values.Select(s => s.NameConcat));

        return results;
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}