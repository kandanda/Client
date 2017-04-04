using System.Linq;
using Kandanda.BusinessLayer.ServiceInterfaces;
using Kandanda.Dal;
using Kandanda.Dal.DataTransferObjects;
using Newtonsoft.Json.Linq;

namespace Kandanda.BusinessLayer.ServiceImplementations
{
    //TODO: Make Class Methods Async
    public class PublishTournamentRequestBuilder : ServiceBase, IPublishTournamentRequestBuilder
    {
        private KandandaDbContext _dbContext;

        public PublishTournamentRequestBuilder(KandandaDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
        
        public string BuildJsonRequest(Tournament tournament)
        {
            return JObject.FromObject(BuildJsonTournamentAsync(tournament)).ToString();
        }

        private object BuildJsonTournamentAsync(Tournament tournament)
        {
            var phases = (from p in _dbContext.Phases
                where p.TournamentId == tournament.Id
                select p).ToList().Select(BuildJsonPhasesAsync).ToList();

            return new
            {
                tournament = new
                {
                    id = tournament.Id,
                    name = tournament.Name,
                    phases
                }
            };
        }

        private object BuildJsonPhasesAsync(Phase phase)
        {
            var matches = (from m in _dbContext.Matches
                where m.PhaseId == phase.Id
                select m).ToList().Select(BuildJsonMatchAsync).ToList();

            return new
            {
                name = phase.Name,
                from = phase.From,
                until = phase.Until,
                matches
            };
        }

        private object BuildJsonMatchAsync(Match match)
        {
            var participant1 = _dbContext.Participants.Find(match.FirstParticipantId);
            var participant2 = _dbContext.Participants.Find(match.SecondParticipantId);

            return new
            {
                from = match.From,
                until = match.Until,
                place = _dbContext.Places.Find(match.PlaceId)?.Name,
                participants = new[]
                {
                    BuildJsonParticipant(participant1),
                    BuildJsonParticipant(participant2)
                }
            };
        }

        private static object BuildJsonParticipant(Participant participant)
        {
            return new
            {
                name = participant.Name,
                result = (string) null
            };
        }
    }
}
