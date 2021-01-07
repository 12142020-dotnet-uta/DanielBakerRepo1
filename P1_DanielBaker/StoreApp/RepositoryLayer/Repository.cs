using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ModelLayer.Models;

namespace RepositoryLayer
{
    public class Repository
    {
        private readonly StoreDbContext _context;

        DbSet<Customer> customers;
        DbSet<StoreLocation> stores;
        DbSet<Product> products;
        DbSet<Inventory> inventories;
        DbSet<Order> orders;
        DbSet<OrderLineDetails> orderLines;

        public Repository(StoreDbContext storeDbContext)
        {
            _context = storeDbContext;
            customers = _context.customers;
            stores = _context.stores;
            products = _context.products;
            inventories = _context.inventories;
            orders = _context.orders;
            orderLines = _context.orderLineDetails;
        }

        public Customer LoginUser(Customer user)
        {
            Customer c = customers.FirstOrDefault(c => c.CustomerUserName == user.CustomerUserName && c.CustomerPassword == user.CustomerPassword);
            return c;
        }

        public Customer CreateUser(Customer newUser)
        {
            Customer c = customers.FirstOrDefault(c => c.CustomerUserName == newUser.CustomerUserName);
            if (c == null)
            {
                Customer newCustomer = new Customer()
                {
                    CustomerUserName = newUser.CustomerUserName,
                    CustomerPassword = newUser.CustomerPassword,
                    CustomerFName = newUser.CustomerFName,
                    CustomerLName = newUser.CustomerLName,
                    CustomerAge = newUser.CustomerAge,
                    CustomerBirthday = newUser.CustomerBirthday
                };

                customers.Add(newCustomer);
                _context.SaveChanges();
                return newCustomer;
            }

            //returning null if a player with that user name already exists
            return null;
        }


        public Customer GetCustomerById(Guid id)
        {
            Customer customer = customers.FirstOrDefault(c => c.CustomerID == id);
            return customer;
        }

        public List<Customer> GetCustomerList()
        {
            return customers.ToList();
        }
    }

}
