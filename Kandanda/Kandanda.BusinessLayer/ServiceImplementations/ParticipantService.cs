using System.Collections.Generic;
using Kandanda.BusinessLayer.ServiceInterfaces;
using Kandanda.Dal;
using Kandanda.Dal.DataTransferObjects;

namespace Kandanda.BusinessLayer.ServiceImplementations
{
    public sealed class ParticipantService : ServiceBase, IParticipantService
    {
        public ParticipantService(KandandaDbContext dbContext) : base(dbContext)
        {
        }

        public Participant CreateEmpty(string name)
        {
            return Create(new Participant
            {
                Name = name
            });
        }

        public Participant GetParticipantById(int id)
        {
            return GetEntryById<Participant>(id);
        }

        public List<Participant> GetAllParticipants()
        {
            return GetAll<Participant>();
        }

        public void DeleteParticipant(Participant participant)
        {
            Delete(participant);
        }
    }
}