using System.Collections.Generic;
using System.Threading.Tasks;
using Kandanda.Dal.Entities;

namespace Kandanda.BusinessLayer.ServiceInterfaces
{
    public interface ITournamentService
    {
        Tournament CreateEmpty(string name);
        Tournament GetTournamentById(int id);

        Phase GeneratePhase(Tournament tournament, int groupSize);

        void EnrolParticipant(Tournament tournament, Participant participant);
        void EnrolParticipant(Tournament tournament, IEnumerable<Participant> participantList);
        void DeregisterParticipant(Tournament tournament, Participant participant);
        void DeregisterParticipant(Tournament tournament, IEnumerable<Participant> participantList);

        void GenerateGroups(Tournament tournament);
        string GetGroupByParticipant(Tournament tournament, Participant participant);
            
        Task<List<Participant>> GetNotEnrolledParticipantsByTournamentAsync(Tournament tournament);
        List<Participant> GetNotEnrolledParticipantsByTournament(Tournament tournament);
        List<Tournament> GetAllTournaments();
        Task<List<Tournament>> GetAllTournamentsAsync();
        Task<List<Phase>> GetPhasesByTournamentAsync(Tournament tournament);
        Task<List<Match>> GetMatchesByPhaseAsync(Phase phase);
        List<Phase> GetPhasesByTournament(Tournament tournament);
        List<Match> GetMatchesByPhase(Phase phase);

        void DeleteTournament(Tournament tournament);
        void Update(Tournament tournament);

        Task<List<Participant>> GetParticipantsByTournamentAsync(Tournament currentTournament);
        List<Participant> GetParticipantsByTournament(Tournament currentTournament);
    }
}
