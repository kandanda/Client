using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure.Pluralization;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using Kandanda.Dal;
using Kandanda.Dal.DataTransferObjects;

namespace Kandanda.BusinessLayer
{
    public abstract class ServiceBase
    {
        protected virtual T Create<T>(T entry) where T : class, IEntry
        {
            using (var db = new KandandaDbContext())
            {
                var set = GetDbSet<T>(db);
                set.Add(entry);
                db.SaveChanges();
                return entry;
            }
        }

        protected virtual void Delete<T>(T entry) where T : class, IEntry
        {
            using (var db = new KandandaDbContext())
            {
                var set = GetDbSet<T>(db);
                set.Attach(entry);
                set.Remove(entry);
                db.SaveChanges();
            }
        }

        protected virtual T GetEntryById<T>(int id) where T : class, IEntry
        {
            using (var db = new KandandaDbContext())
            {
                var set = GetDbSet<T>(db);
                return set.FirstOrDefault(entry => entry.Id == id);
            }
        }

        protected virtual List<T> GetAll<T>() where T : class, IEntry
        {
            using (var db = new KandandaDbContext())
            {
                return GetDbSet<T>(db).ToList();
            }
        }

        protected virtual T GetEntry<T>(Predicate<T> predicate) where T : class, IEntry
        {
            using (var db = new KandandaDbContext())
            {
                var set = GetDbSet<T>(db);
                return set.FirstOrDefault(entry => predicate(entry));
            }
        }
        
        protected void ExecuteDatabaseAction(Action<KandandaDbContext> action)
        {
            ExecuteDatabaseFunc(db =>
            {
                action(db);
                return 0;
            });
        }

        protected T ExecuteDatabaseFunc<T>(Func<KandandaDbContext, T> func)
        {
            try
            {
                using (var db = new KandandaDbContext())
                {
                    return func(db);
                }
            }
            catch (DbEntityValidationException exception)
            {
                throw HandleEntityValidationException(exception);
            }
        }
        
        protected DataAccessException HandleEntityValidationException(DbEntityValidationException exception)
        {
            StringBuilder exceptionText = new StringBuilder();

            foreach (var error in exception.EntityValidationErrors)
            {
                foreach (var validationError in error.ValidationErrors)
                {
                    exceptionText.AppendLine(validationError.PropertyName + ", " + validationError.ErrorMessage);
                }
            }

            return new DataAccessException(exceptionText.ToString(), exception);
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