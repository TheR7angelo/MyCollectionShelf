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

    // private BookSeries _series = new();
    //
    // [Column("book_series_fk")]
    // [ManyToOne(CascadeOperations = CascadeOperation.All)]
    // public BookSeries Series
    // {
    //     get => _series;
    //     set
    //     {
    //         _series = value;
    //         OnPropertyChanged();
    //     }
    // }

    // [ForeignKey(typeof(BookSeries))]
    // public long BokSerieId { get; set; }
    
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
    
    // private ObservableCollection<BookAuthors> _authors = new() { new BookAuthors() };
    //
    // [Column("book_author_fk"), ForeignKey(typeof(BookAuthors))]
    // [ManyToOne(CascadeOperations = CascadeOperation.All)]
    // public ObservableCollection<BookAuthors> Authors
    // {
    //     get => _authors;
    //     set
    //     {
    //         _authors = value;
    //         OnPropertyChanged();
    //     }
    // }

    // private BookCover _bookCover = new();
    //
    // [Column("book_cover_fk"), ForeignKey(typeof(BookCover))]
    // [OneToMany(CascadeOperations = CascadeOperation.All)]
    // public BookCover BookCover
    // {
    //     get => _bookCover;
    //     set
    //     {
    //         _bookCover = value;
    //         OnPropertyChanged();
    //     }
    // }
    
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

    private ObservableCollection<BookGenre> _genres = new() { new BookGenre() };

    [Column("book_genre_fk"), ForeignKey(typeof(BookGenreList))]
    [ManyToMany(typeof(BookGenreList), CascadeOperations = CascadeOperation.All)]
    public ObservableCollection<BookGenre> Genres
    {
        get => _genres;
        set
        {
            _genres = value;
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