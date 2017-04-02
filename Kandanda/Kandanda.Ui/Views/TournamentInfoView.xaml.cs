using System.Windows.Controls;
using Kandanda.Dal.DataTransferObjects;
using Kandanda.Ui.Core;
using Prism.Regions;

namespace Kandanda.Ui.Views
{
    /// <summary>
    /// Interaction logic for TournamentView
    /// </summary>
    public partial class TournamentInfoView : UserControl
    {
        public TournamentInfoView()
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
