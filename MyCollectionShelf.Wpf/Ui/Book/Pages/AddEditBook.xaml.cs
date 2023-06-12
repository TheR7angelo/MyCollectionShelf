using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MyCollectionShelf.Book;
using MyCollectionShelf.Book.Object.Class;
using MyCollectionShelf.Book.Object.Static_Class;
using MyCollectionShelf.WebApi.Object.Book.Class.Json;
using MyCollectionShelf.WebApi.Object.Book.Enum;
using MyCollectionShelf.Wpf.Object.StaticClass;
using MyCollectionShelf.Wpf.Ui.Book.Window;

namespace MyCollectionShelf.Wpf.Ui.Book.Pages;

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
        new PropertyMetadata(new ObservableCollection<string>()));

    public static readonly DependencyProperty GenresListProperty = DependencyProperty.Register(nameof(GenresList),
        typeof(ObservableCollection<string>), typeof(AddEditBook),
        new PropertyMetadata(new ObservableCollection<string>()));

    public static readonly DependencyProperty SeriesListProperty = DependencyProperty.Register(nameof(SeriesList),
        typeof(ObservableCollection<string>), typeof(AddEditBook),
        new PropertyMetadata(new ObservableCollection<string>()));

    public AddEditBook()
    {
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

    private void OnlyNumber_OnTextChanged(object sender, TextCompositionEventArgs e)
    {
        var regex = IsNumber();
        e.Handled = regex.IsMatch(e.Text);
    }

    [GeneratedRegex("[^0-9]+")]
    private static partial Regex IsNumber();

    private async void ButtonScan_OnClick(object sender, RoutedEventArgs e)
    {
        using var scanner = new CameraScan();

        if (scanner.ShowDialog() != true) return;

        var isbn = scanner.Isbn;

        var book = await GetBook(isbn);
        SetBook(book);
    }
    
    private async void ButtonISBN_OnClick(object sender, RoutedEventArgs e)
    {
        var isbn = BookData.BookInformations.Isbn;
        if (string.IsNullOrEmpty(isbn)) return;

        var book = await GetBook(isbn);
        SetBook(book);
    }

    private async void IsbnSearch_OnKeyDown(object sender, KeyEventArgs e)
    {
        if (!e.Key.Equals(Key.Enter)) return;
        
        var isbn = BookData.BookInformations.Isbn;
        if (string.IsNullOrEmpty(isbn)) return;

        var book = await GetBook(isbn);
        SetBook(book);
    }
    
    private static async Task<MyCollectionShelf.Book.Object.Class.Book> GetBook(string isbn)
    {
        var api = new BookAllApi();
        var book = await api.GetBookInformation(isbn);
        
        var tempPicture = CommonPath.GetTemporaryCoverFilePath();
        var sucess = await book.BookInformations.BookCover.DownloadCover(EBookCoverSize.ExtraLarge, tempPicture);
        
        if (sucess)
        {
            book.BookInformations.BookCover.Storage = new Uri(tempPicture);
        }

        return book;
    }
    
    private void SetBook(MyCollectionShelf.Book.Object.Class.Book book)
    {
        foreach (var author in book.BookInformations.Authors.Where(s => !s.NameConcat.Equals(", ")))
        {
            if (!AuthorsList.Contains(author)) AuthorsList.Add(author);
        }

        foreach (var genre in book.BookInformations.Genres.Where(s => !s.Equals(string.Empty)))
        {
            if (!GenresList.Contains(genre)) GenresList.Add(genre);
        }

        if (book.BookInformations.Editor is not null && !EditorList.Contains(book.BookInformations.Editor))
        {
            if (!EditorList.Contains(book.BookInformations.Editor))
                EditorList.Add(book.BookInformations.Editor);
        }

        if (book.BookInformations.Series is not null && !book.BookInformations.Series.Equals(string.Empty))
        {
            if (!SeriesList.Contains(book.BookInformations.Series))
                SeriesList.Add(book.BookInformations.Series);
        }

        BookData = book;
    }

    private void ButtonSelectPicture_OnClick(object sender, RoutedEventArgs e)
    {
        Console.WriteLine("heyy");
    }

    private void ButtonAddRemoveGenres_OnClick(object sender, RoutedEventArgs e)
    {
        var button = (Button)sender;
        var function = (string)button.Content;
        var collection = (ObservableCollection<string>)button.Tag;
        
        var combo = button.FindParent<Grid>()!.Children.OfType<ComboBox>().First();
        var item = combo.Text ?? string.Empty;
        
        var newValue = string.Empty;
        
        AddRemoveList(collection, function, item, newValue);
    }

    private static void AddRemoveList<T>(object collection, string function, T item, T newItem)
    {
        var zcollection = collection as ObservableCollection<T>;

        if (function.Equals("-"))
        {
            zcollection!.Remove(item);
        }
        else
        {
            zcollection!.Add(newItem);
        }
        // var button = (Button)sender;
        //
        // if (button.Content.Equals("-"))
        // {
        //     var tag = button.Tag;
        //     var lst = tag.GetType();
        //     
        //     var z = 
        //     
        //         var item = (string)button.DataContext;
        //     // lst!.Remove(item);
        // }
        // else
        // {
        //     BookData.BookInformations.Genres.Add(string.Empty);
        // }
    }
    
    private void Remove(Button button)
    {
        
    }
    
    private void Add(Button button)
    {
        throw new NotImplementedException();
    }

    private void UIElement_OnLostFocus(object sender, RoutedEventArgs e)
    {
        var comboBox = (ComboBox)sender;

        if (!string.IsNullOrEmpty((string)comboBox.DataContext) && !string.IsNullOrEmpty(comboBox.Text)) return;
        
        GenresList.Add(comboBox.Text);
        comboBox.DataContext = comboBox.Text;
    }
}