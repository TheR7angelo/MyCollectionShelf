using System.ComponentModel;
using System.Runtime.CompilerServices;
using MyCollectionShelf.Sql.Object.Interface;
using SQLite;

namespace MyCollectionShelf.Sql.Object.Book.Class;

[Table("book_cover")]
public class BookCover : ISql, INotifyPropertyChanged
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
    
    private Uri? _smallThumbnail;

    [Column("small_thumbnail")]
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

    [Column("thumbnail")]
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

    [Column("small")]
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

    [Column("medium")]
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

    [Column("large")]
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

    [Column("extra_large")]
    public Uri? ExtraLarge
    {
        get => _extraLarge;
        set
        {
            _extraLarge = value;
            OnPropertyChanged();
        }
    }
    
    private string? _storage;

    [Column("storage")]
    public string? Storage
    {
        get => _storage;
        set
        {
            _storage = value;
            OnPropertyChanged();
        }
    }

    public string Definition =>
        """
        create table if not exists book_cover
        (
            id              integer
                constraint book_cover_pk
                    primary key autoincrement,
            small_thumbnail text,
            thumbnail       text,
            small           text,
            medium          text,
            large           text,
            extra_large     text,
            storage         text
        );
        """;
}