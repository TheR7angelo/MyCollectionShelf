using System.Collections.ObjectModel;
using System.Windows;

namespace MyCollectionShelf.Wpf.Shelf.Book.Ui.UserControls.AddEditBook;

public partial class BookSeries
{
    public static readonly DependencyProperty SeriesProperty = DependencyProperty.Register(nameof(Series),
        typeof(Sql.Table.Book.BookSeries), typeof(BookSeries),
        new PropertyMetadata(default(Sql.Table.Book.BookSeries)));

    public static readonly DependencyProperty SeriesListProperty = DependencyProperty.Register(nameof(SeriesList),
        typeof(ObservableCollection<Sql.Table.Book.BookSeries>), typeof(BookSeries),
        new PropertyMetadata(default(ObservableCollection<Sql.Table.Book.BookSeries>)));

    public static readonly DependencyProperty IsReadOnlyProperty = DependencyProperty.Register(nameof(IsReadOnly),
        typeof(bool), typeof(BookSeries), new PropertyMetadata(default(bool)));

    public Sql.Table.Book.BookSeries Series
    {
        get => (Sql.Table.Book.BookSeries)GetValue(SeriesProperty);
        set => SetValue(SeriesProperty, value);
    }

    public ObservableCollection<Sql.Table.Book.BookSeries> SeriesList
    {
        get => (ObservableCollection<Sql.Table.Book.BookSeries>)GetValue(SeriesListProperty);
        set => SetValue(SeriesListProperty, value);
    }

    public bool IsReadOnly
    {
        get => (bool)GetValue(IsReadOnlyProperty);
        set => SetValue(IsReadOnlyProperty, value);
    }

    public BookSeries()
    {
        InitializeComponent();
    }
}