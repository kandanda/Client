using Kandanda.Dal.DataTransferObjects;

namespace Kandanda.Dal.Repository
{
    public sealed class ParticipantRepository : Repository<Participant>
    {
        public ParticipantRepository(IDatabaseContextFactory dbContextFactory)
            : base(dbContextFactory)
        {
        }
    }
}
