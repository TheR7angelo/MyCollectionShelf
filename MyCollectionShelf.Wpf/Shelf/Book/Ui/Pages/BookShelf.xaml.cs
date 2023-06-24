using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using MyCollectionShelf.Sql.Object.Book.Class.View;
using MyCollectionShelf.Wpf.Shelf.Book.Object.Class.Static;

namespace MyCollectionShelf.Wpf.Shelf.Book.Ui.Pages;

public partial class BookShelf
{
    public ObservableCollection<VBookShelf> BookShelves;
    
    public BookShelf()
    {
        var vBookShelves = CommonUi.SetCollection<VBookShelf>().ToList();
        
        foreach (var vBookShelf in vBookShelves)
        {
            vBookShelf.BookSeriesCover = Path.GetFullPath(vBookShelf.BookSeriesCover);
        }

        BookShelves = new ObservableCollection<VBookShelf>(vBookShelves);
        
        InitializeComponent();

        ShelfBook.VBookShelf = BookShelves.First();
    }
}