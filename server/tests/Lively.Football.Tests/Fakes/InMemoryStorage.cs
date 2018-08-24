using System;
using System.Linq;
using System.Threading.Tasks;
using Lively.Football.Application.Common;
using Microsoft.EntityFrameworkCore;

namespace Lively.Football.Tests.Fakes
{
    public class InMemoryStorage : IStorage
    {
        private readonly IStorage _storage;

        public InMemoryStorage(string name = null)
        {
            _storage = new Storage(GetOptions(name));
        }

        public IQueryable<T> GetAll<T>() where T : class
        {
            return _storage.GetAll<T>();
        }

        public void Add<T>(T entity) where T : class
        {
            _storage.Add(entity);
        }

        public async Task Save()
        {
            await _storage.Save();
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
