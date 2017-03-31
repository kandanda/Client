using System.Threading.Tasks;
using System.Windows.Input;
using Kandanda.Ui.Core;
using Kandanda.Ui.Events;
using Prism.Commands;
using Prism.Events;

namespace Kandanda.Ui.ViewModels
{
    public class ParticipantViewModel: ViewModelBase
    {
        public ParticipantViewModel()
        {
            Title = "Participants";
        }
    }
}
