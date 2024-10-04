using System.Reflection;

namespace Bunny.API.NET.Constants;

/// <summary>
///     Contains metadata information about the assembly.
/// </summary>
public class Meta
{
    private const string Name = "BunnyDotnet";
    private static readonly string? AssemblyVersion = Assembly.GetEntryAssembly()!.GetName().Version!.ToString();
    /// <summary>
    ///     Gets the user agent information for the product.
    /// </summary>
    public static readonly ProductInfoHeaderValue UserAgent = new(Name, AssemblyVersion);
}
