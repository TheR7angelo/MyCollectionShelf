using System.Collections.ObjectModel;
using HtmlAgilityPack;
using MyCollectionShelf.Book.Object.Class;
using MyCollectionShelf.Book.Object.Static_Class;
using MyCollectionShelf.WebApi.Object.Static_Class;

namespace MyCollectionShelf.Book;

public class AmazonApi : IBookApi
{
    private static Uri BaseInformationIsbnApi { get; } = new("https://www.amazon.fr/s/ref=sr_adv_b/");
    private static Uri BaseSite { get; } = new Uri("https://www.amazon.fr/");

    private string? UserAgent { get; }

    public AmazonApi(string? userAgent = null)
    {
        UserAgent = userAgent;
    }

    public async Task<MyCollectionShelf.Book.Object.Class.Book?> GetBookInformation(string isbn13)
    {
        using var client = WebClient.GetWebClient(UserAgent);

        var htmlPage =
            await client.GetAsync(
                $"{BaseInformationIsbnApi}/?search-alias=stripbooks&unfiltered=1&field-keywords=&field-isbn={isbn13}");

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

        var authors = GetAuthors(document.DocumentNode.SelectSingleNode("//div[@id='bylineInfo']"));
        var summarize =
            GetSummarize(document.DocumentNode.SelectSingleNode("//div[@class='a-expander-content a-expander-partial-collapse-content']"));

        return new MyCollectionShelf.Book.Object.Class.Book
        {
            BookInformations = new BookInformations
            {
                Title = document.DocumentNode.SelectSingleNode("//span[@id='productTitle']").InnerHtml,
                Authors = authors,
                Summarize = summarize,
            }
        };
    }

    private static string? GetSummarize(HtmlNode? selectSingleNode)
    {
        if (selectSingleNode is null) return null;

        var document = new HtmlDocument();
        document.LoadHtml(selectSingleNode.InnerHtml);

        var results = new List<string>();

        foreach (var xpath in new List<string> { "//span[@class='a-text-bold']", "//p" })
        {
            var text = document.DocumentNode.SelectSingleNode(xpath).InnerText;
            if (string.IsNullOrEmpty(text)) continue;

            results.Add(text);
        }

        return results.Count.Equals(0) ? null : string.Join('\n', results);
    }

    private static ObservableCollection<BookAuthors> GetAuthors(HtmlNode? singleNode)
    {
        var results = new ObservableCollection<BookAuthors>();

        var nodes = singleNode?.SelectNodes("//span[@class='author notFaded']");

        if (nodes is null)
        {
            results.Add(new BookAuthors());
            return results;
        }

        foreach (var node in nodes)
        {
            var document = new HtmlDocument();
            document.LoadHtml(node.InnerHtml);

            var authorHtml = document.DocumentNode.SelectSingleNode("//a").InnerHtml;

            var authors = authorHtml.SplitAuthorName().ToList();
            while (authors.Count < 2) authors.Add(string.Empty);

            var name = authors[0];
            var familyName = string.Join(' ', authors.Skip(1));

            var author = new BookAuthors
            {
                Name = name,
                FamilyName = familyName
            };

            var roleHtml = document.DocumentNode.SelectSingleNode("//span[@class='contribution']/span").InnerHtml;
            if (roleHtml is not null)
            {
                var role = roleHtml.Trim().Trim(',').Trim('(').Trim(')');
                if (!string.IsNullOrEmpty(role))
                {
                    author.Role = role;
                }
            }

            results.Add(author);
        }

        return results;
    }
}