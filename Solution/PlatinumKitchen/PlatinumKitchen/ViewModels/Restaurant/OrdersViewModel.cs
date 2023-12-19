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
using System.Windows;
using System.Windows.Automation;
using System.Windows.Input;

namespace PlatinumKitchen.ViewModels.Restaurant
{
    public class OrdersViewModel : ViewModelBase
    {
        private ObservableCollection<Menu> _listOfProducts;

        private ObservableCollection<OrderItems> _Orderss = new ObservableCollection<OrderItems>();
        public ObservableCollection<Orders> prevTransaction { get; set; }

        private Orders _orders;

        public Orders Order
        {
            get => _orders;
            set
            {
                _orders = value;
                OnPropertyChanged("Order");
            }
        }

        private decimal? _totalBill = 0;

        private bool _paymentEnabled;

        private bool _isAdmin;

        private string _tableName;


        public Dictionary<string, int> TableIdByName;
        public Customers CurrentUser { get; set; }
        public int OrderId { get; set; }

        public double prevTotalBill { get; private set; }

        public ObservableCollection<Menu> MenuList { get; set; }


        private DelegateCommand<String>? updateMenuList;
        private DelegateCommand? submitCommand;
        private DelegateCommand? cheakStatus;
        private DelegateCommand? returnMenuCommand;
        private DelegateCommand<Menu>? clickMenuItemCommand;

        public ObservableCollection<Menu> ListOfProducts
        {
            get => _listOfProducts;
            set { 
                _listOfProducts = value; 
                OnPropertyChanged("ListOfProducts"); 
            }
        }

        public ICommand SubmitCommand
        {
            get
            {
                if (submitCommand == null)
                {
                    submitCommand = new DelegateCommand(submiT);
                }
                return submitCommand;
            }
        }

        public void submiT()
        {
            Order.Status = "Please Wait";
            OnPropertyChanged("Order.Status");
            Controller.DataBase.Save();
        }

        public ICommand CheakStatus
        {
            get
            {
                if (cheakStatus == null)
                {
                    cheakStatus = new DelegateCommand(ShowStatus);
                }
                return cheakStatus;
            }
        }

        public void ShowStatus()
        {
            MessageBox.Show(Order.Status);
        }

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

        public ICommand ClickMenuItemCommand
        {
            get
            {
                if (clickMenuItemCommand == null)
                {
                    clickMenuItemCommand = new DelegateCommand<Menu>(clickMenuItem);
                }
                return clickMenuItemCommand;
            }
        }

        public void clickMenuItem(Menu menu)
        {
            try
            {

                var Orders = Orderss.SingleOrDefault
                    (x => x.Menu == menu);

                if (Orders == null)
                {
                    var item = new OrderItems()
                    {
                        Menu = menu,
                        Quantity = 1,
                        Notes = "-",
                        Orders = _orders,
                    };

                    Controller.DataBase.OrderItemsRepository.Create(item);
                    Orderss.Add(item);
                }
                else
                {
                    Orders.Quantity++;
                }

                TotalBill += menu.Price;
                Controller.OrdersView.listPanel.ItemsSource = null;
                Controller.OrdersView.listPanel.ItemsSource = Orderss;
                Controller.DataBase.Save();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Please restart application");
            }
        }

        public ICommand ReturnMenuCommand
        {
            get
            {
                if (returnMenuCommand == null)
                {
                    returnMenuCommand = new DelegateCommand(ReturnMenu);
                }
                return returnMenuCommand;
            }
        }

        private void ReturnMenu()
        {
            var items = Controller.OrdersView.listPanel.SelectedItem as OrderItems;

            if (items != null)
            {
                TotalBill -= items.Menu.Price;

                if (items.Quantity > 1)
                {
                    items.Quantity--;
                }
                else
                {
                    Orderss.Remove(items);
                    Controller.DataBase.OrderItemsRepository.Delete(items.Id);
                }
            }

            Controller.OrdersView.listPanel.ItemsSource = null;
            Controller.OrdersView.listPanel.ItemsSource = Orderss;
            Controller.DataBase.Save();
        }

        public ICommand LogOutCommand { get; set; }
        public ICommand PaymentCommand { get; set; }
        public ICommand ChangeTableCommand { get; set; }
        public ObservableCollection<OrderItems> Orderss
        {
            get => _Orderss;
            set
            {
                _Orderss = value;
                OnPropertyChanged("Orderss");
            }
        }

        public decimal? TotalBill
        {
            get => _totalBill;
            set { _totalBill = value; OnPropertyChanged("TotalBill"); }
        }

        public string TableName
        {
            get => _tableName;
            set { _tableName = value; OnPropertyChanged("TableName"); }
        }

        public bool IsAdmin
        {
            get => _isAdmin;
            set { _isAdmin = value; OnPropertyChanged("IsAdmin"); }
        }

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

        public void UpdateList(string filter)
        {
            MenuList = new ObservableCollection<Menu>(
                Controller.DataBase.MenuRepository.GetAll().
                Where(x => x.MenuType == filter));
            Controller.OrdersView.itemControl.ItemsSource = MenuList;
        }

        public OrdersViewModel()
        {
            _orders = Controller.DataBase.OrdersRepository.GetAll().SingleOrDefault(
                x => x.Status != "Paid" && x.Customers.Id == Controller.User.Id);
            _Orderss = new ObservableCollection<OrderItems> ( Controller.DataBase.OrderItemsRepository.GetAll().Where(x => x.Orders == _orders));
            foreach (OrderItems item in _Orderss)
            {
                TotalBill += item.Quantity * item.Menu.Price;
            }
            
            if ( _orders == null )
            {
                try
                {
                    _orders = new Orders()
                    {
                        OrderDate = DateTime.Now,
                        Status = "Active",
                        Notes = "-",
                        Customers = Controller.User,
                        Employees = Controller.DataBase.EmployeeRepository.GetAll().First(x => x.Id == 1),
                        Tables = Controller.DataBase.TablesRepository.GetAll().First(x => x.TableStatus == "Free"),
                    };

                    Controller.DataBase.OrdersRepository.Create( _orders );
                    Controller.DataBase.OrdersRepository.Update(_orders);
                }
                catch (Exception e)
                {
                    MessageBox.Show("не работаем, ожидайте" + e.Message);
                }
            }

            /*
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
