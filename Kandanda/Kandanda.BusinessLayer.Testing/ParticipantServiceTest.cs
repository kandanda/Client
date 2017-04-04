using Effort;
using Kandanda.BusinessLayer.ServiceImplementations;
using Kandanda.BusinessLayer.ServiceInterfaces;
using Kandanda.Dal;
using Kandanda.Dal.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Kandanda.BusinessLayer.Testing
{
    [TestClass]
    public class ParticipantServiceTest
    {
        private const string ParticipantName = "FC Thun";
        private Participant _initialParticipant;
        private IParticipantService _participantService;

        [TestInitialize]
        public void Setup()
        {
            var context = new KandandaDbContext(DbConnectionFactory.CreateTransient());
            _participantService = new ParticipantService(context);
            _initialParticipant = _participantService.CreateEmpty(ParticipantName);
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
