using MyCollectionShelf.WebApi.Object.Static_Class;

namespace MyCollectionShelf.WebApi.Book;

public class GoogleBooksApi : IBookApi
{
    private static Uri BaseInformationIsbnApi { get; } = new Uri("https://www.googleapis.com/books/v1/volumes?q=isbn:");

    private string? UserAgent { get; set; }
    
    public GoogleBooksApi(string? userAgent)
    {
        UserAgent = userAgent;
    }
    
    public async Task GetBookInformation(string isbn13)
    {
        using var client = WebClient.GetWebClient(UserAgent);
        client.BaseAddress = BaseInformationIsbnApi;
        
        var message = await client.GetAsync($"{isbn13}");

        if (!message.IsSuccessStatusCode) return;

        var json = await message.Content.ReadAsStringAsync();
    }
}