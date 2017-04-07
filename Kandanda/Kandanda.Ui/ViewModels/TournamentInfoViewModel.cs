using Kandanda.Ui.Core;

namespace Kandanda.Ui.ViewModels
{
    public class TournamentInfoViewModel : TournamentViewModelBase
    {
        private int _numberOfGroups;
        private int _participantsPerGroup;

        public TournamentInfoViewModel()
        {
            Title = "Information";
        }

        public int NumberOfGroups
        {
            get { return _numberOfGroups; }
            set { SetProperty(ref _numberOfGroups, value); }
        }

        public int ParticipantsPerGroup
        {
            get { return _participantsPerGroup; }
            set { SetProperty(ref _participantsPerGroup, value); }
        }
    }
}
