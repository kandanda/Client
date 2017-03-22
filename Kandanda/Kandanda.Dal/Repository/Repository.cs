using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure.Pluralization;
using System.Linq;
using Kandanda.Dal.DataTransferObjects;

namespace Kandanda.Dal.Repository
{
    public abstract class Repository<T> where T : class, IEntry
    {
        private readonly IDatabaseContextFactory _dbContextFactory;
        
        protected Repository(IDatabaseContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public List<T> GetAll()
        {
            using (var db = _dbContextFactory.Create())
            {
                return GetDbSet(db)
                        .Select(entry => entry)
                        .ToList();
            }
        }
        
        public void DeleteEntry(T entry)
        {
            using (var db = _dbContextFactory.Create())
            {
                GetDbSet(db).Remove(entry);
            }
        }

        public T GetEntryById(int id)
        {
            using (var db = _dbContextFactory.Create())
            {
                return GetDbSet(db)
                    .FirstOrDefault(entry => entry.Id == id);
            }
        }

        public void Save(T entry)
        {
            using (var db = _dbContextFactory.Create())
            {
                if (entry.Id != 0)
                {
                    var originalEntry = GetEntryById(entry.Id);
                    db.Entry(originalEntry).CurrentValues.SetValues(entry);
                }
                else
                {
                    GetDbSet(db).Add(entry);
                }

                db.SaveChanges();
            }
        }

        private DbSet<T> GetDbSet(DbContext db)
        {
            var pluralizedName = GetPluralizedName();
            var propertyInfo = db.GetType().GetProperty(pluralizedName);
            return (DbSet<T>)propertyInfo.GetValue(db);
        }

        private string GetPluralizedName()
        {
            var className = typeof(T).Name;
            var pluralizationService = new EnglishPluralizationService();
            return pluralizationService.Pluralize(className);
        }
    }
}