using System;
using System.Collections.Generic;
using Kandanda.BusinessLayer.ServiceImplementations;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Kandanda.BusinessLayer.Testing
{
    [TestClass]
    public class GroupPhaseGeneratorTest
    {
        private ParticipantService _participantService;
        private TournamentService _tournamentService;

        [TestInitialize]
        public void Setup()
        {
            _participantService = new ParticipantService();
            _tournamentService = new TournamentService();
            TestHelper.ResetDatabase();
        }

        [TestMethod]
        public void TestGroupPhaseGeneration()
        {
            const int groupSize = 4;
            var tournament = _tournamentService.CreateEmpty("SwissCup");

            var participants = new List<string>
            {
                "FC St. Gallen", "FC Thun", "FC Solothurn", "FC Zürich",
                "Young Boys", "FC Vaduz", "GC Zürich", "FC Basel"
            };

            foreach (var participantName in participants)
            {
                var participant = _participantService.CreateEmpty(participantName);
                _tournamentService.EnrolParticipant(tournament, participant);
            }

            var phase = _tournamentService.GeneratePhase(tournament, groupSize);
            var matchList = _tournamentService.GetMatchesByPhase(phase);
            
            Assert.AreEqual(12, matchList.Count);
        }

        [TestMethod]
        public void TestDifferentGroupSizes()
        {
            const int groupSize = 5;
            var tournament = _tournamentService.CreateEmpty("SwissCup");

            var participants = new List<string>
            {
                "Berlin", "Hamburg", "Wuppertal", "Essen",
                "München", "Bonn", "Leipzig", "Stuttgart",
                "Salzburg"
            };

            foreach (var participantName in participants)
            {
                var participant = _participantService.CreateEmpty(participantName);
                _tournamentService.EnrolParticipant(tournament, participant);
            }

            var phase = _tournamentService.GeneratePhase(tournament, groupSize);
            var matchList = _tournamentService.GetMatchesByPhase(phase);

            Assert.AreEqual(16, matchList.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestEmptyGroup()
        {
            var tournament = _tournamentService.CreateEmpty("Test");
            _tournamentService.GeneratePhase(tournament, 0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestZeroGroupSize()
        {
            var participant = _participantService.CreateEmpty("Empty Participant");
            var tournament = _tournamentService.CreateEmpty("Empty Tournament");

            _tournamentService.EnrolParticipant(tournament, participant);

            _tournamentService.GeneratePhase(tournament, 0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestNullTournament()
        {
            _tournamentService.GeneratePhase(null, 10);
        }
    }
}
