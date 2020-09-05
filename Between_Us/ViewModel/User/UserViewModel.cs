using Between_Us.Command;
using Between_Us.Model;
using Between_Us.View.User;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;

namespace Between_Us.ViewModel.User
{
    class UserViewModel : ViewModelBase
    {
        #region Fields
        readonly UserView userView;
        private readonly BetweenUsRepository db = new BetweenUsRepository();
        private List<tblPost> posts;
        private tblPost post;
        private int userId; 
        #endregion

        #region Constructor
        internal UserViewModel(UserView userView, tblUser user)
        {
            userId = user.UserID;
            this.userView = userView;
            Post = new tblPost();
            Posts = LoadPosts();
        }
        #endregion

        #region Properies
        private List<tblPost> LoadPosts()
        {
            return db.LoadPosts();
        }
        public tblPost Post
        {
            get
            {
                return post;
            }
            set
            {
                post = value;
                OnPropertyChanged(nameof(Post));
            }
        }
        public List<tblPost> Posts
        {
            get
            {
                return posts;
            }
            set
            {
                posts = value;
                OnPropertyChanged(nameof(Posts));
            }
        }
        #endregion
        #region Commands

        //FindFriends

        private ICommand findFriends;
        public ICommand FindFriends
        {
            get
            {
                if (findFriends == null)
                {
                    findFriends = new RelayCommand(param => FindFriendsExecute(), param => CanFindFriends());
                }
                return findFriends;
            }
        }

        private void FindFriendsExecute()
        {
            try
            {
                FindFriendsView findFriendsView = new FindFriendsView(userId);
                findFriendsView.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private bool CanFindFriends()
        {
            return true;
        }

        //SeeRequests

        private ICommand seeRequests;
        public ICommand SeeRequests
        {
            get
            {
                if (seeRequests == null)
                {
                    seeRequests = new RelayCommand(param => SeeRequestsExecute(), param => CanSeeRequests());
                }
                return seeRequests;
            }
        }

        private void SeeRequestsExecute()
        {
            try
            {
                SeeRequestsView seeRequestsView = new SeeRequestsView(userId);
                seeRequestsView.ShowDialog();
              
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private bool CanSeeRequests()
        {
            return true;
        }

        //logout

        private ICommand logout;
        public ICommand Logout
        {
            get
            {
                if (logout == null)
                {
                    logout = new RelayCommand(param => ExitExecute(), param => CanExitExecute());
                }
                return logout;
            }
        }

        protected bool CanExitExecute()
        {
            return true;
        }

        protected void ExitExecute()
        {
            MainWindow loginWindow = new MainWindow();
            userView.Close();
            loginWindow.Show();
        }
        #endregion
    }
}
