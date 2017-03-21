using Kandanda.Dal.DataTransferObjects;

namespace Kandanda.Dal.Repository
{
    public sealed class OrganizerRepository : Repository<Organizer>
    {
        public OrganizerRepository(IDatabaseContextFactory dbContextFactory)
            : base(dbContextFactory)
        {
        }
    }
}
