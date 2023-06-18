using MyCollectionShelf.Sql;
using MyCollectionShelf.Sql.Object.Book.Class;
using MyCollectionShelf.Sql.Object.Interface;

namespace TestProjectMyCollectionShelf.Sql;

public class UnitTest1
{
    [Fact]
    public void Test1()
    {
        using var handler = new SqlMainHandler();
        var db = handler.GetSqlConnection();

        var lstInit = new List<ISql>
        {
            new BookGenre(), new BookAuthors(), new BookCover()
        };

        foreach (var cmd in lstInit.Select(s => s.Definition))
        {
            db.Execute(cmd);
        }
    }
}