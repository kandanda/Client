using System.Windows.Controls;
using Kandanda.Ui.ViewModels;

namespace Kandanda.Ui.Views
{
    /// <summary>
    /// Interaction logic for TeamsView
    /// </summary>
    public partial class TeamsView
    {
        public TeamsView()
        {
            InitializeComponent();
        }

        private void DataGrid_OnCellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            var viewModel = DataContext as TeamsViewModel;
            viewModel?.SaveAllCommand.Execute(null);
        }
    }
}
