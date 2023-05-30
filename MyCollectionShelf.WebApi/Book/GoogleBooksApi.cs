using MyCollectionShelf.WebApi.Object.Static_Class;

namespace MyCollectionShelf.WebApi.Book;

public class GoogleBooksApi : IBookApi
{
    private static Uri BaseInformationIsbnApi { get; } = new("https://www.googleapis.com/books/v1/volumes?q=isbn:");

    private string? UserAgent { get; set; }
    
    public GoogleBooksApi(string? userAgent=null)
    {
        UserAgent = userAgent;
    }
    
    public async Task GetBookInformation(string isbn13)
    {
        using var client = WebClient.GetWebClient(UserAgent);
        
        var message = await client.GetAsync($"{BaseInformationIsbnApi}{isbn13}");

        if (!message.IsSuccessStatusCode) return;

        var json = await message.Content.ReadAsStringAsync();
    }
}