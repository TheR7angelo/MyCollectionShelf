using MyCollectionShelf.WebApi.Object.Book.Class;
using MyCollectionShelf.WebApi.Object.Book.Enum;
using MyCollectionShelf.WebApi.Object.Static_Class;

namespace MyCollectionShelf.WebApi.Object.Book.Static_Class;

public static class BookDownloadCover
{
    public static async Task<bool> DownloadCover(this BookCover? bookCover, EBookCoverSize bookCoverSize, string savePath, string? userAgent=null)
    {
        var uri = bookCoverSize switch
        {
            EBookCoverSize.SmallThumbnail => bookCover?.SmallThumbnail,
            EBookCoverSize.Thumbnail => bookCover?.Thumbnail,
            EBookCoverSize.Small => bookCover?.SmallThumbnail,
            EBookCoverSize.Medium => bookCover?.Small,
            EBookCoverSize.Large => bookCover?.Large,
            EBookCoverSize.ExtraLarge => bookCover?.ExtraLarge,
            _ => null
        };

        if (uri is null) return false;
        
        using var client = WebClient.GetWebClient(userAgent);

        var message = await client.GetAsync(uri);
        if (!message.IsSuccessStatusCode) return false;

        var imageBytes = await message.Content.ReadAsByteArrayAsync();
        await File.WriteAllBytesAsync(savePath, imageBytes);

        return true;
    }
}