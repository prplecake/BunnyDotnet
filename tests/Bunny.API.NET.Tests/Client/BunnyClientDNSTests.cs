using System.Net;
using Moq.Protected;
using Newtonsoft.Json;

namespace Bunny.API.NET.Tests.Client;

[TestClass]
// ReSharper disable once InconsistentNaming
public class BunnyClientDNSTests
{
    [TestInitialize]
    public void Init()
    {
        _mockHttpMessageHandler = new Mock<HttpMessageHandler>();
        var httpClient = new HttpClient(_mockHttpMessageHandler.Object);
        _client = new BunnyClient()
            .SetApiKey(TestData.TestApiKey)
            .CreateClient(httpClient);
    }
    [DataTestMethod]
    [DataRow(HttpStatusCode.BadRequest)]
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
        var result = await _client.GetZones();

        // Assert
        Assert.IsFalse(result.Success);
        Assert.AreEqual(expectedStatusCode, result.StatusCode);
    }
    [DataTestMethod]
    [DataRow(HttpStatusCode.BadRequest)]
    [DataRow(HttpStatusCode.InternalServerError)]
    public async Task AddZone_NonOK_Statuses_Returns_Success_False(HttpStatusCode expectedStatusCode)
    {
        // Arrange
        var domain = "example.com";

        _mockHttpMessageHandler.Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage { StatusCode = expectedStatusCode });

        // Act
        var result = await _client.AddZone(domain);

        // Assert
        Assert.IsFalse(result.Success);
        Assert.AreEqual(expectedStatusCode, result.StatusCode);
    }
    [DataTestMethod]
    [DataRow(HttpStatusCode.BadRequest)]
    [DataRow(HttpStatusCode.NotFound)]
    [DataRow(HttpStatusCode.InternalServerError)]
    [DataRow(HttpStatusCode.Unauthorized)]
    public async Task GetZoneById_NonOK_Statuses_Returns_Success_False(HttpStatusCode expectedStatusCode)
    {
        // Arrange

        _mockHttpMessageHandler.Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage { StatusCode = expectedStatusCode });

        // Act
        var result = await _client.GetZoneById(1);

        // Assert
        Assert.IsFalse(result.Success);
        Assert.AreEqual(expectedStatusCode, result.StatusCode);
    }
    [DataTestMethod]
    [DataRow(HttpStatusCode.BadRequest)]
    [DataRow(HttpStatusCode.NotFound)]
    [DataRow(HttpStatusCode.InternalServerError)]
    [DataRow(HttpStatusCode.Unauthorized)]
    [DataRow(HttpStatusCode.Forbidden)]
    public async Task UpdateZone_NonOK_Statuses_Returns_Success_False(HttpStatusCode expectedStatusCode)
    {
        // Arrange

        _mockHttpMessageHandler.Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage { StatusCode = expectedStatusCode });

        // Act
        var result = await _client.UpdateRecord(1, new Record());

        // Assert
        Assert.IsFalse(result.Success);
        Assert.AreEqual(expectedStatusCode, result.StatusCode);
    }
    [TestMethod]
    public async Task AddZone_Success()
    {
        // Arrange
        var domain = "example.com";

        _mockHttpMessageHandler.Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage { StatusCode = HttpStatusCode.OK });

        // Act
        var result = await _client.AddZone(domain);

        // Assert
        Assert.IsTrue(result.Success);
        Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
    }
    [TestMethod]
    public async Task GetZoneById_Success()
    {
        // Arrange
        var expectedZone = new Zone { Domain = "example.com" };

        _mockHttpMessageHandler.Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage
            { StatusCode = HttpStatusCode.OK,
              Content = new StringContent(
                  JsonConvert.SerializeObject(expectedZone)) });

        // Act
        var result = await _client.GetZoneById(1);

        // Assert
        Assert.AreEqual(expectedZone.Domain, result?.Data?.Domain);
    }
    [TestMethod]
    public async Task GetZoneByName_NullZones_Returns()
    {
        // Arrange
        var expectedZone = new Zone { Domain = "example.com" };

        _mockHttpMessageHandler.Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage
            { StatusCode = HttpStatusCode.OK,
              Content = new StringContent(
                  JsonConvert.SerializeObject(new DnsZoneApiListResponse { Items = new List<Zone>() })) });

        // Act
        var result = await _client.GetZoneByName("example.com");

        // Assert
        Assert.IsNull(result?.Domain);
    }
    [TestMethod]
    public async Task GetZoneByName_Success()
    {
        // Arrange
        var expectedZone = new Zone { Domain = "example.com" };

        _mockHttpMessageHandler.Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage
            { StatusCode = HttpStatusCode.OK,
              Content = new StringContent(
                  JsonConvert.SerializeObject(new DnsZoneApiListResponse
                  { Items = new List<Zone>(new[] { expectedZone }) })) });

        // Act
        var result = await _client.GetZoneByName("example.com");

        // Assert
        Assert.AreEqual(expectedZone.Domain, result?.Domain);
    }
    [TestMethod]
    public async Task UpdateRecord_Success()
    {
        // Arrange
        var zoneId = 1;
        var record = new Record { Id = 123, Name = "example.com", Type = DnsRecord.Type.A, Value = "0.0.0.0" };

        _mockHttpMessageHandler.Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage { StatusCode = HttpStatusCode.NoContent });

        // Act
        var result = await _client.UpdateRecord(zoneId, record);

        // Assert
        Assert.IsTrue(result.Success);
        Assert.AreEqual(HttpStatusCode.NoContent, result.StatusCode);
    }
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private BunnyClient _client;
    private Mock<HttpMessageHandler> _mockHttpMessageHandler;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
}
