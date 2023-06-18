using SQLite;

namespace MyCollectionShelf.Sql;

public class SqlMainHandler : IDisposable
{
    private readonly SQLiteConnection _sqLiteConnection = new("MyCollectionShelf.sqlite",
        SQLiteOpenFlags.Create | SQLiteOpenFlags.FullMutex | SQLiteOpenFlags.ReadWrite, false);

    public void Dispose()
    {
        _sqLiteConnection.Dispose();
        GC.SuppressFinalize(this);
    }

    public SQLiteConnection GetSqlConnection() => _sqLiteConnection;
}