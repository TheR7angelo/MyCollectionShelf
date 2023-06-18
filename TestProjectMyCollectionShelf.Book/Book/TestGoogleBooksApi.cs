using MyCollectionShelf.Book;
using MyCollectionShelf.Book.Object.Enum;
using MyCollectionShelf.Book.Object.Static_Class;

namespace TestProjectMyCollectionShelf.WebApi.Book;

public class TestGoogleBooksApi
{
    [Fact]
    private async void GetInformationTest()
    {
        const string isbn = "9782380712308";

        var api = new GoogleBooksApi();
        var book = await api.GetBookInformation(isbn);
        
        var success = await book?.BookInformations.BookCover.DownloadCover(EBookCoverSize.ExtraLarge, "test.jpg")!;
        
        Assert.True(success);
    }
}