using MyCollectionShelf.WebApi.Object.Class.Json;
using MyCollectionShelf.WebApi.Object.Enum;
using MyCollectionShelf.WebApi.Object.Static_Class;
using Newtonsoft.Json;

namespace MyCollectionShelf.WebApi.Book;

public class OpenLibraryApi : IBookApi
{
    private static Uri BaseInformationIsbnApi => new("https://openlibrary.org/isbn/");
    private static Uri BaseCoverIsbnApi => new("https://covers.openlibrary.org/b/isbn/");
    
    private string? UserAgent { get; set; }
    
    public OpenLibraryApi(string? userAgent = null)
    {
        UserAgent = userAgent;
    }

    public async Task<OpenLibraryBook?> GetBookInformation(string isbn13)
    {
        using var client = WebClient.GetWebClient(UserAgent);
        client.BaseAddress = BaseInformationIsbnApi;
        
        var message = await client.GetAsync($"{isbn13}.json");

        if (!message.IsSuccessStatusCode) return null;

        var json = await message.Content.ReadAsStringAsync();

        return JsonConvert.DeserializeObject<OpenLibraryBook>(json);
    }

    public async Task GetCovers(string isbn13, string filePath, EBookSize bookSize)
    {
        using var client = WebClient.GetWebClient(UserAgent);
        client.BaseAddress = BaseCoverIsbnApi;

        bookSize = GetBookSize(bookSize);
        
        var size = bookSize.ToString()[0];

        var message = await client.GetAsync($"{isbn13}-{size}.jpg");
        if (!message.IsSuccessStatusCode) return;

        var imageBytes = await message.Content.ReadAsByteArrayAsync();
        await File.WriteAllBytesAsync(filePath, imageBytes);
    }

    private static EBookSize GetBookSize(EBookSize bookSize)
    {
        switch (bookSize)
        {
            case EBookSize.SmallThumbnail:
            case EBookSize.Thumbnail:
                bookSize = EBookSize.Small;
                break;
            case EBookSize.Small:
            case EBookSize.Medium:
            case EBookSize.Large:
                break;
            case EBookSize.ExtraLarge:
                bookSize = EBookSize.Large;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(bookSize), bookSize, null);
        }

        return bookSize;
    }
}