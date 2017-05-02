using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Documents;
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
        public ICommand SaveCommand { get; }
        private string AidSaveButton => AutomationIds.ParticipantsSaveButton;
        private string AidDataGrid => AutomationIds.ParticipantsDataGrid;
        public ObservableCollection<Participant> Participants { get; } = new ObservableCollection<Participant>();
        

        public ParticipantsViewModel(IParticipantService service)
        {
            _participantService = service;
            Title = "Participants";
            PullParticipants();
            SaveCommand = new DelegateCommand(SaveParticipants, CanSaveParticipants)
                .ObservesProperty(() => Participants);

            AutomationId = AutomationIds.MainViewParticipantsTab;
        }
        private void PullParticipants()
        {
            Participants.Clear();
            foreach (var participant in _participantService.GetAllParticipants())
            {
                Participants.Add(participant);
            }
        }
        private void SaveParticipants()
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

        private bool CanSaveParticipants()
        {
            return true;
        }
    }
}
