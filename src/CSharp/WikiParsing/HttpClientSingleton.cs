using System.Net.Http;


namespace WikiParsing;

internal static class HttpClientSingleton
{
    public static readonly HttpClient Instance = new HttpClient();
}
