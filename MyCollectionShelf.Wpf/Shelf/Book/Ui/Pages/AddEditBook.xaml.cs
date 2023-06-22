using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MyCollectionShelf.Book;
using MyCollectionShelf.Book.Object.Enum;
using MyCollectionShelf.Book.Object.Static_Class;
using MyCollectionShelf.Sql;
using MyCollectionShelf.Sql.Object.Book.Class.Table;
using MyCollectionShelf.Wpf.Shelf.Book.Object.Class.Static;
using MyCollectionShelf.Wpf.Shelf.Book.Ui.Window;
using MyCollectionShelf.Wpf.Shelf.Common.Controls.Buttons;
using SQLiteNetExtensions.Extensions;

namespace MyCollectionShelf.Wpf.Shelf.Book.Ui.Pages;

public partial class AddEditBook
{
    public static readonly DependencyProperty BookDataProperty = DependencyProperty.Register(nameof(BookData),
        typeof(Sql.Object.Book.Class.Table.Book), typeof(AddEditBook),
        new PropertyMetadata(new Sql.Object.Book.Class.Table.Book()));

    public AddEditBook()
    {
        using var sqlHandler = new SqlMainHandler();
        var db = sqlHandler.GetSqlConnection();
        
        AuthorsList.SetCollection(db);
        EditorsList.SetCollection(db);
        GenresList.SetCollection(db);
        SeriesList.SetCollection(db);
        
        InitializeComponent();
    }

    public Sql.Object.Book.Class.Table.Book BookData
    {
        get => (Sql.Object.Book.Class.Table.Book)GetValue(BookDataProperty);
        set => SetValue(BookDataProperty, value);
    }

    public ObservableCollection<BookAuthor> AuthorsList { get; set; } = new();

    public ObservableCollection<BookEditorList> EditorsList { get; set; } = new();

    public ObservableCollection<BookGenre> GenresList { get; set; } = new();

    public ObservableCollection<BookSeries> SeriesList { get; set; } = new();

    private void OnlyNumber_OnTextChanged(object sender, TextCompositionEventArgs e)
    {
        e.Handled = e.Text.IsNumber();
    }

    private async void ButtonScan_OnClick(object sender, RoutedEventArgs e)
    {
        using var scanner = new CameraScan();
        if (scanner.ShowDialog() != true) return;

        var isbn = scanner.Isbn;
        await GetIsbnBook(isbn);
    }

    private async void ButtonISBN_OnClick(object sender, RoutedEventArgs e)
    {
        var isbn = BookData.BookInformations.Isbn;
        if (string.IsNullOrEmpty(isbn)) return;

        await GetIsbnBook(isbn);
    }

    private async void IsbnSearch_OnKeyDown(object sender, KeyEventArgs e)
    {
        if (!e.Key.Equals(Key.Enter)) return;

        var isbn = BookData.BookInformations.Isbn;
        if (string.IsNullOrEmpty(isbn)) return;

        var book = await GetBook(isbn);
        SetBook(book);
    }

    private void ButtonAddRemoveAuthors_OnClick(object sender, RoutedEventArgs e)
    {
        var button = (ButtonAddRemove)sender;
        var function = button.Mode;
        var collection = (ObservableCollection<BookAuthor>)button.Tag;

        var item = button.FindParent<Grid>()!.Children.OfType<ComboBox>().First().DataContext as BookAuthor;

        collection.AddRemoveList(function, item, new BookAuthor());
    }

    private void ButtonAddRemoveGenres_OnClick(object sender, RoutedEventArgs e)
    {
        var button = (ButtonAddRemove)sender;
        var function = button.Mode;
        var collection = (ObservableCollection<BookGenre>)button.Tag;

        var item = button.FindParent<Grid>()!.Children.OfType<ComboBox>().First().DataContext as BookGenre;

        collection.AddRemoveList(function, item, new BookGenre());
    }
    
    private void ButtonSelectPicture_OnClick(object sender, RoutedEventArgs e)
    {
        Console.WriteLine("heyy");
    }

    private void ButtonValidBook_OnClick(object sender, RoutedEventArgs e)
    {
        const string coverName = "cover";
        
        if (string.IsNullOrEmpty(BookData.BookInformations.BookSeries.Title)) return;
        
        var scan = BookData.BookInformations.BookCover.Storage;
        if (!string.IsNullOrEmpty(scan))
        {
            var fileExtension = Path.GetExtension(Path.GetFileName(scan));
            
            var collectionPath = Path.Combine("Collection", "Book", BookData.BookInformations.BookSeries.Title);
            var collectionFullPath = Path.GetFullPath(collectionPath);
            Directory.CreateDirectory(collectionFullPath);
            
            var coverPath = Path.Combine(collectionPath, $"{coverName}{fileExtension}");
            var coverExist = File.Exists(coverPath);
            if (!coverExist)
            {
                var coverPath = Path.Combine(collectionPath, $"{coverName}{fileExtension}");
                BookData.BookInformations.BookSeries.SeriesCover = coverPath;
                
                var coverFullPath = Path.Combine(collectionFullPath, $"{coverName}{fileExtension}");
                File.Copy(scan, coverFullPath);
            }
            BookData.BookInformations.BookSeries.SeriesCover = coverPath;

            var filePath = Path.Combine(collectionFullPath, $"{BookData.BookInformations.Isbn}{fileExtension}");
            var fileCollectionPath = Path.Combine(collectionPath, $"{BookData.BookInformations.Isbn}{fileExtension}");
            File.Copy(scan, filePath, true);

            BookData.BookInformations.BookCover.Storage = fileCollectionPath;
        }
        
        using var sqlHandler = new SqlMainHandler();
        var db = sqlHandler.GetSqlConnection();
        
        db.InsertOrReplaceWithChildren(BookData, true);
        // todo ne fonctionne pas il faut le faire à la main ...
        
        MessageBox.Show("Livre importé");
    }

    private async Task GetIsbnBook(string isbn)
    {
        ButtonValid.Visibility = Visibility.Hidden;
        BusySpinner.Visibility = Visibility.Visible;

        var book = await GetBook(isbn);
        SetBook(book);

        ButtonValid.Visibility = Visibility.Visible;
        BusySpinner.Visibility = Visibility.Hidden;
    }
    
    private static async Task<Sql.Object.Book.Class.Table.Book> GetBook(string isbn)
    {
        var api = new BookAllApi();
        var book = await api.GetBookInformation(isbn);

        var tempPicture = CommonPath.GetTemporaryCoverFilePath();
        var sucess = await book.BookInformations.BookCover.DownloadCover(EBookCoverSize.ExtraLarge, tempPicture);

        if (sucess)
        {
            book.BookInformations.BookCover.Storage = tempPicture;
        }

        return book;
    }

    private void SetBook(Sql.Object.Book.Class.Table.Book book)
    {
        foreach (var author in book.BookInformations.BookAuthors.Where(s => !s.NameConcat.Equals(", ")))
        {
            if (!AuthorsList.Contains(author)) AuthorsList.Add(author);
        }
        
        foreach (var genre in book.BookInformations.BookGenres.Where(s => !s.Genre.Equals(string.Empty)))
        {
            if (!GenresList.Contains(genre)) GenresList.Add(genre);
        }
        
        if (book.BookInformations.BookEditor.Editor is not null && !EditorsList.Contains(book.BookInformations.BookEditor))
        {
            if (!EditorsList.Contains(book.BookInformations.BookEditor))
                EditorsList.Add(book.BookInformations.BookEditor);
        }
        
        if (book.BookInformations.BookSeries.Title is not null && !book.BookInformations.BookSeries.Title.Equals(string.Empty))
        {
            if (!SeriesList.Contains(book.BookInformations.BookSeries))
                SeriesList.Add(book.BookInformations.BookSeries);
        }

        BookData = book;
    }

    private void Selector_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var comboBox = sender as ComboBox;
        
        var selectedBookAuthor = (BookAuthor)comboBox!.SelectedItem!;
        
        var listBoxItem = comboBox.FindParent<ListBoxItem>();
        var listBox = listBoxItem!.FindParent<ListBox>();
        if (listBox == null) return;
        
        var index = listBox.ItemContainerGenerator.IndexFromContainer(listBoxItem!);

        BookData.BookInformations.BookAuthors[index] = selectedBookAuthor;
    }
}