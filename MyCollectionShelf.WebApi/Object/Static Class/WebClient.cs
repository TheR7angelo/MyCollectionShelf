namespace MyCollectionShelf.WebApi.Object.Static_Class;

public static class WebClient
{
    public static HttpClient GetWebClient(string? userAgent = null)
    {
        var client = new HttpClient();
        
        if (userAgent is not null) client.DefaultRequestHeaders.UserAgent.ParseAdd(userAgent);

        return client;
    }
}