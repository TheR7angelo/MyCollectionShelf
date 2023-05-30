using System.Globalization;
using Newtonsoft.Json;

namespace MyCollectionShelf.WebApi.Object.Class.Json.Converter;

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
        if (reader.TokenType != JsonToken.String) return null;
        var dateStr = (reader.Value?.ToString() ?? string .Empty).Trim();

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