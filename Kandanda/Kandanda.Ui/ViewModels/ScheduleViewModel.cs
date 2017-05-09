using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Kandanda.BusinessLayer.ServiceInterfaces;
using Kandanda.Dal.Entities;
using Kandanda.Ui.Core;
using Prism.Commands;
using Prism.Regions;

namespace Kandanda.Ui.ViewModels
{
    public class ScheduleViewModel : TournamentViewModelBase, INavigationAware
    {
        private readonly IPhaseService _phaseService;

        private Phase _currentPhase;

        public ICommand SaveCommand { get; set; }

        public ScheduleViewModel(IPhaseService phaseService)
        {
            Title = "Shedules";
            _phaseService = phaseService;

            SaveCommand = new DelegateCommand(async () => await Save());
        }

        public int GameDuration
        {
            get
            {
                return CurrentTournament?.GameDuration.Minutes ?? 0;
            }
            set
            {
                CurrentTournament.GameDuration = TimeSpan.FromMinutes(value);
            }
        }

        public int BreakBetweenGames
        {
            get
            {
                return CurrentTournament?.BreakBetweenGames.Minutes ?? 0;
            }
            set
            {
                CurrentTournament.BreakBetweenGames = TimeSpan.FromMinutes(value);
            }
        }
        
        // TODO: fix ugly workaround with TimeSpan / DateTime conversion
        public DateTime PlayTimeStart
        {
            get { return DateTime.Today + (CurrentTournament?.PlayTimeStart ?? TimeSpan.Zero); }
            set { CurrentTournament.PlayTimeStart = value - value.Date; }
        }

        public DateTime PlayTimeEnd
        {
            get { return DateTime.Today + (CurrentTournament?.PlayTimeEnd ?? TimeSpan.Zero); }
            set { CurrentTournament.PlayTimeEnd = value - value.Date; }
        }

        public DateTime LunchBreakStart
        {
            get { return DateTime.Today + (CurrentTournament?.LunchBreakStart ?? TimeSpan.Zero); }
            set { CurrentTournament.LunchBreakStart = value - value.Date; }
        }

        public DateTime LunchBreakEnd
        {
            get { return DateTime.Today + (CurrentTournament?.LunchBreakEnd ?? TimeSpan.Zero); }
            set { CurrentTournament.LunchBreakEnd = value - value.Date; }
        }

        //TODO CurrentTournament should not be overwriten 
        public override Tournament CurrentTournament
        {
            get { return base.CurrentTournament; }
            set
            {
                base.CurrentTournament = value;

                if (CurrentTournament.Id != 0)
                    SetupOnePhase(CurrentTournament.Id);
            }
        }

        //TODO Refactor for more Phases
        public Phase CurrentPhase
        {
            get { return _currentPhase; }
            set { SetProperty(ref _currentPhase, value); }
        }

        public async void OnNavigatedTo(NavigationContext navigationContext)
        {
            await Save();
        }
        
        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
        }

        private async Task Save()
        {
            await _phaseService.UpdateAsync(CurrentPhase);
        }

        //TODO should be handled by BLL
        private void SetupOnePhase(int tournamentId)
        {
            var phases = (from p in _phaseService.GetAllPhases()
                          where p.TournamentId == tournamentId
                          select p).ToList();

            if (phases.Count == 0)
            {
                var phase = _phaseService.CreateEmpty();
                phase.TournamentId = tournamentId;
                phase.From = DateTime.Today;
                phase.Until = DateTime.Today.AddDays(1);
                _phaseService.Update(phase);
                CurrentPhase = phase;
            }
            else
            {
                CurrentPhase = phases.First();
            }
        }
    }
}
