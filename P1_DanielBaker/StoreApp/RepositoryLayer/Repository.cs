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

        public Customer EditCustomer(Customer c)
        {
            Customer editedCustomer = GetCustomerById(c.CustomerID);

            if (editedCustomer == null)
            {
                return null;
            }

            editedCustomer.CustomerFName = c.CustomerFName;
            editedCustomer.CustomerLName = c.CustomerLName;
            editedCustomer.CustomerAge = c.CustomerAge;
            editedCustomer.CustomerBirthday = c.CustomerBirthday;

            _context.SaveChanges();

            return editedCustomer;

        }

        public List<StoreLocation> GetStoreList()
        {
            return stores.ToList();
        }

        public StoreLocation CreateStore(StoreLocation newStore)
        {
            StoreLocation s = stores.FirstOrDefault(s => s.StoreLocationName == newStore.StoreLocationName);
            if (s == null)
            {
                StoreLocation newStoreLocation = new StoreLocation()
                {
                    StoreLocationName = newStore.StoreLocationName,
                    StoreLocationAddress = newStore.StoreLocationAddress,
  
                };

                stores.Add(newStoreLocation);
                _context.SaveChanges();
                return newStoreLocation;
            }

            //returning null if a store with that user name already exists
            return null;
        }

        public StoreLocation GetStoreById(Guid id)
        {
            StoreLocation store = stores.FirstOrDefault(s => s.StoreLocationId == id);
            return store;
        }

        public StoreLocation EditStore(StoreLocation storeToEdit)
        {
            StoreLocation store = stores.FirstOrDefault(s => s.StoreLocationId == storeToEdit.StoreLocationId);

            if (store == null)
            {
                return null;
            }

            store.StoreLocationName = storeToEdit.StoreLocationName;
            store.StoreLocationAddress = storeToEdit.StoreLocationAddress;

            _context.SaveChanges();

            return store;
        }

        public List<Product> GetProductList()
        {
            return products.ToList();
        }

        public Product CreateProduct(Product newProduct)
        {

            Product p = products.FirstOrDefault(p => p.ProductName == newProduct.ProductName);
            if (p == null)
            {
                Product product = new Product()
                {
                    ProductName = newProduct.ProductName,
                    ProductDesc = newProduct.ProductDesc,
                    ProductPrice = newProduct.ProductPrice,
                    IsAgeRestricted = newProduct.IsAgeRestricted
                };

                products.Add(product);
                _context.SaveChanges();
                return product;
            }

            //returning null if a products with that user name already exists
            return null;
        }

        public Product GetProductById(Guid id)
        {
           Product product = products.FirstOrDefault(p => p.ProductID == id);
           return product;
        }

        public Product GetProductByName(string name)
        {
            Product product = products.FirstOrDefault(p => p.ProductName == name);
            return product;
        }

        public Product EditProduct(Product product)
        {
            Product editedProduct = products.FirstOrDefault(p => p.ProductID == product.ProductID);
            
            if (editedProduct == null)
            {
                return null;
            }

            editedProduct.ProductName = product.ProductName;
            editedProduct.ProductDesc = product.ProductDesc;
            editedProduct.ProductPrice = product.ProductPrice;
            editedProduct.IsAgeRestricted = product.IsAgeRestricted;

            _context.SaveChanges();

            return editedProduct;

        }

        public List<Inventory> GetStoreInventory(StoreLocation store)
        {
            List<Inventory> storeInventory = inventories
                .Include(i => i.StoreLocation)
                .Include(i => i.Product)
                .Where(i => i.StoreLocation.StoreLocationId == store.StoreLocationId).ToList();
           
            return storeInventory;
        }

        public Inventory AddNewInventory(Inventory addInventory)
        {
            Inventory inventory = inventories.FirstOrDefault(i => i.Product.ProductID == addInventory.Product.ProductID
                                       && i.StoreLocation.StoreLocationId == addInventory.StoreLocation.StoreLocationId);
            if (inventory == null)
            {
                Inventory addedInventory = new Inventory()
                {
                    Product = addInventory.Product,
                    StoreLocation = addInventory.StoreLocation,
                    ProductQuantity = addInventory.ProductQuantity
                };
                inventories.Add(addedInventory);
                _context.SaveChanges();
                return addedInventory;
            }
            inventory.ProductQuantity += addInventory.ProductQuantity;
            _context.SaveChanges();
            return addInventory;
        }




    }

}
