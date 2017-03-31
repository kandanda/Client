using System.Collections.Generic;
using Kandanda.BusinessLayer.ServiceImplementations;
using Kandanda.BusinessLayer.ServiceInterfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Kandanda.BusinessLayer.Testing
{
    [TestClass]
    public class GroupPhaseGeneratorTest
    {
        private IParticipantService _participantService;
        private ITournamentService _tournamentService;
        private ServiceFactory _serviceFactory;

        [TestInitialize]
        public void Setup()
        {
            _serviceFactory = new ServiceFactory();
            _participantService = _serviceFactory.CreateParticipantService();
            _tournamentService = _serviceFactory.CreateTournamentService();
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
    }
}
