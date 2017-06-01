using Kandanda.Dal.Entities;
using Kandanda.Ui.Core;
using Prism.Regions;

namespace Kandanda.Ui.Views
{
    /// <summary>
    /// Interaction logic for ControlPanelView
    /// </summary>
    public partial class ActiveTournamentScheduleView
    {
        public ActiveTournamentScheduleView()
        {
            InitializeComponent();
            
            RegionContext.GetObservableContext(this).PropertyChanged += (s, e)
                                                                        =>
            {
                var viewModel = DataContext as TournamentViewModelBase;
                if (viewModel != null)
                    viewModel.CurrentTournament =
                        RegionContext.GetObservableContext(this).Value
                            as Tournament;
            };
        }
    }
}
