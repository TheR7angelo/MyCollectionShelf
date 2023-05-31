using MyCollectionShelf.WebApi.Object.Book.Enum;
using MyCollectionShelf.WebApi.Object.Book.Static_Class;

namespace TestProjectMyCollectionShelf.WebApi.Book;

public class TestOpenLibraryApi
{
    [Fact]
    private async void GetInformationTest()
    {
        const string isbn = "9780553381689";

        var api = new MyCollectionShelf.WebApi.Book.OpenLibraryApi();
        var book = await api.GetBookInformation(isbn);

        var success = await book?.BookCover.DownloadCover(EBookCoverSize.ExtraLarge, "test.jpg")!;
        
        Assert.True(success);
    }
}