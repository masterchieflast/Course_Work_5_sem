using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatinumKitchen.Models.Database.Entityes
{
    public class Reviews
    {
        public int Id { get; set; }
        public int? Rating { get; set; }
        public string Notes { get; set; }

        public virtual Orders Orders { get; set; }

    }
}
