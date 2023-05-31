using MyCollectionShelf.WebApi.Object.Book.Enum;
using MyCollectionShelf.WebApi.Object.Book.Static_Class;

namespace TestProjectMyCollectionShelf.WebApi.Book;

public class TestGoogleBooksApi
{
    [Fact]
    private async void GetInformationTest()
    {
        const string isbn = "9782343129822";

        var api = new MyCollectionShelf.WebApi.Book.GoogleBooksApi();
        var book = await api.GetBookInformation(isbn);

        var success = await book?.BookCover.DownloadCover(EBookCoverSize.ExtraLarge, "test.jpg")!;
        
        Assert.True(success);
    }
}