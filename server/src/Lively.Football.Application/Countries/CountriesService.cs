using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Lively.Football.Application.Common;
using Lively.Football.Application.Common.FootballApi;
using Lively.Football.Application.Countries.Transformers;

namespace Lively.Football.Application.Countries
{
    public class CountriesService
    {
        private readonly FootballApiDataSource _dataSource;
        private readonly CountryTransformer _transformer;
        private readonly IStorage _storage;

        public CountriesService(IDataSourceConfig config, IStorage storage, IHttpClientFactory httpFactory)
        {
            _dataSource = new FootballApiDataSource(config, httpFactory);
            _transformer = new CountryTransformer();
            _storage = storage;
        }

        public async Task LoadAll()
        {
            var sourceCountries = await _dataSource.GetCountries();
            var countries = sourceCountries.Select(_transformer.ToCountry);
            _storage.Add(countries);
            await _storage.Save();
        }
    }
}
