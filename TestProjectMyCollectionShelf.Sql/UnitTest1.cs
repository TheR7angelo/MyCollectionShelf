using MyCollectionShelf.Sql;
using MyCollectionShelf.Sql.Object.Book.Class;

namespace TestProjectMyCollectionShelf.Sql;

public class UnitTest1
{
    [Fact]
    public void Test1()
    {
        using var handler = new SqlMainHandler();
        var db = handler.GetSqlConnection();

        db.Execute(new BookGenre().Definition);
        db.Execute(new BookAuthors().Definition);
    }
}