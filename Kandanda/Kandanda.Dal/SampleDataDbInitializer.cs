using System.Collections.Generic;
using System.Data.Entity;
using Kandanda.Dal.Entities;

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

            var participants = new List<List<string>>
            {
                new List<string>{"FC Thun","Warren","(916) 178-0733","malesuada.vel@ipsum.edu"},
                new List<string>{"FC St. Gallen","Ramona","(842) 545-0136","libero.at@elit.ca"},
                new List<string>{"FC Real Madrid","Kylee","(331) 478-3654","vitae.orci@DuisgravidaPraesent.co.uk"},
                new List<string> {"FC Barcelona","Kelsey","(892) 392-9176","Curae.Donec@magna.co.uk"},
                new List<string>{"FC Kandanda","Joelle","(498) 115-2834","consectetuer.cursus.et@Curabiturut.org"},
                new List<string>{"GC Zürich","Rama","(893) 568-2097","ipsum.Curabitur@Nulla.ca"},
                new List<string>{"FC Zürich","Amethyst","(645) 953-2475","eu.nibh@eumetus.co.uk"}
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
                    Name = participant[0],
                    Captain = participant[1],
                    Phone = participant[2],
                    Email = participant[3]
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
