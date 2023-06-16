namespace Bunny.NET.Entities;

public class Zone
{
    public string? Domain { get; set; }
    public int Id { get; set; }
    public List<Record>? Records { get; set; }
    /// <inheritdoc />
    public override string ToString() => Domain ?? nameof(Zone);
}