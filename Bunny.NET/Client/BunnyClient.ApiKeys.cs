using Bunny.NET.Entities;
using Newtonsoft.Json;

namespace Bunny.NET.Client;

partial class BunnyClient
{
    private string _apiKeysApiUrl;
    public async Task<List<ApiKey>> GetApiKeyList()
    {
        List<ApiKey> apiKeys = new();
        var response = await Client.GetAsync(_apiKeysApiUrl);
        response.EnsureSuccessStatusCode();
        string responseContent = await response.Content.ReadAsStringAsync();
        var obj = JsonConvert.DeserializeObject<ApiKeyListApiResponse>(responseContent);
        // TODO: implement paging
        apiKeys.AddRange(obj.Items);
        return apiKeys;
    }
}
internal class ApiKeyListApiResponse
{
    public int CurrentPage { get; set; }
    public bool HasMoreItems { get; set; }
    public List<ApiKey> Items { get; set; }
    public int TotalItems { get; set; }
}
