using System.Globalization;

namespace MyCollectionShelf.Sql;

public static class Common
{
    private const string Null = "NULL";

    public static string ToSql(this string? value) =>
        string.IsNullOrEmpty(value) ? Null : $"'{value.Replace("'", "''")}'";

    public static string ToSql(this float? value) =>
        value is null ? Null : value.Value.ToString(CultureInfo.InvariantCulture);

    public static string ToSql(this double? value) =>
        value is null ? Null : value.Value.ToString(CultureInfo.InvariantCulture);

    public static string ToSql(this int? value) => value is null ? Null : $"{value}";
    public static string ToSql(this long? value) => value is null ? Null : $"{value}";

    public static string ToSql(this bool? value)
    {
        return value switch
        {
            null => Null,
            false => "0",
            true => "1"
        };
    }
}
