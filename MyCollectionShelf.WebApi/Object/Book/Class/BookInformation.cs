using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MyCollectionShelf.WebApi.Object.Book.Class;

public class BookInformation : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    private string? _isbn13;

    public string? Isbn13
    {
        get => _isbn13;
        set
        {
            _isbn13 = value;
            OnPropertyChanged();
        }
    }

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
}