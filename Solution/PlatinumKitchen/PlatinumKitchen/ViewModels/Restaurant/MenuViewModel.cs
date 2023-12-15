using PlatinumKitchen.Models;
using PlatinumKitchen.Models.Database.Entityes;
using PlatinumKitchen.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatinumKitchen.ViewModels.Restaurant
{
    public class MenuViewModel : ViewModelBase
    {
        private string _imagePath;

        public string ImagePath
        {
            get { return _imagePath; }
            set
            {
                if (_imagePath != value)
                {
                    _imagePath = value + menu;
                    OnPropertyChanged(nameof(ImagePath));
                }
            }
        }




        private List<Menu>? menu;

        public MenuViewModel()
        {
            Menu = Controller.DataBase.MenuRepository.GetAll().ToList();
        }
        public List<Menu> Menu
        {
            get
            {
                return menu;
            }
            set
            {
                menu = value;
                OnPropertyChanged("Menu");
            }
        }
    }
}
