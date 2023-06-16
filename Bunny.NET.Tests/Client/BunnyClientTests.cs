using Bunny.NET.Client;

namespace Bunny.NET.Tests.Client;

public class BunnyClientTests
{
    [Fact]
    public void HttpClient_BaseUrl_HasProtocol()
    {
        var client = new BunnyClient("testing-token");
        
        Assert.StartsWith(BunnyClient.Scheme, BunnyClient.BaseUrl);
    }
}
