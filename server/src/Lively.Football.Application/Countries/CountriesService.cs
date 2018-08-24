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
        private readonly IFootballApiDataSource _dataSource;
        private readonly ICountryTransformer _transformer;
        private readonly IStorage _storage;

        public CountriesService(IDataSourceConfig config, IStorage storage, IHttpClientFactory httpFactory)
            : this(new FootballApiDataSource(config, httpFactory), new CountryTransformer(), storage)
        {
        }

        internal CountriesService(IFootballApiDataSource dataSource, ICountryTransformer transformer, IStorage storage)
        {
            _dataSource = dataSource;
            _transformer = transformer;
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
