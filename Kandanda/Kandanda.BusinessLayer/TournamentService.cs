using Kandanda.Dal.DataTransferObjects;
using Kandanda.Dal.Repository;

namespace Kandanda.BusinessLayer
{
    public sealed class TournamentService : ServiceBase<Tournament>, ITournamentService
    {
        public TournamentService(TournamentRepository repository) : base(repository)
        {
        }

        public Tournament CreateEmpty(string name)
        {
            Tournament tournament = new Tournament
            {
                Name = name
            };

            Repository.Save(tournament);

            return tournament;
        }

        public void EnrolParticipant(Tournament tournament, Participant participant)
        {
            tournament.Participants.Add(participant);
            Repository.Save(tournament);
        }

        public void DeregisterParticipant(Tournament tournament, Participant participant)
        {
            tournament.Participants.Remove(participant);
            Repository.Save(tournament);
        }
    }
}