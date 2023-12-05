using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PlatinumKitchen.Context;
using PlatinumKitchen.Models;
using PlatinumKitchen.Models.Database.Entityes;
using PlatinumKitchen.Utilities;

namespace PlatinumKitchen.Models.Database.Repositories
{
    public class OrdersRepository : IRepository<Orders>
    {

        private PlatinumDB platinumDB;

        public OrdersRepository(PlatinumDB platinumDB)
        {
            this.platinumDB = platinumDB;
        }

        public void Create(Orders item)
        {
            platinumDB.Orders.Add(item);
            platinumDB.SaveChanges();
        }

        public void Delete(int id)
        {
            Orders? Orders = platinumDB.Orders.Find(id);
            if (Orders != null)
                platinumDB.Orders.Remove(Orders);
            platinumDB.SaveChanges();
        }

        public Orders? Get(int id) => platinumDB.Orders.Find(id);

        public IEnumerable<Orders> GetAll() => platinumDB.Orders;

        public void Update(Orders item)
        {
            platinumDB.Entry(item).State = EntityState.Modified;
            platinumDB.SaveChanges();
        }
    }
}
