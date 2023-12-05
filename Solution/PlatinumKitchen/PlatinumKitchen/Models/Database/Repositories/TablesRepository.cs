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
    public class TablesRepository : IRepository<Tables>
    {

        private PlatinumDB platinumDB;

        public TablesRepository(PlatinumDB platinumDB)
        {
            this.platinumDB = platinumDB;
        }

        public void Create(Tables item)
        {
            platinumDB.Tables.Add(item);
            platinumDB.SaveChanges();
        }

        public void Delete(int id)
        {
            Tables? Tables = platinumDB.Tables.Find(id);
            if (Tables != null)
                platinumDB.Tables.Remove(Tables);
            platinumDB.SaveChanges();
        }

        public Tables? Get(int id) => platinumDB.Tables.Find(id);

        public IEnumerable<Tables> GetAll() => platinumDB.Tables;

        public void Update(Tables item)
        {
            platinumDB.Entry(item).State = EntityState.Modified;
            platinumDB.SaveChanges();
        }
    }
}
