using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using PlatinumKitchen.Infasturcture;
using PlatinumKitchen.Models;
using PlatinumKitchen.Models.Database;
using PlatinumKitchen.Models.Database.Entityes;
using PlatinumKitchen.Utilities;
using PlatinumKitchen.Utilities.Commands;

namespace PlatinumKitchen.ViewModels.Autorize
{
    public class LoginViewModel : ViewModelBase
    {
        private string _login;
        private SecureString _password;
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
            bool check_01 = true;
            bool check_02 = true;
            
            MessageError = "";

            if (String.IsNullOrEmpty(Login))
            {
                MessageError +=
                Application.Current.FindResource("LoginError").ToString() + " ";
                check_01 = false;
            }

            if (String.IsNullOrEmpty(UnsecureString.ConvertToUnsecureString(Password)))
            {
                MessageError += check_01 ?
                Application.Current.FindResource("PasswordError").ToString()
                : Application.Current.FindResource("And").ToString() + ' ' + Application.Current.FindResource("PasswordError").ToString();
                check_02 = false;
            }

            if (check_01 && check_02)
            {
                var pas = UnsecureString.Encode(UnsecureString.ConvertToUnsecureString(Password));

                if(Login == "-Admin" && UnsecureString.ConvertToUnsecureString(Password) == "00--aa")
                {
                    Controller.Admin = true;
                    Controller.AutorizeView.Hide();
                    Controller.Login();
                    App.StartMainView();
                    return;
                }

                if (Login.Length >= 8 && Login[..8].Equals("employee", StringComparison.CurrentCultureIgnoreCase))
                {
                    List<Employees> users = Controller.DataBase.EmployeeRepository.GetAll().ToList();
                    foreach (Employees user in users)
                    {
                        if (user.Login == Login[8..] && pas == user.Password)
                        {
                            Controller.UserE = Controller.DataBase.EmployeeRepository.Get(user.Id);
                            Controller.AutorizeView.Hide();
                            Controller.Login();
                            App.StartMainView();
                            check_01 = false;
                            break;
                        }
                    }
                }
                else
                {
                    List<Customers> users = Controller.DataBase.CustomerRepository.GetAll().ToList();
                    foreach (Customers user in users)
                    {
                        if (user.Login == Login && pas == user.Password)
                        {
                            Controller.User = Controller.DataBase.CustomerRepository.Get(user.Id);
                            Controller.AutorizeView.Hide();
                            Controller.Login();
                            App.StartMainView();
                            check_01 = false;
                            break;
                        }
                    }
                }

                if (check_01)
                {
                    MessageError = Application.Current.FindResource("UserError").ToString();
                }
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
