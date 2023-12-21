using PlatinumKitchen.Models.Database;
using PlatinumKitchen.Models.Database.Entityes;
using PlatinumKitchen.View.Admin;
using PlatinumKitchen.View.Autorize;
using PlatinumKitchen.View.Restaurant;
using PlatinumKitchen.View.User;
using PlatinumKitchen.ViewModels;
using PlatinumKitchen.ViewModels.Admin;
using PlatinumKitchen.ViewModels.Autorize;
using PlatinumKitchen.ViewModels.Restaurant;
using PlatinumKitchen.ViewModels.User;
using System.Windows;

namespace PlatinumKitchen.Models
{
    public static class Controller
    {
        public static string Uri = "/study/sem5/CourseWork/Solution/PlatinumKitchen/PlatinumKitchen";

        public static UnitOfWork DataBase = new UnitOfWork();

        public static Customers? User;
        public static Employees? UserE;
        public static LoginViewModel LoginViewModel;
        public static LoginView LoginView;
        public static RegistrationViewModel RegistrationViewModel;
        public static RegistrationView RegistrationView;
        public static AutorizeViewModel AutorizeViewModel; 
        public static AutorizeView AutorizeView;
        public static MainView MainView;
        public static MainViewModel MainViewModel;
        public static MenuView MenuView;
        public static MenuViewModel MenuViewModel;
        public static MenuDescriptionView MenuDescriptionView;
        public static MenuDescriptionViewModel MenuDescriptionViewModel;
        public static ReportView ReportView;
        public static ReportViewModel ReportViewModel;
        public static CustomersView CustomersView;
        public static CustomersViewModel CustomersViewModel;
        public static EmployeesView EmployeesView;
        public static EmployeesViewModel EmployeesViewModel;
        public static TableView TablesView;
        public static TableViewModel TableViewModel;
        public static OrdersView OrdersView;
        public static OrdersViewModel OrdersViewModel;
        public static OrdersListView OrdersListView;
        public static OrdersListViewModel OrdersListViewModel;
        public static UserView UserView;
        public static UserViewModel UserViewModel;

        public static bool Admin = false;


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

            MenuView = new();
            MenuViewModel = new();
            MenuView.DataContext = MenuViewModel;

            MenuDescriptionView = new();
            MenuDescriptionViewModel = new();
            MenuDescriptionView.DataContext = MenuDescriptionViewModel;

            CustomersView = new();
            CustomersViewModel = new();
            CustomersView.DataContext = CustomersViewModel;


            EmployeesView = new();
            EmployeesViewModel = new();
            EmployeesView.DataContext = EmployeesViewModel;


            TablesView = new();
            TableViewModel = new();
            TablesView.DataContext = TableViewModel;

            OrdersListView = new();
            OrdersListViewModel = new();
            OrdersListView.DataContext = OrdersListViewModel;


            UpdateData();
        }

        public static void Login()
        {
            if(User != null)
            {
                MainView.Report.Visibility = Visibility.Collapsed;
                MainView.Customers.Visibility = Visibility.Collapsed;
                MainView.Employee.Visibility = Visibility.Collapsed;
                MainView.Customers.Visibility = Visibility.Collapsed;
                MainView.OrdersList.Visibility = Visibility.Collapsed;
            }

            if(UserE != null)
            {
                MainView.Report.Visibility = Visibility.Collapsed;
                MainView.Customers.Visibility = Visibility.Collapsed;
                MainView.Employee.Visibility = Visibility.Collapsed;
                MainView.Customers.Visibility = Visibility.Collapsed;
                MainView.OrdersList.Visibility = Visibility.Visible;
            }


            UserView = new();
            UserViewModel = new();
            UserView.DataContext = UserViewModel;

            OrdersView = new();
            OrdersViewModel = new();
            OrdersView.DataContext = OrdersViewModel;

            if (Admin)
            {
                MainView.Report.Visibility = Visibility.Visible;
                MainView.Customers.Visibility = Visibility.Visible;
                MainView.Employee.Visibility = Visibility.Visible;
                MainView.Customers.Visibility = Visibility.Visible;
                MainView.OrdersList.Visibility = Visibility.Visible;
            }
        }

        public static void SetLanguage()
        {
            language = language == "English" ? "Russian" : "English";
            UpdateSettings();
        }

        public static void UpdateSettings()
        {
            var uriLanguage = new Uri("./Assert/Languages/" + language + ".xaml", UriKind.Relative);

            Application.Current.Resources.Clear();

            ResourceDictionary? resourceDictLang = Application.LoadComponent(uriLanguage) as ResourceDictionary;
            Application.Current.Resources.MergedDictionaries.Add(resourceDictLang);
        }

        public static void UpdateData()
        {
            MenuViewModel._menuBase = DataBase.MenuRepository.GetAll().ToList();
            MenuViewModel.UpdateFilteredMenu();
            UpdateSettings();
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

        public static void SetMainPage(string namePage)
        {
            switch (namePage)
            {
                case "Account":
                    UserView = new();
                    UserViewModel = new();
                    UserView.DataContext = UserViewModel;
                    MainViewModel.MainBodyPage = UserView;
                    break;
                case "OrdersList":
                    OrdersListView = new();
                    OrdersListViewModel = new();
                    OrdersListView.DataContext = OrdersListViewModel;
                    MainViewModel.MainBodyPage = OrdersListView;
                    break;
                case "Orders":
                    OrdersView = new();
                    OrdersViewModel = new();
                    OrdersView.DataContext = OrdersViewModel;
                    MainViewModel.MainBodyPage = OrdersView;
                    break;
                case "Customers":
                    CustomersView = new();
                    CustomersViewModel = new();
                    CustomersView.DataContext = CustomersViewModel;
                    MainViewModel.MainBodyPage = CustomersView;
                    break;
                case "Tables":
                    TablesView = new();
                    TableViewModel = new();
                    TablesView.DataContext = TableViewModel;
                    MainViewModel.MainBodyPage = TablesView;


                    if (!Admin)
                    {
                        TablesView.addNew.Visibility = Visibility.Collapsed;
                    }
                    break;
                case "Employees":

                    EmployeesView = new();
                    EmployeesViewModel = new();
                    EmployeesView.DataContext = EmployeesViewModel;
                    MainViewModel.MainBodyPage = EmployeesView;
                    break;
                case "Menu":
                    MenuView = new();
                    MenuViewModel = new();
                    MenuView.DataContext = MenuViewModel;
                    UpdateData();
                    MainViewModel.MainBodyPage = MenuView;


                    if (!Admin)
                    {
                        MenuView.SaveBut.Visibility = Visibility.Collapsed;
                    }
                    break;
                case "MenuDescription":
                    MainViewModel.MainBodyPage = MenuDescriptionView;


                    if (!Admin)
                    {
                        MenuDescriptionView.EditNews.Visibility = Visibility.Collapsed;
                        MenuDescriptionView.DeleteNews.Visibility = Visibility.Collapsed;
                    }
                    break;
                case "Report":
                    ReportView = new();
                    ReportViewModel = new();
                    ReportView.DataContext = ReportViewModel;
                    MainViewModel.MainBodyPage = ReportView;
                    break;
                case "Admin":
                default:
                    MainViewModel.MainBodyPage = RegistrationView;
                    break;
            }
        }
    }
}
