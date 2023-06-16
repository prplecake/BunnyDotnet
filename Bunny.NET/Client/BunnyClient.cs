using System.Net.Http.Headers;
using System.Net.Mime;
using Bunny.NET.Constants;

namespace Bunny.NET.Client;

public partial class BunnyClient
{
    protected const string Scheme = "https";
    protected string? ApiKey;
    protected static string? BaseUrl;
    protected HttpClient Client = new();
    public BunnyClient(string apiKey)
    {
        ApiKey = apiKey;
        BaseUrl = Host.Endpoint.Api;
        // Configure HttpClient
        Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(MediaTypeNames.Application.Json));
        Client.DefaultRequestHeaders.UserAgent.Add(Meta.UserAgent);
        Client.DefaultRequestHeaders.Add("AccessKey", ApiKey);
    }
}
