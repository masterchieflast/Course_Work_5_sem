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
    public class MenuRepository : IRepository<Menu>
    {

        private PlatinumDB platinumDB;

        public MenuRepository(PlatinumDB platinumDB)
        {
            this.platinumDB = platinumDB;
        }

        public void Create(Menu item)
        {
            platinumDB.Menu.Add(item);
            platinumDB.SaveChanges();
        }

        public void Delete(int id)
        {
            Menu? menu = platinumDB.Menu.Find(id);
            if (menu != null)
                platinumDB.Menu.Remove(menu);
            platinumDB.SaveChanges();
        }

        public Menu? Get(int id) => platinumDB.Menu.Find(id);

        public IEnumerable<Menu> GetAll() => platinumDB.Menu;

        public void Update(Menu item)
        {
            platinumDB.Entry(item).State = EntityState.Modified;
            platinumDB.SaveChanges();
        }
    }
}
