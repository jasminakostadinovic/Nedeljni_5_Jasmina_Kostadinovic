using Between_Us.ViewModel.User;
using System.Windows;
using System.Windows.Controls;

namespace Between_Us.View.User
{
    /// <summary>
    /// Interaction logic for SeeRequestsView.xaml
    /// </summary>
    public partial class SeeRequestsView : Window
    {
        public SeeRequestsView(int userId)
        {
            InitializeComponent();
            this.DataContext = new SeeRequestsViewModel(this, userId);
        }
        private void DataGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            //hiding id columns
            if (e.Column.Header.ToString() == "FriendRequestID"
                || e.Column.Header.ToString() == "UserID"
                || e.Column.Header.ToString() == "UserID2"
                || e.Column.Header.ToString() == "tblUser"
                || e.Column.Header.ToString() == "tblUser1")
            {
                e.Column.Visibility = Visibility.Collapsed;
            }
        }
    }
}
