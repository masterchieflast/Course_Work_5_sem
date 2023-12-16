using PlatinumKitchen.Models;
using PlatinumKitchen.Models.Database.Entityes;
using PlatinumKitchen.Utilities;
using PlatinumKitchen.Utilities.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PlatinumKitchen.ViewModels.Restaurant
{
    public class MenuDescriptionViewModel : ViewModelBase
    {
        private int _menuId;

        private Menu _menu;

        private DelegateCommand? setMainPage;
        private DelegateCommand? delete;
        public ICommand Delete
        {
            get
            {
                if (delete == null)
                {
                    delete = new DelegateCommand(DeleteMenu);
                }

                return delete;
            }
        }

        private void DeleteMenu()
        {

            Controller.DataBase.MenuRepository.Delete(_menuId);
            Controller.UpdateData();
            SetMainPageView();
        }

        public ICommand SetMainPage
        {
            get
            {
                if (setMainPage == null)
                {
                    setMainPage = new DelegateCommand(SetMainPageView);
                }

                return setMainPage;
            }
        }

        private void SetMainPageView()
        {
            Controller.UpdateData();
            Controller.SetMainPage("Menu");
        }

        public int MenuId
        {
            get => _menuId;
            set
            {
                if(value == _menuId) 
                    return;
                _menuId = value;

                Menu = Controller.DataBase.MenuRepository.Get(_menuId);

                OnPropertyChanged(nameof(MenuId));
            }
        }

        public Menu Menu
        {
            get => _menu;
            set
            {
                if(value == _menu) 
                    return;
                _menu = value;

                OnPropertyChanged(nameof(Menu));
            }
        }
    }
}
