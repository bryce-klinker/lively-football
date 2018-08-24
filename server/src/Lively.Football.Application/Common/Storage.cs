using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lively.Football.Application.Countries.Entities;
using Microsoft.EntityFrameworkCore;

namespace Lively.Football.Application.Common
{
    public interface IStorage
    {
        IQueryable<T> GetAll<T>()
            where T : class;
        void Add<T>(T entity)
            where T : class;

        void Add<T>(IEnumerable<T> entities)
            where T : class;
        Task Save();
    }

    public class Storage : DbContext, IStorage
    {
        public Storage(DbContextOptions<Storage> options)
            : base(options)
        {

        }

        public IQueryable<T> GetAll<T>() where T : class
        {
            return Set<T>();
        }

        void IStorage.Add<T>(T entity)
        {
            Add(entity);
        }

        public void Add<T>(IEnumerable<T> entities) where T : class
        {
            foreach (var entity in entities)
                Add(entity);
        }

        public async Task Save()
        {
            await SaveChangesAsync();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CountryConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}
