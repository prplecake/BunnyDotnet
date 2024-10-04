using Bunny.API.NET.Entities;

namespace Bunny.API.NET.Client;

partial class BunnyClient
{
    private string _purgeApiUrl;
    public async Task<Result> PurgeUrlGet(
        string url,
        string? headerName = null,
        string? headerValue = null,
        bool async = false)
    {
        Dictionary<string, string> queryParams = new()
        {
            {
                "url", url
            }
        };
        if (headerName is not null) queryParams["headerName"] = headerName;
        if (headerValue is not null) queryParams["headerValue"] = headerValue;
        if (async) queryParams["async"] = async.ToString();
        string? queryString = await new FormUrlEncodedContent(queryParams).ReadAsStringAsync();
        var response = await Client.GetAsync($"{_purgeApiUrl}?{queryString}");
        switch (response.StatusCode)
        {
            case HttpStatusCode.OK:
                return new Result { StatusCode = response.StatusCode, Success = true };
            case HttpStatusCode.BadRequest:
                var responseContent = await response.Content.ReadAsStringAsync();
                var errorObj = JsonConvert.DeserializeObject<ResultError>(responseContent);
                return new Result { StatusCode = response.StatusCode, Success = false, Error = errorObj };
            default:
                return new Result { StatusCode = response.StatusCode, Success = false };
        }
    }
}
