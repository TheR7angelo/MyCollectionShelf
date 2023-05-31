using MyCollectionShelf.Book;
using MyCollectionShelf.Book.Object.Static_Class;
using MyCollectionShelf.WebApi.Object.Book.Enum;

namespace TestProjectMyCollectionShelf.WebApi.Book;

public class TestGoogleBooksApi
{
    [Fact]
    private async void GetInformationTest()
    {
        const string isbn = "9782092552988";

        var api = new GoogleBooksApi();
        var book = await api.GetBookInformation(isbn);

        var success = await book?.BookInformations.BookCover.DownloadCover(EBookCoverSize.ExtraLarge, "test.jpg")!;
        
        Assert.True(success);
    }
}