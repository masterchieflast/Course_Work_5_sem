using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatinumKitchen.Models.Database.Entityes
{
    public class Orders
    {
        public int Id { get; set; }
        public DateTime? OrderDate { get; set; }
        public string Status { get; set; }
        public string Notes { get; set; }

        public virtual Customers Customers { get; set; }
        public virtual Employees Employees { get; set; }
        public virtual ICollection<OrderItems> OrderItems { get; set; }
        public virtual Tables Tables { get; set; }
        public virtual ICollection<Reviews> Reviews { get; set; }

    }
}
