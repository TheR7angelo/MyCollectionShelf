using MyCollectionShelf.WebApi.Object.Book.Enum;
using MyCollectionShelf.WebApi.Object.Book.Static_Class;

namespace TestProjectMyCollectionShelf.WebApi.Book;

public class TestGoogleBooksApi
{
    [Fact]
    private async void GetInformationTest()
    {
        const string isbn = "9782092552988";

        var api = new MyCollectionShelf.WebApi.Book.GoogleBooksApi();
        var book = await api.GetBookInformation(isbn);

        var success = await book?.BookInformations.BookCover.DownloadCover(EBookCoverSize.ExtraLarge, "test.jpg")!;
        
        Assert.True(success);
    }
}