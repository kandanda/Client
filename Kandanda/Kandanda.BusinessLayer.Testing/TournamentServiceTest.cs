using System;
using System.Collections.Generic;
using Effort;
using Kandanda.BusinessLayer.ServiceImplementations;
using Kandanda.BusinessLayer.ServiceInterfaces;
using Kandanda.Dal;
using Kandanda.Dal.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Kandanda.BusinessLayer.Testing
{
    [TestClass]
    public class TournamentServiceTest
    {
        private const string ParticipantName1 = "FC St. Gallen";
        private const string ParticipantName2 = "GC Zürich";
        private const string TournamentName = "Schweizer Cup";

        private Participant _participant1;
        private Participant _participant2;
        private Tournament _initialTournament;
        private ITournamentService _tournamentService;
        private IParticipantService _participantService;
        private KandandaDbContext _context;

        [TestInitialize]
        public void Setup()
        {
            _context = new KandandaDbContext(DbConnectionFactory.CreateTransient());
            _tournamentService = new TournamentService(_context);
            _participantService = new ParticipantService(_context);
            
            _participant1 = _participantService.CreateEmpty(ParticipantName1);
            _participant2 = _participantService.CreateEmpty(ParticipantName2);
            _initialTournament = _tournamentService.CreateEmpty(TournamentName);
        }

        [TestCleanup]
        public void CleanUp()
        {
            _context.Dispose();
        }

        [TestMethod]
        public void TestUpdateTournament()
        {
            const string tournamentName = "Young Boys";
            var tournament = _tournamentService.CreateEmpty(tournamentName);

            tournament.Name = "FC Berlin";

            _tournamentService.Update(tournament);

            var reloadedTournament = _tournamentService.GetTournamentById(tournament.Id);

            Assert.AreEqual(tournament.Name, reloadedTournament.Name);
        }

        [TestMethod]
        public void TestUpdateScheduleInformation()
        {
            const string tournamentName = "SwissCup";
            var tournament = _tournamentService.CreateEmpty(tournamentName);

            tournament.Name = "Meister";
            tournament.PlayTimeStart = TimeSpan.FromHours(8);
            tournament.PlayTimeEnd = TimeSpan.FromHours(12);
            tournament.GameDuration = TimeSpan.FromMinutes(12);
            tournament.BreakBetweenGames = TimeSpan.FromMinutes(10);
            tournament.Monday = true;
            tournament.Wednesday = true;
            tournament.Friday = false;
            tournament.SharedLink = "https://test.ch";
            
            _tournamentService.Update(tournament);

            var reloadedTournament = _tournamentService.GetTournamentById(tournament.Id);

            Assert.AreEqual(tournament.Name, reloadedTournament.Name);
            Assert.AreEqual(tournament.PlayTimeStart, reloadedTournament.PlayTimeStart);
            Assert.AreEqual(tournament.BreakBetweenGames, reloadedTournament.BreakBetweenGames);
            Assert.AreEqual(tournament.Monday, reloadedTournament.Monday);
            Assert.AreEqual(tournament.Friday, reloadedTournament.Friday);
            Assert.AreEqual(tournament.SharedLink, reloadedTournament.SharedLink);
        }
        
        [TestMethod]
        public void TestCreateEmptyTournament()
        {
            const string newTournamentName = "FC Thun";
            
            var createdTournament = _tournamentService.CreateEmpty(newTournamentName);
            var reloadedTournament = _tournamentService.GetTournamentById(createdTournament.Id);

            Assert.AreEqual(createdTournament.Id, reloadedTournament.Id);
            Assert.AreEqual(newTournamentName, reloadedTournament.Name);
        }
        
        [TestMethod]
        public void TestEnrolParticipantToTournament()
        {
            _tournamentService.EnrolParticipant(_initialTournament, _participant1);
            _tournamentService.EnrolParticipant(_initialTournament, _participant2);

            var participants = _tournamentService.GetParticipantsByTournament(_initialTournament);

            Assert.AreEqual(2, participants.Count);
            Assert.AreEqual(ParticipantName1, participants[0].Name);
            Assert.AreEqual(ParticipantName2, participants[1].Name);
        }

        [TestMethod]
        public void TestDeregisterParticipantFromTournament()
        {
            _tournamentService.EnrolParticipant(_initialTournament, _participant1);
            _tournamentService.EnrolParticipant(_initialTournament, _participant2);

            _tournamentService.DeregisterParticipant(_initialTournament, _participant1);

            var participants = _tournamentService.GetParticipantsByTournament(_initialTournament);

            Assert.AreEqual(1, participants.Count);
            Assert.AreEqual(ParticipantName2, participants[0].Name);
        }

        [TestMethod]
        public void TestGetAllTournaments()
        {
            var tournaments = _tournamentService.GetAllTournaments();
            Assert.AreEqual(1, tournaments.Count);
            _tournamentService.CreateEmpty("Test");
            tournaments = _tournamentService.GetAllTournaments();
            Assert.AreEqual(2, tournaments.Count);
        }
        
        [TestMethod]
        public void TestDeleteTournament()
        {
            var tournaments = _tournamentService.GetAllTournaments();
            var tournamentCount = tournaments.Count;

            var tournament = _tournamentService.CreateEmpty(string.Empty);

            tournaments = _tournamentService.GetAllTournaments();
            Assert.AreEqual(tournamentCount + 1, tournaments.Count);
            
            _tournamentService.DeleteTournament(tournament);
            tournaments = _tournamentService.GetAllTournaments();

            Assert.AreEqual(tournamentCount, tournaments.Count);
        }
    }
}