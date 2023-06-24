using System.Collections.ObjectModel;
using MyCollectionShelf.Sql.Object.Book.Class.View;
using MyCollectionShelf.Wpf.Shelf.Book.Object.Class.Static;

namespace MyCollectionShelf.Wpf.Shelf.Book.Ui.Pages;

public partial class BookShelf
{
    public ObservableCollection<VBookShelf> BookShelves;
    public BookShelf()
    {
        var lst = CommonUi.SetCollection<VBookShelf>();
        BookShelves = new ObservableCollection<VBookShelf>(lst);
        
        InitializeComponent();
    }
}