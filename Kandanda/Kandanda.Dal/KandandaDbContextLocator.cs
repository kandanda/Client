using System.Data.Entity;
using Effort;

namespace Kandanda.Dal
{
    public class KandandaDbContextLocator
    {
        public KandandaDbContextLocator()
        {
            Current = new KandandaDbContext(new DbInitializer());
        }
        
        public KandandaDbContext Current { get; set; }

        public void ReInitializeDb(IDatabaseInitializer<KandandaDbContext> databaseInitializer)
        {
            Current.Database.Delete();
            Current.Dispose();
            Current = new KandandaDbContext(databaseInitializer);
            Current.Database.Initialize(true);
        }

        public void SetTestEnvironment()
        {
            Current.Dispose();
            Current = new KandandaDbContext(DbConnectionFactory.CreateTransient());
            Current.Database.Initialize(true);
        }
        
    }
}