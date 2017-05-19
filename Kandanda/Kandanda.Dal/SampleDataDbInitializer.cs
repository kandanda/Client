using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics.CodeAnalysis;
using Kandanda.Dal.Entities;

namespace Kandanda.Dal
{
    [ExcludeFromCodeCoverage]
    public class SampleDataDbInitializer : DropCreateDatabaseAlways<KandandaDbContext>
    {
        protected override void Seed(KandandaDbContext db)
        {
            var tournaments = new List<string>
            {
                "Schweizer Cup",
                "Europa Meisterschaft",
                "Raiffeisen Superleague",
                "FIFA Fussballweltmeisterschaft",
                "Europa Cup",
                "Challenge League"
            };

            var participants = new List<string>
            {
                "A1",
                "A2",
                "A3",
                "A4",
                "B1",
                "B2",
                "B3",
                "B4",
                "C1",
                "C2",
                "C3",
                "C4",
                "D1",
                "D2",
                "D3",
                "D4"

            };
            
            foreach (var tournament in tournaments)
            {
                db.Tournaments.Add(new Tournament
                {
                    Name = tournament,
                    From = DateTime.Now,
                    Until = DateTime.Now.AddDays(2),
                    BreakBetweenGames                    = TimeSpan.FromMinutes(5),
                    DetermineThird = true,
                    FinalsFrom = DateTime.Now.AddDays(1).AddHours(-4),
                    Monday = true,
                    Tuesday = true,
                    Wednesday = true,
                    Thursday = true,
                    Friday = true,
                    Saturday = true,
                    Sunday = true,
                    GameDuration = TimeSpan.FromMinutes(15),
                    GroupSize = 4
                });
            }

            foreach (var participant in participants)
            {
                db.Participants.Add(new Participant
                {
                    Name = participant
                });
            }

            db.TournamentParticipants.Add(new TournamentParticipant
            {
                ParticipantId = 1,
                TournamentId = 2
            });

            db.TournamentParticipants.Add(new TournamentParticipant
            {
                ParticipantId = 1,
                TournamentId = 1
            });

            db.TournamentParticipants.Add(new TournamentParticipant
            {
                ParticipantId = 2,
                TournamentId = 1
            });

            db.TournamentParticipants.Add(new TournamentParticipant
            {
                ParticipantId = 3,
                TournamentId = 1
            });
            db.TournamentParticipants.Add(new TournamentParticipant
            {
                ParticipantId = 4,
                TournamentId = 1
            });
            db.TournamentParticipants.Add(new TournamentParticipant
            {
                ParticipantId = 5,
                TournamentId = 1
            });
            db.TournamentParticipants.Add(new TournamentParticipant
            {
                ParticipantId = 6,
                TournamentId = 1
            });
            db.TournamentParticipants.Add(new TournamentParticipant
            {
                ParticipantId = 7,
                TournamentId = 1
            });
            db.TournamentParticipants.Add(new TournamentParticipant
            {
                ParticipantId = 8,
                TournamentId = 1
            });
            db.TournamentParticipants.Add(new TournamentParticipant
            {
                ParticipantId = 9,
                TournamentId = 1
            });
            db.TournamentParticipants.Add(new TournamentParticipant
            {
                ParticipantId = 10,
                TournamentId = 1
            });
            db.TournamentParticipants.Add(new TournamentParticipant
            {
                ParticipantId = 11,
                TournamentId = 1
            });
            db.TournamentParticipants.Add(new TournamentParticipant
            {
                ParticipantId = 12,
                TournamentId = 1
            });
            db.TournamentParticipants.Add(new TournamentParticipant
            {
                ParticipantId = 13,
                TournamentId = 1
            });
            db.TournamentParticipants.Add(new TournamentParticipant
            {
                ParticipantId = 14,
                TournamentId = 1
            });
            db.TournamentParticipants.Add(new TournamentParticipant
            {
                ParticipantId = 15,
                TournamentId = 1
            });
            db.TournamentParticipants.Add(new TournamentParticipant
            {
                ParticipantId = 16,
                TournamentId = 1
            });

            base.Seed(db);
        }
    }
}
