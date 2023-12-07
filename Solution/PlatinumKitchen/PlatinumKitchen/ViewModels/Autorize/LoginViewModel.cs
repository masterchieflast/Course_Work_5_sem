using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using PlatinumKitchen.Models;
using PlatinumKitchen.Models.Database;
using PlatinumKitchen.Models.Database.Entityes;
using PlatinumKitchen.Utilities;
using PlatinumKitchen.Utilities.Commands;

namespace PlatinumKitchen.ViewModels.Autorize
{
    public class LoginViewModel : ViewModelBase
    {
        private string _Login;
        private string _password;
        private string _errorMessage;

        public string Login
        {
            get
            {
                return _Login;
            }

            set
            {
                _Login = value;
                OnPropertyChanged(nameof(Login));
            }
        }

        public string Password
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

        public string ErrorMessage
        {
            get
            {
                return _errorMessage;
            }

            set
            {
                _errorMessage = value;
                OnPropertyChanged(nameof(ErrorMessage));
            }
        }

        private DelegateCommand? loginApplication;

        public ICommand LoginApplication
        {
            get
            {
                if (loginApplication == null)
                {
                    loginApplication = new DelegateCommand(LoginApplicationEnter);
                }
                return loginApplication;
            }
        }

        private void LoginApplicationEnter()
        {
            bool check_01 = true;
            bool check_02 = true;

            if (Login == "" || Login == null)
            {

                _errorMessage = Application.Current.FindResource("LoginError").ToString();
                check_01 = false;
            }
            if (Password == "" || Password == null)
            {

                _errorMessage = Application.Current.FindResource("PasswordError").ToString();
                check_02 = false;
            }

            if (check_01 && check_02)
            {
                List<Customers> users = Controller.DataBase.CustomerRepository.GetAll().ToList();
                foreach (Customers user in users)
                {
                    if (user.Login == Login && Password == user.Password)
                    {
                        Controller.User = Controller.DataBase.CustomerRepository.Get(user.Id);
                        _errorMessage = "";
                        Controller.AutorizeView.Hide();
                        App.StartMainView();
                    }
                }
            }
            else
            {
                _errorMessage = Application.Current.FindResource("UserError").ToString();
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
