﻿using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MyCollectionShelf.Book;
using MyCollectionShelf.Book.Object.Enum;
using MyCollectionShelf.Book.Object.Static_Class;
using MyCollectionShelf.Sql.Object.Book.Class;
using MyCollectionShelf.Wpf.Object.Class.Enum;
using MyCollectionShelf.Wpf.Object.Class.Static;
using MyCollectionShelf.Wpf.Ui.Book.Window;
using MyCollectionShelf.Wpf.Ui.General.ButtonCustom;

namespace MyCollectionShelf.Wpf.Ui.Book.Pages;

public partial class AddEditBook
{
    public static readonly DependencyProperty AuthorsListProperty = DependencyProperty.Register(nameof(AuthorsList),
        typeof(ObservableCollection<BookAuthor>), typeof(AddEditBook),
        new PropertyMetadata(new ObservableCollection<BookAuthor>()));

    public static readonly DependencyProperty BookDataProperty = DependencyProperty.Register(nameof(BookData),
        typeof(Sql.Object.Book.Class.Book), typeof(AddEditBook),
        new PropertyMetadata(new Sql.Object.Book.Class.Book()));

    public static readonly DependencyProperty EditorListProperty = DependencyProperty.Register(nameof(EditorList),
        typeof(ObservableCollection<string>), typeof(AddEditBook),
        new PropertyMetadata(new ObservableCollection<BookEditorList>()));

    public static readonly DependencyProperty GenresListProperty = DependencyProperty.Register(nameof(GenresList),
        typeof(ObservableCollection<string>), typeof(AddEditBook),
        new PropertyMetadata(new ObservableCollection<string>()));

    public static readonly DependencyProperty SeriesListProperty = DependencyProperty.Register(nameof(SeriesList),
        typeof(ObservableCollection<BookSeries>), typeof(AddEditBook),
        new PropertyMetadata(new ObservableCollection<BookSeries>()));

    public AddEditBook()
    {
        InitializeComponent();
    }

    public Sql.Object.Book.Class.Book BookData
    {
        get => (Sql.Object.Book.Class.Book)GetValue(BookDataProperty);
        set => SetValue(BookDataProperty, value);
    }

    public ObservableCollection<BookAuthor> AuthorsList
    {
        get => (ObservableCollection<BookAuthor>)GetValue(AuthorsListProperty);
        set => SetValue(AuthorsListProperty, value);
    }

    public ObservableCollection<BookEditorList> EditorList
    {
        get => (ObservableCollection<BookEditorList>)GetValue(EditorListProperty);
        set => SetValue(EditorListProperty, value);
    }

    public ObservableCollection<string> GenresList
    {
        get => (ObservableCollection<string>)GetValue(GenresListProperty);
        set => SetValue(GenresListProperty, value);
    }

    public ObservableCollection<BookSeries> SeriesList
    {
        get => (ObservableCollection<BookSeries>)GetValue(SeriesListProperty);
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

        ButtonValid.Visibility = Visibility.Hidden;
        BusySpinner.Visibility = Visibility.Visible;

        var isbn = scanner.Isbn;

        var book = await GetBook(isbn);
        SetBook(book);

        ButtonValid.Visibility = Visibility.Visible;
        BusySpinner.Visibility = Visibility.Hidden;
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

    private static async Task<Sql.Object.Book.Class.Book> GetBook(string isbn)
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

    private void SetBook(Sql.Object.Book.Class.Book book)
    {
        foreach (var author in book.BookInformations.BookAuthors.Where(s => !s.NameConcat.Equals(", ")))
        {
            if (!AuthorsList.Contains(author)) AuthorsList.Add(author);
        }

        foreach (var genre in book.BookInformations.BookGenres.Where(s => !s.Genre.Equals(string.Empty)))
        {
            if (!GenresList.Contains(genre.Genre)) GenresList.Add(genre.Genre);
        }

        if (book.BookInformations.Editor.Editor is not null && !EditorList.Contains(book.BookInformations.Editor))
        {
            if (!EditorList.Contains(book.BookInformations.Editor))
                EditorList.Add(book.BookInformations.Editor);
        }

        if (book.BookInformations.BookSeries.Title is not null && !book.BookInformations.BookSeries.Title.Equals(string.Empty))
        {
            if (!SeriesList.Contains(book.BookInformations.BookSeries))
                SeriesList.Add(book.BookInformations.BookSeries);
        }

        BookData = book;
    }

    private void ButtonAddRemoveAuthors_OnClick(object sender, RoutedEventArgs e)
    {
        var button = (ButtonAddRemove)sender;
        var function = button.Mode;
        var collection = (ObservableCollection<BookAuthor>)button.Tag;

        var item = button.FindParent<Grid>()!.Children.OfType<ComboBox>().First().DataContext as BookAuthor;

        AddRemoveList(collection, function, item, new BookAuthor());
    }

    private void ButtonAddRemoveGenres_OnClick(object sender, RoutedEventArgs e)
    {
        var button = (ButtonAddRemove)sender;
        var function = button.Mode;
        var collection = (ObservableCollection<BookGenre>)button.Tag;

        var item = button.FindParent<Grid>()!.Children.OfType<ComboBox>().First().DataContext as BookGenre;

        AddRemoveList(collection, function, item, new BookGenre());
    }

    private static void AddRemoveList<T>(object collection, EAddRemove function, T item, T newItem)
    {
        var zcollection = (ObservableCollection<T>)collection;

        switch (function)
        {
            case EAddRemove.Add:
                zcollection.Add(newItem);
                break;
            case EAddRemove.Remove when zcollection.Count > 1:
                zcollection.Remove(item);
                break;
            default:
                break;
        }
    }

    private void ButtonSelectPicture_OnClick(object sender, RoutedEventArgs e)
    {
        Console.WriteLine("heyy");
    }

    private void ButtonValidBook_OnClick(object sender, RoutedEventArgs e)
    {
        Console.WriteLine("Book valid");
    }
}