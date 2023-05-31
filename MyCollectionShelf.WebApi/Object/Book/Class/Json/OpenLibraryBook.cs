using MyCollectionShelf.WebApi.Object.Book.Class.Json.Converter;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MyCollectionShelf.WebApi.Object.Book.Class.Json;

public class OpenLibraryBook
{
    [JsonProperty("publishers")] 
    public List<string>? Publishers { get; set; }

    [JsonProperty("last_modified")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime? LastModified { get; set; }

    [JsonProperty("source_records")] 
    public List<string>? SourceRecords { get; set; }

    [JsonProperty("title")] 
    public string? Title { get; set; }

    [JsonProperty("notes")]
    [JsonConverter(typeof(KeyValueConverter))]
    public string? Notes { get; set; }

    [JsonProperty("number_of_pages")] 
    public int? NumberOfPages { get; set; }

    [JsonProperty("isbn_13")] 
    public List<string>? Isbn13 { get; set; }

    [JsonProperty("covers")] 
    public List<int>? Covers { get; set; }

    [JsonProperty("created")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime? Created { get; set; }

    [JsonProperty("physical_format")] 
    public string? PhysicalFormat { get; set; }

    [JsonProperty("isbn_10")] 
    public List<string>? Isbn10 { get; set; }

    [JsonProperty("publish_date")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime? PublishDate { get; set; }

    [JsonProperty("key")] 
    public string? Key { get; set; }

    [JsonProperty("authors")] 
    public List<AuthorOpenLibrary>? Authors { get; set; }

    [JsonProperty("latest_revision")] 
    public int? LatestRevision { get; set; }

    [JsonProperty("works")] 
    public List<Work>? Works { get; set; }

    [JsonProperty("type")] 
    public TypeData? Type { get; set; }

    [JsonProperty("revision")] 
    public int Revision { get; set; }
}

public class AuthorOpenLibrary
{
    [JsonProperty("key")] 
    public string? Key { get; set; }
}

public class Work
{
    [JsonProperty("key")] 
    public string? Key { get; set; }
}

public class TypeData
{
    [JsonProperty("key")] 
    public string? Key { get; set; }
}

public class KeyValueConverter : JsonConverter<string>
{
    public override void WriteJson(JsonWriter writer, string? value, JsonSerializer serializer)
    {
        throw new NotImplementedException();
    }

    public override string? ReadJson(JsonReader reader, Type objectType, string? existingValue, bool hasExistingValue,
        JsonSerializer serializer)
    {
        if (reader.TokenType == JsonToken.StartObject)
        {
            var obj = JObject.Load(reader);
            var valueToken = obj["value"];
            return valueToken?.Value<string>() ?? existingValue;
        }
        else
        {
            return existingValue;
        }
    }
}