using Bunny.NET.Client;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bunny.NET.Tests.Client;

[TestClass]
public class BunnyClientTests
{
    [TestMethod]
    public void HttpClient_BaseUrl_HasProtocol()
    {
        _ = new BunnyClient(TestData.TestApiKey);

        Assert.IsTrue(BunnyClient.BaseUrl?.StartsWith(BunnyClient.Scheme));
    }
}
