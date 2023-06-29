using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using MyCollectionShelf.Sql.Object.Book.Class.Table;
using MyCollectionShelf.Sql.Object.Book.Class.View;
using MyCollectionShelf.Wpf.Shelf.Book.Ui.CustomButton;
using MyCollectionShelf.Wpf.Shelf.Common.Comparator;

namespace MyCollectionShelf.Wpf.Shelf.Book.Ui.Pages;

public partial class BookShelfSeries
{
    public static readonly DependencyProperty VBookShelfProperty = DependencyProperty.Register(nameof(VBookShelf),
        typeof(VBookShelf), typeof(BookShelfSeries), new PropertyMetadata(default(VBookShelf), VBookShelfPropertyChanged_Callback));

    private static void VBookShelfPropertyChanged_Callback(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var sender = (BookShelfSeries)d;
        sender.VBookShelfOriginal = sender.VBookShelf.DeepCopy();
    }

    public static readonly DependencyProperty BookInformationsProperty =
        DependencyProperty.Register(nameof(BookInformations), typeof(ObservableCollection<BookInformations>),
            typeof(BookShelfSeries), new PropertyMetadata(default(ObservableCollection<BookInformations>)));

    private VBookShelf? VBookShelfOriginal { get; set; }
    
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

    private void ImageCover_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        if ((bool)ToggleEdit.IsChecked!)
        {
            Console.WriteLine("hey");
        }
        else
        {
            Console.WriteLine("nop");
        }
    }

    private void ToggleEdit_OnUnchecked(object sender, RoutedEventArgs e)
    {
        var differences = VBookShelfOriginal.Compare(VBookShelf);

        if (differences.Count.Equals(0)) return;
        
        Console.WriteLine("dif");
    }
}