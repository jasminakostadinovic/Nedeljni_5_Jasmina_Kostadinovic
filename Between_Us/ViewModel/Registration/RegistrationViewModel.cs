using Between_Us.Command;
using Between_Us.Model;
using Between_Us.View.Registration;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace Between_Us.ViewModel.Registration
{
    class RegistrationViewModel : ViewModelBase, IDataErrorInfo
    {
        #region Fields
        private readonly RegistrationView registrationView;
        private tblUser user;
        private readonly BetweenUsRepository db = new BetweenUsRepository();
        #endregion
        #region Properties    
    
        public bool IsAddedNewUser { get; internal set; }

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

        public bool CanSave { get; private set; }
        #endregion
        #region Constructors
        public RegistrationViewModel(RegistrationView registrationView)
        {
            this.registrationView = registrationView;
            user = new tblUser();
            user.GivenName = string.Empty;
            user.Surname = string.Empty;
            user.Sex = string.Empty;
            user.Username = string.Empty;
            user.Password = string.Empty;
            user.Email = string.Empty;
        }
        #endregion

        #region IDataErrorInfoImplementation
        //validations

        public string Error
        {
            get
            {
                return null;
            }
        }

        public string this[string name]
        {
            get
            {
                string validationMessage = string.Empty;
                var betweenUsValidations = new BetweenUsValidation();
                if (name == nameof(User.Username))
                {
                    if (!betweenUsValidations.IsUniqueUsername(User.Username))
                    {
                        validationMessage = "Username number must be unique!";
                        CanSave = false;
                    }
                }
                if (string.IsNullOrEmpty(validationMessage))
                    CanSave = true;

                return validationMessage;
            }
        }
        #endregion

        #region Commands
        private ICommand save;
        public ICommand Save
        {
            get
            {
                if (save == null)
                {
                    save = new RelayCommand(param => SaveExecute(), param => CanSaveExecute());
                }
                return save;
            }
        }

        private bool CanSaveExecute()
        {
            if (
                string.IsNullOrWhiteSpace(user.GivenName)
                || string.IsNullOrWhiteSpace(user.Surname)
                || string.IsNullOrWhiteSpace(user.Sex)
                || string.IsNullOrWhiteSpace(user.Username)
                || string.IsNullOrWhiteSpace(user.Password)
                || CanSave == false)
                return false;
            return true;
        }
        private void SaveExecute()
        {
            try
            {
                //adding new user to database 
                IsAddedNewUser = db.TryAddNewUser(user);

                    if (IsAddedNewUser == false)
                    {
                        MessageBox.Show("Something went wrong. New user is not created.");
                    }
                    else
                    {
                        MessageBox.Show("The new user is sucessfully created.");
                    }
                    MainWindow loginWindow = new MainWindow();
                    loginWindow.Show();
                    registrationView.Close();
                    return;                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        //Escaping action
        private ICommand exit;
        public ICommand Exit
        {
            get
            {
                if (exit == null)
                {
                    exit = new RelayCommand(param => ExitExecute(), param => CanExitExecute());
                }
                return exit;
            }
        }

      

        private bool CanExitExecute()
        {
            return true;
        }

        private void ExitExecute()
        {
            MainWindow loginWindow = new MainWindow();
            loginWindow.Show();
            IsAddedNewUser = false;
            registrationView.Close();
        }
        #endregion
    }
}
