using System;
using System.Collections.Generic;
using System.Linq;
using Kandanda.BusinessLayer.PhaseGenerators;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Kandanda.Dal.Entities;

namespace Kandanda.BusinessLayer.Testing
{
    [TestClass]
    public sealed class IntelligentGroupPhaseGeneratorTest
    {
        private IntelligentGroupPhaseGenerator _phaseGenerator;
        private readonly DateTime _tournamentDate = new DateTime(2014, 1, 1);

        [TestInitialize]
        public void Setup()
        {
            _phaseGenerator = new IntelligentGroupPhaseGenerator
            {
                GroupPhaseStart = _tournamentDate,
                GroupPhaseEnd = _tournamentDate,
                PlayTimeStart = TimeSpan.FromHours(8),
                LunchBreakStart = TimeSpan.FromHours(12),
                LunchBreakEnd = TimeSpan.FromHours(13),
                PlayTimeEnd = TimeSpan.FromHours(17),
                GroupSize = 4
            };
        }

        [TestMethod]
        public void TestTest()
        {
            _phaseGenerator.GroupSize = 4;
            //_phaseGenerator.NumberOfTeams = 63;

            var list = _phaseGenerator.GenerateMatches().ToList();
        }

        [TestMethod]
        public void TestTwoDaysTournamentTimeSlotsGeneration()
        {
            _phaseGenerator.GroupPhaseEnd = new DateTime(2014, 1, 3);
            _phaseGenerator.GameDuration = TimeSpan.FromMinutes(30);
            _phaseGenerator.BreakBetweenGames = TimeSpan.Zero;

            var matchList = _phaseGenerator.GenerateMatches().ToList();

            Assert.AreEqual(48, matchList.Count);
            
            Assert.AreEqual(16, GetDayMatchCount(new DateTime(2014, 1, 1), matchList));
            Assert.AreEqual(16, GetDayMatchCount(new DateTime(2014, 1, 2), matchList));
            Assert.AreEqual(16, GetDayMatchCount(new DateTime(2014, 1, 3), matchList));
        }

        [TestMethod]
        public void TestNonLunchBreakTimeSlotsGeneration()
        {
            _phaseGenerator.BreakBetweenGames = TimeSpan.FromMinutes(10);
            _phaseGenerator.GameDuration = TimeSpan.FromMinutes(45);

            var matchList = _phaseGenerator.GenerateMatches().ToList();

            Assert.AreEqual(8, matchList.Count);
            
            TestMatchDate(matchList[0], Time(_tournamentDate, 8),
                Time(_tournamentDate, 8, 45));

            TestMatchDate(matchList[3], Time(_tournamentDate, 10, 45),
                Time(_tournamentDate, 11, 30));

            TestMatchDate(matchList[4], Time(_tournamentDate, 13),
                Time(_tournamentDate, 13, 45));
        }

        [TestMethod]
        public void TestTimeSlotsGeneration()
        {
            _phaseGenerator.BreakBetweenGames = TimeSpan.FromMinutes(5);
            _phaseGenerator.GameDuration = TimeSpan.FromMinutes(25);
            
            var matchList = _phaseGenerator.GenerateMatches().ToList();
            
            Assert.AreEqual(16, matchList.Count);

            TestMatchDate(matchList[0], 
                    Time(_tournamentDate, 8), Time(_tournamentDate, 8, 25));

            TestMatchDate(matchList[1],
                    Time(_tournamentDate, 8, 30), Time(_tournamentDate, 8, 55));

            TestMatchDate(matchList[7],
                    Time(_tournamentDate, 11, 30), Time(_tournamentDate, 11, 55));

            TestMatchDate(matchList[8],
                    Time(_tournamentDate, 13), Time(_tournamentDate, 13, 25));

            TestMatchDate(matchList[15],
                    Time(_tournamentDate, 16, 30), Time(_tournamentDate, 16, 55));
        }

        private static int GetDayMatchCount(DateTime day, IEnumerable<Match> matchList)
        {
            return matchList.Count(m => m.From.Date.Equals(day));
        }

        private static DateTime Time(DateTime day, int hour, int minute = 0)
        {
            return day.Date.Add(TimeSpan.FromHours(hour)).Add(TimeSpan.FromMinutes(minute));
        }

        private static void TestMatchDate(Match match, DateTime from, DateTime until)
        {
            Assert.AreEqual(from, match.From);
            Assert.AreEqual(until, match.Until);
        }
    }
}
