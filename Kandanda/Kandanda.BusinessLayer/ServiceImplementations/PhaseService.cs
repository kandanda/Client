using System.Collections.Generic;
using System.Threading.Tasks;
using Kandanda.BusinessLayer.ServiceInterfaces;
using Kandanda.Dal;
using Kandanda.Dal.Entities;

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

        public async Task UpdateAsync(Phase phase)
        {
            await UpdateAsync<Phase>(phase);
        }

        public List<Phase> GetAllPhases()
        {
            return GetAll<Phase>();
        }
    }
}
