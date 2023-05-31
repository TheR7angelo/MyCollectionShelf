﻿using System.Text.RegularExpressions;
using MyCollectionShelf.WebApi.Object.Book.Class;
using MyCollectionShelf.WebApi.Object.Book.Class.Json;
using MyCollectionShelf.WebApi.Object.Static_Class;
using Newtonsoft.Json;

namespace MyCollectionShelf.WebApi.Book;

public partial class GoogleBooksApi : IBookApi
{
    private static Uri BaseInformationIsbnApi { get; } = new("https://www.googleapis.com/books/v1/volumes?q=isbn:");

    private string? UserAgent { get; set; }
    
    public GoogleBooksApi(string? userAgent=null)
    {
        UserAgent = userAgent;
    }
    
    public async Task<Object.Book.Class.Book?> GetBookInformation(string isbn13)
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

        var authors = new List<BookAuthors>();
        foreach (var authorStr in googleBook?.VolumeInfo?.Authors ?? new List<string>())
        {
            var author = MyRegex().Matches(authorStr).Select(s => s.Value).ToList();

            while (author.Count < 2) author.Add(string.Empty);
            
            var name = author[0];
            var familyName = string.Join(' ', author.Skip(1));
            
            authors.Add(new BookAuthors
            {
                Name = name,
                FamilyName = familyName
            });
        }
        
        return new Object.Book.Class.Book
        {
            BookInformations = new BookInformations
            {
                Title = googleBook?.VolumeInfo?.Title,
                Authors = authors,
                BookCover = new BookCover
                {
                    SmallThumbnail = googleBook?.VolumeInfo?.ImageLinks?.SmallThumbnail,
                    Thumbnail = googleBook?.VolumeInfo?.ImageLinks?.Thumbnail,
                    Small = googleBook?.VolumeInfo?.ImageLinks?.Small,
                    Medium = googleBook?.VolumeInfo?.ImageLinks?.Medium,
                    Large = googleBook?.VolumeInfo?.ImageLinks?.Large,
                    ExtraLarge = googleBook?.VolumeInfo?.ImageLinks?.ExtraLarge
                },
                Summarize = googleBook?.VolumeInfo?.Description,
                // Series = "", // L'API ne le fournit pas
                // TomeNumber = 0, // L'API ne le fournit pas
                Genre = new List<string>(),
                PublishDate = googleBook?.VolumeInfo?.PublishedDate,
                Editor = googleBook?.VolumeInfo?.Publisher,
                PageNumber = googleBook?.VolumeInfo?.PageCount,
                Isbn = isbn13
            }
        };
    }

    [GeneratedRegex(@"(?=[A-Z])\w+")]
    private static partial Regex MyRegex();
}