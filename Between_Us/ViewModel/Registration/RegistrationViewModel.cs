using Between_Us.Command;
using Between_Us.Model;
using Between_Us.View.Registration;
using DataValidations;
using System;
using System.ComponentModel;
using System.Globalization;
using System.Windows;
using System.Windows.Input;

namespace Between_Us.ViewModel.Registration
{
    class RegistrationViewModel : ViewModelBase, IDataErrorInfo
    {
        #region Fields
        private readonly RegistrationView registrationView;
        private tblUser user;
        private string username;
        private string email;
        private string dateOfBirth;
        private DateTime dateOfBirthValue;
        private string[] sexTypes = new string[] { "M", "F", "X" };
        private readonly BetweenUsRepository db = new BetweenUsRepository();
        #endregion
        #region Properties    
    
        public bool IsAddedNewUser { get; internal set; }
        public string[] SexTypes
        {
            get
            {
                return sexTypes;
            }
            set
            {
                if (sexTypes == value) return;
                sexTypes = value;
                OnPropertyChanged(nameof(SexTypes));
            }
        }
        public string DateOfBirth
        {
            get
            {
                return dateOfBirth;
            }
            set
            {
                dateOfBirth = value;
                OnPropertyChanged(nameof(DateOfBirth));
            }
        }
        public string Email
        {
            get
            {
                return email;
            }
            set
            {
                email = value;
                OnPropertyChanged(nameof(Email));
            }
        }
        public string Username
        {
            get
            {
                return username;
            }
            set
            {
                username = value;
                OnPropertyChanged(nameof(Username));
            }
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
            Username = string.Empty;
            user.Password = string.Empty;
            Email = string.Empty;
            DateOfBirth = string.Empty;
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
                if (name == nameof(Username))
                {
                    if (!betweenUsValidations.IsUniqueUsername(Username))
                    {
                        validationMessage = "Username number must be unique!";
                        CanSave = false;
                    }
                }
                else if (name == nameof(DateOfBirth))
                {
                    var culture = CultureInfo.InvariantCulture;
                    var styles = DateTimeStyles.None;
                    if (!DateTime.TryParse(DateOfBirth, culture, styles, out dateOfBirthValue)
                        || dateOfBirthValue.Year < 1900)
                    {
                        validationMessage = "Invalid date format!";
                        CanSave = false;
                    }
                    else if (Validations.CalculateAge(dateOfBirthValue) < 13)
                    {
                        validationMessage = "User must be at least 13 years old!";
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
                || string.IsNullOrWhiteSpace(Username)
                || string.IsNullOrWhiteSpace(user.Password)
                || string.IsNullOrWhiteSpace(Email)
                || string.IsNullOrWhiteSpace(DateOfBirth)
                || CanSave == false)
                return false;
            return true;
        }
        private void SaveExecute()
        {
            try
            {
                //adding new user to database 
                user.Username = Username;
                user.Email = Email;
                user.DateOfBirth = dateOfBirthValue;
                user.Password =  SecurePasswordHasher.Hash(user.Password);
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
