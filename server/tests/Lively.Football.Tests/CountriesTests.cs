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
        [Fact]
        public async Task GivenNoCountries_WhenCountriesAreLoaded_ThenCountriesAreLoadedFromDataSource()
        {
            var config = new FakeDataSourceConfig();
            var storage = new InMemoryStorage();

            var service = new CountriesService(config, storage);
            await service.LoadAll();
            var countries = storage.GetAll<Country>().ToArray();
            Assert.Equal(countries.Length, storage.GetAll<Country>().Count());
            AssertHasCountry("169", "England", storage.GetAll<Country>().ToArray());
            AssertHasCountry("173", "France", storage.GetAll<Country>().ToArray());
        }

        private static void AssertHasCountry(string id, string name, IEnumerable<Country> countries)
        {
            var country = countries.Single(c => c.SourceId == id);
            Assert.Equal(name, country.Name);
        }
    }
}
