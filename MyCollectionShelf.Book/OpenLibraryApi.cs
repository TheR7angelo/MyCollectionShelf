using MyCollectionShelf.Book.Object.Class;
using MyCollectionShelf.Book.Object.Static_Class;
using MyCollectionShelf.WebApi.Object.Book.Class.Json;
using MyCollectionShelf.WebApi.Object.Static_Class;
using Newtonsoft.Json;

namespace MyCollectionShelf.Book;

public class OpenLibraryApi : IBookApi
{
    private static Uri BaseInformationIsbnApi => new("https://openlibrary.org/isbn/");
    private static Uri BaseInformationAuthorApi => new("https://openlibrary.org/");
    private const string BaseCoverIsbnApi = "https://covers.openlibrary.org/b/isbn/";
    
    private string? UserAgent { get; }
    
    public OpenLibraryApi(string? userAgent = null)
    {
        UserAgent = userAgent;
    }

    public async Task<Object.Class.Book?> GetBookInformation(string isbn13)
    {
        using var client = WebClient.GetWebClient(UserAgent);
        client.BaseAddress = BaseInformationIsbnApi;
        
        using var message = await client.GetAsync($"{isbn13}.json");

        if (!message.IsSuccessStatusCode) return null;

        var json = await message.Content.ReadAsStringAsync();

        var openLibraryBook = JsonConvert.DeserializeObject<OpenLibraryBook>(json);

        var authors = await GetAuthors(openLibraryBook?.Authors);
        
        // todo à terminer
        return new Object.Class.Book
        {
            BookInformations = new BookInformations
            {
                Title = openLibraryBook?.Title,
                Authors = authors,
                BookCover = new BookCover
                {
                    SmallThumbnail = new Uri($"{BaseCoverIsbnApi}{isbn13}-S.jpg"),
                    Thumbnail = new Uri($"{BaseCoverIsbnApi}{isbn13}-S.jpg"),
                    Small = new Uri($"{BaseCoverIsbnApi}{isbn13}-S.jpg"),
                    Medium = new Uri($"{BaseCoverIsbnApi}{isbn13}-M.jpg"),
                    Large = new Uri($"{BaseCoverIsbnApi}{isbn13}-L.jpg"),
                    ExtraLarge = new Uri($"{BaseCoverIsbnApi}{isbn13}-L.jpg"),
                },
                // Summarize = "", // Non fournit
                // Series = "", // L'API ne le fournit pas
                // TomeNumber = 0, // L'API ne le fournit pas
                // Genre = new List<string>(),
                PublishDate = openLibraryBook?.PublishDate,
                Editor = openLibraryBook?.Publishers?.FirstOrDefault(),
                PageNumber = openLibraryBook?.NumberOfPages,
                Isbn = isbn13
            },
            // // BookNote = new BookNote // Aucune info fournit 
            // // {
            // //     Price = 0
            // // }
        };
    }

    private async Task<List<BookAuthors>> GetAuthors(IEnumerable<AuthorOpenLibrary>? authors)
    {
        var results = new List<BookAuthors>();

        if (authors is null) return results;

        foreach (var author in authors.Where(author => !string.IsNullOrEmpty(author.Key)))
        {
            using var client = WebClient.GetWebClient(UserAgent);
            client.BaseAddress = BaseInformationAuthorApi;

            using var message = await client.GetAsync($"{author.Key}.json");
            if (!message.IsSuccessStatusCode) continue;
            
            var json = await message.Content.ReadAsStringAsync();

            var nameJson = (string)JsonConvert.DeserializeObject<Dictionary<string, object>>(json)!["name"];
            
            var names = nameJson.SplitAuthorName().ToList();

            while (names.Count < 2) names.Add(string.Empty);
            
            var name = names[0];
            var familyName = string.Join(' ', names.Skip(1));
            
            results.Add(new BookAuthors
            {
                Name = name,
                FamilyName = familyName
            });
        }

        return results;
    }
}