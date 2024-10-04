namespace Bunny.API.NET.Entities;

public class Zone
{
    public string? Domain { get; set; }
    public int Id { get; set; }
    public List<Record>? Records { get; set; }
    /// <inheritdoc />
    public override string ToString() => Domain ?? nameof(Zone);
}
public class ZoneStatistics
{
    public int TotalQueriesServed { get; set; }
    public object QueriesServedChart { get; set; }
    public object QueriesByTypeChart { get; set; }
}
