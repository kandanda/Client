﻿using System;
using System.Linq;
using Kandanda.BusinessLayer.ServiceInterfaces;
using Kandanda.Dal;
using Kandanda.Dal.Entities;
using Newtonsoft.Json.Linq;

namespace Kandanda.BusinessLayer.ServiceImplementations
{
    public class PublishTournamentRequestBuilder : ServiceBase, IPublishTournamentRequestBuilder
    {
        public PublishTournamentRequestBuilder(KandandaDbContextLocator contextLocator) : base(contextLocator)
        {
        }
        
        public string BuildJsonRequest(Tournament tournament)
        {
            return JObject.FromObject(BuildJsonTournamentAsync(tournament)).ToString();
        }

        private object BuildJsonTournamentAsync(Tournament tournament)
        {
            var phases = (from p in DbContext.Phases
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
            var matches = (from m in DbContext.Matches
                where m.PhaseId == phase.Id
                select m).ToList().Select(BuildJsonMatchAsync).ToList();

            return new
            {
                name = string.IsNullOrWhiteSpace(phase.Name) ? "Phase": phase.Name,
                from = phase.From,
                until = phase.Until,
                matches
            };
        }
        
        private object BuildJsonMatchAsync(Match match)
        {
            var participant1 = DbContext.Participants.Find(match.FirstParticipantId);
            var participant2 = DbContext.Participants.Find(match.SecondParticipantId);
            var place = DbContext.Places.Find(match.PlaceId);

            return new
            {
                from = match.From,
                until = match.Until,
                place = place == null? "Main Hall": place.Name,
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
