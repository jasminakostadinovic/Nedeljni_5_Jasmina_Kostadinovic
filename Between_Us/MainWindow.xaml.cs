using Between_Us.ViewModel.Login;
using System.Windows;

namespace Between_Us
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new LoginViewModel(this);
        }
    }
}
