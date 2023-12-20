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
            }
            catch{}
        }

        private void ChangeAll()
        {
            try
            {
                Controller.DataBase.Save();
                Controller.UpdateData();
                UpdateDate();
            }
            catch { }
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
            members = new ObservableCollection<Orders>(Controller.DataBase.OrdersRepository.GetAll().Where(item => item.Status.Contains(Filter, StringComparison.OrdinalIgnoreCase)));
            Controller.OrdersListView.membersDataGrid.ItemsSource = members;
        }

        public OrdersListViewModel()
        {
            UpdateDate();
        }
    }
}
