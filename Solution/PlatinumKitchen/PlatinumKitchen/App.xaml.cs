using PlatinumKitchen.Models;
using PlatinumKitchen.View.Autorize;
using PlatinumKitchen.ViewModels;
using PlatinumKitchen.ViewModels.Autorize;
using System.Configuration;
using System.Data;
using System.Windows;

namespace PlatinumKitchen
{
    public partial class App : Application
    {
        public static void StartMainView()
        {
            Controller.MainView.Show();
        }

        private void OnStartup(object sender, StartupEventArgs e)
        {
            Controller.UpdateSettings();
            Controller.DataBase.Save();

            MainView mainView = new MainView();
            MainViewModel mainViewModel = new MainViewModel();
            //mainView.DataContext = mainViewModel;
            //Controller.mainViewModel = mainViewModel;
            //Controller.SetMainPage("PK");
            //Controller.mainView = mainView;

            AutorizeView autorizeView = new AutorizeView();
            AutorizeViewModel autorizeViewModel = new AutorizeViewModel();
            autorizeView.DataContext = autorizeViewModel;
            autorizeView.Show();
            //Controller.User = Controller.DataBase.CustomerRepository.Get(1);
            Controller.AutorizeViewModel = autorizeViewModel;
            Controller.AutorizeView = autorizeView;
            autorizeViewModel.MainBodyAuthenticationPage = Controller.LoginView;

        }
    }

}
