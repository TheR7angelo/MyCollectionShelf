using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using MyCollectionShelf.Book.Object.Class;
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

    private void TextBoxTomeNumber_OnTextChanged(object sender, TextCompositionEventArgs e)
    {
        var regex = IsNumber();
        e.Handled = regex.IsMatch(e.Text);
    }

    [GeneratedRegex("[^0-9]+")]
    private static partial Regex IsNumber();

    private void ButtonScan_OnClick(object sender, RoutedEventArgs e)
    {
        var scanner = new CameraScan();

        if (scanner.ShowDialog() != true) return;

        foreach (var author in scanner.Book.BookInformations.Authors.Where(s => !s.NameConcat.Equals(", ")))
        {
            if (!AuthorsList.Contains(author)) AuthorsList.Add(author);
        }

        foreach (var genre in scanner.Book.BookInformations.Genres.Where(s => !s.Equals(string.Empty)))
        {
            if (!GenresList.Contains(genre)) GenresList.Add(genre);
        }

        if (scanner.Book.BookInformations.Editor is not null &&
            !EditorList.Contains(scanner.Book.BookInformations.Editor))
        {
            if (!EditorList.Contains(scanner.Book.BookInformations.Editor))
                EditorList.Add(scanner.Book.BookInformations.Editor);
        }

        if (scanner.Book.BookInformations.Series is not null &&
            !scanner.Book.BookInformations.Series.Equals(string.Empty))
        {
            if (!EditorList.Contains(scanner.Book.BookInformations.Series))
                EditorList.Add(scanner.Book.BookInformations.Series);
        }

        BookData = scanner.Book;
    }

    private void ButtonSelectPicture_OnClick(object sender, RoutedEventArgs e)
    {
        Console.WriteLine("hey");
    }
}