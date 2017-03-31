using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure.Pluralization;
using System.Linq;
using System.Threading.Tasks;
using Kandanda.Dal;
using Kandanda.Dal.DataTransferObjects;

namespace Kandanda.BusinessLayer
{
    public abstract class ServiceBase
    {
        protected readonly KandandaDbContext _dbContext;

        protected ServiceBase(KandandaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        protected virtual T Create<T>(T entry) where T : class, IEntry
        {
            var set = GetDbSet<T>(_dbContext);
            set.Add(entry);
            _dbContext.SaveChanges();

            return entry;
        }

        protected virtual void Delete<T>(T entry) where T : class, IEntry
        {
            var set = GetDbSet<T>(_dbContext);
            set.Attach(entry);
            set.Remove(entry);

            _dbContext.SaveChanges();
        }

        protected virtual T GetEntryById<T>(int id) where T : class, IEntry
        {
            var set = GetDbSet<T>(_dbContext);
            return set.FirstOrDefault(entry => entry.Id == id);
        }

        protected virtual async Task<List<T>> GetAllAsync<T>() where T : class, IEntry
        {
            return await GetDbSet<T>(_dbContext).ToListAsync();
        }

        protected virtual List<T> GetAll<T>() where T : class, IEntry
        {
            return GetDbSet<T>(_dbContext).ToList();
        }

        protected virtual T GetEntry<T>(Predicate<T> predicate) where T : class, IEntry
        {
            var set = GetDbSet<T>(_dbContext);
            return set.FirstOrDefault(entry => predicate(entry));
        }
        
        protected void ExecuteDatabaseAction(Action<KandandaDbContext> action)
        {
            action(_dbContext);
        }
        
        private DbSet<T> GetDbSet<T>(DbContext db) where T : class
        {
            var pluralizedName = GetPluralizedName<T>();
            var propertyInfo = db.GetType().GetProperty(pluralizedName);
            return (DbSet<T>) propertyInfo.GetValue(db);
        }

        private string GetPluralizedName<T>()
        {
            var className = typeof(T).Name;
            var pluralizationService = new EnglishPluralizationService();
            return pluralizationService.Pluralize(className);
        }
    }
}