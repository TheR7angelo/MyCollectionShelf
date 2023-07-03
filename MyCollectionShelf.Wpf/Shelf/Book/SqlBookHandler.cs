using System;
using System.Linq;
using MyCollectionShelf.Sql;
using MyCollectionShelf.Sql.Table.Book;
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

    public Sql.Table.Book.Book GetBook(BookInformations bookInformations)
    {
        var book = Connection.Query<Sql.Table.Book.Book>(
            $"SELECT * FROM book WHERE book_information_fk = {bookInformations.Id}").First();
        return Connection.GetWithChildren<Sql.Table.Book.Book>(book.Id, true);
    }

    public void Dispose()
    {
        Connection.Dispose();
        GC.SuppressFinalize(this);
    }
}