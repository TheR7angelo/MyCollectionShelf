using MyCollectionShelf.Book;

namespace TestProjectMyCollectionShelf.WebApi.Book;

public class TestAmazonBookApi
{
    [Fact]
    private async void GetInformationTest()
    {
        const string isbn = "9782380712308";

        var api = new AmazonApi();
        var book = await api.GetBookInformation(isbn);
        
        // var success = await book?.BookInformations.BookCover.DownloadCover(EBookCoverSize.ExtraLarge, "test.jpg")!;
        //
        // Assert.True(success);
    }
}