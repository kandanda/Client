using System;
using System.Windows.Input;
using Kandanda.BusinessLayer.ServiceInterfaces;
using Kandanda.Ui.Core;
using Prism.Commands;
using System.Collections.ObjectModel;
using Kandanda.Dal.Entities;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Collections;

namespace Kandanda.Ui.ViewModels
{
    public class TournamentParticipantViewModel: TournamentViewModelBase
    {
        private readonly ITournamentService _tournamentService;
        private readonly IParticipantService _participantService;
        private Participant _selectedTeam;

        public ObservableCollection<Participant> Participants { get; }
        public ObservableCollection<Participant> AvailableTeams { get; }

        public DelegateCommand EnrollParticipantCommand { get; set; }
        public DelegateCommand DeregisterParticipantCommand { get; set; }
        public DelegateCommand<IList> ParticipantsToRemoveCommand { get; set; }
        public DelegateCommand<IList> ParticipantsToAddCommand { get; set; }

        public ObservableCollection<Participant> ParticipantListToRemove { get; }
        public ObservableCollection<Participant> ParticipantListToAdd { get; }

        public TournamentParticipantViewModel(ITournamentService tournamentService, IParticipantService participantService)
        {
            _tournamentService = tournamentService;
            _participantService = participantService;
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
