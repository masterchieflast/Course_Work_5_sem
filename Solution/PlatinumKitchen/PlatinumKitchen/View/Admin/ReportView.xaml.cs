using Microsoft.Research.DynamicDataDisplay.DataSources;
using Microsoft.Research.DynamicDataDisplay.PointMarkers;
using Microsoft.Research.DynamicDataDisplay;
using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using PlatinumKitchen.Models.Database.Entityes;
using PlatinumKitchen.Models;

namespace PlatinumKitchen.View.Restaurant
{
    /// <summary>
    /// Interaction logic for ReportView.xaml
    /// </summary>
    public partial class ReportView : Page
    {
        public ReportView()
        {
            InitializeComponent();
        }
/*
        private void Window1_Loaded(object sender, RoutedEventArgs e)
        {
            List<Orders> ordersList = Controller.DataBase.OrdersRepository.GetAll().OrderBy(x => x.OrderDate).ToList();
            List<OrderItems> orderssList = Controller.DataBase.OrderItemsRepository.GetAll().ToList();

            DateTime[] dates = new DateTime[ordersList.Count];
            int[] numberItems = new int[ordersList.Count];

            for (int i = 0; i < ordersList.Count; ++i)
            {
                dates[i] = ordersList[i].OrderDate ?? DateTime.MinValue;
                numberItems[i] = 0;
            }

            for(int i = 0;i < numberItems.Length; ++i) 
                for(int j = 0; j < orderssList.Count; ++j)
                {
                    if (orderssList[j].Orders.Id == ordersList[i].Id) numberItems[i]++;
                }

            var datesDataSource = new EnumerableDataSource<DateTime>(dates);
            datesDataSource.SetXMapping(x => dateAxis.ConvertToDouble(x));

            var numberItemsDataSource = new EnumerableDataSource<int>(numberItems);
            numberItemsDataSource.SetYMapping(y => y);

            CompositeDataSource compositeDataSource = new
              CompositeDataSource(datesDataSource, numberItemsDataSource);

            plotter.AddLineGraph(compositeDataSource,
              new Pen(Brushes.Blue, 3),
              new CirclePointMarker { Size = 10.0, Fill = Brushes.Red },
              new PenDescription("Number of items in orders"));

            plotter.Viewport.FitToView();
        }

        private static List<Orders> LoadOrders(string fileName)
        {
            var result = new List<Orders>();
            Random random = new Random();

            // Load orders from file or database as needed
            // Replace this with your actual data loading logic
            for (var i = 0; i < 10; i++)
            {
                DateTime randomDate = DateTime.Now.AddDays(-random.Next(1, 365));
                var order = new Orders
                {
                    OrderDate = randomDate,
                    OrderItems = new List<OrderItems>()
                };

                // Simulate order items
                for (int j = 0; j < random.Next(1, 5); j++)
                {
                    order.OrderItems.Add(new OrderItems
                    {
                        Quantity = random.Next(1, 10),
                        Notes = "Sample Note"
                    });
                }

                result.Add(order);
            }

            return result;
        }*/
    }
}