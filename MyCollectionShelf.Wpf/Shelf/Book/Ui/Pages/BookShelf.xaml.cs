using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using MyCollectionShelf.Sql;
using MyCollectionShelf.Sql.Object.Book.Class.Table;
using MyCollectionShelf.Sql.Object.Book.Class.View;
using MyCollectionShelf.Wpf.Shelf.Book.Ui.CustomButton;
using MyCollectionShelf.Wpf.Shelf.Common.Static;
using SQLiteNetExtensions.Extensions;

namespace MyCollectionShelf.Wpf.Shelf.Book.Ui.Pages;

public partial class BookShelf
{
    public ObservableCollection<VBookShelf> BookShelves { get; set; }
    
    public BookShelf()
    {
        var vBookShelves = CommonCollection.GetCollection<VBookShelf>().ToList();
        vBookShelves.ToCoverFullPath();

        BookShelves = new ObservableCollection<VBookShelf>(vBookShelves);
        
        InitializeComponent();
    }

    private void ShelfBook_OnClick(object sender, RoutedEventArgs e)
    {
        using var sqlHandler = new SqlMainHandler();
        var db = sqlHandler.GetSqlConnection();
        
        var shelfBook = (ButtonShelfBook)sender;
        var bookShelf = shelfBook.VBookShelf;
        
        var bookInformations = db.GetAllWithChildren<BookInformations>(s => s.BookSeriesId.Equals(bookShelf.BookSeriesId));
        bookInformations.ToCoverFullPath();

        var bookShelfSerie = new BookShelfSeries
        {
            VBookShelf = bookShelf,
            BookInformations = new ObservableCollection<BookInformations>(bookInformations)
        };
        
        bookShelfSerie.Navigate();
    }
}