namespace Bunny.API.NET.Entities;

public class Statistics
{
    public int AverageOriginResponseTime { get; set; }
    public Dictionary<string, double> BandwidthCachedChart { get; set; }
    public Dictionary<string, double> BandwidthUsedChart { get; set; }
    public double CacheHitRate { get; set; }
    public Dictionary<string, double> CacheHitRateChart { get; set; }
    public Dictionary<string, double> Error3xxChart { get; set; }
    public Dictionary<string, double> Error4xxChart { get; set; }
    public Dictionary<string, double> Error5xxChart { get; set; }
    public Dictionary<string, long> GeoTrafficDistribution { get; set; }
    public Dictionary<string, double> OriginResponseTimeChart { get; set; }
    public Dictionary<string, double> OriginShieldBandwidthUsedChart { get; set; }
    public Dictionary<string, double> OriginShieldInternalBandwidthUsedChart { get; set; }
    public Dictionary<string, double> OriginTrafficChart { get; set; }
    public Dictionary<string, double> PullRequestsPulledChart { get; set; }
    public Dictionary<string, double> RequestsServedChart { get; set; }
    public long TotalBandwidthUsed { get; set; }
    public int TotalOriginTraffic { get; set; }
    public long TotalRequestsServed { get; set; }
    public Dictionary<string, double> UserBalanceHistoryChart { get; set; }
}
