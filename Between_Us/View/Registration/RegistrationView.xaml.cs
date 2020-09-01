using Between_Us.ViewModel.Registration;
using System.Windows;

namespace Between_Us.View.Registration
{
    /// <summary>
    /// Interaction logic for RegistrationView.xaml
    /// </summary>
    public partial class RegistrationView : Window
    {
        public RegistrationView()
        {
            InitializeComponent();
            this.DataContext = new RegistrationViewModel(this);
        }
    }
}
