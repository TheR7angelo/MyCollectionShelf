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
            new BookAuthors(), new BookCover(), new BookNote(),
            new BookGenre(), new BookGenreList(),
        };

        foreach (var cmd in lstInit.Select(s => s.Definition))
        {
            db.Execute(cmd);
        }
    }
    
    [Fact]
    public void Test2()
    {
        using var handler = new SqlMainHandler();
        var db = handler.GetSqlConnection();

        var genres = new List<BookGenre>
        {
            new() { Genre = "test 1" },
            new() { Genre = "test 2" },
            new() { Genre = "test 3" },
            new() { Genre = "test 4" },
        };

        db.Execute("INSERT INTO book(title) VALUES ('Test livre')");
        db.InsertAll(genres);
        var bookGenreList = genres.Select(genre => new BookGenreList { BookId = 1, GenreId = genre.Id });

        db.InsertAll(bookGenreList);
        // db.InsertAll(genreList);
    }
    
    [Fact]
    public void Test3()
    {
        using var handler = new SqlMainHandler();
        var db = handler.GetSqlConnection();

        db.CreateTable<BookGenreList>();
    }
}