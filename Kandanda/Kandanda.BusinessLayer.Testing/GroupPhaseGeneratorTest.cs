using System;
using System.Collections.Generic;
using Kandanda.BusinessLayer.ServiceImplementations;
using Kandanda.Dal.DataTransferObjects;
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
            TestHelper.ResetDatabase();
        }

        [TestMethod]
        public void TestGroupPhaseGeneration()
        {
            const int groupSize = 4;
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

            var phase = tournamentService.GeneratePhase(tournament, groupSize);
            var matchList = tournamentService.GetMatchesByPhase(phase);
            
            Assert.AreEqual(12, matchList.Count);
        }

        [TestMethod]
        public void TestDifferentGroupSizes()
        {
            const int groupSize = 5;
            var tournament = tournamentService.CreateEmpty("SwissCup");

            var participants = new List<string>
            {
                "Berlin", "Hamburg", "Wuppertal", "Essen",
                "München", "Bonn", "Leipzig", "Stuttgart",
                "Salzburg"
            };

            foreach (var participantName in participants)
            {
                var participant = participantService.CreateEmpty(participantName);
                tournamentService.EnrolParticipant(tournament, participant);
            }

            var phase = tournamentService.GeneratePhase(tournament, groupSize);
            var matchList = tournamentService.GetMatchesByPhase(phase);

            Assert.AreEqual(16, matchList.Count);
        }
    }
}
