using System.Data.Entity;

namespace Kandanda.Dal
{
    public sealed class KandandaDatabaseContextFactory : IDatabaseContextFactory
    {
        public DbContext Create()
        {
            return new KandandaDbContext();
        }
    }
}
