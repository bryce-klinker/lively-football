using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Lively.Football.Tests
{
    public class CountriesTests
    {
        [Fact]
        public async Task GivenNoCountries_WhenCountriesAreLoaded_ThenCountriesAreLoadedFromDataSource()
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync("https://apifootball.com/api/?action=get_countries&APIkey=f0bd2551475a981323ba7a83eeea738e96820b9206e5a6f51503af4d5b375c63");
                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            }
        }
    }
}
