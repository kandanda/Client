using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kandanda.BusinessLayer.PhaseGenerators;
using Kandanda.Dal.Entities;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            var groupPhaseGenerator = new IntelligentGroupPhaseGenerator
            {
                GroupSize = 4,
                GameDuration = TimeSpan.FromMinutes(10),
                BreakBetweenGames = TimeSpan.FromMilliseconds(10)
            };

            groupPhaseGenerator.AddParticipant(new Participant {Name = "A"});
            groupPhaseGenerator.AddParticipant(new Participant { Name = "B" });
            groupPhaseGenerator.AddParticipant(new Participant { Name = "C" });
            groupPhaseGenerator.AddParticipant(new Participant { Name = "D" });
            groupPhaseGenerator.AddParticipant(new Participant { Name = "E" });
            groupPhaseGenerator.AddParticipant(new Participant { Name = "F" });
            groupPhaseGenerator.AddParticipant(new Participant { Name = "G" });
            groupPhaseGenerator.AddParticipant(new Participant { Name = "H" });
            groupPhaseGenerator.AddParticipant(new Participant { Name = "I" });
            groupPhaseGenerator.AddParticipant(new Participant { Name = "J" });
            groupPhaseGenerator.AddParticipant(new Participant { Name = "K" });
            groupPhaseGenerator.AddParticipant(new Participant { Name = "L" });
            groupPhaseGenerator.AddParticipant(new Participant { Name = "M" });
            groupPhaseGenerator.AddParticipant(new Participant { Name = "N" });
            
            var matchList = groupPhaseGenerator.GenerateMatches();


            /*
            foreach (var match in matchList)
            {
                Console.WriteLine(match.FirstParticipantId + " vs. " + match.SecondParticipantId);
            }
            */
            Console.ReadKey(true);
        }
    }
}
