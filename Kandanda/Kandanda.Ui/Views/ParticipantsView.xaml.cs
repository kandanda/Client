using System.Windows.Controls;
using Kandanda.Ui.ViewModels;

namespace Kandanda.Ui.Views
{
    /// <summary>
    /// Interaction logic for TeamsView
    /// </summary>
    public partial class ParticipantsView : UserControl
    {
        public ParticipantsView()
        {
            InitializeComponent();
        }

        private void DataGrid_OnCellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            var viewModel = DataContext as ParticipantsViewModel;
            viewModel?.SaveAllCommand.Execute(null);
        }
    }
}
