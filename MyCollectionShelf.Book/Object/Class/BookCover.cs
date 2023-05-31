using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MyCollectionShelf.Book.Object.Class;

public class BookCover : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    private Uri? _smallThumbnail;

    public Uri? SmallThumbnail
    {
        get => _smallThumbnail;
        set
        {
            _smallThumbnail = value;
            OnPropertyChanged();
        }
    }

    private Uri? _thumbnail;

    public Uri? Thumbnail
    {
        get => _thumbnail;
        set
        {
            _thumbnail = value;
            OnPropertyChanged();
        }
    }

    private Uri? _small;

    public Uri? Small
    {
        get => _small;
        set
        {
            _small = value;
            OnPropertyChanged();
        }
    }

    private Uri? _medium;

    public Uri? Medium
    {
        get => _medium;
        set
        {
            _medium = value;
            OnPropertyChanged();
        }
    }

    private Uri? _large;

    public Uri? Large
    {
        get => _large;
        set
        {
            _large = value;
            OnPropertyChanged();
        }
    }

    private Uri? _extraLarge;

    public Uri? ExtraLarge
    {
        get => _extraLarge;
        set
        {
            _extraLarge = value;
            OnPropertyChanged();
        }
    }

}