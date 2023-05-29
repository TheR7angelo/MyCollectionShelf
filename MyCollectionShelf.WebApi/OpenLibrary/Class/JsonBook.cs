using System.Text.Json.Serialization;

namespace MyCollectionShelf.WebApi.OpenLibrary.Class;

public class JsonBook
{
    [JsonPropertyName("publishers")] 
    public List<string> Publishers { get; set; } = new();
    // public LastModified last_modified { get; set; }
    // public List<string> source_records { get; set; }
    // public string title { get; set; }
    // public Notes notes { get; set; }
    // public int number_of_pages { get; set; }
    // public List<string> isbn_13 { get; set; }
    // public List<int> covers { get; set; }
    // public Created created { get; set; }
    // public string physical_format { get; set; }
    // public List<string> isbn_10 { get; set; }
    // public string publish_date { get; set; }
    // public string key { get; set; }
    // public List<Author> authors { get; set; }
    // public int latest_revision { get; set; }
    // public List<Work> works { get; set; }
    // public Type type { get; set; }
    // public int revision { get; set; }
}

public class LastModified
{
    public string type { get; set; }
    public string value { get; set; }
}

public class Notes
{
    public string type { get; set; }
    public string value { get; set; }
}

public class Created
{
    public string type { get; set; }
    public string value { get; set; }
}

public class Author
{
    public string key { get; set; }
}

public class Work
{
    public string key { get; set; }
}

public class Type
{
    public string key { get; set; }
}