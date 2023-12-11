using PlatinumKitchen.Models.Database;
using PlatinumKitchen.Models.Database.Entityes;
using PlatinumKitchen.View.Autorize;
using PlatinumKitchen.ViewModels;
using PlatinumKitchen.ViewModels.Autorize;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PlatinumKitchen.Models
{
    public static class Controller
    {

        public static Customers? User;
        public static Employees? UserE;
        public static LoginViewModel LoginViewModel;
        public static LoginView LoginView;
        public static RegistrationViewModel RegistrationViewModel;
        public static RegistrationView RegistrationView;
        public static AutorizeViewModel AutorizeViewModel; 
        public static AutorizeView AutorizeView;
        //public static MainViewModel MainViewModel;
        public static MainView MainView;
        public static MainViewModel MainViewModel;
        /*public static MainView mainView;
        public static AuthenticationView authenticationView;*/
        public static UnitOfWork DataBase = new UnitOfWork();

        private static string language;

        static Controller()
        {
            language = "English";

            MainView = new();
            MainViewModel = new();
            MainView.DataContext = MainViewModel;

            LoginView = new();
            LoginViewModel = new();
            LoginView.DataContext = LoginViewModel;

            RegistrationView = new();
            RegistrationViewModel = new();
            RegistrationView.DataContext = RegistrationViewModel;
        }

        public static void SetLanguage(string languag)
        {
            language = languag;
            UpdateSettings();
        }

        public static void UpdateSettings()
        {
            var uriLanguage = new Uri("./Assert/Languages/" + language + ".xaml", UriKind.Relative);

            Application.Current.Resources.Clear();

            ResourceDictionary? resourceDictLang = Application.LoadComponent(uriLanguage) as ResourceDictionary;
            Application.Current.Resources.MergedDictionaries.Add(resourceDictLang);
        }

        public static void SetAuthenticationPage(string namePage)
        {
            switch (namePage)
            {
                case "Login":
                    AutorizeViewModel.MainBodyAuthenticationPage = LoginView;
                    break;
                case "Registration":
                default:
                    AutorizeViewModel.MainBodyAuthenticationPage = RegistrationView;
                    break;
            }
        }

        internal static void SetMainPage(string namePage)
        {
            throw new NotImplementedException();
        }
    }
}
