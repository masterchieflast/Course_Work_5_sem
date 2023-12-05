using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace PlatinumKitchen.Models.Database.Entityes
{
    public class Customers
    {
        public int Id { get; set; }
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Phone_Number { get; set; }
        public virtual ICollection<Orders> Orders { get; set; }
    }
}
