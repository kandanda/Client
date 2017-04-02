using Kandanda.Dal.DataTransferObjects;

namespace Kandanda.Ui.Core
{
    public class TournamentViewModelBase: ViewModelBase
    {
        private Tournament _currentTournament;

        public Tournament CurrentTournament
        {
            get { return _currentTournament; }
            set { SetProperty(ref _currentTournament, value); }
        }
    }
}
