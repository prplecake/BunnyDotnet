using System.Net;
using Bunny.NET.Client;
using Bunny.NET.Entities;
using Bunny.NET.Tests.TestHelpers;
using Moq.Protected;
using Newtonsoft.Json;

namespace Bunny.NET.Tests.Client;

[TestClass]
public class BunnyClientRegionTests
{
    [TestInitialize]
    public void Init()
    {
        _mockHttpMessageHandler = new Mock<HttpMessageHandler>();
        var httpClient = new HttpClient(_mockHttpMessageHandler.Object);
        _client = new BunnyClient(TestData.TestApiKey, httpClient);
    }
    [DataTestMethod]
    [DataRow(HttpStatusCode.InternalServerError)]
    public async Task GetRegionList_NonOK_Statuses_Returns_Success_False(HttpStatusCode expectedStatusCode)
    {
        // Arrange
        _mockHttpMessageHandler.Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage { StatusCode = expectedStatusCode });

        // Act
        var result = await _client.GetRegionList();

        // Assert
        Assert.IsFalse(result.Success);
        Assert.AreEqual(expectedStatusCode, result.StatusCode);
    }
    [TestMethod]
    public async Task GetRegionList_Success()
    {
        // Arrange
        var expectedRegionList = new List<Region> { new() { Id = 1, Name = "test" } };

        _mockHttpMessageHandler.Protected().Setup<Task<HttpResponseMessage>>("SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage
            { StatusCode = HttpStatusCode.OK,
              Content = new StringContent(JsonConvert.SerializeObject(expectedRegionList)) });

        // Act
        var result = await _client.GetRegionList();

        // Assert
        Assert.IsTrue(result.Success);
        Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
        CustomAssert.PropertiesAreEqual(expectedRegionList[0], result.Data[0]);
    }
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private BunnyClient _client;
    private Mock<HttpMessageHandler> _mockHttpMessageHandler;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
}
