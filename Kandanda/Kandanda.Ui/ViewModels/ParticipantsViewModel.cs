using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Kandanda.BusinessLayer.ServiceInterfaces;
using Kandanda.Dal.Entities;
using Kandanda.Ui.Core;
using Kandanda.Ui.Events;
using Prism.Commands;
using Prism.Events;

namespace Kandanda.Ui.ViewModels
{
    public class ParticipantsViewModel : ViewModelBase
    {
        public ParticipantsViewModel(IParticipantService service, IEventAggregator eventAggregator)
        {
            _participantService = service;
            Title = "Participants";
            SaveAllCommand = new DelegateCommand(SaveAllParticipants, CanSaveParticipants);
            AddNewTeamCommand = new DelegateCommand(AddNewTeam);
            DeleteTeamCommand = new DelegateCommand(DeleteTeam);
            AutomationId = AutomationIds.MainViewParticipantsTab;
            eventAggregator.GetEvent<KandandaDbContextChanged>().Subscribe(RefreshData);
            RefreshData();
        }

        private void DeleteTeam()
        {
            if (SelectedParticipant != null)
            {
                _participantService.DeleteParticipant(SelectedParticipant);
                Participants.Remove(SelectedParticipant);
            }
        }

        public DelegateCommand DeleteTeamCommand { get; set; }

        public Participant SelectedParticipant { get; set; }
        private void AddNewTeam()
        {
            Participants.Add(_participantService.CreateEmpty("<new team>"));
        }

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
        
        public ICommand AddNewTeamCommand { get; set; }
        public ICommand SaveAllCommand { get; set; }
        

        private void RefreshData()
        {
            Participants.Clear();
            foreach (var participant in _participantService.GetAllParticipants())
                Participants.Add(participant);
        }

        private void SaveAllParticipants()
        {
            foreach (var participant in Participants)
                _participantService.Update(participant);
        }

        private bool CanSaveParticipants()
        {
            return Participants.Count > 0;
        }
    }
}
