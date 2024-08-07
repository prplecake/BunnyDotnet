using Bunny.NET.Client;

namespace Bunny.NET.Entities;

public class Record
{
    public bool? Accelerated { get; set; }
    public int? Id { get; set; }
    public string? LinkName { get; set; }
    public string? Name { get; set; }
    public int? Ttl { get; set; }
    public DnsRecord.Type? Type { get; set; }
    public string? Value { get; set; }
    public string? Comment { get; set; }
    public bool? Disabled { get; set; }
    public int? Flags { get; set; }
    public double? GeolocationLatitude { get; set; }
    public double? GeolocationLongitude { get; set; }
    public string? LatencyZone { get; set; }
    public DnsRecord.MonitorType? MonitorType { get; set; }
    public int? Priority { get; set; }
    public int? PullZoneId { get; set; }
    public int? ScriptId { get; set; }
    public DnsRecord.SmartRoutingType? SmartRoutingType { get; set; }
    public string? Tag { get; set; }
    public int? Weight { get; set; }
    /// <inheritdoc />
    public override string ToString() => Name ?? nameof(Record);
}
