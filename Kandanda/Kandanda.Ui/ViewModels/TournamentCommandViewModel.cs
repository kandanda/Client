using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Kandanda.Ui.Events;
using Prism.Events;

namespace Kandanda.Ui.ViewModels
{
    public class TournamentCommandViewModel : BindableBase
    {
        private readonly IEventAggregator _eventAggregator;
        public ICommand GeneratePlanCommand;

        public TournamentCommandViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            GeneratePlanCommand = new DelegateCommand(RequestGeneratePlan);
        }

        private void RequestGeneratePlan()
        {
            _eventAggregator.GetEvent<PublishRequestEvent>().Publish();
        }
    }
}
