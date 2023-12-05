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
    public class EmployeeRepository : IRepository<Employees>
    {

        private PlatinumDB platinumDB;

        public EmployeeRepository(PlatinumDB platinumDB)
        {
            this.platinumDB = platinumDB;
        }

        public void Create(Employees item)
        {
            platinumDB.Employees.Add(item);
            platinumDB.SaveChanges();
        }

        public void Delete(int id)
        {
            Employees? employees = platinumDB.Employees.Find(id);
            if (employees != null)
                platinumDB.Employees.Remove(employees);
            platinumDB.SaveChanges();
        }

        public Employees? Get(int id) => platinumDB.Employees.Find(id);

        public IEnumerable<Employees> GetAll() => platinumDB.Employees;

        public void Update(Employees item)
        {
            platinumDB.Entry(item).State = EntityState.Modified;
            platinumDB.SaveChanges();
        }
    }
}
