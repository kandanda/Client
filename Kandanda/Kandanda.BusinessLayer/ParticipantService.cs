using Kandanda.Dal.DataTransferObjects;
using Kandanda.Dal.Repository;

namespace Kandanda.BusinessLayer
{
    public sealed class ParticipantService : ServiceBase<Participant>, IParticipantService
    {
        public ParticipantService(ParticipantRepository repository) : base(repository)
        {
        }

        public Participant CreateEmpty(string name)
        {
            Participant participant = new Participant
            {
                Name = name
            };

            Repository.Save(participant);

            return participant;
        }
    }
}