﻿using PlatinumKitchen.Models;
using PlatinumKitchen.Models.Database.Entityes;
using PlatinumKitchen.Utilities;
using PlatinumKitchen.Utilities.Commands;
using System.Windows.Input;

namespace PlatinumKitchen.ViewModels.Restaurant
{
    public class MenuViewModel : ViewModelBase
    {
        private string _menuFilter;

        private List<Menu>? _menu;
        public List<Menu> _menuBase;

        private DelegateCommand<int>? getDescriptionMenu;

        public MenuViewModel()
        {
            UpdateFilteredMenu();
        }

        public ICommand GetDescriptionMenu
        {
            get
            {
                if (getDescriptionMenu == null)
                {
                    getDescriptionMenu = new DelegateCommand<int>(GetDescriptionMenuView);
                }

                return getDescriptionMenu;
            }
        }

        private void GetDescriptionMenuView(int id)
        {
            Controller.MenuDescriptionViewModel.MenuId = id;
            Controller.SetMainPage("MenuDescription");
        }

        public string MenuFilter
        {
            get { return _menuFilter; }
            set
            {
                _menuFilter = value;
                UpdateFilteredMenu();
                OnPropertyChanged(nameof(MenuFilter));
            }
        }

        public List<Menu> Menu
        {
            get { return _menu; }
            set
            {
                _menu = value;
                OnPropertyChanged(nameof(Menu));
            }
        }

        public void UpdateFilteredMenu()
        {
            if (!string.IsNullOrWhiteSpace(_menuFilter))
            {
                var filteredMenu = _menuBase.Where(item => item.Name.Contains(_menuFilter, StringComparison.OrdinalIgnoreCase))
                                        .OrderBy(item => item.Name)
                                        .ToList();
                Menu = filteredMenu;
            }
            else
            {
                Menu = _menuBase;
            }
        }
    }

}
