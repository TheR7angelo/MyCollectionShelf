using System.ComponentModel;
using System.Runtime.CompilerServices;
using SQLite;

namespace MyCollectionShelf.Sql.Table.Book;

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

    private string? _seriesCover;

    [Column("series_cover")]
    public string? SeriesCover
    {
        get => _seriesCover;
        set
        {
            _seriesCover = value;
            OnPropertyChanged();
        }
    }
    
    private string? _seriesSummarize;

    [Column("series_summarize")]
    public string? SeriesSummarize
    {
        get => _seriesSummarize;
        set
        {
            _seriesSummarize = value;
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
            series_cover text,
            series_summarize text
        );
        """;
}