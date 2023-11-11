using Bunny.NET.Entities;
using Newtonsoft.Json;

namespace Bunny.NET.Client;

partial class BunnyClient
{
    private string _regionApiUrl;
    public async Task<Result<List<Region>>> GetRegionList()
    {
        var response = await Client.GetAsync(_regionApiUrl);
        if (!response.IsSuccessStatusCode)
            return new Result<List<Region>> { StatusCode = response.StatusCode, Success = false };
        string responseContent = await response.Content.ReadAsStringAsync();
        var obj = JsonConvert.DeserializeObject<List<Region>>(responseContent);
        return new Result<List<Region>> { StatusCode = response.StatusCode, Success = true, Data = obj };
    }
}
