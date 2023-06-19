using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace MyCollectionShelf.Sql.Object.Book.Class;

[Table("book_informations")]
public class BookInformations : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    private long _id;

    [PrimaryKey, AutoIncrement, Column("id")]
    public long Id
    {
        get => _id;
        set
        {
            _id = value;
            OnPropertyChanged();
        }
    }
    
    private string? _title;

    [Column("title")]
    public string? Title
    {
        get => _title;
        set
        {
            _title = value;
            OnPropertyChanged();
        }
    }

    [Column("book_series_fk")]
    [ForeignKey(typeof(BookSeries))]
    public long BookSeriesId { get; set; }
    
    private BookSeries _bookSeries = new();
    
    [ManyToOne(CascadeOperations = CascadeOperation.All)]
    public BookSeries BookSeries
    {
        get => _bookSeries;
        set
        {
            _bookSeries = value;
            OnPropertyChanged();
        }
    }
    
    private long? _tomeNumber;

    [Column("tome_number")]
    public long? TomeNumber
    {
        get => _tomeNumber;
        set
        {
            _tomeNumber = value;
            OnPropertyChanged();
        }
    }
    
    private ObservableCollection<BookAuthor> _bookAuthors = new() { new BookAuthor() };
    
    [Column("book_author_fk"), ForeignKey(typeof(BookAuthor))]
    [ManyToMany(typeof(BookAuthorList), CascadeOperations = CascadeOperation.All)]
    public ObservableCollection<BookAuthor> BookAuthors
    {
        get => _bookAuthors;
        set
        {
            _bookAuthors = value;
            OnPropertyChanged();
        }
    }

    [Column("book_cover_fk")]
    [ForeignKey(typeof(BookCover))]
    public long BookCoverId { get; set; }
    
    private BookCover _bookCover = new();
    
    [OneToMany(CascadeOperations = CascadeOperation.All)]
    public BookCover BookCover
    {
        get => _bookCover;
        set
        {
            _bookCover = value;
            OnPropertyChanged();
        }
    }
    
    private string? _summarize;

    [Column("summarize")]
    public string? Summarize
    {
        get => _summarize;
        set
        {
            _summarize = value;
            OnPropertyChanged();
        }
    }

    private ObservableCollection<BookGenre> _bookGenres = new() { new BookGenre() };

    [Column("book_genre_fk"), ForeignKey(typeof(BookGenreList))]
    [ManyToMany(typeof(BookGenreList), CascadeOperations = CascadeOperation.All)]
    public ObservableCollection<BookGenre> BookGenres
    {
        get => _bookGenres;
        set
        {
            _bookGenres = value;
            OnPropertyChanged();
        }
    }

    private DateTime? _publishDate;

    [Column("publish_date")]
    public DateTime? PublishDate
    {
        get => _publishDate;
        set
        {
            _publishDate = value;
            OnPropertyChanged();
        }
    }

    private string? _editor;

    [Column("editor")]
    public string? Editor
    {
        get => _editor;
        set
        {
            _editor = value;
            OnPropertyChanged();
        }
    }
    
    private long? _pageNumber;

    [Column("page_number")]
    public long? PageNumber
    {
        get => _pageNumber;
        set
        {
            _pageNumber = value;
            OnPropertyChanged();
        }
    }

    private string? _isbn;

    [Column("isbn")]
    public string? Isbn
    {
        get => _isbn;
        set
        {
            _isbn = value;
            OnPropertyChanged();
        }
    }
}