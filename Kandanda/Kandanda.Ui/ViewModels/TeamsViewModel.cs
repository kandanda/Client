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
        public TeamsViewModel(ServiceFactory service)
        {
            _participantService = service.CreateParticipantService();
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
            foreach (Participant participant in Participants)
            {
                _participantService.Update(participant);
            }
        }

        private bool CanSaveTeams()
        {
            return true;
        }
    }
}
