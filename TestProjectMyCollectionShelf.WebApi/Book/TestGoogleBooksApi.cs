namespace TestProjectMyCollectionShelf.WebApi.Book;

public class TestGoogleBooksApi
{
    [Fact]
    private async void GetInformationTest()
    {
        const string isbn = "9782072934902";

        var api = new MyCollectionShelf.WebApi.Book.GoogleBooksApi();
        await api.GetBookInformation(isbn);
    }
}