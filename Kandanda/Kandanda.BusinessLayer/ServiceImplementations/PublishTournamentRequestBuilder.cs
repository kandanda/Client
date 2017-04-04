using System.Linq;
using Kandanda.BusinessLayer.ServiceInterfaces;
using Kandanda.Dal;
using Kandanda.Dal.DataTransferObjects;
using Newtonsoft.Json.Linq;

namespace Kandanda.BusinessLayer.ServiceImplementations
{
    public class PublishTournamentRequestBuilder : ServiceBase, IPublishTournamentRequestBuilder
    {
        public PublishTournamentRequestBuilder(KandandaDbContext dbContext) : base(dbContext)
        {
        }
        
        public string BuildJsonRequest(Tournament tournament)
        {
            return JObject.FromObject(BuildJsonTournamentAsync(tournament)).ToString();
        }

        private object BuildJsonTournamentAsync(Tournament tournament)
        {
            return new
            {
                tournament = new
                {
                    id = tournament.Id,
                    name = tournament.Name,
                    phases = from p in _dbContext.Phases
                                    where p.TournamentId == tournament.Id
                                    select BuildJsonPhasesAsync(p)
                }
            };
        }

        private object BuildJsonPhasesAsync(Phase phase)
        {
            return new
            {
                name = phase.Name,
                from = phase.From,
                until = phase.Until,
                matches = from m in _dbContext.Matches
                                 where m.PhaseId == phase.Id
                                 select BuildJsonMatchAsync(m)
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
