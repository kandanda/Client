using System;
using System.Collections.Generic;
using Kandanda.BusinessLayer.ServiceImplementations;
using Kandanda.BusinessLayer.ServiceInterfaces;
using Kandanda.Dal;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Kandanda.BusinessLayer.Testing
{
    [TestClass]
    public class GroupPhaseGeneratorTest
    {
        private IParticipantService _participantService;
        private ITournamentService _tournamentService;
        private KandandaDbContextLocator _contextLocator;
        private KandandaDbContext Context => _contextLocator.Current;

        [TestInitialize]
        public void Setup()
        {
            _contextLocator = new KandandaDbContextLocator();
            _contextLocator.SetTestEnvironment();

            _participantService = new ParticipantService(_contextLocator);
            _tournamentService = new TournamentService(_contextLocator);
        }

        [TestCleanup]
        public void CleanUp()
        {
            Context.Dispose();
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
