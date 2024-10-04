namespace Bunny.API.NET.Entities;

/// <summary>
/// Represents an API key with associated roles.
/// </summary>
public class ApiKey
{
    /// <summary>
    ///     Gets or sets the unique identifier for the API key.
    /// </summary>
    public int Id { get; set; }
    /// <summary>
    ///     Gets or sets the API key value.
    /// </summary>
    public string? Key { get; set; }
    /// <summary>
    ///     Gets or sets the roles associated with the API key.
    /// </summary>
    public string[]? Roles { get; set; }
}
