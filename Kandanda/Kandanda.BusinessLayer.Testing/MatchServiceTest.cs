using System;
using Effort;
using Kandanda.BusinessLayer.ServiceImplementations;
using Kandanda.BusinessLayer.ServiceInterfaces;
using Kandanda.Dal;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Kandanda.BusinessLayer.Testing
{
    [TestClass]
    public sealed class MatchServiceTest
    {
        private IMatchService _matchService;
        private IParticipantService _participantService;
        
        [TestInitialize]
        public void Setup()
        {
            var dbContext = new KandandaDbContext(DbConnectionFactory.CreateTransient());
            _matchService = new MatchService(dbContext);
            _participantService = new ParticipantService(dbContext);
        }

        [TestMethod]
        public void TestCreateMatch()
        {
            var participant1 = _participantService.CreateEmpty("Bern");
            var participant2 = _participantService.CreateEmpty("Basel");

            var match = _matchService.CreateMatch(participant1, participant2);
            var reloadedMatch = _matchService.GetMatchById(match.Id);

            Assert.AreEqual(match.Id, reloadedMatch.Id);
            Assert.AreEqual(match.FirstParticipantId, reloadedMatch.FirstParticipantId);
            Assert.AreEqual(match.SecondParticipantId, reloadedMatch.SecondParticipantId);
        }

        [TestMethod]
        public void TestUpdateMatch()
        {
            var participant1 = _participantService.CreateEmpty("Bern");
            var participant2 = _participantService.CreateEmpty("Basel");

            var match = _matchService.CreateMatch(participant1, participant2);
            match.From = new DateTime(2012, 1, 1);
            match.Until = new DateTime(2013, 1, 1);

            _matchService.Update(match);

            var reloadedMatch = _matchService.GetMatchById(match.Id);

            Assert.AreEqual(match.From, reloadedMatch.From);
            Assert.AreEqual(match.Until, reloadedMatch.Until);
        }
    }
}
