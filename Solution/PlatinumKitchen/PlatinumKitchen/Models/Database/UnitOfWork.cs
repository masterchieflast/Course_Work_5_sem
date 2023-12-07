using PlatinumKitchen.Context;
using PlatinumKitchen.Models.Database.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatinumKitchen.Models.Database
{
    public class UnitOfWork
    {
        PlatinumDB platinumDB = new();

        private CustomerRepository customerRepository;
        private EmployeeRepository? employeeRepository;
        private MenuRepository? menuRepository;
        private OrderItemsRepository? orderItemsRepository;
        private OrdersRepository? ordersRepository;
        private ReviewsRepository? reviewsRepository;
        private TablesRepository? tablesRepository;

        private bool disposed = false;

        public CustomerRepository CustomerRepository
        {
            get
            {
                customerRepository ??= new CustomerRepository(platinumDB);
                return customerRepository;
            }
        }
        public EmployeeRepository EmployeeRepository
        {
            get
            {
                employeeRepository ??= new EmployeeRepository(platinumDB);
                return employeeRepository;
            }
        }
        public MenuRepository MenuRepository
        {
            get
            {
                menuRepository ??= new MenuRepository(platinumDB);
                return menuRepository;
            }
        }
        public OrderItemsRepository OrderItemsRepository
        {
            get
            {
                orderItemsRepository ??= new OrderItemsRepository(platinumDB);
                return orderItemsRepository;
            }
        }
        public OrdersRepository OrdersRepository
        {
            get
            {
                ordersRepository ??= new OrdersRepository(platinumDB);
                return ordersRepository;
            }
        }
        public ReviewsRepository ReviewsRepository
        {
            get
            {
                reviewsRepository ??= new ReviewsRepository(platinumDB);
                return reviewsRepository;
            }
        }
        public TablesRepository TablesRepository
        {
            get
            {
                tablesRepository ??= new TablesRepository(platinumDB);
                return tablesRepository;
            }
        }

        public void Save()
        {
            platinumDB.SaveChanges();
        }
        public virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    platinumDB.Dispose();
                }
                disposed = true;
            }
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
