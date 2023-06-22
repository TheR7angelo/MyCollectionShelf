using System.Text.RegularExpressions;

namespace MyCollectionShelf.Wpf.Shelf.Book.Object.Class.Static;

public static partial class RegexFunction
{
    [GeneratedRegex("[^0-9]+")]
    private static partial Regex IsNumberRegex();

    public static bool IsNumber(this string str) => IsNumberRegex().IsMatch(str);
}