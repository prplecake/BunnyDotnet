using System.Net;
using Bunny.API.NET.Tests.TestHelpers;
using Moq.Protected;
using Newtonsoft.Json;

namespace Bunny.API.NET.Tests.Client;

[TestClass]
public class BunnyClientCountriesTests
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
    [DataRow(HttpStatusCode.Unauthorized)]
    [DataRow(HttpStatusCode.InternalServerError)]
    public async Task GetRegionList_NonOK_Statuses_Returns_Success_False(HttpStatusCode expectedStatusCode)
    {
        // Arrange
        _mockHttpMessageHandler.Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage { StatusCode = expectedStatusCode });

        // Act
        var result = await _client.GetCountryList();

        // Assert
        Assert.IsFalse(result.Success);
        Assert.AreEqual(expectedStatusCode, result.StatusCode);
    }
    [TestMethod]
    public async Task GetCountryList_Success()
    {
        // Arrange
        var expectedCountryList = new List<Country>
        { new()
          { FlagUrl = "test",
            IsoCode = "test",
            IsEU = false,
            Name = "test",
            PopList = new List<string>(),
            TaxRate = 0.0 } };

        _mockHttpMessageHandler.Protected().Setup<Task<HttpResponseMessage>>("SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage
            { StatusCode = HttpStatusCode.OK,
              Content = new StringContent(JsonConvert.SerializeObject(expectedCountryList)) });

        // Act
        var result = await _client.GetCountryList();

        // Assert
        Assert.IsTrue(result.Success);
        Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
        CustomAssert.PropertiesAreEqual(expectedCountryList[0], result.Data[0]);
        CollectionAssert.AreEqual(expectedCountryList[0].PopList, result.Data[0].PopList);
    }
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private BunnyClient _client;
    private Mock<HttpMessageHandler> _mockHttpMessageHandler;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
}
