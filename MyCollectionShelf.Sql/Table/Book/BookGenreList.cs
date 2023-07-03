using System.ComponentModel;
using System.Runtime.CompilerServices;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace MyCollectionShelf.Sql.Table.Book;

[Table("book_genre_list")]
public class BookGenreList : ISql, INotifyPropertyChanged
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
    
    private long _bookInformationsId;

    [Column("book_informations_fk"), ForeignKey(typeof(BookInformations))]
    public long BookInformationsId
    {
        get => _bookInformationsId;
        set
        {
            _bookInformationsId = value;
            OnPropertyChanged();
        }
    }
    
    private long _bookGenreId;

    [Column("book_genre_fk"), ForeignKey(typeof(BookGenre))]
    public long BookGenreId
    {
        get => _bookGenreId;
        set
        {
            _bookGenreId = value;
            OnPropertyChanged();
        }
    }

    public string Definition =>
        """
        create table if not exists book_genre_list
        (
            id            integer
                constraint book_genre_list_pk
                    primary key autoincrement,
            book_informations_fk    integer
                constraint book_genre_list_book_informations_fk
                    references book_informations,
            book_genre_fk integer
                constraint book_genre_list_book_genre_fk
                    references book_genre
        );
        """;
}