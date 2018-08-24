using System.Net.Http;
using System.Threading.Tasks;
using Lively.Football.Application.Common;
using Lively.Football.Application.Common.FootballApi;
using Lively.Football.Application.Countries.Entities;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Lively.Football.Application.Countries
{
    public class CountriesService
    {
        private readonly FootballApiDataSource _dataSource;
        private readonly IStorage _storage;

        public CountriesService(IDataSourceConfig config, IStorage storage, IHttpClientFactory httpFactory)
        {
            _dataSource = new FootballApiDataSource(config, httpFactory);
            _storage = storage;
        }

        public async Task LoadAll()
        {
            var sourceCountries = await _dataSource.GetCountries();
            foreach (var country in sourceCountries)
            {
                _storage.Add(new Country
                {
                    SourceId = country.Id,
                    Name = country.Name
                });
            }
            await _storage.Save();
        }
    }
}
