using MyCollectionShelf.WebApi.Object.Class.Json;
using MyCollectionShelf.WebApi.Object.Enum;
using Newtonsoft.Json;

namespace MyCollectionShelf.WebApi.Book;

public class OpenLibraryApi
{
    private static Uri BaseInformationIsbnApi => new("https://openlibrary.org/isbn/");
    private static Uri BaseCoverIsbnApi => new("https://covers.openlibrary.org/b/isbn/");
    private HttpClient Client { get; set; } = new();
    
    private string? UserAgent { get; set; }
    
    public OpenLibraryApi(string userAgent)
    {
        UserAgent = userAgent;
    }
    
    public OpenLibraryApi()
    {
        
    }

    private void SetHttpClient()
    {
        Client = new HttpClient();
        if (UserAgent is null) return;
        {
            Client.DefaultRequestHeaders.UserAgent.ParseAdd(UserAgent);
        }
    }

    public async Task<OpenLibraryBook?> GetBookInformation(string isbn13)
    {
        SetHttpClient();
        Client.BaseAddress = BaseInformationIsbnApi;
        var message = await Client.GetAsync($"{isbn13}.json");

        if (!message.IsSuccessStatusCode) return null;

        var json = await message.Content.ReadAsStringAsync();

        return JsonConvert.DeserializeObject<OpenLibraryBook>(json);
    }

    public async Task GetCovers(string isbn13, string filePath, EOpenLibrarySize openLibrarySize)
    {
        SetHttpClient();
        Client.BaseAddress = BaseCoverIsbnApi;

        var size = openLibrarySize.ToString()[0];

        var message = await Client.GetAsync($"{isbn13}-{size}.jpg");
        if (!message.IsSuccessStatusCode) return;

        var imageBytes = await message.Content.ReadAsByteArrayAsync();
        await File.WriteAllBytesAsync(filePath, imageBytes);
    }
}