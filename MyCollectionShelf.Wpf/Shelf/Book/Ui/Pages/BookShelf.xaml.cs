using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;
using MyCollectionShelf.Sql.Object.Book.Class.View;
using MyCollectionShelf.Wpf.Shelf.Book.Ui.UserControls.CustomButton;
using MyCollectionShelf.Wpf.Shelf.Common.Static;

namespace MyCollectionShelf.Wpf.Shelf.Book.Ui.Pages;

public partial class BookShelf
{
    public ObservableCollection<VBookShelf> BookShelves;
    
    public BookShelf()
    {
        var vBookShelves = CommonCollection.SetCollection<VBookShelf>().ToList();
        
        foreach (var vBookShelf in vBookShelves)
        {
            vBookShelf.BookSeriesCover = Path.GetFullPath(vBookShelf.BookSeriesCover);
        }

        BookShelves = new ObservableCollection<VBookShelf>(vBookShelves);
        
        InitializeComponent();

        ShelfBook.VBookShelf = BookShelves.First();
    }

    private void ShelfBook_OnClick(object sender, RoutedEventArgs e)
    {
        var shelfBook = (ButtonShelfBook)sender;
        Console.WriteLine(shelfBook.VBookShelf.BookSeriesTitle);
    }
}