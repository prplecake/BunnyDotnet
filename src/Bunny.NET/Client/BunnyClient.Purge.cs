using Bunny.NET.Entities;
using Newtonsoft.Json;

namespace Bunny.NET.Client;

partial class BunnyClient
{
    private string _purgeApiUrl;
    public async Task<List<Region>> PurgeUrl(
        string url,
        string? headerName = null,
        string? headerValue = null,
        bool async = false)
    {
        Dictionary<string, string> queryParams = new()
        {
            {
                "url", url
            }
        };
        if (headerName is not null) queryParams["headerName"] = headerName;
        if (headerValue is not null) queryParams["headerValue"] = headerValue;
        if (async) queryParams["async"] = async.ToString();
        string? queryString = await new FormUrlEncodedContent(queryParams).ReadAsStringAsync();
        var response = await Client.GetAsync($"{_purgeApiUrl}?{queryString}");
        response.EnsureSuccessStatusCode();
        string responseContent = await response.Content.ReadAsStringAsync();
        var obj = JsonConvert.DeserializeObject<List<Region>>(responseContent);
        return obj;
    }
}
