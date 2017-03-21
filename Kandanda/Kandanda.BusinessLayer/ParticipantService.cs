using System.Collections.Generic;
using Kandanda.Dal;
using Kandanda.Dal.DataTransferObjects;
using Kandanda.Dal.Repository;

namespace Kandanda.BusinessLayer
{
    public sealed class ParticipantService : IParticipantService
    {
        private readonly ParticipantRepository _repository;

        public ParticipantService()
        {
            _repository = new ParticipantRepository(new KandandaDatabaseContextFactory());
        }

        public List<Participant> GetAll()
        {
            return _repository.GetAll();
        }

        public void DeleteAll()
        {
            foreach (Participant participant in _repository.GetAll())
            {
                _repository.DeleteEntry(participant);
            }
        }
    }
}