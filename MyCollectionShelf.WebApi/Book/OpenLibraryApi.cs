using MyCollectionShelf.WebApi.Object.Book.Class;
using MyCollectionShelf.WebApi.Object.Book.Class.Json;
using MyCollectionShelf.WebApi.Object.Static_Class;
using Newtonsoft.Json;

namespace MyCollectionShelf.WebApi.Book;

public class OpenLibraryApi : IBookApi
{
    private static Uri BaseInformationIsbnApi => new("https://openlibrary.org/isbn/");
    private const string BaseCoverIsbnApi = "https://covers.openlibrary.org/b/isbn/";
    
    private string? UserAgent { get; set; }
    
    public OpenLibraryApi(string? userAgent = null)
    {
        UserAgent = userAgent;
    }

    public async Task<BookInformation?> GetBookInformation(string isbn13)
    {
        using var client = WebClient.GetWebClient(UserAgent);
        client.BaseAddress = BaseInformationIsbnApi;
        
        var message = await client.GetAsync($"{isbn13}.json");

        if (!message.IsSuccessStatusCode) return null;

        var json = await message.Content.ReadAsStringAsync();

        var openLibraryBook = JsonConvert.DeserializeObject<OpenLibraryBook>(json);

        return new BookInformation
        {
            Isbn13 = isbn13,
            Title = openLibraryBook?.Title,
            BookCover = new BookCover
            {
                SmallThumbnail = new Uri($"{BaseCoverIsbnApi}{isbn13}-S.jpg"),
                Thumbnail = new Uri($"{BaseCoverIsbnApi}{isbn13}-S.jpg"),
                Small = new Uri($"{BaseCoverIsbnApi}{isbn13}-S.jpg"),
                Medium = new Uri($"{BaseCoverIsbnApi}{isbn13}-M.jpg"),
                Large = new Uri($"{BaseCoverIsbnApi}{isbn13}-L.jpg"),
                ExtraLarge = new Uri($"{BaseCoverIsbnApi}{isbn13}-L.jpg"),
            }
        };
    }

}