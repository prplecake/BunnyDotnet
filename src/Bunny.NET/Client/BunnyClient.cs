using System.Net.Http.Headers;
using System.Net.Mime;
using Bunny.NET.Constants;
using Serilog;

namespace Bunny.NET.Client;

public partial class BunnyClient
{
    public const string Scheme = "https://";
    public static string? BaseUrl;
    private readonly ILogger _logger = Log.ForContext<BunnyClient>();
    protected readonly string? ApiKey;
    protected HttpClient Client;
    public BunnyClient(HttpClient client)
    {
        BaseUrl = $"{Scheme}{Host.Endpoint.Api}";
        _apiKeysApiUrl = $"{BaseUrl}/apikey";
        _countriesApiUrl = $"{BaseUrl}/country";
        _dnsApiUrl = $"{BaseUrl}/dnszone";
        _purgeApiUrl = $"{BaseUrl}/purge";
        _regionApiUrl = $"{BaseUrl}/region";
        _statisticsApiUrl = $"{BaseUrl}/statistics";
        // Configure HttpClient
        Client = client;
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
    public BunnyClient(string apiKey) : this(new HttpClient())
    {
        ApiKey = apiKey;
    }
    public BunnyClient(string apiKey, HttpClient client) : this(client)
    {
        ApiKey = apiKey;
    }
}
