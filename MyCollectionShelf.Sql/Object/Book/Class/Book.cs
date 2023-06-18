using System.ComponentModel;
using System.Runtime.CompilerServices;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace MyCollectionShelf.Sql.Object.Book.Class;

[Table("book")]
public class Book : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    private BookInformations _bookInformations = new();

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
    
    [Column("book_information"), ForeignKey(typeof(BookInformations))]
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

    private BookNote _bookNote = new();

    [Column("book_note"), ForeignKey(typeof(BookInformations))]
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
}