using System.Collections.ObjectModel;
using HtmlAgilityPack;
using MyCollectionShelf.Book.Object.Static_Class;
using MyCollectionShelf.Sql.Object.Book.Class;
using MyCollectionShelf.WebApi.Object.Static_Class;

namespace MyCollectionShelf.Book;

public class AmazonApi : IBookApi
{
    private static Uri BaseInformationIsbnApi { get; } = new("https://www.amazon.fr/s/ref=sr_adv_b/");
    private static Uri BaseSite { get; } = new("https://www.amazon.fr/");

    private string? UserAgent { get; }

    public AmazonApi(string? userAgent = null)
    {
        UserAgent = userAgent;
    }

    public async Task<Sql.Object.Book.Class.Book?> GetBookInformation(string isbn13)
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

        var authors = GetAuthors(document.DocumentNode.SelectSingleNode("//div[@id='bylineInfo']"));
        var summarize = GetSummarize(document.DocumentNode.SelectSingleNode("//div[@class='a-expander-content a-expander-partial-collapse-content']"));

        var carousel = document.DocumentNode.SelectSingleNode("//div[@class='a-carousel-row-inner']")?.InnerHtml;
        var carouselResult = GetCarouselResult(carousel);

        var img = document.DocumentNode.SelectSingleNode("//div[@id='imageBlockContainer']//img[@id='imgBlkFront']")?.GetAttributeValue("src", string.Empty);
        var imgUri = string.IsNullOrEmpty(img) ? null : new Uri(img);
        
        return new Sql.Object.Book.Class.Book
        {
            BookInformations = new BookInformations
            {
                Title = document.DocumentNode.SelectSingleNode("//span[@id='productTitle']").InnerHtml,
                BookAuthors = authors,
                Summarize = summarize,
                BookSeries = new BookSeries { Title = carouselResult.series },
                PageNumber = carouselResult.pageNumber,
                Editor = carouselResult.editor,
                PublishDate = carouselResult.publishDate,
                Isbn = isbn13,
                BookCover = new BookCover
                {
                    SmallThumbnail = imgUri,
                    Thumbnail = imgUri,
                    Small = imgUri,
                    Medium = imgUri,
                    Large = imgUri,
                    ExtraLarge = imgUri
                }
            }
        };
    }

    private static (string? series, long? pageNumber, string? editor, DateTime? publishDate) GetCarouselResult(string? carousel)
    {
        (string? series, long? pageNumber, string? editor, DateTime? publishDate) result = new();

        if (carousel is null) return result;

        result.series = GetSeries(carousel);
        result.pageNumber = GetPageNumber(carousel);
        result.editor = GetEditor(carousel);
        result.publishDate = GetPublishDate(carousel);
        
        return result;
    }

    private static DateTime? GetPublishDate(string carousel)
    {
        var document = new HtmlDocument();
        document.LoadHtml(carousel);

        var publishElement = document.DocumentNode.SelectSingleNode("//div[@id='rpi-attribute-book_details-publication_date']//div[@class='a-section a-spacing-none a-text-center rpi-attribute-value']/span");
        var publishValue = publishElement?.InnerText.Trim();
        
        if (string.IsNullOrEmpty(publishValue)) return null;
        
        var sucess = DateTime.TryParse(publishValue, out var value);
        return sucess ? value : null;
    }

    private static string? GetEditor(string carousel)
    {
        var document = new HtmlDocument();
        document.LoadHtml(carousel);

        var editorElement = document.DocumentNode.SelectSingleNode("//div[@id='rpi-attribute-book_details-publisher']//div[@class='a-section a-spacing-none a-text-center rpi-attribute-value']/span");
        var editorValue = editorElement?.InnerText.Trim();

        return string.IsNullOrEmpty(editorValue) ? null : editorValue;
    }

    private static long? GetPageNumber(string carousel)
    {
        var document = new HtmlDocument();
        document.LoadHtml(carousel);
        
        var pagesElement = document.DocumentNode.SelectSingleNode("//div[@id='rpi-attribute-book_details-fiona_pages']/div[@class='a-section a-spacing-none a-text-center rpi-attribute-value']/span");
        var pagesValue = pagesElement?.InnerText.Trim();

        if (string.IsNullOrEmpty(pagesValue)) return null;

        var pageNumberStr = pagesValue.Split(' ');
        var sucess = long.TryParse(pageNumberStr[0], out var value);

        return sucess ? value : null;
    }

    private static string? GetSeries(string carousel)
    {
        var document = new HtmlDocument();
        document.LoadHtml(carousel);
        
        var linkElement = document.DocumentNode.SelectSingleNode("//div[@id='rpi-attribute-book_details-series']//a[@class='a-link-normal']");
        var linkValue = linkElement?.InnerText.Trim();

        return linkValue;
    }

    private static string? GetSummarize(HtmlNode? selectSingleNode)
    {
        if (selectSingleNode is null) return null;

        var document = new HtmlDocument();
        document.LoadHtml(selectSingleNode.InnerHtml);

        var results = new List<string>();

        foreach (var xpath in new List<string> { "//span[@class='a-text-bold']", "//p" })
        {
            var text = document.DocumentNode.SelectSingleNode(xpath)?.InnerText;
            if (string.IsNullOrEmpty(text)) continue;

            results.Add(text);
        }

        return results.Count.Equals(0) ? null : string.Join('\n', results);
    }

    private static ObservableCollection<BookAuthor> GetAuthors(HtmlNode? singleNode)
    {
        var results = new ObservableCollection<BookAuthor>();

        var nodes = singleNode?.SelectNodes("//span[@class='author notFaded']");

        if (nodes is null) return results;

        foreach (var node in nodes)
        {
            var document = new HtmlDocument();
            document.LoadHtml(node.InnerHtml);

            var authorHtml = document.DocumentNode.SelectSingleNode("//a").InnerHtml;

            var authors = authorHtml.SplitAuthorName().ToList();
            while (authors.Count < 2) authors.Add(string.Empty);

            var name = authors[0];
            var familyName = string.Join(' ', authors.Skip(1));

            var author = new BookAuthor
            {
                Name = name,
                FamilyName = familyName
            };

            // var roleHtml = document.DocumentNode.SelectSingleNode("//span[@class='contribution']/span").InnerHtml;
            // if (roleHtml is not null)
            // {
            //     var role = roleHtml.Trim().Trim(',').Trim('(').Trim(')');
            //     if (!string.IsNullOrEmpty(role))
            //     {
            //         author.Role = role;
            //     }
            // }

            results.Add(author);
        }

        return results;
    }
}