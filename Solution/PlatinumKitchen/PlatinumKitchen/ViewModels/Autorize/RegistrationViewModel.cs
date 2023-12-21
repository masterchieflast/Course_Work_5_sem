using PlatinumKitchen.Infasturcture;
using PlatinumKitchen.Models;
using PlatinumKitchen.Models.Database.Entityes;
using PlatinumKitchen.Utilities;
using PlatinumKitchen.Utilities.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace PlatinumKitchen.ViewModels.Autorize
{
    public class RegistrationViewModel : ViewModelBase
    {
        private string _login;
        private string _email;
        private SecureString _password;
        private SecureString _repeatPassword;
        private string _messageError;

        public string Login
        {
            get
            {
                return _login;
            }

            set
            {
                _login = value;
                OnPropertyChanged(nameof(Login));
            }
        }
        public string Email
        {
            get
            {
                return _email;
            }

            set
            {
                _email = value;
                OnPropertyChanged(nameof(Email));
            }
        }

        public SecureString Password
        {
            get
            {
                return _password;
            }

            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }

        public SecureString RepeatPassword
        {
            get
            {
                return _repeatPassword;
            }

            set
            {
                if (UnsecureString.ConvertToUnsecureString(value) != UnsecureString.ConvertToUnsecureString(Password))
                    MessageError = Application.Current.FindResource("PasswordNotEquals").ToString();
                else
                    MessageError = "";
                _repeatPassword = value;
                OnPropertyChanged(nameof(RepeatPassword));
            }
        }

        public string MessageError
        {
            get
            {
                return _messageError;
            }

            set
            {
                _messageError = value;
                OnPropertyChanged(nameof(MessageError));
            }
        }

        private DelegateCommand? loginCommand;

        public ICommand LoginCommand
        {
            get
            {
                if (loginCommand == null)
                {
                    loginCommand = new DelegateCommand(LoginApplication);
                }
                return loginCommand;
            }
        }

        private void LoginApplication()
        {
            try
            {
                bool check_01 = true;
                bool check_02 = true;
                bool check_03 = true;
                bool check_04 = true;

                var pas = UnsecureString.ConvertToUnsecureString(Password);
                MessageError = "";

                if (String.IsNullOrEmpty(Login) || Login.Length < 3)
                {
                    MessageError +=
                    Application.Current.FindResource("LoginError").ToString() + " ";
                    check_01 = false;
                }

                if (String.IsNullOrEmpty(pas)
                    || pas.Length < 8 || !pas.Any(char.IsDigit) || !pas.Any(char.IsUpper))
                {
                    MessageError += check_01 ?
                    Application.Current.FindResource("PasswordError").ToString()
                    : Application.Current.FindResource("And").ToString() + ' ' + Application.Current.FindResource("PasswordError").ToString();
                    check_02 = false;
                }

                if (UnsecureString.ConvertToUnsecureString(RepeatPassword) != UnsecureString.ConvertToUnsecureString(Password))
                {
                    check_03 = false;
                }

                if (!Validator.EmailValid(Email))
                {
                    MessageError = Application.Current.FindResource("EmailError").ToString();
                    check_04 = false;
                }

                if (check_01 && check_02 && check_03 && check_04)
                {

                    if (!(Login.Length >= 9) || !Login[..9].Equals("employees", StringComparison.CurrentCultureIgnoreCase))
                    {
                        List<Customers> users = Controller.DataBase.CustomerRepository.GetAll().ToList();
                        if (!users.Any(user => user.Login == Login))
                        {
                            var user = new Customers
                            {
                                First_Name = this.Login,
                                Last_Name = this.Login,
                                Login = this.Login,
                                Password = UnsecureString.Encode(pas),
                                Email = this.Email,
                                Phone_Number = "80298467707"
                            };
                            Controller.DataBase.CustomerRepository.Create(user);
                            check_01 = false;

                            MessageBox.Show(Application.Current.FindResource("Good").ToString());

                            Controller.SetAuthenticationPage("Login");
                        }
                    }

                    if (check_01)
                    {
                        MessageError = Application.Current.FindResource("UserError").ToString();
                    }
                }
            } catch (Exception) {
            }
        }
        private DelegateCommand<string>? setAuthenticationPage;

        public ICommand SetAuthenticationPage
        {
            get
            {
                if (setAuthenticationPage == null)
                {
                    setAuthenticationPage = new DelegateCommand<string>(SetAuthenticationPageView);
                }
                return setAuthenticationPage;
            }
        }

        private void SetAuthenticationPageView(string LoginPage)
        {
            Controller.SetAuthenticationPage(LoginPage);
        }
    }
}
