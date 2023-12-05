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
    public class CustomerRepository : IRepository<Customers>
    {

        private PlatinumDB platinumDB;

        public CustomerRepository(PlatinumDB platinumDB)
        {
            this.platinumDB = platinumDB;
        }

        public void Create(Customers item)
        {
            platinumDB.Customers.Add(item);
            platinumDB.SaveChanges();
        }

        public void Delete(int id)
        {
            Customers? customers = platinumDB.Customers.Find(id);
            if (customers != null)
                platinumDB.Customers.Remove(customers);
            platinumDB.SaveChanges();
        }

        public Customers? Get(int id) => platinumDB.Customers.Find(id);

        public IEnumerable<Customers> GetAll() => platinumDB.Customers;

        public void Update(Customers item)
        {
            platinumDB.Entry(item).State = EntityState.Modified;
            platinumDB.SaveChanges();
        }
    }
}
