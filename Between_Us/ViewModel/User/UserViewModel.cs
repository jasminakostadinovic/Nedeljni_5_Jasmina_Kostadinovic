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
        #endregion
    }
}
