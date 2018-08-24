using System.Net.Http;
using System.Threading.Tasks;
using Lively.Football.Application.Common.FootballApi.Models;
using Newtonsoft.Json;

namespace Lively.Football.Application.Common.FootballApi
{
    internal class FootballApiDataSource
    {
        private readonly IDataSourceConfig _config;
        private readonly IHttpClientFactory _httpClientFactory;

        private string BaseUrl => _config.BaseUrl;
        private string ApiKey => _config.ApiKey;

        public FootballApiDataSource(IDataSourceConfig config, IHttpClientFactory httpClientFactory)
        {
            _config = config;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<FootballApiCountry[]> GetCountries()
        {
            return await GetAsync<FootballApiCountry[]>("get_countries");
        }

        private async Task<T> GetAsync<T>(string action)
        {
            using (var client = _httpClientFactory.CreateClient())
            {
                var response = await client.GetAsync($"{BaseUrl}?action={action}&APIkey={ApiKey}");
                return JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());
            }
        }
    }
}
