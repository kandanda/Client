using Kandanda.Dal.Entities;
using Kandanda.Ui.Core;
using Prism.Regions;

namespace Kandanda.Ui.Views
{
    /// <summary>
    /// Interaction logic for ParticipantView
    /// </summary>
    public partial class TournamentParticipantView
    {
        public TournamentParticipantView()
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
