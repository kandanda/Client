using System.Collections.Generic;
using Kandanda.Dal.DataTransferObjects;
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
        private ServiceFactory _serviceFactory;

        [TestInitialize]
        public void Setup()
        {
            _serviceFactory = new ServiceFactory();
            TestHelper.ResetDatabase();
            _participant1 = CreateParticipant(ParticipantName1);
            _participant2 = CreateParticipant(ParticipantName2);
            _initialTournament = CreateTournament(TournamentName);
        }
        
        [TestMethod]
        public void TestCreateEmptyTournament()
        {
            const string newTournamentName = "FC Thun";
            
            var createdTournament = CreateTournament(newTournamentName);
            var reloadedTournament = GetTournament(createdTournament.Id);

            Assert.AreEqual(createdTournament.Id, reloadedTournament.Id);
            Assert.AreEqual(newTournamentName, reloadedTournament.Name);
        }
        
        [TestMethod]
        public void TestEnrolParticipantToTournament()
        {
            EnrolParticipant(_initialTournament, _participant1);
            EnrolParticipant(_initialTournament, _participant2);

            var participants = GetParticipants(_initialTournament);

            Assert.AreEqual(2, participants.Count);
            Assert.AreEqual(ParticipantName1, participants[0].Name);
            Assert.AreEqual(ParticipantName2, participants[1].Name);
        }

        [TestMethod]
        public void TestDeregisterParticipantFromTournament()
        {
            EnrolParticipant(_initialTournament, _participant1);
            EnrolParticipant(_initialTournament, _participant2);
            
            DeregisterParticipant(_initialTournament, _participant1);

            var participants = GetParticipants(_initialTournament);

            Assert.AreEqual(1, participants.Count);
            Assert.AreEqual(ParticipantName2, participants[0].Name);
        }

        [TestMethod]
        public void TestDeleteTournament()
        {
            var tournaments = GetAllTournaments();
            var tournamentCount = tournaments.Count;

            var tournament = CreateTournament(string.Empty);

            tournaments = GetAllTournaments();
            Assert.AreEqual(tournamentCount + 1, tournaments.Count);
            
            DeleteTournament(tournament);
            tournaments = GetAllTournaments();

            Assert.AreEqual(tournamentCount, tournaments.Count);
        }

        private void DeleteTournament(Tournament tournament)
        {
            var tournamentService = _serviceFactory.CreateTournamentService();
            tournamentService.DeleteTournament(tournament);
        }

        private Tournament GetTournament(int id)
        {
            var tournamentService = _serviceFactory.CreateTournamentService();
            return tournamentService.GetTournamentById(id);
        }

        private List<Tournament> GetAllTournaments()
        {
            var tournamentService = _serviceFactory.CreateTournamentService();
            return tournamentService.GetAllTournaments();
        }

        private List<Participant> GetParticipants(Tournament tournament)
        {
            var tournamentService = _serviceFactory.CreateTournamentService();
            return tournamentService.GetParticipantsByTournament(tournament);
        }

        private void EnrolParticipant(Tournament tournament, Participant participant)
        {
            var tournamentService = _serviceFactory.CreateTournamentService();
            tournamentService.EnrolParticipant(tournament, participant);
        }

        private void DeregisterParticipant(Tournament tournament, Participant participant)
        {
            var tournamentService = _serviceFactory.CreateTournamentService();
            tournamentService.DeregisterParticipant(tournament, participant);
        }

        private Participant CreateParticipant(string name)
        {
            var participantService = _serviceFactory.CreateParticipantService();
            return participantService.CreateEmpty(name);
        }

        private Tournament CreateTournament(string name)
        {
            var tournamentService = _serviceFactory.CreateTournamentService();
            return tournamentService.CreateEmpty(name);
        }
    }
}