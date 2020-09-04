using Between_Us.ViewModel.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Between_Us.View.User
{
    /// <summary>
    /// Interaction logic for FindFriendsView.xaml
    /// </summary>
    public partial class FindFriendsView : Window
    {
        public FindFriendsView(int userId)
        {
            InitializeComponent();
            this.DataContext = new FindFriendsViewModel(this, userId);
        }
        private void DataGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            //hiding id columns
            if (e.Column.Header.ToString() == "UserID"
                || e.Column.Header.ToString() == "DateOfBirth"
                || e.Column.Header.ToString() == "Password"
                || e.Column.Header.ToString() == "Email"
                || e.Column.Header.ToString() == "tblFriends"
                || e.Column.Header.ToString() == "tblFriends1"
                || e.Column.Header.ToString() == "tblPosts"
                || e.Column.Header.ToString() == "tblPostLikers")
            {
                e.Column.Visibility = Visibility.Collapsed;
            }
        }
    }
}
