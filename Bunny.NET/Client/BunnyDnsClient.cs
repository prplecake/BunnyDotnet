using System.Net.Mime;
using System.Text;
using Bunny.NET.Entities;
using Newtonsoft.Json;

namespace Bunny.NET.Client;

partial class BunnyClient
{
    private string _apiUrl = $"{BaseUrl}/dnszone";
    private List<Zone> _zones = new();
    public async Task<Zone> GetDnsZoneByName(string zoneName)
    {
        if (!_zones.Any()) _zones = await GetDnsZones();
        return _zones.First(z => z.Domain.ToLower() == zoneName.ToLower());
    }
    async public Task<List<Zone>> GetDnsZones()
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
    public async void UpdateRecord(int zoneId, Record record, string newValue)
    {
        // Prep payload
        Dictionary<string, object?> payload = new()
        {
            {
                "Id", record.Id
            },
            {
                "Value", newValue
            }
        };
        var stringContent = new StringContent(JsonConvert.SerializeObject(payload), Encoding.Default,
            MediaTypeNames.Application.Json);
        var requestUri = $"{_apiUrl}/{zoneId}/records/{record.Id}";
        var response = await Client.PostAsync(requestUri, stringContent);
        response.EnsureSuccessStatusCode();
    }
}
internal class DnsZoneApiListResponse
{
    public int CurrentPage { get; set; }
    public bool HasMoreItems { get; set; }
    public List<Zone> Items { get; set; }
    public int TotalItems { get; set; }
}
