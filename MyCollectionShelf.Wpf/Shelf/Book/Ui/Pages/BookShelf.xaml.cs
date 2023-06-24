using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using MyCollectionShelf.Sql.Object.Book.Class.View;
using MyCollectionShelf.Wpf.Shelf.Book.Ui.CustomButton;
using MyCollectionShelf.Wpf.Shelf.Common.Static;

namespace MyCollectionShelf.Wpf.Shelf.Book.Ui.Pages;

public partial class BookShelf
{
    public ObservableCollection<VBookShelf> BookShelves { get; set; }
    
    public BookShelf()
    {
        var vBookShelves = CommonCollection.SetCollection<VBookShelf>().ToList();
        vBookShelves.UpdateCollection();

        BookShelves = new ObservableCollection<VBookShelf>(vBookShelves);
        
        InitializeComponent();
    }

    private void ShelfBook_OnClick(object sender, RoutedEventArgs e)
    {
        var shelfBook = (ButtonShelfBook)sender;
        Console.WriteLine(shelfBook.VBookShelf.BookSeriesTitle);
    }
}