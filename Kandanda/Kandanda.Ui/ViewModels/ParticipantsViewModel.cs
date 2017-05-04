using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Kandanda.BusinessLayer.ServiceInterfaces;
using Kandanda.Dal.Entities;
using Kandanda.Ui.Core;
using Prism.Commands;

namespace Kandanda.Ui.ViewModels
{
    public class ParticipantsViewModel : ViewModelBase
    {
        private readonly IParticipantService _participantService;

        private string _searchParticipants = "";
        public string SearchParticipants
        {
            get { return _searchParticipants; }
            set
            {
                SetProperty(ref _searchParticipants, value);
                UpdateViews();
            }
        }

        private void UpdateViews()
        {
            Participants.Clear();

            var teams = _participantService.GetAllParticipants();
            Participants.AddRange(teams.Where(search => search.Name.ToLower().Contains(SearchParticipants.ToLower())));
        }


        public ObservableCollection<Participant> Participants
        {
            get { return _participants; }
            set
            {
                _participants = value;
                SaveAllParticipants();
            }
        }
        private ObservableCollection<Participant> _participants = new ObservableCollection<Participant>();
        
        public ICommand SaveParticipantCommand { get; set; }
        public ICommand SaveAllCommand { get; set; }
        public ParticipantsViewModel(IParticipantService service)
        {
            _participantService = service;
            Title = "Participants";
            PullParticipants();
            SaveAllCommand = new DelegateCommand(SaveAllParticipants, CanSaveParticipants);
            AutomationId = AutomationIds.MainViewParticipantsTab;
        }

        private void PullParticipants()
        {
            Participants.Clear();
            foreach (var participant in _participantService.GetAllParticipants())
                Participants.Add(participant);
        }
        private void SaveAllParticipants()
        {
            foreach (var participant in Participants)
                _participantService.Save(participant);
        }

        private bool CanSaveParticipants()
        {
            return Participants.Count > 0;
        }
    }
}
