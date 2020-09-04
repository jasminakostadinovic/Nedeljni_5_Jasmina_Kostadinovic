using Between_Us.Command;
using Between_Us.Model;
using Between_Us.View;
using Between_Us.View.Registration;
using Between_Us.View.User;
using System.Windows.Controls;
using System.Windows.Input;

namespace Between_Us.ViewModel.Login
{
    class LoginViewModel : ViewModelBase
    {
        #region Fields
        private string userName;
        readonly MainWindow loginView;
        private readonly BetweenUsRepository db = new BetweenUsRepository();
        #endregion

        #region Constructor
        internal LoginViewModel(MainWindow view)
        {
            this.loginView = view;
        }

        #endregion

        #region Properties
        public string UserName
        {
            get
            {
                return userName;
            }
            set
            {
                userName = value;
                OnPropertyChanged(nameof(UserName));
            }
        }
        #endregion
        //login
        private ICommand submitCommand;
        public ICommand SubmitCommand
        {
            get
            {
                if (submitCommand == null)
                {
                    submitCommand = new RelayCommand(Submit);
                    return submitCommand;
                }
                return submitCommand;
            }
        }

        void Submit(object obj)
        {
            string password = (obj as PasswordBox).Password;
            var validateHealthcareData = new BetweenUsValidation();
            
            if (validateHealthcareData.IsCorrectUser(userName, password))
            {
                int userDataId = db.GetUserDataId(userName);
                if (userDataId != 0)
                {
                    UserView userView = new UserView(db.LoadUser(userDataId));
                    loginView.Close();
                    userView.Show();
                    return;                   
                }
            }
            else
            {
                WarningView warning = new WarningView(loginView);
                warning.Show("User name or password are not correct!");
                UserName = null;
                (obj as PasswordBox).Password = null;
                return;
            }
        }

        //registrate
        private ICommand registerCommand;
        public ICommand RegisterCommand
        {
            get
            {
                if (registerCommand == null)
                {
                    registerCommand = new RelayCommand(Register);
                    return registerCommand;
                }
                return registerCommand;
            }
        }

        private void Register(object obj)
        {
            RegistrationView registrateView = new RegistrationView();
            loginView.Close();
            registrateView.Show();
            return;
        }
    }
}
