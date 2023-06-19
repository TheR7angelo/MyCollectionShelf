using System.ComponentModel;
using System.Runtime.CompilerServices;
using MyCollectionShelf.Sql.Object.Interface;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace MyCollectionShelf.Sql.Object.Book.Class;

[Table("book_author_list")]
public class BookAuthorList : ISql, INotifyPropertyChanged
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
    
    private long _bookAuthorId;

    [Column("book_author_fk"), ForeignKey(typeof(BookAuthor))]
    public long BookAuthorId
    {
        get => _bookAuthorId;
        set
        {
            _bookAuthorId = value;
            OnPropertyChanged();
        }
    }

    public string Definition =>
        """
        create table if not exists book_author_list
        (
            id                   integer
                constraint book_author_list_pk
                    primary key autoincrement,
            book_informations_fk integer
                constraint book_author_list_book_informations_id_fk
                    references book_informations,
            book_author_fk          integer
                constraint book_author_list_book_author_id_fk
                    references book_author
        );
        """;
}