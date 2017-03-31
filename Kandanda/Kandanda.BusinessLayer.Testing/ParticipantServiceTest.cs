using Kandanda.BusinessLayer.ServiceImplementations;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Kandanda.BusinessLayer.Testing
{
    [TestClass]
    public class ParticipantServiceTest
    {
        private const string _participantName = "FC Thun";
        private ParticipantService _service;

        [TestInitialize]
        public void Setup()
        {
            TestHelper.ResetDatabase();
            _service = new ParticipantService();
            _service.CreateEmpty(_participantName);
        }

        [TestMethod]
        public void TestCreateEmptyParticipant()
        {
            const string newParticipantName = "FC Kandanda";

            var createdParticipant = _service.CreateEmpty(newParticipantName);
            var reloadedParticipant = _service.GetParticipantById(createdParticipant.Id);

            Assert.AreEqual(createdParticipant.Id, reloadedParticipant.Id);
            Assert.AreEqual(newParticipantName, reloadedParticipant.Name);
        }

        [TestMethod]
        public void TestGetParticipantById()
        {
            const string newParticipantName = "FC Kandanda";

            var createdParticipant = _service.CreateEmpty(newParticipantName);
            var reloadedParticipant = _service.GetParticipantById(createdParticipant.Id);

            Assert.AreEqual(newParticipantName, reloadedParticipant.Name);
        }

        [TestMethod]
        public void TestDeleteParticipant()
        {
            var participants = _service.GetAllParticipants();
            var participantCount = participants.Count;

            var participant = _service.CreateEmpty(string.Empty);

            participants = _service.GetAllParticipants();
            Assert.AreEqual(participantCount + 1, participants.Count);

            _service.DeleteParticipant(participant);

            participants = _service.GetAllParticipants();
            Assert.AreEqual(participantCount, participants.Count);
        }

        [TestMethod]
        public void TestGetAllParticipants()
        {
            var participants = _service.GetAllParticipants();
            
            Assert.AreEqual(1, participants.Count);

            _service.CreateEmpty("Empty");
            participants = _service.GetAllParticipants();

            Assert.AreEqual(2, participants.Count);
        }
    }
}
