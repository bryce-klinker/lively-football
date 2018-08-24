using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lively.Football.Application.Countries;
using Lively.Football.Application.Countries.Entities;
using Lively.Football.Tests.Fakes;
using Xunit;

namespace Lively.Football.Tests
{
    public class CountriesTests
    {
        private readonly FakeDataSourceConfig _config;
        private readonly InMemoryStorage _storage;
        private readonly FakeHttpClientFactory _httpFactory;
        private readonly CountriesService _service;

        public CountriesTests()
        {
            _config = new FakeDataSourceConfig();
            _storage = new InMemoryStorage();
            _httpFactory = new FakeHttpClientFactory();

            _service = new CountriesService(_config, _storage, _httpFactory);
        }

        [Fact]
        public async Task GivenNoCountries_WhenCountriesAreLoaded_ThenCountriesAreLoadedFromDataSource()
        {
            SetupDataSourceCountries(
                new {country_id = "456", country_name = "IDK"},
                new {country_id = "78", country_name = "USA"},
                new {country_id = "34", country_name = "Bob"}
            );

            await _service.LoadAll();
            var countries = _storage.GetAll<Country>().ToArray();
            Assert.Equal(3, countries.Length);
            AssertHasCountry("456", "IDK", countries);
            AssertHasCountry("78", "USA", countries);
            AssertHasCountry("34", "Bob", countries);
        }

        [Fact]
        public async Task GivenExistingCountries_WhenCountriesAreLoaded_ThenCountriesAreUpdatedFromDataSource()
        {
            _storage.Add(new Country { Name = "OldUSA", SourceId = "78" });
            await _storage.Save();

            SetupDataSourceCountries(
                new {country_id = "456", country_name = "IDK"},
                new {country_id = "78", country_name = "USA"},
                new {country_id = "34", country_name = "Bob"}
            );

            await _service.LoadAll();
            var countries = _storage.GetAll<Country>().ToArray();
            Assert.Equal(3, countries.Length);
            AssertHasCountry("456", "IDK", countries);
            AssertHasCountry("78", "USA", countries);
            AssertHasCountry("34", "Bob", countries);
        }

        private static void AssertHasCountry(string id, string name, IEnumerable<Country> countries)
        {
            var country = countries.Single(c => c.SourceId == id);
            Assert.Equal(name, country.Name);
        }

        private void SetupDataSourceCountries(params object[] countries)
        {
            _httpFactory.SetupGet($"{_config.BaseUrl}?action=get_countries&APIkey={_config.ApiKey}", countries);
        }
    }
}
