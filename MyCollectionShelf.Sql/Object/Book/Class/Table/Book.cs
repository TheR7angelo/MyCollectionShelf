using System.ComponentModel;
using System.Runtime.CompilerServices;
using MyCollectionShelf.Sql.Object.Interface;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace MyCollectionShelf.Sql.Object.Book.Class.Table;

[Table("book")]
public class Book : ISql, INotifyPropertyChanged
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
    
    [Column("book_information_fk"), ForeignKey(typeof(BookInformations))]
    public long BookInformationsId { get; set; }
    
    private BookInformations _bookInformations = new();
    
    [OneToOne(CascadeOperations = CascadeOperation.All)]
    public BookInformations BookInformations
    {
        get => _bookInformations;
        set
        {
            _bookInformations = value;
            OnPropertyChanged();
        }
    }

    [Column("book_note_fk"), ForeignKey(typeof(BookNote))]
    public long BookNoteId { get; set; }
    
    private BookNote _bookNote = new();
    
    [OneToOne(CascadeOperations = CascadeOperation.All)]
    public BookNote BookNote
    {
        get => _bookNote;
        set
        {
            _bookNote = value;
            OnPropertyChanged();
        }
    }

    public string Definition =>
        """
        create table if not exists book
        (
            id                integer
                constraint book_pk
                    primary key autoincrement,
            book_information_fk integer
                constraint book_book_informations_id_fk
                    references book_informations,
            book_note_fk         integer
                constraint book_book_note_id_fk
                    references book_note
        );
        """;
}