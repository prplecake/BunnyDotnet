using Bunny.API.NET.Constants;

namespace Bunny.API.NET.Client;

public partial class BunnyClient
{
    public const string Scheme = "https://";
    public static string? BaseUrl;
    private readonly ILogger _logger = Log.ForContext<BunnyClient>();
    protected string? ApiKey;
    protected HttpClient? Client;
    public BunnyClient()
    {
        BaseUrl = $"{Scheme}{Host.Endpoint.Api}";
        _apiKeysApiUrl = $"{BaseUrl}/apikey";
        _countriesApiUrl = $"{BaseUrl}/country";
        _dnsApiUrl = $"{BaseUrl}/dnszone";
        _purgeApiUrl = $"{BaseUrl}/purge";
        _regionApiUrl = $"{BaseUrl}/region";
        _statisticsApiUrl = $"{BaseUrl}/statistics";
    }
    public BunnyClient CreateClient(HttpClient? client = null)
    {
        Client = client ?? new HttpClient();
        Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(MediaTypeNames.Application.Json));
        Client.DefaultRequestHeaders.UserAgent.Add(Meta.UserAgent);
        Client.DefaultRequestHeaders.Add("AccessKey", ApiKey);
        return this;
    }
    public BunnyClient SetApiKey(string apiKey)
    {
        _logger.Verbose("Setting API key");
        ApiKey = apiKey;
        return this;
    }
}
