using Between_Us.Model;
using Between_Us.ViewModel.User;
using System.Windows;
using System.Windows.Controls;

namespace Between_Us.View.User
{
    /// <summary>
    /// Interaction logic for UserView.xaml
    /// </summary>
    public partial class UserView : Window
    {
        public UserView(tblUser user)
        {
            InitializeComponent();
            this.DataContext = new UserViewModel(this, user);
        }
        private void DataGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            //hiding id columns
            if (e.Column.Header.ToString() == "PostID"
                 || e.Column.Header.ToString() == "UserID"
                  || e.Column.Header.ToString() == "tblUser"
                   || e.Column.Header.ToString() == "tblPostLikers")
            {
                e.Column.Visibility = Visibility.Collapsed;
            }
        }
    }
}
