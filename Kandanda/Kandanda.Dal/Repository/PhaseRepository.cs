using Kandanda.Dal.DataTransferObjects;

namespace Kandanda.Dal.Repository
{
    public sealed class PhaseRepository : Repository<Phase>
    {
        public PhaseRepository(IDatabaseContextFactory dbContextFactory)
            : base(dbContextFactory)
        {
        }
    }
}
