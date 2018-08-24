using System.Linq;
using System.Threading.Tasks;
using Lively.Football.Application.Countries;
using Lively.Football.Application.Countries.Entities;
using Lively.Football.Tests.Fakes;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
            var response = await service.LoadAll();
            var countries = JsonConvert.DeserializeObject<JObject[]>(await response.Content.ReadAsStringAsync());
            Assert.Equal(countries.Length, storage.GetAll<Country>().Count());
        }
    }
}
