namespace Bunny.API.NET.Entities;

public class ApiKey
{
    public int Id { get; set; }
    public string? Key { get; set; }
    public string[]? Roles { get; set; }
}
