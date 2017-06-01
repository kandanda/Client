using Kandanda.BusinessLayer.ServiceInterfaces;
using Kandanda.Ui.Core;
using Prism.Commands;
using System.Collections.ObjectModel;
using Kandanda.Dal.Entities;
using System.Collections;
using System.Linq;

namespace Kandanda.Ui.ViewModels
{
    public class TournamentParticipantViewModel: TournamentViewModelBase
    {
        private readonly ITournamentService _tournamentService;
        private string _searchParticipantsToRemove = "";
        private string _searchParticipantsToAdd = "";

        public ObservableCollection<Participant> Participants { get; }
        public ObservableCollection<Participant> AvailableTeams { get; }
        
        public DelegateCommand EnrollParticipantCommand { get; set; }
        public DelegateCommand DeregisterParticipantCommand { get; set; }
        public DelegateCommand<IList> ParticipantsToRemoveCommand { get; set; }
        public DelegateCommand<IList> ParticipantsToAddCommand { get; set; }

        public ObservableCollection<Participant> ParticipantListToRemove { get; }
        public ObservableCollection<Participant> ParticipantListToAdd { get; }

        public string SearchParticipantsToRemove
        {
            get { return _searchParticipantsToRemove; }
            set
            {
                SetProperty(ref _searchParticipantsToRemove, value); 
                UpdateViewsAsync();
            }
        }

        public string SearchParticipantsToAdd
        {
            get { return _searchParticipantsToAdd; }
            set
            {
                SetProperty(ref _searchParticipantsToAdd, value);
                UpdateViewsAsync();
            }
        }

        public TournamentParticipantViewModel(ITournamentService tournamentService)
        {
            _tournamentService = tournamentService;
            ParticipantListToRemove = new ObservableCollection<Participant>();
            ParticipantListToAdd = new ObservableCollection<Participant>();
            ParticipantListToRemove.CollectionChanged += ParticipantListToRemove_CollectionChanged;
            ParticipantListToAdd.CollectionChanged += ParticipantsToAdd_CollectionChanged;

            Title = "Participants";

            AvailableTeams = new ObservableCollection<Participant>();
            Participants = new ObservableCollection<Participant>();
            
            ParticipantsToRemoveCommand = new DelegateCommand<IList>(selected => ReplaceParticipantList(selected, ParticipantListToRemove));
            ParticipantsToAddCommand = new DelegateCommand<IList>(selected => ReplaceParticipantList(selected, ParticipantListToAdd));
            EnrollParticipantCommand = new DelegateCommand(EnrollParticipant, CanEnrollParticipant);
            DeregisterParticipantCommand = new DelegateCommand(DeregisterParticipant, CanDeregisterParticipant);
        }

        private void ParticipantsToAdd_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            EnrollParticipantCommand.RaiseCanExecuteChanged();
        }

        private void ParticipantListToRemove_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            DeregisterParticipantCommand.RaiseCanExecuteChanged();
        }

        private void ReplaceParticipantList(IList selected, ObservableCollection<Participant> list) 
        {
            list.Clear();
            foreach (var item in selected)
            {
                list.Add(item as Participant);
            }
        }

        private async void UpdateViewsAsync()
        {
            AvailableTeams.Clear();
            Participants.Clear();

            var enrolledParticipants = await _tournamentService.GetNotEnrolledParticipantsByTournamentAsync(CurrentTournament);
            AvailableTeams.AddRange(enrolledParticipants.Where(search => search.Name.ToLower().Contains(SearchParticipantsToAdd.ToLower())));
            var tournaments = await _tournamentService.GetParticipantsByTournamentAsync(CurrentTournament);
            Participants.AddRange(tournaments.Where(search => search.Name.ToLower().Contains(SearchParticipantsToRemove.ToLower())));
        }
        public override Tournament CurrentTournament
        {
            get { return base.CurrentTournament; }
            set
            {
                base.CurrentTournament = value;
                if (CurrentTournament.Id != 0)
                    UpdateViewsAsync();
            }
        }

        private void EnrollParticipant()
        {
            if (ParticipantListToAdd != null)
            {
                _tournamentService.EnrolParticipant(CurrentTournament, ParticipantListToAdd);
                UpdateViewsAsync();
            }
        }

        private bool CanEnrollParticipant()
        {
            return ParticipantListToAdd.Count > 0;
        } 

        private void DeregisterParticipant()
        {
            if (ParticipantListToRemove != null)
            {
                _tournamentService.DeregisterParticipant(CurrentTournament, ParticipantListToRemove);
                UpdateViewsAsync();
            }
        }

        private bool CanDeregisterParticipant()
        {
            return ParticipantListToRemove.Count > 0;
        }
    }
}
