using MyCollectionShelf.Sql.Object.Book.Class;

namespace MyCollectionShelf.Book;

public class BookAllApi
{
    private string? UserAgent { get; }

    public BookAllApi(string? userAgent = null)
    {
        UserAgent = userAgent;
    }

    public async Task<Sql.Object.Book.Class.Book> GetBookInformation(string isbn13)
    {
        var book = new Sql.Object.Book.Class.Book
        {
            BookInformations = new BookInformations
            {
                Isbn = isbn13,
                // todo à remettre
                // BookCover = new BookCover()
            },
            BookNote = new BookNote()
        };

        var apis = new List<IBookApi>
        {
            new GoogleBooksApi(UserAgent),
            new OpenLibraryApi(UserAgent),
            new AmazonApi(UserAgent)
        };

        foreach (var api in apis)
        {
            var bookTemp = await api.GetBookInformation(isbn13);
            if (bookTemp is null) continue;

            Copy(bookTemp.BookInformations, book.BookInformations);
            Copy(bookTemp.BookNote, book.BookNote);
        }
        
        var tmp = book.BookInformations.BookAuthors.Where(s =>
            s.Name.Equals(string.Empty) && s.FamilyName.Equals(string.Empty)).ToList();
        
        foreach (var author in tmp)
        {
            book.BookInformations.BookAuthors.Remove(author);
        }
            
        return book;
    }

    private static void Copy(BookNote tempBookNote, BookNote bookNote)
    {
        bookNote.Price ??= tempBookNote.Price;
    }

    private static void Copy(BookInformations tempInformations, BookInformations bookInformations)
    {
        bookInformations.Title ??= tempInformations.Title;
        bookInformations.BookSeries ??= tempInformations.BookSeries;
        bookInformations.TomeNumber ??= tempInformations.TomeNumber;

        bookInformations.Summarize ??= tempInformations.Summarize;
        bookInformations.PublishDate ??= tempInformations.PublishDate;
        bookInformations.Editor ??= tempInformations.Editor;
        bookInformations.PageNumber ??= tempInformations.PageNumber;
        bookInformations.Isbn ??= tempInformations.Isbn;
        
        Copy(tempInformations.BookAuthors, bookInformations.BookAuthors);
        // todo à remettre
        // Copy(tempInformations.BookCover, bookInformations.BookCover);
        Copy(tempInformations.BookGenres, bookInformations.BookGenres);
    }

    private static void Copy(IReadOnlyCollection<BookGenre> tempInformationsGenre, ICollection<BookGenre> bookInformationsGenre)
    {
        if (tempInformationsGenre.Count.Equals(0)) return;

        foreach (var genre in tempInformationsGenre.Where(genre => !bookInformationsGenre.Contains(genre) && !string.IsNullOrEmpty(genre.Genre)))
        {
            bookInformationsGenre.Add(genre);
        }
    }

    private static void Copy(BookCover? tempBookCover, BookCover bookCover)
    {
        if (tempBookCover is null) return;

        bookCover.SmallThumbnail ??= tempBookCover.SmallThumbnail;
        bookCover.Thumbnail ??= tempBookCover.Thumbnail;
        bookCover.Small ??= tempBookCover.Small;
        bookCover.Medium ??= tempBookCover.Medium;
        bookCover.Large ??= tempBookCover.Large;
        bookCover.ExtraLarge ??= tempBookCover.ExtraLarge;
    }

    private static void Copy(IReadOnlyCollection<BookAuthor> tempAuthors, ICollection<BookAuthor> bookAuthors)
    {
        if (tempAuthors.Count.Equals(0)) return;

        // var uniqueAuthors = tempAuthors.Where(tmp => !bookAuthors.Any(book =>
        //     book.Name.Equals(tmp.Name) && book.FamilyName.Equals(tmp.FamilyName) && string.IsNullOrEmpty(book.Role) &&
        //     !string.IsNullOrEmpty(tmp.Role))).ToList();

        // foreach (var author in uniqueAuthors)
        // {
        //     bookAuthors.Add(author);
        // }

        // var uniqueAuthors = tempAuthors.Where(author => bookAuthors
        //     .Any(s => s.Name != author.Name && s.FamilyName != author.FamilyName)).ToList();

        var uniqueAuthors = tempAuthors.Where(tmp => !bookAuthors.Any(book =>
            book.Name.Equals(tmp.Name) && book.FamilyName.Equals(tmp.FamilyName)));

        foreach (var author in uniqueAuthors)
        {
            bookAuthors.Add(author);
        }
    }
}