using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics.CodeAnalysis;
using Kandanda.Dal.Entities;

namespace Kandanda.Dal
{
    [ExcludeFromCodeCoverage]
    public class DbInitializer : DropCreateDatabaseIfModelChanges<KandandaDbContext>
    {
    }
}
