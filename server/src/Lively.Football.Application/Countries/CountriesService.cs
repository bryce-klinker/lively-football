using System.Net.Http;
using System.Threading.Tasks;
using Lively.Football.Application.Common;

namespace Lively.Football.Application.Countries
{
    public class CountriesService
    {
        private readonly IDataSourceConfig _config;

        private string BaseUrl => _config.BaseUrl;
        private string ApiKey => _config.ApiKey;

        public CountriesService(IDataSourceConfig config)
        {
            _config = config;
        }

        public async Task<HttpResponseMessage> LoadAll()
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync($"{BaseUrl}?action=get_countries&APIkey={ApiKey}");
                return response;
            }
        }
    }
}
