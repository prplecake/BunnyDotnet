namespace Bunny.API.NET.Entities;

/// <summary>
///     Represents a country with various properties such as flag URL, EU membership status, ISO code, name, population
///     list, and tax rate.
/// </summary>
public class Country
{
    /// <summary>
    ///     Gets or sets the URL of the country's flag.
    /// </summary>
    public string? FlagUrl { get; set; }
    /// <summary>
    ///     Gets or sets a value indicating whether the country is a member of the European Union.
    /// </summary>
    public bool IsEU { get; set; }
    /// <summary>
    ///     Gets or sets the ISO code of the country.
    /// </summary>
    public string? IsoCode { get; set; }
    /// <summary>
    ///     Gets or sets the name of the country.
    /// </summary>
    public string Name { get; set; }
    /// <summary>
    ///     Gets or sets the list of population data for the country.
    /// </summary>
    public List<string>? PopList { get; set; }
    /// <summary>
    ///     Gets or sets the tax rate of the country.
    /// </summary>
    public double TaxRate { get; set; }
}
