using System.Net;
using Bunny.NET.Client;
using Bunny.NET.Entities;
using Bunny.NET.Tests.TestHelpers;
using Moq.Protected;
using Newtonsoft.Json;

namespace Bunny.NET.Tests.Client;

[TestClass]
public class BunnyClientStatisticsTests
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
    [DataRow(HttpStatusCode.Unauthorized)]
    public async Task GetZones_NonOK_Statuses_Returns_Success_False(HttpStatusCode expectedStatusCode)
    {
        // Arrange
        _mockHttpMessageHandler.Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage { StatusCode = expectedStatusCode });

        // Act
        var result = await _client.GetStatistics();

        // Assert
        Assert.IsFalse(result.Success);
        Assert.AreEqual(expectedStatusCode, result.StatusCode);
    }
    private Statistics NewRandomStatistics()
    {
        var rnd = new Random();
        return new Statistics
        { TotalBandwidthUsed = rnd.NextInt64(),
          AverageOriginResponseTime = (int)rnd.NextInt64(),
          TotalOriginTraffic = (int)rnd.NextInt64(),
          TotalRequestsServed = rnd.NextInt64(),
          CacheHitRate = rnd.NextDouble() };
    }
    [TestMethod]
    public async Task GetStatistics_Success()
    {
        // Arrange
        var expectedStatistics = NewRandomStatistics();

        _mockHttpMessageHandler.Protected().Setup<Task<HttpResponseMessage>>("SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage
            { StatusCode = HttpStatusCode.OK,
              Content = new StringContent(JsonConvert.SerializeObject(expectedStatistics)) });

        // Act
        var result = await _client.GetStatistics();

        // Assert
        Assert.IsTrue(result.Success);
        Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
        CustomAssert.PropertiesAreEqual(expectedStatistics, result.Data);
    }
    [TestMethod]
    public async Task GetStatistics_WithQueryParams_Success()
    {
        // Arrange
        var expectedStatistics = NewRandomStatistics();

        _mockHttpMessageHandler.Protected().Setup<Task<HttpResponseMessage>>("SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage
            { StatusCode = HttpStatusCode.OK,
              Content = new StringContent(JsonConvert.SerializeObject(expectedStatistics)) });

        // Act
        var result = await _client.GetStatistics(
            DateTime.Now.AddDays(-5),
            DateTime.Now.AddDays(-1),
            1,
            1,
            true,
            true
        );
        // Assert
        Assert.IsTrue(result.Success);
        Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
        CustomAssert.PropertiesAreEqual(expectedStatistics, result.Data);
    }
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private BunnyClient _client;
    private Mock<HttpMessageHandler> _mockHttpMessageHandler;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
}
