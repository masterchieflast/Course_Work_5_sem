using LiveCharts;
using LiveCharts.Wpf;
using PlatinumKitchen.Models.Database.Entityes;
using PlatinumKitchen.Models;
using PlatinumKitchen.Utilities;
using System.Windows.Media;

namespace PlatinumKitchen.ViewModels.Restaurant
{
    public class ReportViewModel : ViewModelBase
    {
        public SeriesCollection SeriesCollection { get; set; }
        public List<string> Labels { get; set; }
        public Func<double, string> YFormatter { get; set; }
        public string Income
        {
            get
            {

                decimal? result = 0;
                List<OrderItems> ordersList = Controller.DataBase.OrderItemsRepository.GetAll().ToList();

                foreach (OrderItems orders in ordersList)
                {
                    if(orders.Orders.Status == "Paid")
                    {
                        result += orders.Menu.Price * orders.Quantity;
                    }
                }

                return result.ToString() + "$";
            }
        }
        
        public string Employees
        {
            get
            {
                return Controller.DataBase.EmployeeRepository.GetAll().Count().ToString();
            }
        }
        public string Salary
        {
            get
            {
                var emp = Controller.DataBase.EmployeeRepository.GetAll().ToList();
                decimal? result = 0;

                foreach (var item in emp)
                {
                    result += item.Salary;
                }

                return result.ToString() + "$";
            }
        }
        public ReportViewModel(){
            List<Orders> ordersList = Controller.DataBase.OrdersRepository.GetAll().Where(x => x.Status == "Paid").OrderBy(x => x.OrderDate).ToList();
            List<OrderItems> orderssList = Controller.DataBase.OrderItemsRepository.GetAll().ToList();
            var ordersDateList = Controller.DataBase.OrdersRepository.GetAll().OrderBy(x => x.OrderDate).Select(x => x.OrderDate.ToString()).ToList();

            DateTime[] dates = new DateTime[ordersList.Count];
            int[] numberItems = new int[ordersList.Count];

            for (int i = 0; i < ordersList.Count; ++i)
            {
                dates[i] = ordersList[i].OrderDate ?? DateTime.MinValue;
                numberItems[i] = 0;
            }

            for (int i = 0; i < numberItems.Length; ++i)
                for (int j = 0; j < orderssList.Count; ++j)
                {
                    if (orderssList[j].Orders.Id == ordersList[i].Id) numberItems[i]++;
                }

            SeriesCollection = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "Orders",
                    Values = new ChartValues<int>(numberItems)
                }
            };

            Labels = ordersDateList;

        }
    }
}
