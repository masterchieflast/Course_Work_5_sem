using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace PlatinumKitchen.Models.Database.Entityes
{
    public class Employees : Customers
    {
        public string Position { get; set; }
        public decimal? Salary { get; set; }

    }
}
