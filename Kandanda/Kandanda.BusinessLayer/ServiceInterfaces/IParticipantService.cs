using System.Collections.Generic;
using Kandanda.Dal.DataTransferObjects;

namespace Kandanda.BusinessLayer.ServiceInterfaces
{
    public interface IParticipantService
    {
        Participant CreateEmpty(string name);
        Participant GetParticipantById(int id);
        void DeleteParticipant(Participant participant);
        List<Participant> GetAllParticipants();
    }
}
