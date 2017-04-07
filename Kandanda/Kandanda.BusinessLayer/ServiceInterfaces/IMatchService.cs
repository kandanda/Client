using System.Collections.Generic;
using System.Threading.Tasks;
using Kandanda.Dal.Entities;

namespace Kandanda.BusinessLayer.ServiceInterfaces
{
    public interface IMatchService
    {
        Match CreateMatch(Participant firstParticipant, Participant secondParticipant);
        void SaveMatch(Match match);
        Task<List<Match>> GetMatchesByTournamentAsync(Tournament tournament);
        List<Match> GetMatchesByTournament(Tournament tournament);
        Match GetMatchById(int id);
        void Update(Match match);
    }
}