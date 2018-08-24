using System;
using Lively.Football.Application.Common;
using Microsoft.EntityFrameworkCore;

namespace Lively.Football.Tests.Fakes
{
    public class InMemoryStorage : Storage
    {
        public InMemoryStorage(string name = null)
            : base(GetOptions(name))
        {

        }

        private static DbContextOptions<Storage> GetOptions(string name)
        {
            var dbName = string.IsNullOrWhiteSpace(name) ? Guid.NewGuid().ToString() : name;
            return new DbContextOptionsBuilder<Storage>()
                .UseInMemoryDatabase(dbName)
                .Options;
        }
    }
}
