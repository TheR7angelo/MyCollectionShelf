using System;
using System.Collections;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using MyCollectionShelf.Book.Object.Class;

namespace MyCollectionShelf.Wpf.Ui.Book.UserControls.BookInformation;

public partial class BookInformationEdit
{
    public static readonly DependencyProperty BookInformationDataProperty =
        DependencyProperty.Register(nameof(BookInformationData), typeof(BookInformations), typeof(BookInformationEdit),
            new PropertyMetadata(default(BookInformations)));

    public BookInformationEdit()
    {
        InitializeComponent();
    }

    public BookInformations BookInformationData
    {
        get => (BookInformations)GetValue(BookInformationDataProperty);
        set => SetValue(BookInformationDataProperty, value);
    }

    private void ButtonAuthors_OnClick(object sender, RoutedEventArgs e)
    {
        var button = (Button)sender;
        var content = (char)button.Content;
        var item = (BookAuthors)button.DataContext;
        
        if (content == '+')
        {
            BookInformationData.Authors.Add(new BookAuthors());
        }
        else
        {
            BookInformationData.Authors.Remove(item);
        }
    }

    private void ButtonGenres_OnClick(object sender, RoutedEventArgs e)
    {
        var button = (Button)sender;
        var content = (char)button.Content;
        var item = (string)button.DataContext;
        
        if (content == '+')
        {
            BookInformationData.Genres.Add(string.Empty);
        }
        else
        {
            BookInformationData.Genres.Remove(item);
        }
    }
}

internal class ItemToImageConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var listBoxItem = (ListBoxItem)value;

        var sourceItems = (IList)listBoxItem.Tag;
        var item = listBoxItem.DataContext;

        var index = sourceItems.IndexOf(item);

        return index == 0 ? '+' : '-';
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}