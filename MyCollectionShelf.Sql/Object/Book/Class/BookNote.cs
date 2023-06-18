using System.ComponentModel;
using System.Runtime.CompilerServices;
using MyCollectionShelf.Sql.Object.Interface;
using SQLite;

namespace MyCollectionShelf.Sql.Object.Book.Class;

[Table("book_note")]
public class BookNote : ISql, INotifyPropertyChanged
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
    
    private bool _isRead;

    [Column("is_read")]
    public bool IsRead
    {
        get => _isRead;
        set
        {
            _isRead = value;
            OnPropertyChanged();

            if (value && EndDate is null) EndDate = DateTime.Now;
        }
    }

    private DateTime? _startDate;

    [Column("start_date")]
    public DateTime? StartDate
    {
        get => _startDate;
        set
        {
            _startDate = value;
            OnPropertyChanged();
        }
    }

    private DateTime? _endDate;

    [Column("end_date")]
    public DateTime? EndDate
    {
        get => _endDate;
        set
        {
            _endDate = value;
            OnPropertyChanged();
        }
    }

    private double? _price;

    [Column("price")]
    public double? Price
    {
        get => _price;
        set
        {
            _price = value;
            OnPropertyChanged();
        }
    }

    private string? _note;

    [Column("note")]
    public string? Note
    {
        get => _note;
        set
        {
            _note = value;
            OnPropertyChanged();
        }
    }

    public string Definition =>
    """
    create table if not exists book_note
    (
        id         integer
            primary key autoincrement,
        is_read    integer,
        start_date datetime,
        end_date   datetime,
        price      float,
        note       text
    );
    """;
}