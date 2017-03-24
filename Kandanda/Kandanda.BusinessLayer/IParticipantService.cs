using Kandanda.Dal.DataTransferObjects;

namespace Kandanda.BusinessLayer
{
    public interface IParticipantService
    {
        Participant CreateEmpty(string name);
    }
}
