using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Reflection;
using System.Configuration;

namespace CourseSolution.Entity
{
    public class DBContextPK : DbContext
    {
        private const string Connection = "server=localhost; Database=PlatinumKitchen; User=SA; Password=Qwer1234; Encrypt=False;";

        public DbSet<CUSTOMERS> Customers { get; set; }
        public DbSet<EMPLOYEES> Employees { get; set; }
        public DbSet<MENU> Menu { get; set; }
        public DbSet<ORDERITEMS> OrderItems { get; set; }
        public DbSet<ORDERS> Orders { get; set; }
        public DbSet<REVIEWS> Reviews { get; set; }
        public DbSet<TABLES> Tables { get; set; }

        public DBContextPK(DbContextOptions<DBContextPK> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
