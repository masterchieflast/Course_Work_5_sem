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
        private string tableNumber;
        public string TableNumber { get => tableNumber; set {
                if (!Controller.Admin)
                {
                    return;
                }
                tableNumber = value;
            } }
        private string tableSize;
        public string TableSize { get => tableSize; set
            {
                if (!Controller.Admin)
                {
                    return;
                }
                tableSize = value;
            }
        }
        private string tableStatus;
        public string TableStatus { get => tableStatus; set {
                if (!Controller.Admin)
                {
                    return;
                }
                tableStatus = value;
                    } }

        public virtual ICollection<Orders> Orders { get; set; }
    }
}
