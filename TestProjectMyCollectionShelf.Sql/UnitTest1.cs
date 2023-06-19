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
            new BookInformations(), new BookNote(),
            new Book()
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

        db.CreateTable<Book>();
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

        var book = new Book
        {
            BookInformations = info,
        };
        
        db.InsertWithChildren(book, true);
    
        var z = db.GetWithChildren<BookInformations>(info.Id);
    }

    [Fact]
    private void TestInsertNewBook()
    {
        var book = new Book
        {
            BookInformations = new BookInformations
            {
                Title = "oshi no ko",
                BookCover = new BookCover(),
                BookSeries = new BookSeries { Title = "Oshi no Ko" },
                BookGenres = new ObservableCollection<BookGenre>
                {
                    new() { Genre = "Fantastique" }
                },
                BookEditor = new BookEditorList
                {
                    Editor = "Kurokawa"
                },
                Isbn = "123456",
                BookAuthors = new ObservableCollection<BookAuthor>
                {
                    new() { Name = "Merde" }
                },
                Summarize = "sdsdd",
                PageNumber = 123456,
                PublishDate = DateTime.Now,
                TomeNumber = 5
            },
            BookNote = new BookNote
            {
                Note = "yolo la note",
                Price = 9.25
            }
        };
    
        using var handler = new SqlMainHandler();
        var db = handler.GetSqlConnection();
    
        // var series = new BookSeries { Title = "yolo" };
        // db.Insert(series);
        //
        // var cover = new BookCover { Large = new Uri("Merde", UriKind.Relative) };
        // db.Insert(cover);
        //
        // var editor = new BookEditorList { Editor = "Kurokawa" };
        // db.Insert(editor);
        //
        // var genre = new ObservableCollection<BookGenre> { new() { Genre = "Fantastique" } };
        // db.InsertAllWithChildren(genre);
        //
        // var test = new BookInformations
        // {
        //     BookSeries = series,
        //     BookCover = cover,
        //     BookEditor = editor,
        //     BookGenres = genre
        // };
        
        db.InsertWithChildren(book, true);
    }
}