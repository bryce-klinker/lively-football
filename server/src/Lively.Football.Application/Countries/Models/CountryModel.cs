using System;
using System.Linq.Expressions;
using Lively.Football.Application.Countries.Entities;

namespace Lively.Football.Application.Countries.Models
{
    public class CountryModel
    {
        public string Name { get; set; }
        public long Id { get; set; }

        public static Expression<Func<Country, CountryModel>> FromCountryExpression()
        {
            return c => new CountryModel
            {
                Id = c.Id,
                Name = c.Name
            };
        }
    }
}
