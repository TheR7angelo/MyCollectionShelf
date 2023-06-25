using System;
using System.Collections.ObjectModel;
using System.Windows;
using MyCollectionShelf.Sql.Object.Book.Class.Table;
using MyCollectionShelf.Sql.Object.Book.Class.View;
using MyCollectionShelf.Wpf.Shelf.Book.Ui.CustomButton;

namespace MyCollectionShelf.Wpf.Shelf.Book.Ui.Pages;

public partial class BookShelfSeries
{
    public static readonly DependencyProperty VBookShelfProperty = DependencyProperty.Register(nameof(VBookShelf),
        typeof(VBookShelf), typeof(BookShelfSeries), new PropertyMetadata(default(VBookShelf)));

    public static readonly DependencyProperty BookInformationsProperty =
        DependencyProperty.Register(nameof(BookInformations), typeof(ObservableCollection<BookInformations>),
            typeof(BookShelfSeries), new PropertyMetadata(default(ObservableCollection<BookInformations>)));

    public BookShelfSeries()
    {
        InitializeComponent();
    }

    public VBookShelf VBookShelf
    {
        get => (VBookShelf)GetValue(VBookShelfProperty);
        init => SetValue(VBookShelfProperty, value);
    }

    public ObservableCollection<BookInformations> BookInformations
    {
        get => (ObservableCollection<BookInformations>)GetValue(BookInformationsProperty);
        set => SetValue(BookInformationsProperty, value);
    }

    private void ButtonShelfTome_OnClick(object sender, RoutedEventArgs e)
    {
        var button = (ButtonShelfTome)sender;
        Console.WriteLine(button.BookInformations.TomeNumber);
    }
}