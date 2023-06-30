using System.Globalization;

namespace MyCollectionShelf.Sql;

public static class Common
{
    private const string Null = "NULL";

    public static string SqlNull(this string? value) =>
        string.IsNullOrEmpty(value) ? Null : $"'{value.Replace("'", "''")}'";

    public static string SqlNull(this float? value) =>
        value is null ? Null : value.Value.ToString(CultureInfo.InvariantCulture);

    public static string SqlNull(this double? value) =>
        value is null ? Null : value.Value.ToString(CultureInfo.InvariantCulture);

    public static string SqlNull(this int? value) => value is null ? Null : $"{value}";
    public static string SqlNull(this long? value) => value is null ? Null : $"{value}";

    public static string SqlNull(this bool? value)
    {
        return value switch
        {
            null => Null,
            false => "0",
            true => "1"
        };
    }
}
