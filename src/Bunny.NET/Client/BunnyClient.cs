namespace Bunny.NET.Client;

public partial class BunnyClient
{
    public const string Scheme = "https://";
    public static string? BaseUrl;
    private readonly ILogger _logger = Log.ForContext<BunnyClient>();
    protected readonly string? ApiKey;
    protected readonly HttpClient Client = new();
    public BunnyClient(string apiKey)
    {
        ApiKey = apiKey;
        BaseUrl = $"{Scheme}{Host.Endpoint.Api}";
        _apiKeysApiUrl = $"{BaseUrl}/apikey";
        _countriesApiUrl = $"{BaseUrl}/country";
        _dnsApiUrl = $"{BaseUrl}/dnszone";
        _purgeApiUrl = $"{BaseUrl}/purge";
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
