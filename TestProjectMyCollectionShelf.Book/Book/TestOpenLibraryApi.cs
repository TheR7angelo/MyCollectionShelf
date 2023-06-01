using MyCollectionShelf.Book;
using MyCollectionShelf.Book.Object.Static_Class;
using MyCollectionShelf.WebApi.Object.Book.Enum;

namespace TestProjectMyCollectionShelf.WebApi.Book;

public class TestOpenLibraryApi
{
    [Fact]
    private async void GetInformationTest()
    {
        const string isbn = "9788419185143";

        var api = new OpenLibraryApi();
        var book = await api.GetBookInformation(isbn);

        var success = await book?.BookInformations.BookCover.DownloadCover(EBookCoverSize.ExtraLarge, "test.jpg")!;
        
        Assert.True(success);
    }
}