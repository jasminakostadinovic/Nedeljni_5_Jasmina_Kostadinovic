using Between_Us.Command;
using Between_Us.Model;
using Between_Us.View.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace Between_Us.ViewModel.User
{
    class FindFriendsViewModel : ViewModelBase
    {
        #region Fields
        readonly FindFriendsView findFriendsView;
        private readonly BetweenUsRepository db = new BetweenUsRepository();
        private List<tblUser> users;
        private tblUser user;
        private int userId;
        #endregion

        #region Constructor
        internal FindFriendsViewModel(FindFriendsView findFriendsView, int userId)
        {
            this.userId = userId;
            this.findFriendsView = findFriendsView;
            User = new tblUser();            
            Users = LoadUsers();
        }
        #endregion

        #region Properies
        private List<tblUser> LoadUsers()
        {
            return db.LoadUsers(userId);
        }
        public tblUser User
        {
            get
            {
                return user;
            }
            set
            {
                user = value;
                OnPropertyChanged(nameof(User));
            }
        }
        public List<tblUser> Users
        {
            get
            {
                return users;
            }
            set
            {
                users = value;
                OnPropertyChanged(nameof(Users));
            }
        }
        #endregion
        #region Commands

        //FindFriends

        private ICommand addUser;
        public ICommand AddUser
        {
            get
            {
                if (addUser == null)
                {
                    addUser = new RelayCommand(param => AddUserExecute(), param => CanAddUser());
                }
                return addUser;
            }
        }

        private void AddUserExecute()
        {
            try
            {
                if(db.SendRequest(userId, User.UserID))
                {
                    MessageBox.Show($"You have sent a new friend request to the user {user.Username}");
                    Users = LoadUsers();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private bool CanAddUser()
        {
            if(User != null)
            {
                if (User.tblFriendRequests.Any(x => x.UserID2 == userId))
                    return false;
                return true;
            }
            return false;
        }
        #endregion
    }
}
