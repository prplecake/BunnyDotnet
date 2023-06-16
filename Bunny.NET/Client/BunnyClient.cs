using System.Net.Http.Headers;
using System.Net.Mime;
using Bunny.NET.Constants;
using Serilog;

namespace Bunny.NET.Client;

public partial class BunnyClient
{
    public const string Scheme = "https://";
    protected readonly string? ApiKey;
    public static string? BaseUrl;
    protected readonly HttpClient Client = new();
    private readonly ILogger _logger = Log.ForContext<BunnyClient>();
    public BunnyClient(string apiKey)
    {
        ApiKey = apiKey;
        BaseUrl = $"{Scheme}{Host.Endpoint.Api}";
        _apiKeysApiUrl = $"{BaseUrl}/apikey";
        _countriesApiUrl = $"{BaseUrl}/country";
        _dnsApiUrl = $"{BaseUrl}/dnszone";
        _regionApiUrl = $"{BaseUrl}/region";
        _statisticsApiUrl = $"{BaseUrl}/statistics";
        // Configure HttpClient
        Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(MediaTypeNames.Application.Json));
        Client.DefaultRequestHeaders.UserAgent.Add(Meta.UserAgent);
        Client.DefaultRequestHeaders.Add("AccessKey", ApiKey);
        
        _logger.Debug("Finished setting up {Type}\n" +
                      "BaseUrl: {BaseUrl}\n" +
                      "_dnsApiUrl: {DnsApiUrl}",
            typeof(BunnyClient),
            BaseUrl,
            _dnsApiUrl
            );
    }
}
