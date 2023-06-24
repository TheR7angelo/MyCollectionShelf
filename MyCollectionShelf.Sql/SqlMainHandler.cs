using MyCollectionShelf.Sql.Object.Book.Class.Table;
using MyCollectionShelf.Sql.Object.Book.Class.View;
using MyCollectionShelf.Sql.Object.Interface;
using SQLite;

namespace MyCollectionShelf.Sql;

public class SqlMainHandler : IDisposable
{
    private readonly SQLiteConnection _sqLiteConnection = 
        new("MyCollectionShelf.sqlite",
        SQLiteOpenFlags.Create | SQLiteOpenFlags.FullMutex | SQLiteOpenFlags.ReadWrite, 
        false);

    public SqlMainHandler(bool foreignKeys=false)
    {
        SetForeignKeys(foreignKeys);
        InitDataBase();
    }
    
    public void Dispose()
    {
        _sqLiteConnection.Dispose();
        GC.SuppressFinalize(this);
    }

    private void SetForeignKeys(bool on)
    {
        var onOff = on ? "ON" : "OFF";
        _sqLiteConnection.Execute($"PRAGMA foreign_keys = {onOff}");
    }

    public SQLiteConnection GetSqlConnection() => _sqLiteConnection;

    private void InitDataBase()
    {
        var lstInit = new List<ISql>
        {
            new BookAuthor(), new BookGenre(),
            new BookSeries(), new BookCover(),
            new BookAuthorList(), new BookGenreList(), new BookEditorList(),
            new BookInformations(), new BookNote(),
            new Book(),
            
            new VBookShelf()
        };

        foreach (var cmd in lstInit.Select(s => s.Definition))
        {
            _sqLiteConnection.Execute(cmd);
        }
    }
}