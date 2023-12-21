using Microsoft.Research.DynamicDataDisplay;
using Microsoft.Win32;
using PlatinumKitchen.Models;
using PlatinumKitchen.Models.Database.Entityes;
using PlatinumKitchen.Utilities;
using PlatinumKitchen.Utilities.Commands;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace PlatinumKitchen.ViewModels.Restaurant
{
    public class MenuDescriptionViewModel : ViewModelBase
    {
        private int _menuId;

        private Menu _menu;

        private DelegateCommand? setMainPage;
        private DelegateCommand? delete;
        private DelegateCommand? edit;
        private DelegateCommand? getImage;
        private DelegateCommand? save;

        public ICommand Save
        {
            get
            {
                if (save == null)
                {
                    save = new DelegateCommand(SaveMenu);
                }
                return save;
            }
        }

        private void SaveMenu()
        {
            if(_menu.Price < 0)
            {
                MessageBox.Show("Price");
                return;
            }


            Controller.MenuDescriptionView.Name.IsHitTestVisible = false;
            Controller.MenuDescriptionView.Price.IsHitTestVisible = false;
            Controller.MenuDescriptionView.Description.IsHitTestVisible = false;
            Controller.MenuDescriptionView.GetImage.Visibility = Visibility.Hidden;
            Controller.MenuDescriptionView.ToSave.Visibility = Visibility.Hidden;
            try
            {
                Controller.DataBase.MenuRepository.Update(_menu);
                Controller.UpdateData();

                var sourcePath = Controller.Uri + $"/Assert/Images/Menu/PreImage.jpg";
                var destinationPath = Controller.Uri + $"/Assert/Images/Menu/Pre{MenuId}.jpg";
                File.Copy(sourcePath, destinationPath, true);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error" + ex.Message);
            }

        }

        public ICommand GetImage
        {
            get
            {
                if (getImage == null)
                {
                    getImage = new DelegateCommand(GetImageMenu);
                }
                return getImage;
            }
        }

        private void GetImageMenu()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.png;*.jpg;*.jpeg)|*.png;*.jpg;*.jpeg|All files (*.*)|*.*";
            var destinationPath = Controller.Uri + $"/Assert/Images/Menu/PreImage.jpg";

            if (openFileDialog.ShowDialog() == true)
            {
                string sourcePath = openFileDialog.FileName;

                try
                {
                    // Копируем файл из исходной директории в указанную директорию
                    File.Copy(sourcePath, destinationPath, true);

                    MessageBox.Show("Image Save");
                }
                catch (Exception ex)
                {
                    // Обрабатываем возможные ошибки, например, если файл уже используется другим процессом
                    MessageBox.Show($"Ошибка при копировании файла: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
        public ICommand Edit
        {
            get
            {
                if (edit == null)
                {
                    edit = new DelegateCommand(EditMenu);
                }
                return edit;
            }
        }

        private void EditMenu()
        {
            Controller.MenuDescriptionView.Name.IsHitTestVisible = true;
            Controller.MenuDescriptionView.Description.IsHitTestVisible = true;
            Controller.MenuDescriptionView.Price.IsHitTestVisible = true;
            Controller.MenuDescriptionView.GetImage.Visibility = Visibility.Visible;
            Controller.MenuDescriptionView.ToSave.Visibility = Visibility.Visible;
        }

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
