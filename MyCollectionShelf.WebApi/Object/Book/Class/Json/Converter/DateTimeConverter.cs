using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MyCollectionShelf.WebApi.Object.Book.Class.Json.Converter;

internal class DateTimeConverter : JsonConverter<DateTime?>
{
    private string[] Formats { get; } = { "MMM dd, yyyy", "yyyy" };

    public override void WriteJson(JsonWriter writer, DateTime? value, JsonSerializer serializer)
    {
        throw new NotImplementedException();
    }

    public override DateTime? ReadJson(JsonReader reader, Type objectType, DateTime? existingValue, bool hasExistingValue,
        JsonSerializer serializer)
    {
        string? dateStr = null;
        switch (reader.TokenType)
        {
            case JsonToken.String:
                dateStr = (reader.Value?.ToString() ?? string .Empty).Trim();
                break;
            case JsonToken.StartObject:
                var temp = JObject.Load(reader);
                dateStr = temp["value"]?.ToString();
                break;
            default:
                Console.WriteLine(reader.TokenType);
                break;
        }

        if (string.IsNullOrEmpty(dateStr)) return null;

        if (DateTime.TryParseExact(dateStr, Formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out var dateTimeParse))
        {
            return dateTimeParse;
        }
        
        try
        {
            dateTimeParse = DateTime.Parse(dateStr);
            return dateTimeParse;
        }
        catch (Exception)
        {
            // pass
        }

        return null;
    }
}