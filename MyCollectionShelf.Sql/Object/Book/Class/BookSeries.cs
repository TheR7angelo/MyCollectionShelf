using System.ComponentModel;
using System.Runtime.CompilerServices;
using MyCollectionShelf.Sql.Object.Interface;
using SQLite;

namespace MyCollectionShelf.Sql.Object.Book.Class;

[Table("book_series")]
public class BookSeries : ISql, INotifyPropertyChanged
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

    private Uri? _seriesCover;

    [Column("series_cover")]
    public Uri? SeriesCover
    {
        get => _seriesCover;
        set
        {
            _seriesCover = value;
            OnPropertyChanged();
        }
    }

    public string Definition =>
        """
        create table if not exists book_series
        (
            id           integer
                primary key autoincrement,
            title        text,
            series_cover text
        );
        """;
}