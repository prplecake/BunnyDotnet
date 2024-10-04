using Bunny.API.NET.Entities;

namespace Bunny.API.NET.Client;

partial class BunnyClient
{
    private string _statisticsApiUrl;
    public async Task<Result<Statistics>> GetStatistics(
        DateTime? dateFrom = null,
        DateTime? dateTo = null,
        int? pullZoneId = null,
        int? serverZoneId = null,
        bool loadErrors = false,
        bool hourly = false
    )
    {
        string? queryString = null;
        Dictionary<string, string> queryParams = new();
        if (dateFrom is not null) queryParams["dateFrom"] = dateFrom.ToString();
        if (dateTo is not null) queryParams["dateTo"] = dateTo.ToString();
        if (pullZoneId is not null) queryParams["pullZone"] = pullZoneId.ToString();
        if (serverZoneId is not null) queryParams["serverzoneId"] = serverZoneId.ToString();
        if (loadErrors) queryParams["loadErrors"] = loadErrors.ToString();
        if (hourly) queryParams["hourly"] = hourly.ToString();
        if (queryParams.Count > 0)
        {
            queryString = new FormUrlEncodedContent(queryParams).ReadAsStringAsync().Result;
        }
        var response = await Client.GetAsync(string.IsNullOrEmpty(queryString)
            ? _statisticsApiUrl
            : $"{_statisticsApiUrl}?{queryString}");
        if (!response.IsSuccessStatusCode)
            return new Result<Statistics> { StatusCode = response.StatusCode, Success = false };
        string responseContent = await response.Content.ReadAsStringAsync();
        var obj = JsonConvert.DeserializeObject<Statistics>(responseContent);
        return new Result<Statistics> { StatusCode = response.StatusCode, Success = true, Data = obj };
    }
}
