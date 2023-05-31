using MyCollectionShelf.WebApi.Object.Book.Class.Json.Converter;
using Newtonsoft.Json;

namespace MyCollectionShelf.WebApi.Object.Book.Class.Json;

public class GoogleBooksBook
{
    [JsonProperty("kind")]
    public string? Kind { get; set; }
    
    [JsonProperty("id")]
    public string? Id { get; set; }
    
    [JsonProperty("etag")]
    public string? Etag { get; set; }
    
    [JsonProperty("selfLink")]
    public Uri? SelfLink { get; set; }
    
    [JsonProperty("volumeInfo")]
    public VolumeInfo? VolumeInfo { get; set; }
    
    [JsonProperty("saleInfo")]
    public SaleInfo? SaleInfo { get; set; }
    
    [JsonProperty("accessInfo")]
    public AccessInfo? AccessInfo { get; set; }
}

public class AccessInfo
{
    [JsonProperty("country")]
    public string? Country { get; set; }
    
    [JsonProperty("viewability")]
    public string? Viewability { get; set; }
    
    [JsonProperty("embeddable")]
    public bool? Embeddable { get; set; }
    
    [JsonProperty("publicDomain")]
    public bool? PublicDomain { get; set; }
    
    [JsonProperty("textToSpeechPermission")]
    public string? TextToSpeechPermission { get; set; }
    
    [JsonProperty("epub")]
    public dynamic? Epub { get; set; }
    
    [JsonProperty("pdf")]
    public dynamic? Pdf { get; set; }
    
    [JsonProperty("webReaderLink")]
    public Uri? WebReaderLink { get; set; }
    
    [JsonProperty("accessViewStatus")]
    public string? AccessViewStatus { get; set; }
    
    [JsonProperty("quoteSharingAllowed")]
    public bool? QuoteSharingAllowed { get; set; }
}

public class SaleInfo
{
    [JsonProperty("country")]
    public string? Country { get; set; }
    
    [JsonProperty("saleability")]
    public string? Saleability { get; set; }
    
    [JsonProperty("isEbook")]
    public bool? IsEbook { get; set; }
}

public class VolumeInfo
{
    [JsonProperty("title")]
    public string? Title { get; set; }
    
    [JsonProperty("authors")]
    public List<string>? Authors { get; set; }
    
    [JsonProperty("publisher")]
    public string? Publisher { get; set; }
    
    [JsonProperty("publishedDate")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime? PublishedDate { get; set; }
    
    [JsonProperty("description")]
    public string? Description { get; set; }
    
    [JsonProperty("industryIdentifiers")]
    public List<IndustryIdentifier>? IndustryIdentifiers { get; set; }
    
    [JsonProperty("readingModes")]
    public ReadingModes? ReadingModes { get; set; }
    
    [JsonProperty("pageCount")]
    public int? PageCount { get; set; }
    
    [JsonProperty("printedPageCount")]
    public int? PrintedPageCount { get; set; }
    
    [JsonProperty("dimensions")]
    public Dimensions? Dimensions { get; set; }
    
    [JsonProperty("printType")]
    public string? PrintType { get; set; }
    
    [JsonProperty("maturityRating")]
    public string? MaturityRating { get; set; }
    
    [JsonProperty("allowAnonLogging")]
    public bool? AllowAnonLogging { get; set; }
    
    [JsonProperty("contentVersion")]
    public string? ContentVersion { get; set; }
    
    [JsonProperty("panelizationSummary")]
    public PanelizationSummary? PanelizationSummary { get; set; }
    
    [JsonProperty("imageLinks")]
    public ImageLinks? ImageLinks { get; set; }
    
    [JsonProperty("language")]
    public string? Language { get; set; }
    
    [JsonProperty("previewLink")]
    public Uri? PreviewLink { get; set; }
    
    [JsonProperty("infoLink")]
    public Uri? InfoLink { get; set; }
    
    [JsonProperty("canonicalVolumeLink")]
    public Uri? CanonicalVolumeLink { get; set; }
}

public class ReadingModes
{
    [JsonProperty("text")]
    public bool? Text { get; set; }
    
    [JsonProperty("image")]
    public bool? Image { get; set; }
}

public class IndustryIdentifier
{
    [JsonProperty("type")]
    public string? Type { get; set; }
    
    [JsonProperty("identifier")]
    public string? Identifier { get; set; }
}

public class Dimensions
{
    [JsonProperty("height")]
    public string? Height { get; set; }
    
    [JsonProperty("width")]
    public string? Width { get; set; }
    
    [JsonProperty("thickness")]
    public string? Thickness { get; set; }
}

public class ImageLinks
{
    [JsonProperty("smallThumbnail")]
    public Uri? SmallThumbnail { get; set; }
    
    [JsonProperty("thumbnail")]
    public Uri? Thumbnail { get; set; }
    
    [JsonProperty("small")]
    public Uri? Small { get; set; }
    
    [JsonProperty("medium")]
    public Uri? Medium { get; set; }
    
    [JsonProperty("large")]
    public Uri? Large { get; set; }
    
    [JsonProperty("extraLarge")]
    public Uri? ExtraLarge { get; set; }
}

public class PanelizationSummary
{
    [JsonProperty("containsEpubBubbles")]
    public bool? ContainsEpubBubbles { get; set; }
    
    [JsonProperty("containsImageBubbles")]
    public bool? ContainsImageBubbles { get; set; }
}

public class Root
{
    [JsonProperty("items")]
    public List<Item>? Items { get; set; }
}

public class Item
{
    [JsonProperty("selfLink")]
    public Uri? SelfLink { get; set; }
}