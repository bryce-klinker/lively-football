using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Lively.Football.Application.Common;
using Lively.Football.Application.Common.FootballApi;
using Lively.Football.Application.Countries.Entities;
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
            foreach (var country in countries)
                await AddOrUpdateCountry(country);
            await _storage.Save();
        }

        private async Task AddOrUpdateCountry(Country country)
        {
            var existing = await _storage.GetSingle<Country>(c => c.SourceId == country.SourceId);
            if (existing == null)
                _storage.Add(country);
            else
                existing.Name = country.Name;
        }
    }
}
