using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Prism.Commands;
using System.Windows.Input;
using Kandanda.Ui.Core;
using Prism.Regions;

namespace Kandanda.Ui.ViewModels
{
    public class TournamentInfoViewModel : ViewModelBase, INotifyDataErrorInfo
    {
        private readonly IRegionManager _regionManager;
        private string _name = "Tournament1";
        private int _numberOfGroups = 4;
        private Dictionary<string, string> _errorDictionary;
        public ICommand ContinueCommand { get; set; }

        public TournamentInfoViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;
            Title = "Information";
            _errorDictionary = new Dictionary<string, string>();
            ContinueCommand = new DelegateCommand(ContinueToNextStep, CanContinue)
                .ObservesProperty(() => Name)
                .ObservesProperty(() => NumberOfGroups);
        }

        private void ContinueToNextStep()
        {
            _regionManager.RequestNavigate(RegionNames.TournamentsRegion, "/ParticipantView");
        }

        private bool CanContinue()
        {
            return !string.IsNullOrWhiteSpace(Name) && NumberOfGroups > 1;
        }

        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }

        public int NumberOfGroups
        {
            get { return _numberOfGroups; }
            set { SetProperty(ref _numberOfGroups, value); }
        }

        public IEnumerable GetErrors(string propertyName)
        {
            return _errorDictionary[propertyName];
        }

        public bool HasErrors { get; }
        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;
    }
}
