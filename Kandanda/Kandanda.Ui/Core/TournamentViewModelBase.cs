using Kandanda.Dal.Entities;

namespace Kandanda.Ui.Core
{
    public class TournamentViewModelBase: ViewModelBase
    {
        private Tournament _currentTournament;

        public virtual Tournament CurrentTournament
        {
            get { return _currentTournament; }
            set { SetProperty(ref _currentTournament, value); }
        }
    }
}
