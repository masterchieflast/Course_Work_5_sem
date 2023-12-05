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
    public class OrderItemsRepository : IRepository<OrderItems>
    {

        private PlatinumDB platinumDB;

        public OrderItemsRepository(PlatinumDB platinumDB)
        {
            this.platinumDB = platinumDB;
        }

        public void Create(OrderItems item)
        {
            platinumDB.OrderItems.Add(item);
            platinumDB.SaveChanges();
        }

        public void Delete(int id)
        {
            OrderItems? OrderItems = platinumDB.OrderItems.Find(id);
            if (OrderItems != null)
                platinumDB.OrderItems.Remove(OrderItems);
            platinumDB.SaveChanges();
        }

        public OrderItems? Get(int id) => platinumDB.OrderItems.Find(id);

        public IEnumerable<OrderItems> GetAll() => platinumDB.OrderItems;

        public void Update(OrderItems item)
        {
            platinumDB.Entry(item).State = EntityState.Modified;
            platinumDB.SaveChanges();
        }
    }
}
