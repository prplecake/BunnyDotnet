using System.Net.Mime;
using System.Text;
using Bunny.NET.Entities;
using Newtonsoft.Json;

namespace Bunny.NET.Client;

[PublicAPI]
partial class BunnyClient
{
    private string _dnsApiUrl;
    private List<Zone> _zones = new();
    public async Task<Zone> GetZoneByName(string zoneName)
    {
        if (!_zones.Any()) _zones = await GetZones();
        return _zones.First(z => z.Domain.ToLower() == zoneName.ToLower());
    }
    public async Task<List<Zone>> GetZones()
    {
        List<Zone> zones = new();
        var response = await Client.GetAsync(_dnsApiUrl);
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
        DnsRecord.ChangeRequestPayload payload2 = new DnsRecord.ChangeRequestPayload
        {
            Id = record.Id,
            Value = newValue
        };
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
        var stringContent2 = new StringContent(JsonConvert.SerializeObject(payload2), Encoding.Default,
            MediaTypeNames.Application.Json);
        var requestUri = $"{_dnsApiUrl}/{zoneId}/records/{record.Id}";
        var response = await Client.PostAsync(requestUri, stringContent);
        response.EnsureSuccessStatusCode();
    }
}
public static class DnsRecord
{
    public class ChangeRequestPayload
    {
        public Type Type { get; set; }
        public int Ttl { get; set; }
        public string Value { get; set; }
        public string Name { get; set; }
        public int Weight { get; set; }
        public int Priority { get; set; }
        public int Flags { get; set; }
        public string Tag { get; set; }
        public int PullZoneId { get; set; }
        public int ScriptId { get; set; }
        public bool Accelerated { get; set; }
        public MonitorType MonitorType { get; set; }
        public double GeolocationLatitude { get; set; }
        public double GeolocationLongitude { get; set; }
        public string LatencyZone { get; set; }
        public SmartRoutingType SmartRoutingType { get; set; }
        public bool Disabled { get; set; }
        public int Id { get; set; }
        public string Comment { get; set; }
    }
    public enum Type
    {
        A,
        AAAA,
        CNAME,
        TXT,
        MX,
        Redirect,
        Flatten,
        PullZone,
        SRV,
        CAA,
        PTR,
        Script,
        NS
    }
    public enum MonitorType
    {
        None,
        Ping,
        Http,
        Monitor
    }
    public enum SmartRoutingType
    {
        None,
        Latency,
        Geolocation
    }
}
internal class DnsZoneApiListResponse
{
    public int CurrentPage { get; set; }
    public bool HasMoreItems { get; set; }
    public List<Zone> Items { get; set; }
    public int TotalItems { get; set; }
}
