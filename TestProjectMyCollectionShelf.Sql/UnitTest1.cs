using System.Collections.ObjectModel;
using MyCollectionShelf.Sql;
using MyCollectionShelf.Sql.Object.Book.Class;
using MyCollectionShelf.Sql.Object.Interface;
using SQLiteNetExtensions.Extensions;

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
            new BookAuthor(), new BookGenre(),
            new BookSeries(), new BookCover(),
            new BookAuthorList(), new BookGenreList(), new BookEditorList(),
            
            new BookInformations(),
        };

        foreach (var cmd in lstInit.Select(s => s.Definition))
        {
            db.Execute(cmd);
        }
    }
    
    [Fact]
    private void CreateBaseTable()
    {
        using var handler = new SqlMainHandler();
        var db = handler.GetSqlConnection();

        db.CreateTable<BookInformations>();
    }
    
    [Fact]
    public void TestTableList()
    {
        using var handler = new SqlMainHandler();
        var db = handler.GetSqlConnection();
        
        db.CreateTable<BookAuthor>();
        db.CreateTable<BookAuthorList>();
        db.CreateTable<BookGenre>();
        db.CreateTable<BookGenreList>();
        db.CreateTable<BookInformations>();


        var info = new BookInformations
        {
            BookAuthors = new ObservableCollection<BookAuthor>
            {
                new() { FamilyName = "family Name", Name = "name" }
            },
            BookGenres = new ObservableCollection<BookGenre>
            {
                new() { Genre = "Test 1" },
                new() { Genre = "Test 2" },
                new() { Genre = "Test 3" },
            }
        };

        db.InsertWithChildren(info);
        
        var z = db.GetWithChildren<BookInformations>(info.Id);
    }

    [Fact]
    private void TestManyToOne()
    {
        using var handler = new SqlMainHandler();
        var db = handler.GetSqlConnection();

        db.CreateTable<BookAuthor>();
        db.CreateTable<BookAuthorList>();
        db.CreateTable<BookGenre>();
        db.CreateTable<BookGenreList>();
        
        db.CreateTable<BookSeries>();
        db.CreateTable<BookInformations>();

        var info = new BookInformations
        {
            BookSeries = new BookSeries { Title = "Série de test" }
        };
        
        db.InsertWithChildren(info);

        var z = db.GetWithChildren<BookInformations>(info.Id);
    }
}