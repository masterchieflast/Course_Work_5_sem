using PlatinumKitchen.Models.Database.Entityes;
using PlatinumKitchen.Models;
using PlatinumKitchen.Utilities;
using PlatinumKitchen.Utilities.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using PlatinumKitchen.View.Restaurant;
using System.Windows;

namespace PlatinumKitchen.ViewModels.Restaurant
{
    public class OrdersListViewModel : ViewModelBase
    {
        private string filter = "";

        private ObservableCollection<Orders> members;

        private DelegateCommand<int>? remove;
        private DelegateCommand<int>? assign;

        private DelegateCommand? change;

        public ICommand Remove
        {
            get
            {
                if (remove == null)
                {
                    remove = new DelegateCommand<int>(RemoveById);
                }
                return remove;
            }
        }
        public ICommand Change
        {
            get
            {
                if (change == null)
                {
                    change = new DelegateCommand(ChangeAll);
                }
                return change;
            }
        }

        public ICommand Assign
        {
            get
            {
                if (assign == null)
                {
                    assign = new DelegateCommand<int>(AssignToOrders);
                }
                return assign;
            }
        }
        private void AssignToOrders(int orderId)
        {
            try
            {
                Controller.User = Controller.DataBase.OrdersRepository.Get(orderId).Customers;
                Controller.OrdersView = null;
                Controller.OrdersViewModel = null;
                Controller.OrdersView = new();
                Controller.OrdersViewModel = new();
                Controller.OrdersView.DataContext = Controller.OrdersViewModel;
                Controller.SetMainPage("Orders");
                Controller.MainView.OrdersList.IsChecked = false;
                Controller.MainView.Orders.IsChecked = true;
                Controller.DataBase.OrdersRepository.Get(orderId).Employees = Controller.UserE;
            }
            catch{}
        }
        private void ChangeAll()
        {
            Controller.DataBase.Save();
        }


        public bool EditAvaible
        {
            get
            {
                return !Controller.Admin;
            }
        }

        public Visibility VisRemove
        {
            get
            {
                if (!Controller.Admin)
                {
                    return Visibility.Collapsed;
                }
                else
                {
                    return Visibility.Visible;
                }
            }
        }
        private void RemoveById(int id)
        {
            try
            {
                Controller.DataBase.OrdersRepository.Delete(id);
                UpdateDate();
            }
            catch { }
        }
        public string Filter
        {
            get { return filter; }
            set
            {
                filter = value;
                UpdateDate();
                OnPropertyChanged(nameof(Filter));
            }
        }

        public void UpdateDate()
        {
            var items = Controller.DataBase.OrdersRepository.GetAll().Where(item => item.Status.Contains(Filter, StringComparison.OrdinalIgnoreCase));
            if (!Controller.Admin)
            {
                items = items.Where(x => x.Status != "Paid");
            }

            members = new ObservableCollection<Orders>(items);
            Controller.OrdersListView.membersDataGrid.ItemsSource = members;
        }

        public OrdersListViewModel()
        {
            UpdateDate();
        }
    }
}
