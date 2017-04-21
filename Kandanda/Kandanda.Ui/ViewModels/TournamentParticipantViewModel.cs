using System;
using System.Windows.Input;
using Kandanda.BusinessLayer.ServiceInterfaces;
using Kandanda.Ui.Core;
using Prism.Commands;
using System.Collections.ObjectModel;
using Kandanda.Dal.Entities;
using System.Threading.Tasks;

namespace Kandanda.Ui.ViewModels
{
    public class TournamentParticipantViewModel: TournamentViewModelBase
    {
        private readonly ITournamentService _tournamentService;
        private readonly IParticipantService _participantService;
        private Participant _selectedTeam;

        public ObservableCollection<Participant> Participants { get; }
        public ObservableCollection<Participant> AvailableTeams { get; }

        public ICommand EnrollParticipantCommand { get; set; }
        public ICommand DeregisterParticipantCommand { get; set; }


        public Participant ParticipantToAdd
        {
            get { return _selectedTeam; }
            set { SetProperty(ref _selectedTeam, value); }
        }

        public Participant ParticipantToRemove
        {
            get { return _selectedTeam; }
            set { SetProperty(ref _selectedTeam, value); }
        }

        public TournamentParticipantViewModel(ITournamentService tournamentService, IParticipantService participantService)
        {
            _tournamentService = tournamentService;
            _participantService = participantService;
            Title = "Participants";

            AvailableTeams = new ObservableCollection<Participant>();
            Participants = new ObservableCollection<Participant>();

            EnrollParticipantCommand = new DelegateCommand(EnrollParticipant,
                CanEnrollParticipant).ObservesProperty(() => ParticipantToAdd);
            DeregisterParticipantCommand = new DelegateCommand(DeregisterParticipant,
                CanDeregisterParticipant).ObservesProperty(() => ParticipantToRemove);
        }

        private async void UpdateViewsAsync()
        {
            AvailableTeams.Clear();
            Participants.Clear();

            AvailableTeams.AddRange(await _tournamentService.GetNotEnrolledParticipantsByTournamentAsync(CurrentTournament));
            Participants.AddRange(await _tournamentService.GetParticipantsByTournamentAsync(CurrentTournament));
        }

        //TODO CurrentTournament should not be overwriten 
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
            if (ParticipantToAdd != null)
            {
                _tournamentService.EnrolParticipant(CurrentTournament, ParticipantToAdd);
                UpdateViewsAsync();
            }
        }

        private bool CanEnrollParticipant()
        {
            return ParticipantToAdd != null;
        } 

        private void DeregisterParticipant()
        {
            if (ParticipantToRemove != null)
            {
                _tournamentService.DeregisterParticipant(CurrentTournament, ParticipantToRemove);
                UpdateViewsAsync();
            }
        }

        private bool CanDeregisterParticipant()
        {
            return ParticipantToRemove != null;
        }
    }
}
