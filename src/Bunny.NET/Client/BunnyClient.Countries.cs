namespace Bunny.NET.Client;

partial class BunnyClient
{
    private string _countriesApiUrl;
    public async Task<List<Country>> GetCountryList()
    {
        var response = await Client.GetAsync(_countriesApiUrl);
        response.EnsureSuccessStatusCode();
        string responseContent = await response.Content.ReadAsStringAsync();
        var obj = JsonConvert.DeserializeObject<List<Country>>(responseContent);
        return obj;
    }
}
