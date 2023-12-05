using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatinumKitchen.Models.Database.Entityes
{
    public class Menu
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal? Price { get; set; }
        public string MenuType { get; set; }
        public string Notes { get; set; }

        public virtual ICollection<OrderItems> OrderItems { get; set; }

    }
}
