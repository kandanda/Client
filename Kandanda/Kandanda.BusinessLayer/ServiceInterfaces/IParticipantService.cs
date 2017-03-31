using System.Collections.Generic;
using Kandanda.Dal.DataTransferObjects;

namespace Kandanda.BusinessLayer.ServiceInterfaces
{
    public interface IParticipantService
    {
        Participant CreateEmpty(string name);
        void DeleteParticipant(Participant participant);
        List<Participant> GetAllParticipants();
        Participant GetParticipantById(int id);
    }
}
