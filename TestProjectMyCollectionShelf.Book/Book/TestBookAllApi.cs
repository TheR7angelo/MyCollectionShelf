using MyCollectionShelf.Book;
using MyCollectionShelf.Book.Object.Static_Class;
using MyCollectionShelf.WebApi.Object.Book.Enum;

namespace TestProjectMyCollectionShelf.WebApi.Book;

public class TestBookAllApi
{
    [Fact]
    private async void TestGetBookInformation()
    {
        const string isbn = "9782818989036";

        var api = new BookAllApi();
        var book = await api.GetBookInformation(isbn);
        
        var success = await book.BookInformations.BookCover.DownloadCover(EBookCoverSize.ExtraLarge, "test.jpg")!;
        
        Assert.True(success);
    }
}