using System.Collections.Generic;
using Kandanda.Dal.Entities;

namespace Kandanda.BusinessLayer.ServiceInterfaces
{
    public interface IParticipantService
    {
        Participant CreateEmpty(string name, string captain, string phone, string email);
        Participant CreateEmpty(string name);
        void DeleteParticipant(Participant participant);
        List<Participant> GetAllParticipants();
        Participant GetParticipantById(int id);
        void Update(Participant participant);
        Participant Save(Participant participant);
    }
}
