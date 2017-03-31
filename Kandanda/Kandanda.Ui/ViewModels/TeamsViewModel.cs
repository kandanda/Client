using System.Collections.ObjectModel;
using System.Linq;
using Kandanda.BusinessLayer;
using Kandanda.BusinessLayer.ServiceInterfaces;
using Kandanda.Ui.Core;
using Kandanda.Dal.DataTransferObjects;

namespace Kandanda.Ui.ViewModels
{
    public class TeamsViewModel : ViewModelBase
    {
        private IParticipantService _participantService;
        public ObservableCollection<Participant> Participants { get; } = new ObservableCollection<Participant>();
        public TeamsViewModel(ServiceFactory service)
        {
            _participantService = service.CreateParticipantService();
            Title = "Teams";
            PullParticipants();
        }
        private void PullParticipants()
        {
            Participants.Clear();
            Participants.Add(new Participant()
            {
                Captain = "Mike",
                Email = "mikes@mail.com",
                Id = 1,
                Name = "Supersoccers",
                Phone = "0799618760",
                RowVersion = null
            });
            /*
            foreach (var participant in _participantService.GetAllParticipants())
            {
                Participants.Add(participant);
            }
            */
        }
    }
}
