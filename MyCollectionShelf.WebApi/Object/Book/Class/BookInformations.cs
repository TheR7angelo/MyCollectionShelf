using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MyCollectionShelf.WebApi.Object.Book.Class.Json;

public class BookInformations : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    private string? _title;

    public string? Title
    {
        get => _title;
        set
        {
            _title = value;
            OnPropertyChanged();
        }
    }

    private string? _series;

    public string? Series
    {
        get => _series;
        set
        {
            _series = value;
            OnPropertyChanged();
        }
    }
    
    private long? _tomeNumber;

    public long? TomeNumber
    {
        get => _tomeNumber;
        set
        {
            _tomeNumber = value;
            OnPropertyChanged();
        }
    }
    
    private List<BookAuthors> _authors = new();

    public List<BookAuthors> Authors
    {
        get => _authors;
        set
        {
            _authors = value;
            OnPropertyChanged();
        }
    }

    private BookCover? _bookCover;

    public BookCover? BookCover
    {
        get => _bookCover;
        set
        {
            _bookCover = value;
            OnPropertyChanged();
        }
    }
    
    private string? _summarize;

    public string? Summarize
    {
        get => _summarize;
        set
        {
            _summarize = value;
            OnPropertyChanged();
        }
    }

    private List<string> _genre = new();

    public List<string> Genre
    {
        get => _genre;
        set
        {
            _genre = value;
            OnPropertyChanged();
        }
    }

    private DateTime? _publishDate;

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