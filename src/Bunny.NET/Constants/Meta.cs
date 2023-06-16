using System.Net.Http.Headers;

namespace Bunny.NET.Constants;

public class Meta
{
    private const string
        Name = "BunnyDotnet",
        Version = "1.0";
    public static readonly ProductInfoHeaderValue UserAgent = new(Name, Version);
}
