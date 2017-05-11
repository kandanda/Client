using System.Collections.Generic;
using System.Threading.Tasks;
using Kandanda.Dal.Entities;

namespace Kandanda.BusinessLayer.ServiceInterfaces
{
    public interface IPhaseService
    {
        Phase CreateEmpty();
        Phase GetPhaseById(int id);
        void Update(Phase phase);
        Task UpdateAsync(Phase phase);
        List<Phase> GetAllPhases();
    }
}
