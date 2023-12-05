using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatinumKitchen.Models.Database.Entityes
{
    public class Tables
    {
        public int Id { get; set; }
        public string TableNumber { get; set; }
        public string TableSize { get; set; }
        public string TableStatus { get; set; }

        public virtual ICollection<Orders> Orders { get; set; }
    }
}
