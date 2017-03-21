using Kandanda.Dal.DataTransferObjects;

namespace Kandanda.Dal.Repository
{
    public sealed class MatchRepository : Repository<Match>
    {
        public MatchRepository(IDatabaseContextFactory dbContextFactory)
            : base(dbContextFactory)
        {
        }
    }
}