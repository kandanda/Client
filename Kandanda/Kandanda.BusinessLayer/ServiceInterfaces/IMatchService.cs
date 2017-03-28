using System.Collections.Generic;
using Kandanda.Dal.DataTransferObjects;

namespace Kandanda.BusinessLayer.ServiceInterfaces
{
    public interface IMatchService
    {
        Match CreateMatch(Participant firstParticipant, Participant secondParticipant);
        void SaveMatch(Match match);
        List<Match> GetMatchesByTournament(Tournament tournament);
        Match GetMatchById(int id);
    }
}
