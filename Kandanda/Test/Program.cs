using System;
using Kandanda.BusinessLayer.PhaseGenerators;
using Kandanda.BusinessLayer.ServiceImplementations;
using Kandanda.Dal;

namespace Test
{
    internal class Program
    {
        public static void Main()
        {
            try
            {
                var groupPhaseGenerator = new IntelligentGroupPhaseGenerator
                {
                    GroupSize = 7,

                    GameDuration = TimeSpan.FromMinutes(10),
                    BreakBetweenGames = TimeSpan.FromMinutes(10),
                    GroupPhaseStart = new DateTime(2014, 1, 1),
                    GroupPhaseEnd = new DateTime(2014, 1, 3),

                    PlayTimeStart = TimeSpan.FromHours(8),
                    PlayTimeEnd = TimeSpan.FromHours(17),
                    LunchBreakStart = TimeSpan.FromHours(12),
                    LunchBreakEnd = TimeSpan.FromHours(13)
                };

                var contextLocator = new KandandaDbContextLocator();
                contextLocator.SetTestEnvironment();

                var participantService =
                    new ParticipantService(contextLocator);

                for (var name = 'A'; name <= 'Z'; ++name)
                {
                    groupPhaseGenerator.AddParticipant(participantService.CreateEmpty(name.ToString()));
                }

                var matchList = groupPhaseGenerator.GenerateMatches();

                foreach (var match in matchList)
                {
                    var firstParticipant = participantService.GetParticipantById(match.FirstParticipantId);
                    var secondParticipant = participantService.GetParticipantById(match.SecondParticipantId);

                    Console.WriteLine(match.From);
                    Console.WriteLine(firstParticipant.Name + " gegen " + secondParticipant.Name);
                    Console.WriteLine();
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);   
            }

            Console.ReadKey(true);
        }
    }
}
