using System.Collections.Generic;
using Kandanda.BusinessLayer.ServiceImplementations;
using Kandanda.Dal;
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

        [TestInitialize]
        public void Setup()
        {
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

        private static void DeleteTournament(Tournament tournament)
        {
            var tournamentService = new TournamentService();
            tournamentService.DeleteTournament(tournament);
        }

        private static Tournament GetTournament(int id)
        {
            var tournamentService = new TournamentService();
            return tournamentService.GetTournamentById(id);
        }

        private static List<Tournament> GetAllTournaments()
        {
            var tournamentService = new TournamentService();
            return tournamentService.GetAllTournaments();
        }

        private static List<Participant> GetParticipants(Tournament tournament)
        {
            var tournamentService = new TournamentService();
            return tournamentService.GetParticipantsByTournament(tournament);
        }

        private static void EnrolParticipant(Tournament tournament, Participant participant)
        {
            var tournamentService = new TournamentService();
            tournamentService.EnrolParticipant(tournament, participant);
        }

        private static void DeregisterParticipant(Tournament tournament, Participant participant)
        {
            var tournamentService = new TournamentService();
            tournamentService.DeregisterParticipant(tournament, participant);
        }

        private static Participant CreateParticipant(string name)
        {
            var participantService = new ParticipantService();
            return participantService.CreateEmpty(name);
        }

        private static Tournament CreateTournament(string name)
        {
            var tournamentService = new TournamentService();
            return tournamentService.CreateEmpty(name);
        }
    }
}