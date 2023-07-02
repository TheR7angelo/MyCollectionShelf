using System;
using System.Linq;
using MyCollectionShelf.Sql;
using MyCollectionShelf.Sql.Object.Book.Class.Table;
using SQLite;
using SQLiteNetExtensions.Extensions;

namespace MyCollectionShelf.Wpf.Shelf.Book;

public class SqlBookHandler : IDisposable
{
    private SQLiteConnection Connection { get; }
    
    public SqlBookHandler()
    {
        var sqlHander = new SqlMainHandler();
        Connection = sqlHander.GetSqlConnection();
    }

    public Sql.Object.Book.Class.Table.Book GetBook(BookInformations bookInformations)
    {
        var book = Connection.Query<Sql.Object.Book.Class.Table.Book>(
            $"SELECT * FROM book WHERE book_information_fk = {bookInformations.Id}").First();
        return Connection.GetWithChildren<Sql.Object.Book.Class.Table.Book>(book.Id, true);
    }

    public void Dispose()
    {
        Connection.Dispose();
        GC.SuppressFinalize(this);
    }
}