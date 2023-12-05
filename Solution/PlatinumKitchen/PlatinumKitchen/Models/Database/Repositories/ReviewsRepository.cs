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
    public class ReviewsRepository : IRepository<Reviews>
    {

        private PlatinumDB platinumDB;

        public ReviewsRepository(PlatinumDB platinumDB)
        {
            this.platinumDB = platinumDB;
        }

        public void Create(Reviews item)
        {
            platinumDB.Reviews.Add(item);
            platinumDB.SaveChanges();
        }

        public void Delete(int id)
        {
            Reviews? Reviews = platinumDB.Reviews.Find(id);
            if (Reviews != null)
                platinumDB.Reviews.Remove(Reviews);
            platinumDB.SaveChanges();
        }

        public Reviews? Get(int id) => platinumDB.Reviews.Find(id);

        public IEnumerable<Reviews> GetAll() => platinumDB.Reviews;

        public void Update(Reviews item)
        {
            platinumDB.Entry(item).State = EntityState.Modified;
            platinumDB.SaveChanges();
        }
    }
}
