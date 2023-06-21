using System.ComponentModel;
using System.Runtime.CompilerServices;
using MyCollectionShelf.Sql.Object.Interface;
using SQLite;

namespace MyCollectionShelf.Sql.Object.Book.Class.Table;

[Table("book_author")]
public class BookAuthor : ISql, INotifyPropertyChanged
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
    
    private string _familyName = string.Empty;

    [Column("family_name")]
    public string FamilyName
    {
        get => _familyName;
        set
        {
            _familyName = value;
            OnPropertyChanged();
        }
    }
    
    private string _name = string.Empty;

    [Column("name")]
    public string Name
    {
        get => _name;
        set
        {
            _name = value;
            OnPropertyChanged();
        }
    }
    
    // private string _role = string.Empty;
    //
    // public string Role
    // {
    //     get => _role;
    //     set
    //     {
    //         _role = value;
    //         OnPropertyChanged();
    //     }
    // }
    
    public string NameConcat => $"{FamilyName}, {Name}";

    public string Definition =>
        """
        create table if not exists book_author
        (
            id          integer
                constraint book_author_pk
                    primary key autoincrement,
            family_name text,
            name        text
        );
        """;
}