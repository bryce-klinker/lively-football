using Lively.Football.Application.Common.FootballApi.Models;
using Lively.Football.Application.Countries.Entities;

namespace Lively.Football.Application.Countries.Transformers
{
    internal class CountryTransformer
    {
        public Country ToCountry(FootballApiCountry country)
        {
            return new Country
            {
                SourceId = country.Id,
                Name = country.Name
            };
        }
    }
}
