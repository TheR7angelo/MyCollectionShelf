using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MyCollectionShelf.Book.Object.Class;

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
    
    private ObservableCollection<BookAuthors> _authors = new() { new BookAuthors() };

    public ObservableCollection<BookAuthors> Authors
    {
        get => _authors;
        set
        {
            _authors = value;
            OnPropertyChanged();
        }
    }

    private BookCover _bookCover = new();

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

    public string? Summarize
    {
        get => _summarize;
        set
        {
            _summarize = value;
            OnPropertyChanged();
        }
    }

    private ObservableCollection<string> _genres = new() { "" };

    public ObservableCollection<string> Genres
    {
        get => _genres;
        set
        {
            _genres = value;
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