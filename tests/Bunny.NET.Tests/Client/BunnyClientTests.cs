using Bunny.NET.Client;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bunny.NET.Tests.Client;

[TestClass]
public class BunnyClientTests
{
    [TestMethod]
    public void HttpClient_BaseUrl_HasProtocol()
    {
        var client = new BunnyClient("testing-token");

        Assert.IsTrue(BunnyClient.BaseUrl?.StartsWith(BunnyClient.Scheme));
    }
}
