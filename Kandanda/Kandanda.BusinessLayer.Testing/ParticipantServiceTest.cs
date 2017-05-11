using Kandanda.BusinessLayer.ServiceImplementations;
using Kandanda.Dal;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Kandanda.BusinessLayer.Testing
{
    [TestClass]
    public class ParticipantServiceTest
    {
        private const string ParticipantName = "FC Thun";
        private ParticipantService _participantService;
        private KandandaDbContextLocator _contextLocator;
        private KandandaDbContext Context => _contextLocator.Current;

        [TestInitialize]
        public void Setup()
        {
            
            _contextLocator = new KandandaDbContextLocator();
            _contextLocator.SetTestEnvironment();

            _participantService = new ParticipantService(_contextLocator);

            _participantService.CreateEmpty(ParticipantName);
        }

        [TestCleanup]
        public void CleanUp()
        {
            Context.Dispose();
        }

        [TestMethod]
        public void TestCreateEmptyParticipant()
        {
            const string newParticipantName = "FC Kandanda";

            var createdParticipant = _participantService.CreateEmpty(newParticipantName);
            var reloadedParticipant = _participantService.GetParticipantById(createdParticipant.Id);

            Assert.AreEqual(createdParticipant.Id, reloadedParticipant.Id);
            Assert.AreEqual(newParticipantName, reloadedParticipant.Name);
        }

        [TestMethod]
        public void TestGetParticipantById()
        {
            const string newParticipantName = "FC Kandanda";

            var createdParticipant = _participantService.CreateEmpty(newParticipantName);
            var reloadedParticipant = _participantService.GetParticipantById(createdParticipant.Id);

            Assert.AreEqual(newParticipantName, reloadedParticipant.Name);
        }

        [TestMethod]
        public void TestDeleteParticipant()
        {
            var participants = _participantService.GetAllParticipants();
            var participantCount = participants.Count;

            var participant = _participantService.CreateEmpty(string.Empty);

            participants = _participantService.GetAllParticipants();
            Assert.AreEqual(participantCount + 1, participants.Count);

            _participantService.DeleteParticipant(participant);

            participants = _participantService.GetAllParticipants();
            Assert.AreEqual(participantCount, participants.Count);
        }

        [TestMethod]
        public void TestGetAllParticipants()
        {
            var participants = _participantService.GetAllParticipants();
            
            Assert.AreEqual(1, participants.Count);

            _participantService.CreateEmpty("Empty");
            participants = _participantService.GetAllParticipants();

            Assert.AreEqual(2, participants.Count);
        }
    }
}
