using System.Collections.Generic;
using System.Linq;
using Kandanda.BusinessLayer.ServiceInterfaces;
using Kandanda.Dal.DataTransferObjects;

namespace Kandanda.BusinessLayer.ServiceImplementations
{
    public sealed class MatchService : ServiceBase, IMatchService
    {
        public Match CreateMatch(Participant firstParticipant, Participant secondParticipant)
        {
            var match = new Match
            {
                FirstParticipantId = firstParticipant.Id,
                SecondParticipantId = secondParticipant.Id
            };

            SaveMatch(match);
                
            return match;
        }

        public void SaveMatch(Match match)
        {
            ExecuteDatabaseAction(db =>
            {
                db.Matches.Add(match);
                db.SaveChanges();
            });
        }

        public Match GetMatchById(int id)
        {
            return GetEntryById<Match>(id);
        }

        public List<Match> GetMatchesByTournament(Tournament tournament)
        {
            return ExecuteDatabaseFunc(db => (from entry in db.Tournaments
                join phase in db.Phases
                on entry.Id equals phase.TournamentId
                join match in db.Matches
                on phase.Id equals match.PhaseId
                where entry.Id == tournament.Id
                select match).ToList());
        }
    }
}
