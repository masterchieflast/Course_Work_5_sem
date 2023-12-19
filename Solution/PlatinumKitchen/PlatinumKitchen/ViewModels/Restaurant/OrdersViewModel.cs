using PlatinumKitchen.Models;
using PlatinumKitchen.Models.Database.Entityes;
using PlatinumKitchen.Utilities;
using PlatinumKitchen.Utilities.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Automation;
using System.Windows.Input;

namespace PlatinumKitchen.ViewModels.Restaurant
{
    public class OrdersViewModel : ViewModelBase
    {
        private ObservableCollection<Menu> _listOfProducts;
        public ObservableCollection<Menu> ListOfProducts
        {
            get => _listOfProducts;
            set { 
                _listOfProducts = value; 
                OnPropertyChanged("ListOfProducts"); 
            }
        }

        // Binded into View
        private ObservableCollection<Orders> _Orderss = new ObservableCollection<Orders>();
        public ObservableCollection<Orders> Orderss
        {
            get => _Orderss;
            set => _Orderss = value;
        }

        private double _totalBill = 0;
        public double TotalBill
        {
            get => _totalBill;
            set { _totalBill = value; OnPropertyChanged("TotalBill"); }
        }

        private DateTime _date = DateTime.Now;
        public DateTime Date
        {
            get => _date;
            set { _date = value; OnPropertyChanged("Date"); }
        }

        private string _tableName;
        public string TableName
        {
            get => _tableName;
            set { _tableName = value; OnPropertyChanged("TableName"); }
        }

        private bool _isAdmin;
        public bool IsAdmin
        {
            get => _isAdmin;
            set { _isAdmin = value; OnPropertyChanged("IsAdmin"); }
        }

        private bool _paymentEnabled;
        public bool PaymentEnabled
        {
            get => _paymentEnabled;
            set { _paymentEnabled = value; OnPropertyChanged("PaymentEnabled"); }
        }

        private bool _tableChangeEnabled;
        public bool TableChangeEnabled
        {
            get => _tableChangeEnabled;
            set { _tableChangeEnabled = value; OnPropertyChanged("TableChangeEnabled"); }
        }

        public Dictionary<string, int> TableIdByName;
        public Customers CurrentUser { get; set; }
        public int OrderId { get; set; }

        public ObservableCollection<Orders> prevTransaction { get; set; }
        public double prevTotalBill { get; private set; }

        #region MenuItemCommands
        public ObservableCollection<Menu> MenuList { get; set; }


        private DelegateCommand<String>? updateMenuList;

        public ICommand UpdateMenuList
        {
            get
            {
                if (updateMenuList == null)
                {
                    updateMenuList = new DelegateCommand<string>(UpdateList);
                }
                return updateMenuList;
            }
        }

        public void UpdateList(string filter)
        {
            MenuList = new ObservableCollection<Menu>(
                Controller.DataBase.MenuRepository.GetAll().
                Where(x => x.MenuType == filter));
            Controller.OrdersView.itemControl.ItemsSource = MenuList;
        }

        public ICommand ClickMenuItemCommand { get; set; }
        public ICommand ReturnMenuCommand { get; set; }
        public ICommand LogOutCommand { get; set; }
        public ICommand SubmitCommand { get; set; }
        public ICommand PaymentCommand { get; set; }
        public ICommand ChangeTableCommand { get; set; }
        #endregion


        public OrdersViewModel()
        {/*
            AppetizersMenuCommand = new RelayCommand(() => ListOfProducts = Appetizers);
            MainDishesMenuCommand = new RelayCommand(() => ListOfProducts = MainDishes);
            BeveragesMenuCommand = new RelayCommand(() => ListOfProducts = Beverages);
            DessertsMenuCommand = new RelayCommand(() => ListOfProducts = Desserts);
            OthersMenuCommand = new RelayCommand(() => ListOfProducts = Others);
            ClickMenuItemCommand = new RelayCommand<Menu>(ClickMenuItem);
            ReturnMenuCommand = new RelayCommand(ReturnMenu);
            SubmitCommand = new RelayCommand(Submit);
            PaymentCommand = new RelayCommand(Payment);
            LogOutCommand = new RelayCommand(Logout);
            ChangeTableCommand = new RelayCommand(ChangeTable);*/
        }

        public void Initialize()
        {/*
            Appetizers = Database.GetProducts(ProductCategory.Appetizers);
            MainDishes = Database.GetProducts(ProductCategory.MainDishes);
            Beverages = Database.GetProducts(ProductCategory.Beverages);
            Desserts = Database.GetProducts(ProductCategory.Desserts);
            Others = Database.GetProducts(ProductCategory.Others);
            TableIdByName = Mains.TableIdByName;*/
        }

        public void NotifyTableClicked(string tableNumb,
                                       int orderId,
                                       Customers staff,
                                       ObservableCollection<Orders> Orderss,
                                       DateTime dt)
        {/*
            if (tableNumb == null) return;

            this.TableName = tableNumb;
            this.CurrentUser = staff;
            this.OrderId = orderId;
            this.Orderss = Orderss == null ? new ObservableCollection<Orders>() : Orderss;
            this.Date = dt == default(DateTime) ? DateTime.Now : dt;

            //TotalBill = Orderss.Sum(x => x.Quantity * x.SelectedProduct.Price);
            TotalBill = Orderss.Sum(x => x.TotalPrice);

            prevTransaction = Orderss.GetShallowClones();
            prevTotalBill = TotalBill;
            PaymentEnabled = prevTotalBill != 0;
            TableChangeEnabled = prevTransaction.Count() != 0;*/
        }

        private void ReturnMenu()
        {/*
            Mains.TableOverviewVM.OrderssByTable[TableName] = prevTransaction;
            Helpers.SwitchWindow(this, new TablesOverview());*/
        }

        private void Submit()
        {
            /*if (Orderss.Count() == 0)
            {
                Helpers.SwitchWindow(this, new TablesOverview());
                return;
            }

            Mains.TableOverviewVM.OrderssByTable[TableName] = Orderss;

            string orderId = OrderId == 0 ? "NULL" : OrderId.ToString();
            int lastId = Database.InsertOrder($@"{orderId},
                                              {CurrentUser.Id},
                                              {TableIdByName[TableName]},
                                              '{Date.ToString("yyyy-MM-dd HH:mm:ss")}',
                                              {TotalBill},
                                               0
            "

                                               , TotalBill);
            OrderId = lastId;
            Mains.TableOverviewVM.OrderIdByTable[TableName] = lastId;

            foreach (var line in Orderss)
            {
                string OrdersId = line.Id == -1 ? "NULL" : line.Id.ToString();
                int id = Database.Replace(Columns.Orders, $@"{OrdersId},
                                                                {lastId},
                                                                {line.SelectedProduct.ID},
                                                                {line.TotalPrice},
                                                                {line.Quantity}");
                line.Id = id;
            }

            prevTransaction.Clear();
            Database.SetTableTaken(TableName, true);

            Helpers.SwitchWindow(this, new TablesOverview());*/
        }

        private void Logout()
        {/*
            Helpers.SwitchWindow(this, new MainWindow());*/
        }

        private void ClickMenuItem(Menu product)
        {/*
            var Orders = Orderss.SingleOrDefault
                (x => x.SelectedProduct.Name == product.Name);

            if (Orders == null)
            {
                Orderss.Add(new Orders()
                {
                    SelectedProduct = product,
                    Quantity = 1,
                    TotalPrice = product.Price
                });
            }
            else
            {
                Orders.Quantity++;
                Orders.TotalPrice += product.Price;
            }
            TotalBill += product.Price;*/
        }

        private void Payment()
        {/*
            if (prevTotalBill == 0)
            {
                return;
            }

            Mains.PaymentVM.TableNumber = this.TableName;
            Mains.PaymentVM.TotalBill = this.TotalBill;
            Mains.PaymentVM.OrderId = this.OrderId;
            var window = new PaymentWindow();
            window.ShowDialog();*/
        }

        private void ChangeTable()
        {/*
            if (prevTransaction.Count() == 0)
            {
                return;
            }

            Mains.TableChangeVM.NotifyTableClicked(TableName, CurrentUser);
            Helpers.SwitchWindow(this, new TableChange());*/
        }

        public void ReturnToTableMenu()
        {/*
            Helpers.SwitchWindow(this, new TablesOverview());*/
        }
    }
}
