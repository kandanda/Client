using Kandanda.BusinessLayer.ServiceImplementations;
using Kandanda.BusinessLayer.ServiceInterfaces;
using Kandanda.Dal.DataTransferObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Kandanda.BusinessLayer.Testing
{
    [TestClass]
    public class ParticipantServiceTest
    {
        private const string _participantName = "FC Thun";
        private Participant _initialParticipant;
        private IParticipantService _service;
        private ServiceFactory _serviceFactory;

        [TestInitialize]
        public void Setup()
        {
            TestHelper.ResetDatabase();
            _serviceFactory = new ServiceFactory();
            _service = _serviceFactory.CreateParticipantService();
            _initialParticipant = _service.CreateEmpty(_participantName);
        }

        [TestMethod]
        public void CreateEmptyParticipant()
        {
            const string newParticipantName = "FC Kandanda";

            var createdParticipant = _service.CreateEmpty(newParticipantName);
            var reloadedParticipant = _service.GetParticipantById(createdParticipant.Id);

            Assert.AreEqual(createdParticipant.Id, reloadedParticipant.Id);
            Assert.AreEqual(newParticipantName, reloadedParticipant.Name);
        }

        [TestMethod]
        public void DeleteParticipantTest()
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
    }
}
