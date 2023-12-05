using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatinumKitchen.Models.Database.Entityes
{
    public class OrderItems
    {
        public int Id { get; set; }
        public int? OrderId { get; set; }
        public int? MenuId { get; set; }
        public int? Quantity { get; set; }
        public string Notes { get; set; }

        public virtual Menu Menu { get; set; }
        public virtual Orders Orders { get; set; }

    }
}
