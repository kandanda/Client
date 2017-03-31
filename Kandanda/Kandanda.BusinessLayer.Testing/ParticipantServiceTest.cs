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
        private IParticipantService _participantService;
        private ServiceFactory _serviceFactory;

        [TestInitialize]
        public void Setup()
        {
            TestHelper.ResetDatabase();
            _serviceFactory = new ServiceFactory();
            _participantService = _serviceFactory.CreateParticipantService();
            _initialParticipant = _participantService.CreateEmpty(_participantName);
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
        public void TestUpdateParticipant()
        {
            var participant = _participantService.CreateEmpty("Test");
            participant.Name = "Fisch";

            _participantService.Update(participant);

            var reloadedParticipant = _participantService.GetParticipantById(participant.Id);

            Assert.AreEqual(participant.Name, reloadedParticipant.Name);
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
    }
}
