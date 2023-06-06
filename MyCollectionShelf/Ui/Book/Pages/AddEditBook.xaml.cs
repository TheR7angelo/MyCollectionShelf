using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using MyCollectionShelf.Book.Object.Class;

namespace MyCollectionShelf.Ui.Book.Pages;

public partial class AddEditBook
{
    public static readonly DependencyProperty AuthorsListProperty = DependencyProperty.Register(nameof(AuthorsList),
        typeof(ObservableCollection<BookAuthors>), typeof(AddEditBook),
        new PropertyMetadata(new ObservableCollection<BookAuthors>()));

    public static readonly DependencyProperty BookDataProperty = DependencyProperty.Register(nameof(BookData),
        typeof(MyCollectionShelf.Book.Object.Class.Book), typeof(AddEditBook),
        new PropertyMetadata(new MyCollectionShelf.Book.Object.Class.Book()));
    
    public AddEditBook()
    {
        AuthorsList = new ObservableCollection<BookAuthors>
        {
            new() { FamilyName = "test family", Name = "prenom" },
            new() { FamilyName = "test family2", Name = "prenom2" },
            new() { FamilyName = "test family3", Name = "prenom3" },
        };
        
        BookData = new MyCollectionShelf.Book.Object.Class.Book
        {
            BookInformations = new BookInformations
            {
                Title = "yolo",
                Authors = new ObservableCollection<BookAuthors>
                {
                    AuthorsList.First(),
                    AuthorsList.Last()
                }
            }
        };
        
        InitializeComponent();
    }

    public MyCollectionShelf.Book.Object.Class.Book BookData
    {
        get => (MyCollectionShelf.Book.Object.Class.Book)GetValue(BookDataProperty);
        set => SetValue(BookDataProperty, value);
    }

    public ObservableCollection<BookAuthors> AuthorsList
    {
        get => (ObservableCollection<BookAuthors>)GetValue(AuthorsListProperty);
        set => SetValue(AuthorsListProperty, value);
    }

    private void Selector_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var z = sender;
    }
}