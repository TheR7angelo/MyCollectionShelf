using MyCollectionShelf.WebApi.Object.Book.Class;
using MyCollectionShelf.WebApi.Object.Book.Class.Json;
using MyCollectionShelf.WebApi.Object.Static_Class;
using Newtonsoft.Json;

namespace MyCollectionShelf.WebApi.Book;

public class GoogleBooksApi : IBookApi
{
    private static Uri BaseInformationIsbnApi { get; } = new("https://www.googleapis.com/books/v1/volumes?q=isbn:");

    private string? UserAgent { get; set; }
    
    public GoogleBooksApi(string? userAgent=null)
    {
        UserAgent = userAgent;
    }
    
    public async Task<BookInformation?> GetBookInformation(string isbn13)
    {
        using var client = WebClient.GetWebClient(UserAgent);
        
        using var messagePreview = await client.GetAsync($"{BaseInformationIsbnApi}{isbn13}");

        if (!messagePreview.IsSuccessStatusCode) return null;

        var json = await messagePreview.Content.ReadAsStringAsync();

        var selfLink = JsonConvert.DeserializeObject<Root>(json)?.Items;
        
        using var message = await client.GetAsync(selfLink?.First().SelfLink);
        
        if (!message.IsSuccessStatusCode) return null;
        
        json = await message.Content.ReadAsStringAsync();
        var googleBook = JsonConvert.DeserializeObject<GoogleBooksBook>(json);

        return new BookInformation
        {
            Title = googleBook?.VolumeInfo?.Title,
            BookCover = new BookCover
            {
                SmallThumbnail = googleBook?.VolumeInfo?.ImageLinks?.SmallThumbnail,
                Thumbnail = googleBook?.VolumeInfo?.ImageLinks?.Thumbnail,
                Small = googleBook?.VolumeInfo?.ImageLinks?.Small,
                Medium = googleBook?.VolumeInfo?.ImageLinks?.Medium,
                Large = googleBook?.VolumeInfo?.ImageLinks?.Large,
                ExtraLarge = googleBook?.VolumeInfo?.ImageLinks?.ExtraLarge
            }
        };
    }
}