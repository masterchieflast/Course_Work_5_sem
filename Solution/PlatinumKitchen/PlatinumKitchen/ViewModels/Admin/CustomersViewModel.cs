using Microsoft.EntityFrameworkCore.Update.Internal;
using PlatinumKitchen.Infasturcture;
using PlatinumKitchen.Models;
using PlatinumKitchen.Models.Database.Entityes;
using PlatinumKitchen.Utilities;
using PlatinumKitchen.Utilities.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;

namespace PlatinumKitchen.ViewModels.Admin
{
    public class CustomersViewModel : ViewModelBase
    {
        private string filter ="";

        private ObservableCollection<Customers> members;

        private DelegateCommand<int>? remove;

        private DelegateCommand? change;
        private DelegateCommand? addNew;

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

        public ICommand AddNew
        {
            get
            {
                if (addNew == null)
                {
                    addNew = new DelegateCommand(AddNewRecords);
                }
                return addNew;
            }
        }
        private void AddNewRecords()
        {
            try
            {
                var customer = new Customers {
                    First_Name = "NewRecord",
                    Last_Name = "NewRecord",
                    Email = "NewRecord",
                    Login = "Test",
                    Password = UnsecureString.Encode("Test"),
                    Phone_Number = "+375-29-846-7707",
                };
                Controller.DataBase.CustomerRepository.Create(customer);
                UpdateDate();
            }
            catch { }
        }

        private void ChangeAll()
        {
            try
            {
                Controller.DataBase.Save();
                Controller.UpdateData();
            }
            catch { }
        }

        private void RemoveById(int id)
        {
            try
            {
                Controller.DataBase.CustomerRepository.Delete(id);
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
            members = new ObservableCollection<Customers> (Controller.DataBase.CustomerRepository.GetAll().Where(item => item.First_Name.Contains(Filter, StringComparison.OrdinalIgnoreCase)));
            Controller.CustomersView.membersDataGrid.ItemsSource = members;
        }

        public CustomersViewModel()
        {
            UpdateDate();
        }
    }
}
