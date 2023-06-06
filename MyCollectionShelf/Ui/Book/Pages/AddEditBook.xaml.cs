using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
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

    public static readonly DependencyProperty EditorListProperty = DependencyProperty.Register(nameof(EditorList),
        typeof(ObservableCollection<string>), typeof(AddEditBook),
        new PropertyMetadata(default(ObservableCollection<string>)));

    public static readonly DependencyProperty GenresListProperty = DependencyProperty.Register(nameof(GenresList),
        typeof(ObservableCollection<string>), typeof(AddEditBook), new PropertyMetadata(default(ObservableCollection<string>)));

    public static readonly DependencyProperty SeriesListProperty = DependencyProperty.Register(nameof(SeriesList), typeof(ObservableCollection<string>), typeof(AddEditBook), new PropertyMetadata(default(ObservableCollection<string>)));

    public AddEditBook()
    {
        AuthorsList = new ObservableCollection<BookAuthors>
        {
            new() { FamilyName = "test family", Name = "prenom" },
            new() { FamilyName = "test family2", Name = "prenom2" },
            new() { FamilyName = "test family3", Name = "prenom3" },
        };

        EditorList = new ObservableCollection<string>
        {
            "Tst", "test", "yolo"
        };

        GenresList = new ObservableCollection<string>
        {
            "Fantastique", "Génial", "Merde ?"
        };

        SeriesList = new ObservableCollection<string>
        {
            "Aucune idée", "Bof", "Non renseigner"
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
                },
                Editor = EditorList.First(),
                Genres = new ObservableCollection<string>
                {
                    GenresList.First(),
                    GenresList.Last()
                },
                Series = SeriesList.First(),
                Summarize = "Je suis un résumée à la con hehe"
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

    public ObservableCollection<string> EditorList
    {
        get => (ObservableCollection<string>)GetValue(EditorListProperty);
        set => SetValue(EditorListProperty, value);
    }

    public ObservableCollection<string> GenresList
    {
        get => (ObservableCollection<string>)GetValue(GenresListProperty);
        set => SetValue(GenresListProperty, value);
    }

    public ObservableCollection<string> SeriesList
    {
        get => (ObservableCollection<string>)GetValue(SeriesListProperty);
        set => SetValue(SeriesListProperty, value);
    }
}