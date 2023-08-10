using Bunny.NET.Entities;
using Newtonsoft.Json;

namespace Bunny.NET.Client;

partial class BunnyClient
{
    private string _regionApiUrl;
    public async Task<List<Region>> GetRegionList()
    {
        var response = await Client.GetAsync(_regionApiUrl);
        response.EnsureSuccessStatusCode();
        string responseContent = await response.Content.ReadAsStringAsync();
        var obj = JsonConvert.DeserializeObject<List<Region>>(responseContent);
        return obj;
    }
}
