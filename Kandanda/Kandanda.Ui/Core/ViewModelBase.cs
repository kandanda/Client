using Prism.Mvvm;

namespace Kandanda.Ui.Core
{
    public class ViewModelBase : BindableBase
    {
        private string _title;

        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        private string _automationId;

        public string AutomationId
        {
            get { return _automationId; }
            set { SetProperty(ref _automationId, value); }
        }
    }
}