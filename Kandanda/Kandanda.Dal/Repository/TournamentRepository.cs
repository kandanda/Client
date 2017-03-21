using Kandanda.Dal.DataTransferObjects;

namespace Kandanda.Dal.Repository
{
    public sealed class TournamentRepository : Repository<Tournament>
    {
        public TournamentRepository(IDatabaseContextFactory dbContextFactory)
            : base(dbContextFactory)
        {
        }
    }
}
