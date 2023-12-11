using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PlatinumKitchen.View.Autorize
{
    /// <summary>
    /// Interaction logic for AutorizeView.xaml
    /// </summary>
    public partial class AutorizeView : Window
    {
        public AutorizeView()
        {
            InitializeComponent();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        private void MainBodyAuthentication_Navigated(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {

        }
    }
}
