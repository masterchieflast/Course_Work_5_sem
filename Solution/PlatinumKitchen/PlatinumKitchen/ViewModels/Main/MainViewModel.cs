using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using PlatinumKitchen.Utilities;
using PlatinumKitchen.Models;
using PlatinumKitchen.Utilities.Commands;
using System.Windows.Input;
using System.Windows;
using System.Windows.Controls;
using System.Reflection.Metadata;

namespace PlatinumKitchen.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private Page mainBodyPage = null;

        private DelegateCommand? closeView;
        private DelegateCommand? minimizeView;
        private DelegateCommand? maximizeView;

        private DelegateCommand<string>? showMainViewCommand;

        public Page MainBodyPage
        {
            get { return mainBodyPage; }
            set
            {
                if (mainBodyPage == value)
                    return;

                mainBodyPage = value;
                OnPropertyChanged("mainBodyPage");
            }
        }
        
        public ICommand CloseView
        {
            get
            {
                if (closeView == null)
                {
                    closeView = new DelegateCommand(CloseViewWindow);
                }
                return closeView;
            }
        }
        private void CloseViewWindow()
        {
            Application.Current.Shutdown();
        }
        public ICommand MinimizeView
        {
            get
            {
                if (minimizeView == null)
                {
                    minimizeView = new DelegateCommand(MinimizeViewWindow);
                }
                return minimizeView;
            }
        }
        private void MinimizeViewWindow()
        {
            Controller.MainView.WindowState = WindowState.Minimized;
        } 
        public ICommand MaximizeView
        {
            get
            {
                if (maximizeView == null)
                {
                    maximizeView = new DelegateCommand(MaximizeViewWindow);
                }
                return maximizeView;
            }
        }
        private void MaximizeViewWindow()
        {
            if (Controller.MainView.WindowState == WindowState.Normal)
                Controller.MainView.WindowState = WindowState.Maximized;
            else Controller.MainView.WindowState = WindowState.Normal;
        }

        public ICommand ShowMainViewCommand
        {
            get
            {
                if (showMainViewCommand == null)
                {
                    showMainViewCommand = new DelegateCommand<string>(SetMainPageView);
                }
                return showMainViewCommand;
            }
        }
        private void SetMainPageView(string namePage)
        {
            Controller.SetMainPage(namePage);
        }
    }
}
