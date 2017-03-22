using System.Data.Entity;

namespace Kandanda.Dal
{
    public interface IDatabaseContextFactory
    {
        DbContext Create();
    }
}