namespace Bunny.API.NET.Entities;

public class Region
{
    public bool AllowLatencyRouting { get; set; }
    public string ContinentCode { get; set; }
    public string CountryCode { get; set; }
    public int Id { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public string Name { get; set; }
    public double PricePerGigabyte { get; set; }
    public string RegionCode { get; set; }
}
