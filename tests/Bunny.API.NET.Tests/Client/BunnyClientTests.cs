namespace Bunny.API.NET.Tests.Client;

[TestClass]
public class BunnyClientTests
{
    [TestMethod]
    public void HttpClient_BaseUrl_HasProtocol()
    {
        _ = new BunnyClient()
            .SetApiKey(TestData.TestApiKey);

        Assert.IsTrue(BunnyClient.BaseUrl?.StartsWith(BunnyClient.Scheme));
    }
}
