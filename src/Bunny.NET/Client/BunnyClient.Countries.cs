namespace Bunny.NET.Client;

partial class BunnyClient
{
    private string _countriesApiUrl;
    public async Task<Result<List<Country>>> GetCountryList()
    {
        var response = await Client.GetAsync(_countriesApiUrl);
        if (!response.IsSuccessStatusCode)
            return new Result<List<Country>> { StatusCode = response.StatusCode, Success = false };
        string responseContent = await response.Content.ReadAsStringAsync();
        var obj = JsonConvert.DeserializeObject<List<Country>>(responseContent);
        return new Result<List<Country>> { StatusCode = response.StatusCode, Success = true, Data = obj };
    }
}
