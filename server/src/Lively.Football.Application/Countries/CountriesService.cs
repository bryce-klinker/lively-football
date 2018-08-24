using System.Net.Http;
using System.Threading.Tasks;
using Lively.Football.Application.Common;
using Lively.Football.Application.Countries.Entities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Lively.Football.Application.Countries
{
    public class CountriesService
    {
        private readonly IDataSourceConfig _config;
        private readonly IStorage _storage;
        private readonly IHttpClientFactory _httpFactory;

        private string BaseUrl => _config.BaseUrl;
        private string ApiKey => _config.ApiKey;

        public CountriesService(IDataSourceConfig config, IStorage storage, IHttpClientFactory httpFactory)
        {
            _config = config;
            _storage = storage;
            _httpFactory = httpFactory;
        }

        public async Task<HttpResponseMessage> LoadAll()
        {
            using (var client = _httpFactory.CreateClient())
            {
                var response = await client.GetAsync($"{BaseUrl}?action=get_countries&APIkey={ApiKey}");
                var content = JsonConvert.DeserializeObject<JObject[]>(await response.Content.ReadAsStringAsync());
                foreach (var country in content)
                {
                    _storage.Add(new Country
                    {
                        SourceId = country.Value<string>("country_id"),
                        Name = country.Value<string>("country_name")
                    });
                }

                await _storage.Save();
                return response;
            }
        }
    }
}
