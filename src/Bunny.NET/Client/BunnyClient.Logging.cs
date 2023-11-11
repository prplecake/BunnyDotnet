namespace Bunny.NET.Client;

partial class BunnyClient
{
    private string _loggingApiUrl = "https://logging.bunnycdn.com";
    public async Task<dynamic> GetLogFile(DateOnly date, string pullZoneId)
    {
        var month = date.ToString("MM");
        var day = date.ToString("dd");
        var year = date.ToString("yy");
        var response = await Client.GetAsync($"{_loggingApiUrl}/{month}-{day}-{year}/{pullZoneId}");
        response.EnsureSuccessStatusCode();
        return null;
    }
}
