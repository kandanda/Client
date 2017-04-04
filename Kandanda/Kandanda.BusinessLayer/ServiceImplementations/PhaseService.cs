using System.Collections.Generic;
using Kandanda.BusinessLayer.ServiceInterfaces;
using Kandanda.Dal;
using Kandanda.Dal.DataTransferObjects;

namespace Kandanda.BusinessLayer.ServiceImplementations
{
    public sealed class PhaseService : ServiceBase, IPhaseService
    {
        public PhaseService(KandandaDbContext dbContext) : base(dbContext)
        {
        }

        public Phase CreateEmpty()
        {
            return Create(new Phase());
        }

        public Phase GetPhaseById(int id)
        {
            return GetEntryById<Phase>(id);
        }

        public void Update(Phase phase)
        {
            Update<Phase>(phase);
        }

        public List<Phase> GetAllPhases()
        {
            return GetAll<Phase>();
        }
    }
}
