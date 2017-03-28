using System.Collections.Generic;
using Kandanda.BusinessLayer.ServiceImplementations;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Kandanda.BusinessLayer.Testing
{
    [TestClass]
    public class GroupPhaseGeneratorTest
    {
        private ParticipantService participantService;
        private TournamentService tournamentService;

        [TestInitialize]
        public void Setup()
        {
            participantService = new ParticipantService();
            tournamentService = new TournamentService();
        }

        [TestMethod]
        public void TestGroupPhaseGeneration()
        {
            var tournament = tournamentService.CreateEmpty("SwissCup");
            var participants = new List<string>
            {
                "FC St. Gallen", "FC Thun", "FC Solothurn", "FC Zürich",
                "Young Boys", "FC Vaduz", "GC Zürich", "FC Basel"
            };

            foreach (var participantName in participants)
            {
                var participant = participantService.CreateEmpty(participantName);
                tournamentService.EnrolParticipant(tournament, participant);
            }

            tournamentService.GeneratePhase(tournament, 4);
        }
    }
}
