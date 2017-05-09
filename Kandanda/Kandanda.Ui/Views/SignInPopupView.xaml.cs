using System.Windows;
using System.Windows.Controls;

namespace Kandanda.Ui.Views
{
    /// <summary>
    /// Interaction logic for SignInPopupView
    /// </summary>
    public partial class SignInPopupView
    {
        private PasswordBox _passwordBox;

        public SignInPopupView()
        {
            InitializeComponent();
        }

        private void Password_OnPasswordChanged(object sender, RoutedEventArgs e)
        {
            _passwordBox = sender as PasswordBox;
            var viewModel = DataContext as dynamic;
            if (viewModel != null && _passwordBox != null)
            {
                viewModel?.PasswordChanged(_passwordBox?.Password);
            }
        }
    }
}
