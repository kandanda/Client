using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure.Pluralization;
using System.Linq;
using System.Threading.Tasks;
using Kandanda.Dal;
using Kandanda.Dal.Entities;

namespace Kandanda.BusinessLayer
{
    public abstract class ServiceBase
    {
        protected readonly KandandaDbContext DbContext;

        protected ServiceBase(KandandaDbContext dbContext)
        {
            DbContext = dbContext;
        }

        protected virtual T Create<T>(T entry) where T : class, IEntity
        {
            var set = GetDbSet<T>(DbContext);
            set.Add(entry);
            DbContext.SaveChanges();

            return entry;
        }

        protected virtual void Update<T>(T entry) where T : class, IEntity
        {
            var set = GetDbSet<T>(DbContext);
            set.Attach(entry);
            DbContext.Entry(entry).State = EntityState.Modified;
            DbContext.SaveChanges();
        }

        protected virtual void Delete<T>(T entry) where T : class, IEntity
        {
            var set = GetDbSet<T>(DbContext);
            set.Attach(entry);
            set.Remove(entry);

            DbContext.SaveChanges();
        }

        protected virtual T GetEntryById<T>(int id) where T : class, IEntity
        {
            var set = GetDbSet<T>(DbContext);
            return set.FirstOrDefault(entry => entry.Id == id);
        }

        protected virtual async Task<List<T>> GetAllAsync<T>() where T : class, IEntity
        {
            return await GetDbSet<T>(DbContext).ToListAsync();
        }

        protected virtual List<T> GetAll<T>() where T : class, IEntity
        {
            return GetDbSet<T>(DbContext).ToList();
        }

        protected virtual T GetEntry<T>(Predicate<T> predicate) where T : class, IEntity
        {
            var set = GetDbSet<T>(DbContext);
            return set.FirstOrDefault(entry => predicate(entry));
        }
        
        protected void ExecuteDatabaseAction(Action<KandandaDbContext> action)
        {
            action(DbContext);
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