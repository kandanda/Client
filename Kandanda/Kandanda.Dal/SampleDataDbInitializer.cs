using System.Collections.Generic;
using System.Data.Entity;
using Kandanda.Dal.DataTransferObjects;

namespace Kandanda.Dal
{
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
                "FC Thun",
                "FC St. Gallen",
                "FC Real Madrid",
                "FC Barcelona",
                "FC Kandanda",
                "GC Zürich",
                "FC Zürich"
            };
            
            foreach (var tournament in tournaments)
            {
                db.Tournaments.Add(new Tournament
                {
                    Name = tournament
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

            base.Seed(db);
        }
    }
}
