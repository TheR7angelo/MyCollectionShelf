using System.ComponentModel;
using System.Runtime.CompilerServices;
using MyCollectionShelf.Sql.Object.Interface;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace MyCollectionShelf.Sql.Object.Book.Class;

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
    
    private long _bookId;

    [Column("book_fk"), ForeignKey(typeof(Book))]
    public long BookId
    {
        get => _bookId;
        set
        {
            _bookId = value;
            OnPropertyChanged();
        }
    }
    
    private long _genreId;

    [Column("book_genre_fk"), ForeignKey(typeof(BookGenre))]
    public long GenreId
    {
        get => _genreId;
        set
        {
            _genreId = value;
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
            book_fk    integer
                constraint book_genre_list_book_fk
                    references book,
            book_genre_fk integer
                constraint book_genre_list_book_genre_fk
                    references book_genre
        );
        """;
}