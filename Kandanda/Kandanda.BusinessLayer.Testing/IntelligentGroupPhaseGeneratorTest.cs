using System;
using System.Collections.Generic;
using System.Linq;
using Kandanda.BusinessLayer.PhaseGenerators;
using Kandanda.BusinessLayer.ServiceImplementations;
using Kandanda.BusinessLayer.ServiceInterfaces;
using Kandanda.Dal;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Kandanda.Dal.Entities;

namespace Kandanda.BusinessLayer.Testing
{
    [TestClass]
    public sealed class IntelligentGroupPhaseGeneratorTest
    {
        private IntelligentGroupPhaseGenerator _phaseGenerator;
        private readonly DateTime _tournamentDate = new DateTime(2014, 1, 1);
        private IParticipantService _participantService;
        private KandandaDbContextLocator _contextLocator;
        private KandandaDbContext Context => _contextLocator.Current;

        [TestInitialize]
        public void Setup()
        {
            _contextLocator = new KandandaDbContextLocator();
            _contextLocator.SetTestEnvironment();

            _participantService = new ParticipantService(_contextLocator);

            _phaseGenerator = new IntelligentGroupPhaseGenerator
            {
                GroupPhaseStart = _tournamentDate,
                GroupPhaseEnd = _tournamentDate,
                PlayTimeStart = TimeSpan.FromHours(10),
                LunchBreakStart = TimeSpan.FromHours(12),
                LunchBreakEnd = TimeSpan.FromHours(13),
                PlayTimeEnd = TimeSpan.FromHours(17),
                GameDuration = TimeSpan.FromMinutes(19),
                BreakBetweenGames = TimeSpan.FromMinutes(1),
                GroupSize = 4
            };
        }

        [TestCleanup]
        public void CleanUp()
        {
            Context.Dispose();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestTooLessTimeslotCount()
        {
            var participants = CreateRandomParticipants(20);
            
            _phaseGenerator.GroupSize = 5;
            _phaseGenerator.AddParticipants(participants);

            _phaseGenerator.GenerateMatches();
        }

        [TestMethod]
        public void TestSameGroupSizesGroups()
        {
            var participants = CreateRandomParticipants(20);

            _phaseGenerator.GroupSize = 5;
            _phaseGenerator.GroupPhaseEnd = new DateTime(2014, 1, 3);
            _phaseGenerator.PlayTimeStart = TimeSpan.FromHours(8);
            _phaseGenerator.AddParticipants(participants);

            var matchList = _phaseGenerator.GenerateMatches().ToList();

            Assert.AreEqual(40, matchList.Count);
            Assert.AreEqual(matchList[0].From, new DateTime(2014, 1, 1, 8, 0, 0));
            Assert.AreEqual(matchList[2].From, new DateTime(2014, 1, 1, 8, 40, 0));
            Assert.AreEqual(matchList[9].From, new DateTime(2014, 1, 1, 11, 0, 0));
            Assert.AreEqual(matchList[11].From, new DateTime(2014, 1, 1, 11, 40, 0));
            Assert.AreEqual(matchList[12].From, new DateTime(2014, 1, 1, 13, 0, 0));
        }

        [TestMethod]
        public void TestDifferentGroupSizes()
        {
            var participants = CreateRandomParticipants(25);

            _phaseGenerator.GroupSize = 6;
            _phaseGenerator.AddParticipants(participants);
            _phaseGenerator.PlayTimeStart = TimeSpan.FromHours(8);
            _phaseGenerator.GroupPhaseEnd = new DateTime(2014, 1, 3);

            var matchList = _phaseGenerator.GenerateMatches().ToList();

            Assert.AreEqual(66, matchList.Count);
            Assert.AreEqual(matchList[0].From, new DateTime(2014, 1, 1, 8, 0, 0));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestInvalidGroupSize()
        {
            _phaseGenerator.GroupSize = 12;
            _phaseGenerator.GenerateMatches();
        }

        [TestMethod]
        public void TestOneDayTournament()
        {
            var participants = CreateRandomParticipants(16);

            _phaseGenerator.GameDuration = TimeSpan.FromMinutes(20);
            _phaseGenerator.BreakBetweenGames = TimeSpan.Zero;
            _phaseGenerator.PlayTimeStart = TimeSpan.FromMinutes(8);
            _phaseGenerator.GroupSize = 4;
            _phaseGenerator.AddParticipants(participants);

            var matchList = _phaseGenerator.GenerateMatches().ToList();

            Assert.AreEqual(24, matchList.Count);
        }

        [TestMethod]
        public void TestTwoDayTournament()
        {
            var participants = CreateRandomParticipants(32);

            _phaseGenerator.GameDuration = TimeSpan.FromMinutes(20);
            _phaseGenerator.BreakBetweenGames = TimeSpan.Zero;
            _phaseGenerator.PlayTimeStart = TimeSpan.FromMinutes(8);
            _phaseGenerator.GroupSize = 4;
            _phaseGenerator.GroupPhaseEnd = new DateTime(2014, 1, 2);
            _phaseGenerator.AddParticipants(participants);

            var matchList = _phaseGenerator.GenerateMatches().ToList();

            Assert.AreEqual(48, matchList.Count);
        }

        private IEnumerable<Participant> CreateRandomParticipants(int count)
        {
            for (var name = 'A'; name < 'A' + count; ++name)
            {
                yield return _participantService.CreateEmpty(name.ToString());
            }
        }
    }
}
