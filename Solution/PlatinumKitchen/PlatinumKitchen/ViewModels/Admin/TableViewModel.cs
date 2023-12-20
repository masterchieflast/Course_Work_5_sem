﻿using PlatinumKitchen.Infasturcture;
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

namespace PlatinumKitchen.ViewModels.Admin
{
    public class TableViewModel : ViewModelBase
    {
        private string filter = "";

        private ObservableCollection<Tables> members;

        private DelegateCommand<int>? remove;
        private DelegateCommand<int>? assign;

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
        private void AssignToOrders(int tableId)
        {
            try
            {
                var table = Controller.DataBase.TablesRepository.Get(tableId);
                if(table.TableStatus == "Free")
                {
                    var order = Controller.DataBase.OrdersRepository.GetAll()
                        .Where(x => x.Customers == Controller.User).First();
                    order.Tables.TableStatus = "Free";
                    table.TableStatus = "Busy";
                    order.Tables = table;
                }

                Controller.OrdersView = null;
                Controller.OrdersViewModel = null;
                Controller.OrdersView = new();
                Controller.OrdersViewModel = new();
                Controller.OrdersView.DataContext = Controller.OrdersViewModel;
                Controller.SetMainPage("Orders");
                Controller.DataBase.Save();
                Controller.UpdateData();
                UpdateDate();
            }
            catch { }
        }
        private void AddNewRecords()
        {
            try
            {
                var Tables = new Tables
                {
                    TableNumber = "NewRecord",
                    TableSize = "NewRecord",
                    TableStatus = "Busy",
                };
                Controller.DataBase.TablesRepository.Create(Tables);
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
                Controller.DataBase.TablesRepository.Delete(id);
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
            members = new ObservableCollection<Tables>(Controller.DataBase.TablesRepository.GetAll().Where(item => item.TableNumber.Contains(Filter, StringComparison.OrdinalIgnoreCase)));
            Controller.TablesView.membersDataGrid.ItemsSource = members;
        }

        public TableViewModel()
        {
            UpdateDate();
        }
    }
}
