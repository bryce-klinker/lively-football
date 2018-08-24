using System.Net;
using System.Threading.Tasks;
using Lively.Football.Application.Countries;
using Xunit;

namespace Lively.Football.Tests
{
    public class CountriesTests
    {
        [Fact]
        public async Task GivenNoCountries_WhenCountriesAreLoaded_ThenCountriesAreLoadedFromDataSource()
        {
            var service = new CountriesService();
            var response = await service.LoadAll();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
