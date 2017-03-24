using Kandanda.Dal;
using Kandanda.Dal.DataTransferObjects;
using Kandanda.Dal.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Kandanda.BusinessLayer.Testing
{
    [TestClass]
    public class TournamentServiceTest
    {
        [TestMethod]
        public void TestCreateEmptyTournament()
        {
            const string tournamentName = "Schweizer Cup";

            var tournamentService = CreateTournamentService();
            var createdTournament = tournamentService.CreateEmpty(tournamentName);
            var reloadedTournament = tournamentService.GetById(createdTournament.Id);

            Assert.AreEqual(createdTournament.Id, reloadedTournament.Id);
            Assert.AreEqual(tournamentName, reloadedTournament.Name);
        }

        [TestMethod]
        public void TestEnrolParticipantToTournament()
        {
            var tournamentService = CreateTournamentService();
            var participantService = CreateParticipantService();

            var participant = new Participant
            {
                Name = "FC Kandanda"
            };

            var tournament = tournamentService.CreateEmpty("Schweizermeisterschaft");

            tournamentService.EnrolParticipant(tournament, participant);

            // TODO: Assert.AreEquals
        }

        private TournamentService CreateTournamentService()
        {
            var dbContextFactory = new KandandaDatabaseContextFactory();
            var tournamentRepository = new TournamentRepository(dbContextFactory);
            var tournamentService = new TournamentService(tournamentRepository);

            return tournamentService;
        }

        private ParticipantService CreateParticipantService()
        {
            var dbContextFactory = new KandandaDatabaseContextFactory();
            var repository = new ParticipantRepository(dbContextFactory);
            var service = new ParticipantService(repository);

            return service;
        }
    }
}