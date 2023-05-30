using MyCollectionShelf.WebApi.Object.Class.Json;
using MyCollectionShelf.WebApi.Object.Static_Class;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace MyCollectionShelf.WebApi.Book;

public class GoogleBooksApi : IBookApi
{
    private static Uri BaseInformationIsbnApi { get; } = new("https://www.googleapis.com/books/v1/volumes?q=isbn:");

    private string? UserAgent { get; set; }
    
    public GoogleBooksApi(string? userAgent=null)
    {
        UserAgent = userAgent;
    }
    
    public async Task<GoogleBooksBook?> GetBookInformation(string isbn13)
    {
        using var client = WebClient.GetWebClient(UserAgent);
        
        var message = await client.GetAsync($"{BaseInformationIsbnApi}{isbn13}");

        if (!message.IsSuccessStatusCode) return null;

        var json = await message.Content.ReadAsStringAsync();

        var selfLink = JsonConvert.DeserializeObject<Root>(json)?.Items;
        
        message = await client.GetAsync(selfLink?.First().SelfLink);
        
        if (!message.IsSuccessStatusCode) return null;
        
        json = await message.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<GoogleBooksBook>(json);
    }
}