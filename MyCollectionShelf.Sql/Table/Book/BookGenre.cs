using System.ComponentModel;
using System.Runtime.CompilerServices;
using SQLite;

namespace MyCollectionShelf.Sql.Table.Book;

[Table("book_genre")]
public class BookGenre : ISql, INotifyPropertyChanged
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
    
    private string _genre = string.Empty;

    [Column("genre")]
    public string Genre
    {
        get => _genre;
        set
        {
            _genre = value;
            OnPropertyChanged();
        }
    }

    public string Definition => 
        """
        create table if not exists book_genre
        (
            id    integer
                constraint book_genre_pk
                    primary key autoincrement,
            genre text
        );                     
        """;
}