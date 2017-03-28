using System.Collections.Generic;
using Kandanda.Dal.DataTransferObjects;

namespace Kandanda.BusinessLayer.ServiceInterfaces
{
    public interface ITournamentService
    {
        Tournament CreateEmpty(string name);
        void EnrolParticipant(Tournament tournament, Participant participant);
        void DeregisterParticipant(Tournament tournament, Participant participant);
        List<Participant> GetParticipantsByTournament(Tournament tournament);
        List<Tournament> GetAllTournaments();
        void DeleteTournament(Tournament tournament);
    }
}