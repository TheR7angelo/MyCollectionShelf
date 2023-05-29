using System.Diagnostics;
using MyCollectionShelf.WebApi.OpenLibrary.Class;
using Newtonsoft.Json;

namespace MyCollectionShelf.WebApi.OpenLibrary;

public class OpenLibraryApi
{
    private static Uri BaseInformationIsbnApi => new("https://openlibrary.org/isbn/");
    private static Uri BaseCoverIsbnApi => new("https://covers.openlibrary.org/b/isbn/");
    private HttpClient Client { get; }
    
    public OpenLibraryApi(string userAgent)
    {
        Client = new HttpClient();
        Client.DefaultRequestHeaders.UserAgent.ParseAdd(userAgent);
    }
    
    public OpenLibraryApi()
    {
        Client = new HttpClient();
    }

    public async Task GetBookInformation(string isbn13)
    {
        Client.BaseAddress = BaseInformationIsbnApi;
        var message = await Client.GetAsync($"{isbn13}.json");

        if (!message.IsSuccessStatusCode) return;

        var json = await message.Content.ReadAsStringAsync();

        var obj = JsonConvert.DeserializeObject<JsonBook>(json);
    }

    public async Task GetCovers(string isbn13, string filePath, EOpenLibrarySize openLibrarySize)
    {
        Client.BaseAddress = BaseCoverIsbnApi;

        var size = openLibrarySize.ToString()[0];

        var message = await Client.GetAsync($"{isbn13}-{size}.jpg");
        if (!message.IsSuccessStatusCode) return;

        var imageBytes = await message.Content.ReadAsByteArrayAsync();
        await File.WriteAllBytesAsync(filePath, imageBytes);
    }
}