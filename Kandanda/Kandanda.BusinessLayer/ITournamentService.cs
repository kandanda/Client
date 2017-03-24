using Kandanda.Dal.DataTransferObjects;

namespace Kandanda.BusinessLayer
{
    public interface ITournamentService
    {
        Tournament CreateEmpty(string name);
        void EnrolParticipant(Tournament tournament, Participant participant);
        void DeregisterParticipant(Tournament tournament, Participant participant);
    }
}