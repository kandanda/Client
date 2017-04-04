using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Kandanda.BusinessLayer.ServiceInterfaces;
using Kandanda.Dal;
using Kandanda.Dal.DataTransferObjects;

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
            _dbContext.Matches.Add(match);
            _dbContext.SaveChanges();
        }

        public Match GetMatchById(int id)
        {
            return GetEntryById<Match>(id);
        }

        public async Task<List<Match>> GetMatchesByTournamentAsync(Tournament tournament)
        {
            return await (from entry in _dbContext.Tournaments
                join phase in _dbContext.Phases
                on entry.Id equals phase.TournamentId
                join match in _dbContext.Matches
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
