using Kandanda.Dal.DataTransferObjects;

namespace Kandanda.BusinessLayer.ServiceInterfaces
{
    public interface IPhaseService
    {
        void AddMatchToPhase(Phase phase, Match match);
    }
}
