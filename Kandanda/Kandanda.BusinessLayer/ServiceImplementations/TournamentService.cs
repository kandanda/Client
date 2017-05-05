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

        public TournamentService(KandandaDbContextLocator contextLocator) : base(contextLocator)
        {
            _phaseService = new PhaseService(contextLocator);
        }

        public Tournament CreateEmpty(string name)
        {
            return Create(new Tournament
            {
                Name = name
            });
        }

        public Task<List<Tournament>> GetAllTournamentsAsync()
        {
            return GetAllAsync<Tournament>();
        }
        
        public async Task<List<Phase>> GetPhasesByTournamentAsync(Tournament tournament)
        {
            return await GetPhasesByTournamentQueryable(tournament).ToListAsync();
        }

        public List<Phase> GetPhasesByTournament(Tournament tournament)
        {
            return GetPhasesByTournamentQueryable(tournament).ToList();
        }

        private IQueryable<Phase> GetPhasesByTournamentQueryable(Tournament tournament)
        {
            return DbContext.Phases
                .Where(phase => phase.TournamentId == tournament.Id);
        }

        public async Task<List<Match>> GetMatchesByPhaseAsync(Phase phase)
        {
            return await GetMatchesByPhaseQueryable(phase)
                .ToListAsync();
        }

        public List<Match> GetMatchesByPhase(Phase phase)
        {
            return GetMatchesByPhaseQueryable(phase).ToList();
        }

        private IQueryable<Match> GetMatchesByPhaseQueryable(Phase phase)
        {
            return DbContext.Matches
                .Where(match => match.PhaseId == phase.Id);
        }
        
        public Phase GeneratePhase(Tournament tournament, int groupSize)
        {
            if (tournament == null)
                throw new ArgumentException("Phase generation tournament");

            var participants = GetParticipantsByTournament(tournament);

            var groupPhaseGenerator = new IntelligentGroupPhaseGenerator
            {
                BreakBetweenGames = tournament.BreakBetweenGames,
                GameDuration = tournament.GameDuration,
                GroupPhaseStart = tournament.From,
                GroupPhaseEnd = tournament.Until,
                GroupSize = 4,
                LunchBreakEnd = tournament.LunchBreakEnd,
                LunchBreakStart = tournament.LunchBreakStart,
                PlayTimeStart = tournament.PlayTimeStart,
                PlayTimeEnd = tournament.PlayTimeEnd
            };

            groupPhaseGenerator.AddParticipants(participants);

            //var groupPhaseGenerator = new GroupPhaseGenerator(participants, groupSize);
            var matches = groupPhaseGenerator.GenerateMatches();

            var matchService = new MatchService(KandandaDbContextLocator);

            var phase = GetPhasesByTournament(tournament).FirstOrDefault() ?? _phaseService.CreateEmpty();

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

        public void EnrolParticipant(Tournament tournament, IEnumerable<Participant> participantList)
        {
            ExecuteDatabaseAction(db =>
            {
                foreach (var participant in participantList)
                {
                    var alreadyExists =
                        DbContext.TournamentParticipants.Any(item => item.TournamentId == tournament.Id &&
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
            });
        }

        public void DeregisterParticipant(Tournament tournament, Participant participant)
        {
            ExecuteDatabaseAction(db =>
            {
                var tournamentParticipant = (from entry in db.TournamentParticipants
                    where (entry.ParticipantId == participant.Id) &&
                          (entry.TournamentId == tournament.Id)
                    select entry).FirstOrDefault();

                if (tournamentParticipant != null)
                {
                    db.TournamentParticipants.Remove(tournamentParticipant);
                    db.SaveChanges();
                }
            });
        }

        public void DeregisterParticipant(Tournament tournament, IEnumerable<Participant> participantList)
        {
            ExecuteDatabaseAction(db =>
            {
                foreach (var participant in participantList)
                {
                    var tournamentParticipant = (from entry in db.TournamentParticipants
                                                 where (entry.ParticipantId == participant.Id) &&
                                                       (entry.TournamentId == tournament.Id)
                                                 select entry).FirstOrDefault();

                    if (tournamentParticipant != null)
                    {
                        db.TournamentParticipants.Remove(tournamentParticipant);
                        db.SaveChanges();
                    }
                }
            });
        }

        public List<Participant> GetParticipantsByTournament(Tournament tournament)
        {
            return GetParticipantByTournamentEnumerable(tournament).ToList();
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

        public async Task<List<Participant>> GetParticipantsByTournamentAsync(Tournament tournament)
        {
            return await GetParticipantByTournamentEnumerable(tournament).ToListAsync();
        }

        private IQueryable<Participant> GetParticipantByTournamentEnumerable(Tournament tournament)
        {
            return from entry in DbContext.TournamentParticipants
                join participant in DbContext.Participants
                on entry.ParticipantId equals participant.Id
                where entry.TournamentId == tournament.Id
                select participant;
        }
    }
}