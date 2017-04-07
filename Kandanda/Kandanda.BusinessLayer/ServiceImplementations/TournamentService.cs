using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Kandanda.BusinessLayer.PhaseGenerators;
using Kandanda.BusinessLayer.ServiceInterfaces;
using Kandanda.Dal;
using Kandanda.Dal.Entities;

namespace Kandanda.BusinessLayer.ServiceImplementations
{
    public sealed class TournamentService : ServiceBase, ITournamentService
    {
        private readonly IPhaseService _phaseService;

        public TournamentService(KandandaDbContext dbContext) : base(dbContext)
        {
            _phaseService = new PhaseService(dbContext);
        }

        public Tournament CreateEmpty(string name)
        {
            return Create(new Tournament
            {
                Name = name
            });
        }

        public async Task<List<Phase>> GetPhasesByTournamentAsync(Tournament tournament)
        {
            return await DbContext.Phases
                .Where(phase => phase.TournamentId == tournament.Id)
                .ToListAsync();
        }
        
        public async Task<List<Match>> GetMatchesByPhaseAsync(Phase phase)
        {
            return await DbContext.Matches
                .Where(match => match.PhaseId == phase.Id)
                .ToListAsync();
        }

        public List<Phase> GetPhasesByTournament(Tournament tournament)
        {
            return GetPhasesByTournamentAsync(tournament).Result;
        }

        public List<Match> GetMatchesByPhase(Phase phase)
        {
            return GetMatchesByPhaseAsync(phase).Result;
        }

        public Phase GeneratePhase(Tournament tournament, int groupSize)
        {
            var participants = GetParticipantsByTournament(tournament);

            var groupPhaseGenerator = new GroupPhaseGenerator(participants, groupSize);
            var matches = groupPhaseGenerator.GenerateMatches();

            var matchService = new MatchService(DbContext);
            var phase = _phaseService.CreateEmpty();
            
            foreach (var match in matches)
            {
                match.PhaseId = phase.Id;
                matchService.SaveMatch(match);
            }

            phase.TournamentId = tournament.Id;
            
            _phaseService.Update(phase);

            return phase;
        }

        public Tournament GetTournamentById(int id)
        {
            return GetEntryById<Tournament>(id);
        }

        public List<Participant> GetNotEnrolledParticipantsByTournament(Tournament tournament)
        {
            return GetNotEnrolledParticipantsByTournamentQueryable(tournament).ToList();
        }

        Task<List<Participant>> ITournamentService.GetNotEnrolledParticipantsByTournamentAsync(Tournament tournament)
        {
            return GetNotEnrolledParticipantsByTournamentQueryable(tournament).ToListAsync();
        }

        private IQueryable<Participant> GetNotEnrolledParticipantsByTournamentQueryable(Tournament tournament)
        {
            var enrolledParticipants = (from participant in DbContext.Participants
                                        join participantTournament in DbContext.TournamentParticipants
                                        on participant.Id equals participantTournament.ParticipantId
                                        where participantTournament.TournamentId == tournament.Id
                                        select participant);

            return DbContext.Participants.Where(item => !enrolledParticipants.Contains(item));
        }

        public void EnrolParticipant(Tournament tournament, Participant participant)
        {
            var alreadyExists = DbContext.TournamentParticipants.Any(item => item.TournamentId == tournament.Id &&
                item.ParticipantId == participant.Id);

            if (!alreadyExists)
            {
                Create(new TournamentParticipant
                {
                    TournamentId = tournament.Id,
                    ParticipantId = participant.Id
                });
            }
        }

        public void DeregisterParticipant(Tournament tournament, Participant participant)
        {
            ExecuteDatabaseAction(db =>
            {
                var tournamentParticipant = (from entry in db.TournamentParticipants
                    where entry.ParticipantId == participant.Id &&
                            entry.TournamentId == tournament.Id
                    select entry).FirstOrDefault();

                if (tournamentParticipant != null)
                {
                    db.TournamentParticipants.Remove(tournamentParticipant);
                    db.SaveChanges();
                }
            });
        }
        
        public async Task<List<Participant>> GetParticipantsByTournamentAsync(Tournament tournament)
        {
            return await (from entry in DbContext.TournamentParticipants
                join participant in DbContext.Participants
                on entry.ParticipantId equals participant.Id
                select participant).ToListAsync();
        }

        public List<Participant> GetParticipantsByTournament(Tournament tournament)
        {
            return GetParticipantsByTournamentAsync(tournament).Result;
        }
        
        public void DeleteTournament(Tournament tournament)
        {
            Delete(tournament);
        }

        public List<Tournament> GetAllTournaments()
        {
            return GetAll<Tournament>();
        }

        public void Update(Tournament tournament)
        {
            Update<Tournament>(tournament);
        }
    }
}