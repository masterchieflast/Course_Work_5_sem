
using Microsoft.EntityFrameworkCore;
using PlatinumKitchen.Models.Database.Entityes;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatinumKitchen.Context
{
    public class PlatinumDB : DbContext 
    {
        public DbSet<Customers> Customers { get; set; }
        public DbSet<Employees> Employees { get; set; }
        public DbSet<Menu> Menu { get; set; }
        public DbSet<OrderItems> OrderItems { get; set; }
        public DbSet<Orders> Orders { get; set; }
        public DbSet<Reviews> Reviews { get; set; }
        public DbSet<Tables> Tables { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
            => builder.UseSqlServer("server=localhost; Database=PlatinumKitchen; User=SA; Password=Qwer1234; Encrypt=False;");
    }
}
