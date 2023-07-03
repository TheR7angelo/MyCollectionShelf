using System.ComponentModel;
using System.Runtime.CompilerServices;
using SQLite;

namespace MyCollectionShelf.Sql.Table.Book;

[Table("book_editor_list")]
public class BookEditorList : ISql, INotifyPropertyChanged
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
    
    private string? _editor = string.Empty;

    [Column("editor")]
    public string? Editor
    {
        get => _editor;
        set
        {
            _editor = value;
            OnPropertyChanged();
        }
    }

    public string Definition =>
        """
        create table if not exists book_editor_list
        (
            id     integer
                constraint book_editor_list_pk
                    primary key autoincrement,
            editor text
        );
        """;
}