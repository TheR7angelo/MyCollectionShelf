using System.Text.RegularExpressions;

namespace MyCollectionShelf.Book.Object.Static_Class;

public static partial class RegexStatic
{
    public static IEnumerable<string> SplitAuthorName(this string values)
    {
        return MatchAuthorName().Matches(values).Select(s => s.Value);
    }
    
    [GeneratedRegex(@"(?=[A-Z])\w+")]
    public static partial Regex MatchAuthorName(); 
}