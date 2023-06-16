using Bunny.NET.Entities;
using Newtonsoft.Json;

namespace Bunny.NET.Client;

partial class BunnyClient
{
    private string _apiUrl = $"{BaseUrl}/dnszone";
    private List<Zone> _zones = new();
    public async Task<Zone> GetZoneByName(string zoneName)
    {
        if (!_zones.Any()) _zones = await GetZones();
        return _zones.First(z => z.Domain.ToLower() == zoneName.ToLower());
    }
    async public Task<List<Zone>> GetZones()
    {
        List<Zone> zones = new();
        var response = await Client.GetAsync(_apiUrl);
        response.EnsureSuccessStatusCode();
        string responseContent = await response.Content.ReadAsStringAsync();
        var obj = JsonConvert.DeserializeObject<DnsZoneApiListResponse>(responseContent);
        // TODO: implement paging
        zones.AddRange(obj.Items);
        _zones = zones;
        return zones;
    }
}
internal class DnsZoneApiListResponse
{
    public int CurrentPage { get; set; }
    public bool HasMoreItems { get; set; }
    public List<Zone> Items { get; set; }
    public int TotalItems { get; set; }
}
