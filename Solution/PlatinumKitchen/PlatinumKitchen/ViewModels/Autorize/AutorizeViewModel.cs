using PlatinumKitchen.Models;
using PlatinumKitchen.Utilities;
using PlatinumKitchen.Utilities.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        private DelegateCommand? closeAuthenticationView;

        public ICommand CloseAuthenticationView
        {
            get
            {
                if (closeAuthenticationView == null)
                {
                    closeAuthenticationView = new DelegateCommand(CloseAuthenticationViewViewWindow);
                }
                return closeAuthenticationView;
            }
        }

        private void CloseAuthenticationViewViewWindow()
        {
            App.Current.Shutdown();
        }

        private DelegateCommand<string>? setAuthenticationViewPage;

        public ICommand SetAuthenticationViewPage
        {
            get
            {
                if (setAuthenticationViewPage == null)
                {
                    setAuthenticationViewPage = new DelegateCommand<string>(SetAuthenticationPageView);
                }
                return setAuthenticationViewPage;
            }
        }

        private void SetAuthenticationPageView(string namePage)
        {
            Controller.SetMainPage(namePage);
        }
    }
}
