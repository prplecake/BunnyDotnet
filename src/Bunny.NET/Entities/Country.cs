namespace Bunny.NET.Entities;

public class Country
{
    public string? FlagUrl { get; set; }
    public bool IsEU { get; set; }
    public string? IsoCode { get; set; }
    public string Name { get; set; }
    public List<string>? PopList { get; set; }
    public double TaxRate { get; set; }
}
