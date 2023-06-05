using HtmlAgilityPack;
using MyCollectionShelf.WebApi.Object.Static_Class;

namespace MyCollectionShelf.Book;

public class AmazonApi : IBookApi
{
    private static Uri BaseInformationIsbnApi { get; } = new("https://www.amazon.fr/s/ref=sr_adv_b/");
    private static Uri BaseSite { get; } = new Uri("https://www.amazon.fr/");

    private string? UserAgent { get; }
    
    public AmazonApi(string? userAgent=null)
    {
        UserAgent = userAgent;
    }
    
    public async Task<MyCollectionShelf.Book.Object.Class.Book?> GetBookInformation(string isbn13)
    {
        using var client = WebClient.GetWebClient(UserAgent);
        
        var htmlPage = await client.GetAsync($"{BaseInformationIsbnApi}/?search-alias=stripbooks&unfiltered=1&field-keywords=&field-isbn={isbn13}");

        if (!htmlPage.IsSuccessStatusCode) return null;
        
        var html = await htmlPage.Content.ReadAsStringAsync();

        var document = new HtmlDocument();
        document.LoadHtml(html);

        var linkNode = document.DocumentNode.SelectSingleNode(
            "/html[1]/body[1]/div[1]/div[2]/div[1]/div[1]/div[1]/span[1]/div[1]/div[2]/div[1]/div[1]/div[1]/div[1]/div[1]/div[2]/div[1]/div[1]/div[1]/h2[1]/a[1]");

        var link = linkNode?.GetAttributeValue("href", "");
        if (link is null) return null;

        htmlPage = await client.GetAsync($"{BaseSite}{link}");
        if (!htmlPage.IsSuccessStatusCode) return null;
        
        html = await htmlPage.Content.ReadAsStringAsync();
        document.LoadHtml(html);
        
        // todo à finir
        
        return new MyCollectionShelf.Book.Object.Class.Book
        {
            
        };
    }
}