using Bunny.API.NET.Entities;

namespace Bunny.API.NET.Client;

partial class BunnyClient
{
    private string _apiKeysApiUrl;
    public async Task<Result<List<ApiKey>>> GetApiKeyList()
    {
        List<ApiKey> apiKeys = new();
        var response = await Client.GetAsync(_apiKeysApiUrl);
        try
        {
            response.EnsureSuccessStatusCode();
            string responseContent = await response.Content.ReadAsStringAsync();
            var obj = JsonConvert.DeserializeObject<ApiKeyListApiResponse>(responseContent);
            // TODO: implement paging
            apiKeys.AddRange(obj.Items);
            return new Result<List<ApiKey>> { StatusCode = response.StatusCode, Success = true, Data = apiKeys };
        }
        catch (HttpRequestException)
        {
            return new Result<List<ApiKey>> { StatusCode = response.StatusCode, Success = false };
        }
    }
}
internal class ApiKeyListApiResponse
{
    public int CurrentPage { get; set; }
    public bool HasMoreItems { get; set; }
    public List<ApiKey> Items { get; set; }
    public int TotalItems { get; set; }
}
