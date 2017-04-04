using System.Windows.Input;
using Kandanda.BusinessLayer.ServiceInterfaces;
using Kandanda.Dal.DataTransferObjects;
using Kandanda.Ui.Core;
using Prism.Commands;
using Prism.Events;
using System.Windows;

namespace Kandanda.Ui.ViewModels
{
    public class ParticipantViewModel: TournamentViewModelBase
    {
        private readonly ITournamentService _tournamentService;
        private readonly IParticipantService _participantService;
        private Participant _selectedTeam;

        public ICommand EnrollParticipantCommand { get; set; }
        public ICommand DeregisterParticipantCommand { get; set; }


        public Participant SelectedTeam
        {
            get { return _selectedTeam; }
            set { SetProperty(ref _selectedTeam, value); }
        }

        public Participant SelectedParticipant
        {
            get { return _selectedTeam; }
            set { SetProperty(ref _selectedTeam, value); }
        }

        public ParticipantViewModel(ITournamentService tournamentService, IParticipantService participantService)
        {
            _tournamentService = tournamentService;
            _participantService = participantService;
            Title = "Participants";
            EnrollParticipantCommand = new DelegateCommand(EnrollParticipant, CanEnrollParticipant)
                .ObservesProperty(() => SelectedTeam);
            DeregisterParticipantCommand = new DelegateCommand(DeregisterParticipant, CanDeregisterParticipant)
                .ObservesProperty(() => SelectedParticipant);
        }

        private void EnrollParticipant()
        {
            if (SelectedTeam != null)
                _tournamentService.EnrolParticipant(CurrentTournament, SelectedTeam);
        }

        private bool CanEnrollParticipant()
        {
            return SelectedTeam != null;
        } 

        private void DeregisterParticipant()
        {
            if (SelectedParticipant != null)
                _tournamentService.DeregisterParticipant(CurrentTournament, SelectedParticipant);
        }

        private bool CanDeregisterParticipant()
        {
            return SelectedParticipant != null;
        }
    }
}
