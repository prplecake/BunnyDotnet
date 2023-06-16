namespace Bunny.NET.Entities;

public class Record
{
    public int Id { get; set; }
    public string? LinkName { get; set; }
    public string? Name { get; set; }
    public int Ttl { get; set; }
    public int Type { get; set; }
    public string? Value { get; set; }
    /// <inheritdoc />
    public override string ToString() => Name ?? nameof(Record);
}
