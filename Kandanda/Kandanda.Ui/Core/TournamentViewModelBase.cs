using Kandanda.Dal.Entities;
using Prism.Commands;

namespace Kandanda.Ui.Core
{
    public abstract class TournamentViewModelBase: ViewModelBase
    {
        private Tournament _currentTournament;
        
        public DelegateCommand DeleteTournamentCommand { get; set; }
        
        public virtual Tournament CurrentTournament
        {
            get { return _currentTournament; }
            set
            {
                SetProperty(ref _currentTournament, value);
                DeleteTournamentCommand?.RaiseCanExecuteChanged();
            }
        }
    }
}
