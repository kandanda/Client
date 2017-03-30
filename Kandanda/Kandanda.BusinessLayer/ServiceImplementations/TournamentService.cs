using System.Collections.Generic;
using System.Linq;
using Kandanda.BusinessLayer.PhaseGenerators;
using Kandanda.BusinessLayer.ServiceInterfaces;
using Kandanda.Dal;
using Kandanda.Dal.DataTransferObjects;

namespace Kandanda.BusinessLayer.ServiceImplementations
{
    public sealed class TournamentService : ServiceBase, ITournamentService
    {
        public Tournament CreateEmpty(string name)
        {
            return Create(new Tournament
            {
                Name = name
            });
        }

        public List<Phase> GetPhasesByTournament(Tournament tournament)
        {
            using (var db = new KandandaDbContext())
            {
                return db.Phases
                    .Where(phase => phase.TournamentId == tournament.Id)
                    .ToList();
            }
        }
        
        public List<Match> GetMatchesByPhase(Phase phase)
        {
            using (var db = new KandandaDbContext())
            {
                return db.Matches
                    .Where(match => match.PhaseId == phase.Id)
                    .ToList();
            }
        }
        
        public Phase GeneratePhase(Tournament tournament, int groupSize)
        {
            var participants = GetParticipantsByTournament(tournament);

            GroupPhaseGenerator groupPhaseGenerator = new GroupPhaseGenerator(participants, groupSize);
            var matches = groupPhaseGenerator.GenerateMatches();

            var matchService = new MatchService();

            foreach (var match in matches)
            {
                matchService.SaveMatch(match);
            }

            var phase = new Phase();

            return phase;
        }

        public Tournament GetTournamentById(int id)
        {
            return GetEntryById<Tournament>(id);
        }
        
        public void EnrolParticipant(Tournament tournament, Participant participant)
        {
            Create(new TournamentParticipant
            {
                TournamentId = tournament.Id,
                ParticipantId = participant.Id
            });
        }

        public void DeregisterParticipant(Tournament tournament, Participant participant)
        {
            ExecuteDatabaseAction(db =>
            {
                var tournamentParticipant = (from entry in db.TournamentParticipants
                    where entry.ParticipantId == participant.Id &&
                            entry.TournamentId == tournament.Id
                    select entry).FirstOrDefault();

                db.TournamentParticipants.Remove(tournamentParticipant);
                db.SaveChanges();
            });
        }
        
        public List<Participant> GetParticipantsByTournament(Tournament tournament)
        {
            return ExecuteDatabaseFunc(db => 
                (from entry in db.TournamentParticipants
                join participant in db.Participants
                on entry.ParticipantId equals participant.Id
                select participant).ToList());
        }

        public void DeleteTournament(Tournament tournament)
        {
            Delete(tournament);
        }

        public List<Tournament> GetAllTournaments()
        {
            return GetAll<Tournament>();
        }
    }
}