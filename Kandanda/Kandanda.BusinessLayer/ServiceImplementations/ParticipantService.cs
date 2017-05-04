using System.Collections.Generic;
using Kandanda.BusinessLayer.ServiceInterfaces;
using Kandanda.Dal;
using Kandanda.Dal.Entities;

namespace Kandanda.BusinessLayer.ServiceImplementations
{
    public sealed class ParticipantService : ServiceBase, IParticipantService
    {
        public ParticipantService(KandandaDbContext dbContext) : base(dbContext)
        {
        }

        public Participant CreateEmpty(string name, string captain, string phone, string email)
        {
            return Create(new Participant
            {
                Name = name,
                Captain = captain,
                Phone = phone,
                Email = email
            });
        }

        public Participant CreateEmpty(string name)
        {
            return CreateEmpty(name, null, null, null);
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

        public void Update(Participant participant)
        {
            Update<Participant>(participant);
        }

        public Participant Save(Participant participant)
        {
            if (participant.Id != 0)
            {
                Update(participant);
                return participant;
            }
            return Create(participant);
        }
    }
}