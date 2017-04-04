using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Kandanda.BusinessLayer;
using Kandanda.BusinessLayer.ServiceInterfaces;
using Kandanda.Ui.Core;
using Kandanda.Dal.DataTransferObjects;
using Prism.Commands;

namespace Kandanda.Ui.ViewModels
{
    //TODO: Refactor to ParticipantViewModel
    public class TeamsViewModel : ViewModelBase
    {
        private IParticipantService _participantService;

        public ICommand SaveCommand { get; }

        public ObservableCollection<Participant> Participants { get; } = new ObservableCollection<Participant>();
        public TeamsViewModel(IParticipantService service)
        {
            _participantService = service;
            Title = "Teams";
            PullParticipants();
            SaveCommand = new DelegateCommand(SaveTeams, CanSaveTeams)
                .ObservesProperty(() => Participants);
        }
        private void PullParticipants()
        {
            Participants.Clear();
            foreach (var participant in _participantService.GetAllParticipants())
            {
                Participants.Add(participant);
            }
        }
        private void SaveTeams()
        {
            var refreshNeeded = false;
            foreach (var participant in Participants)
            {
                if (participant.Id != 0)
                {
                    _participantService.Update(participant);
                }
                else
                {
                    _participantService.CreateEmpty(participant.Name, participant.Captain, participant.Phone, participant.Email);
                    refreshNeeded = true;
                }
            }
            if (refreshNeeded)
            {
                PullParticipants();
            }
        }

        private bool CanSaveTeams()
        {
            return true;
        }
    }
}
