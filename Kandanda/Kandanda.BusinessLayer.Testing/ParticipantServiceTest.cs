using Kandanda.BusinessLayer.ServiceImplementations;
using Kandanda.Dal.DataTransferObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Kandanda.BusinessLayer.Testing
{
    [TestClass]
    public class ParticipantServiceTest
    {
        private const string participantName = "FC Thun";
        private Participant initialParticipant;
        private ParticipantService service;

        [TestInitialize]
        public void Setup()
        {
            TestHelper.ResetDatabase();
            service = new ParticipantService();
            initialParticipant = service.CreateEmpty(participantName);
        }

        [TestMethod]
        public void TestCreateEmptyParticipant()
        {
            const string newParticipantName = "FC Kandanda";

            var createdParticipant = service.CreateEmpty(newParticipantName);
            var reloadedParticipant = service.GetParticipantById(createdParticipant.Id);

            Assert.AreEqual(createdParticipant.Id, reloadedParticipant.Id);
            Assert.AreEqual(newParticipantName, reloadedParticipant.Name);
        }

        [TestMethod]
        public void TestGetParticipantById()
        {
            const string newParticipantName = "FC Kandanda";

            var createdParticipant = service.CreateEmpty(newParticipantName);
            var reloadedParticipant = service.GetParticipantById(createdParticipant.Id);

            Assert.AreEqual(newParticipantName, reloadedParticipant.Name);
        }

        [TestMethod]
        public void TestDeleteParticipant()
        {
            var participants = service.GetAllParticipants();
            var participantCount = participants.Count;

            var participant = service.CreateEmpty(string.Empty);

            participants = service.GetAllParticipants();
            Assert.AreEqual(participantCount + 1, participants.Count);

            service.DeleteParticipant(participant);

            participants = service.GetAllParticipants();
            Assert.AreEqual(participantCount, participants.Count);
        }

        [TestMethod]
        public void TestGetAllParticipants()
        {
            var participants = service.GetAllParticipants();
            
            Assert.AreEqual(1, participants.Count);

            service.CreateEmpty("Empty");
            participants = service.GetAllParticipants();

            Assert.AreEqual(2, participants.Count);
        }
    }
}
