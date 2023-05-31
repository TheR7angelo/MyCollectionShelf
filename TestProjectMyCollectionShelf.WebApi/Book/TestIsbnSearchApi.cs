using MyCollectionShelf.WebApi.Object.Static_Class;

namespace TestProjectMyCollectionShelf.WebApi.Book;

public class TestIsbnSearchApi
{
    [Fact]
    private async void GetInformationTest()
    {
        const string isbn = "9782072934902";

        using var client = WebClient.GetWebClient();
        client.BaseAddress = new Uri("https://isbnsearch.org/isbn/");

        using var messagePreview = await client.GetAsync(isbn);

        if (!messagePreview.IsSuccessStatusCode) return;

        var json = await messagePreview.Content.ReadAsStringAsync();

        // ESSAI PAS CONCLUANT PAS ASSEZ D'INFORMATION
    }
}