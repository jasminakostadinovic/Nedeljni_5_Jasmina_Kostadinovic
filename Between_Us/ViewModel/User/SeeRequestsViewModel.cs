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
    class SeeRequestsViewModel : ViewModelBase
    {
        #region Fields
        readonly SeeRequestsView seeRequestsView;
        private readonly BetweenUsRepository db = new BetweenUsRepository();
        private List<tblFriendRequest> requests;
        private tblFriendRequest request;
        private int userId;
        #endregion

        #region Constructor
        internal SeeRequestsViewModel(SeeRequestsView seeRequestsView, int userId)
        {
            this.userId = userId;
            this.seeRequestsView = seeRequestsView;
            Request = new tblFriendRequest();
            Requests = LoadRequests();
        }


        #endregion

        #region Methods
        private List<tblFriendRequest> LoadRequests()
        {
            return db.LoadRequestsForUser(userId);
        }
        #endregion

        #region Properies

        public tblFriendRequest Request
        {
            get
            {
                return request;
            }
            set
            {
                request = value;
                OnPropertyChanged(nameof(Request));
            }
        }
        public List<tblFriendRequest> Requests
        {
            get
            {
                return requests;
            }
            set
            {
                requests = value;
                OnPropertyChanged(nameof(Requests));
            }
        }
        #endregion
        #region Commands

        //Accept request

        private ICommand accept;
        public ICommand Accept
        {
            get
            {
                if (accept == null)
                {
                    accept = new RelayCommand(param => AcceptExecute(), param => CanAccept());
                }
                return accept;
            }
        }

        private void AcceptExecute()
        {
            try
            {
                var friendName = Request.tblUser.Username;
                if (db.AddNewFriend(Request.UserID2, Request.UserID))
                {
                    if (db.RemoveFriendRequest(Request.FriendRequestID))
                    {
                        MessageBox.Show($"You are now friend with the user {friendName}");
                        Requests = LoadRequests();
                    }                 
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private bool CanAccept()
        {
            if(Request != null)
                return true;
            return false;
        }

        //Delete request

        private ICommand delete;
        public ICommand Delete
        {
            get
            {
                if (delete == null)
                {
                    delete = new RelayCommand(param => DeleteExecute(), param => CanDelete());
                }
                return delete;
            }
        }

        private void DeleteExecute()
        {
            try
            {
                var friendName = Request.tblUser.Username;

                if (db.RemoveFriendRequest(Request.FriendRequestID))
                {
                    MessageBox.Show($"You have deleted the friend request of the user {friendName}");
                    Requests = LoadRequests();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private bool CanDelete()
        {
            if (Request != null)
                return true;
            return false;
        }
        #endregion
    }
}
