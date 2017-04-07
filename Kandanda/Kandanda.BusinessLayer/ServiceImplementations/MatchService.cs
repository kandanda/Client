using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Kandanda.BusinessLayer.ServiceInterfaces;
using Kandanda.Dal;
using Kandanda.Dal.Entities;

namespace Kandanda.BusinessLayer.ServiceImplementations
{
    public sealed class MatchService : ServiceBase, IMatchService
    {
        public MatchService(KandandaDbContext dbContext) : base(dbContext)
        {
        }

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
            DbContext.Matches.Add(match);
            DbContext.SaveChanges();
        }

        public Match GetMatchById(int id)
        {
            return GetEntryById<Match>(id);
        }

        public async Task<List<Match>> GetMatchesByTournamentAsync(Tournament tournament)
        {
            return await (from entry in DbContext.Tournaments
                join phase in DbContext.Phases
                on entry.Id equals phase.TournamentId
                join match in DbContext.Matches
                on phase.Id equals match.PhaseId
                where entry.Id == tournament.Id
                select match).ToListAsync();
        }

        public List<Match> GetMatchesByTournament(Tournament tournament)
        {
            return GetMatchesByTournamentAsync(tournament).Result;
        }

        public void Update(Match match)
        {
            Update<Match>(match);
        }
    }
}
