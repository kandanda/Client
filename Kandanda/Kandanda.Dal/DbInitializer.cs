using System.Data.Entity;
using System.Diagnostics.CodeAnalysis;

namespace Kandanda.Dal
{
    [ExcludeFromCodeCoverage]
    public class DbInitializer : DropCreateDatabaseIfModelChanges<KandandaDbContext>
    {
    }
}
