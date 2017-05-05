using System;
using System.Collections.Generic;
using Effort;
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
        private KandandaDbContext _context;

        [TestInitialize]
        public void Setup()
        {
            _context = new KandandaDbContext(DbConnectionFactory.CreateTransient());
            _participantService = new ParticipantService(_context);
            _tournamentService = new TournamentService(_context);
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
