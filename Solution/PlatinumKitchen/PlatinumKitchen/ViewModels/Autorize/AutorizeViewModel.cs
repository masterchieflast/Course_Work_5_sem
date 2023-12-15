using PlatinumKitchen.Models;
using PlatinumKitchen.Utilities;
using PlatinumKitchen.Utilities.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace PlatinumKitchen.ViewModels.Autorize
{
    public class AutorizeViewModel : ViewModelBase
    {

        private Page mainBodyAuthenticationPage = null;

        public Page MainBodyAuthenticationPage
        {
            get { return mainBodyAuthenticationPage; }
            set
            {
                if (mainBodyAuthenticationPage == value)
                    return;

                mainBodyAuthenticationPage = value;
                OnPropertyChanged("MainBodyAuthenticationPage");
            }
        }

        private DelegateCommand? closeView;
        private DelegateCommand? minimizeView;

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
            Controller.AutorizeView.WindowState= WindowState.Minimized;
        }
        
        
        private DelegateCommand<string>? languageSwitch;

        public ICommand LanguageSwitch
        {
            get
            {
                languageSwitch ??= new DelegateCommand<string>(LanguageSwitchF);
                return languageSwitch;
            }
        }

        private void LanguageSwitchF(string namePage)
        {
            Controller.SetLanguage();
        }
    }
}
