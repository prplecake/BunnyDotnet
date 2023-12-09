namespace Bunny.NET.Client;

[PublicAPI]
partial class BunnyClient
{
    private string _dnsApiUrl;
    private List<Zone> _zones = new();
    public async Task<Result> AddRecord(int zoneId, Record record)
    {
        // Prep payload
        var payload = new DnsRecord.ChangeRequestPayload(record);
        var stringContent = new StringContent(
            JsonConvert.SerializeObject(payload, Formatting.None,
                new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }), Encoding.Default,
            MediaTypeNames.Application.Json);

        var requestUri = $"{_dnsApiUrl}/{zoneId}/records";
        var response = await Client.PutAsync(requestUri, stringContent);
        switch (response.StatusCode)
        {
            case HttpStatusCode.NoContent:
                return new Result { StatusCode = response.StatusCode, Success = true };
            case HttpStatusCode.BadRequest:
                string responseContent = await response.Content.ReadAsStringAsync();
                var errorObj = JsonConvert.DeserializeObject<ResultError>(responseContent);
                return new Result { StatusCode = response.StatusCode, Success = false, Error = errorObj };
            default:
                return new Result { StatusCode = response.StatusCode, Success = false };
        }
    }
    public async Task<Result> AddZone(string domain)
    {
        var response = await Client.PostAsync(_dnsApiUrl,
            new StringContent(JsonConvert.SerializeObject(new { Domain = domain }), Encoding.Default,
                MediaTypeNames.Application.Json));
        switch (response.StatusCode)
        {
            case HttpStatusCode.OK:
                return new Result { StatusCode = response.StatusCode, Success = true };
            case HttpStatusCode.BadRequest:
                string responseContent = await response.Content.ReadAsStringAsync();
                var errorObj = JsonConvert.DeserializeObject<ResultError>(responseContent);
                return new Result { StatusCode = response.StatusCode, Success = false, Error = errorObj };
            default:
                return new Result { StatusCode = response.StatusCode, Success = false };
        }
    }
    public async Task<Result> DeleteRecord(int zoneId, int recordId)
    {
        var response = await Client.DeleteAsync($"{_dnsApiUrl}/{zoneId}/records/{recordId}");
        switch (response.StatusCode)
        {
            case HttpStatusCode.NoContent:
                return new Result { StatusCode = response.StatusCode, Success = true };
            case HttpStatusCode.BadRequest:
                string responseContent = await response.Content.ReadAsStringAsync();
                var errorObj = JsonConvert.DeserializeObject<ResultError>(responseContent);
                return new Result { StatusCode = response.StatusCode, Success = false, Error = errorObj };
            default:
                return new Result { StatusCode = response.StatusCode, Success = false };
        }
    }
    public async Task<Result> DeleteZone(int zoneId)
    {
        var response = await Client.DeleteAsync($"{_dnsApiUrl}/{zoneId}");
        switch (response.StatusCode)
        {
            case HttpStatusCode.NoContent:
                return new Result { StatusCode = response.StatusCode, Success = true };
            case HttpStatusCode.BadRequest:
                string responseContent = await response.Content.ReadAsStringAsync();
                var errorObj = JsonConvert.DeserializeObject<ResultError>(responseContent);
                return new Result { StatusCode = response.StatusCode, Success = false, Error = errorObj };
            default:
                return new Result { StatusCode = response.StatusCode, Success = false };
        }
    }
    public async Task<Result<string>> ExportZone(int zoneId)
    {
        var result = await Client.GetAsync($"{_dnsApiUrl}/{zoneId}/export");
        switch (result.StatusCode)
        {
            case HttpStatusCode.OK:
                string responseContent = await result.Content.ReadAsStringAsync();
                return new Result<string> { StatusCode = result.StatusCode, Success = true, Data = responseContent };
            default:
                return new Result<string> { StatusCode = result.StatusCode, Success = false };
        }
    }
    public async Task<Result<object>> GetZoneAvailability(string zoneName)
    {
        var stringContent = new StringContent(
            JsonConvert.SerializeObject(new { Name = zoneName }), Encoding.Default,
            MediaTypeNames.Application.Json);
        var response = await Client.PostAsync($"{_dnsApiUrl}/checkavailability", stringContent);
        string responseContent;
        switch (response.StatusCode)
        {
            case HttpStatusCode.OK:
                responseContent = await response.Content.ReadAsStringAsync();
                object? obj = JsonConvert.DeserializeObject(responseContent);
                return new Result<object> { StatusCode = response.StatusCode, Success = true, Data = obj };
            case HttpStatusCode.BadRequest:
                responseContent = await response.Content.ReadAsStringAsync();
                var errorObj = JsonConvert.DeserializeObject<ResultError>(responseContent);
                return new Result<object> { StatusCode = response.StatusCode, Success = false, Error = errorObj };
            default:
                return new Result<object> { StatusCode = response.StatusCode, Success = false };
        }
    }
    public async Task<Result<Zone>> GetZoneById(int zoneId)
    {
        var response = await Client.GetAsync($"{_dnsApiUrl}/{zoneId}");
        string responseContent = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
            return new Result<Zone> { StatusCode = response.StatusCode, Success = false };
        var zone = JsonConvert.DeserializeObject<Zone>(responseContent);
        return new Result<Zone> { StatusCode = response.StatusCode, Success = true, Data = zone };
    }
    public async Task<Zone?> GetZoneByName(string zoneName)
    {
        if (!_zones.Any())
        {
            var result = await GetZones();
            if (result is
                { Success: true, Data: not null })
            {
                _zones = result.Data;
            }
        }
        return _zones.FirstOrDefault(z => z.Domain?.ToLower() == zoneName.ToLower()) ?? null;
    }
    public async Task<Result<List<Zone>>> GetZones()
    {
        List<Zone> zones = new();
        var response = await Client.GetAsync(_dnsApiUrl);
        if (!response.IsSuccessStatusCode)
            return new Result<List<Zone>> { StatusCode = response.StatusCode, Success = false };

        string responseContent = await response.Content.ReadAsStringAsync();
        var obj = JsonConvert.DeserializeObject<DnsZoneApiListResponse>(responseContent);
        // TODO: implement paging
        zones.AddRange(obj.Items);
        _zones = zones;
        return new Result<List<Zone>> { StatusCode = response.StatusCode, Success = true, Data = zones };
    }
    public async Task<Result<ZoneStatistics>> GetZoneStatistics(int zoneId)
    {
        var response = await Client.GetAsync($"{_dnsApiUrl}/{zoneId}/statistics");
        string responseContent = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
            return new Result<ZoneStatistics> { StatusCode = response.StatusCode, Success = false };
        var zoneStatistics = JsonConvert.DeserializeObject<ZoneStatistics>(responseContent);
        return new Result<ZoneStatistics> { StatusCode = response.StatusCode, Success = true, Data = zoneStatistics };
    }
    public async Task<Result> UpdateRecord(int zoneId, Record record)
    {
        // Prep payload
        var payload = new DnsRecord.ChangeRequestPayload(record);
        var stringContent = new StringContent(
            JsonConvert.SerializeObject(payload, Formatting.None,
                new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }), Encoding.Default,
            MediaTypeNames.Application.Json);

        var requestUri = $"{_dnsApiUrl}/{zoneId}/records/{record.Id}";
        var response = await Client.PostAsync(requestUri, stringContent);
        switch (response.StatusCode)
        {
            case HttpStatusCode.NoContent:
                return new Result { StatusCode = response.StatusCode, Success = true };
            case HttpStatusCode.BadRequest:
                string responseContent = await response.Content.ReadAsStringAsync();
                var errorObj = JsonConvert.DeserializeObject<ResultError>(responseContent);
                return new Result { StatusCode = response.StatusCode, Success = false, Error = errorObj };
            default:
                return new Result { StatusCode = response.StatusCode, Success = false };
        }
    }
}
[PublicAPI]
public static class DnsRecord
{
    [PublicAPI]
    public enum MonitorType
    {
        None,
        Ping,
        Http,
        Monitor
    }
    [PublicAPI]
    public enum SmartRoutingType
    {
        None,
        Latency,
        Geolocation
    }
    [PublicAPI]
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
    [PublicAPI]
    public class ChangeRequestPayload
    {
        public ChangeRequestPayload(Record record)
        {
            Accelerated = record.Accelerated;
            Comment = record.Comment;
            Disabled = record.Disabled;
            Flags = record.Flags;
            GeolocationLatitude = record.GeolocationLatitude;
            GeolocationLongitude = record.GeolocationLongitude;
            Id = record.Id;
            LatencyZone = record.LatencyZone;
            MonitorType = record.MonitorType;
            Name = record.Name;
            Priority = record.Priority;
            PullZoneId = record.PullZoneId;
            ScriptId = record.ScriptId;
            SmartRoutingType = record.SmartRoutingType;
            Tag = record.Tag;
            Ttl = record.Ttl;
            Type = record.Type;
            Value = record.Value;
            Weight = record.Weight;
        }
        public bool? Accelerated { get; set; }
        public string? Comment { get; set; }
        public bool? Disabled { get; set; }
        public int? Flags { get; set; }
        public double? GeolocationLatitude { get; set; }
        public double? GeolocationLongitude { get; set; }
        public int? Id { get; set; }
        public string? LatencyZone { get; set; }
        public MonitorType? MonitorType { get; set; }
        public string? Name { get; set; }
        public int? Priority { get; set; }
        public int? PullZoneId { get; set; }
        public int? ScriptId { get; set; }
        public SmartRoutingType? SmartRoutingType { get; set; }
        public string? Tag { get; set; }
        public int? Ttl { get; set; }
        public Type? Type { get; set; }
        public string? Value { get; set; }
        public int? Weight { get; set; }
    }
}
internal class DnsZoneApiListResponse
{
    public int CurrentPage { get; set; }
    public bool HasMoreItems { get; set; }
    public List<Zone> Items { get; set; }
    public int TotalItems { get; set; }
}
