﻿using System;
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

        public Inventory GetInventoryByStoreAndName(StoreLocation store, Product product)
        {
            Inventory newInventory = inventories
                .Include(i => i.StoreLocation)
                .Include(i => i.Product)
                .Where(i => i.StoreLocation.StoreLocationId == store.StoreLocationId && i.Product.ProductID == product.ProductID)
                .FirstOrDefault();
            return newInventory;
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

        public Order CreateOrder(Order order)
        {
            Order o = orders.FirstOrDefault(c => c.Store == order.Store && c.Customer == order.Customer && c.isCart == true);
            
            if (o == null)
            {
                Order newOrder = new Order()
                {
                    Store = order.Store,
                    Customer = order.Customer,
                    isCart = order.isCart
                };
                orders.Add(newOrder);
                _context.SaveChanges();
                return newOrder;
            }

            return null;
        }

        public Order GetOrderById(Guid id)
        {
            Order order = orders
                .Include(o => o.Store)
                .Include(o => o.Customer)
                .Where(o => o.OrderId == id)
                .FirstOrDefault();

            return order;
        }

        public Order GetOrderByStoreAndCustomer(Guid storeId, Guid customerId)
        {
            Order order = orders
                .Include(o => o.Store)
                .Include(o => o.Customer)
                .Where(o => o.Store.StoreLocationId == storeId && o.isCart == true)
                .FirstOrDefault();

            return order;
        }

        public List<Order> GetCartsByCustomerId(Guid customerId)
        {
            List<Order> orderList = orders
                .Include(o => o.Customer)
                .Include(o => o.Store)
                .Where(o => o.Customer.CustomerID == customerId && o.isCart == true && o.isOrdered == false)
                .ToList();

            return orderList;
        }


        public List<Order> GetOrdersByCustomerId(Guid customerId)
        {
            List<Order> orderList = orders
                .Include(o => o.Customer)
                .Include(o => o.Store)
                .Where(o => o.Customer.CustomerID == customerId && o.isCart == false && o.isOrdered == true)
                .ToList();

            return orderList;
        }

        public List<Order> GetOrdersByStoreId(Guid storeId)
        {
            List<Order> orderList = orders
                .Include(o => o.Customer)
                .Include(o => o.Store)
                .Where(o => o.Store.StoreLocationId == storeId && o.isCart == false && o.isOrdered == true)
                .ToList();

            return orderList;
        }

        public OrderLineDetails AddOrderLineDetail(Inventory inventory, Order cart, int quantity)
        {
            OrderLineDetails orderLineDetail = orderLines
                .Include(o => o.Item)
                .ThenInclude(i => i.Product)
                .Include(o => o.Order)
                .Where(o => o.Item.InventoryId == inventory.InventoryId && o.Order.OrderId == cart.OrderId)
                .FirstOrDefault();

            if (orderLineDetail == null)
            {
                OrderLineDetails orderLine = new OrderLineDetails()
                {
                    Item = inventory,
                    Order = cart,
                    OrderDetailsQuantity = quantity,
                };

                if (orderLine.OrderDetailsQuantity > inventory.ProductQuantity)
                {
                    throw new Exception($"Ordering too many {inventory.Product.ProductName}");
                }
                double totalCost = (double)orderLine.Item.Product.ProductPrice * orderLine.OrderDetailsQuantity;
                orderLine.OrderDetailsPrice = (decimal)totalCost;

                orderLines.Add(orderLine);
                _context.SaveChanges();
                return orderLine;
            }

            orderLineDetail.OrderDetailsQuantity += quantity;

            if (orderLineDetail.OrderDetailsQuantity > inventory.ProductQuantity)
            {
                throw new Exception($"Ordering too many {inventory.Product.ProductName}");
            }

            double cost = (double)orderLineDetail.Item.Product.ProductPrice * orderLineDetail.OrderDetailsQuantity;
            orderLineDetail.OrderDetailsPrice = (decimal)cost;
            _context.SaveChanges();

            return orderLineDetail;
        }

        public List<OrderLineDetails> GetOrderLineListByCart(Order cart)
        {
            List<OrderLineDetails> orderLineDetails = orderLines
                .Include(o => o.Order)
                .Include(o => o.Item)
                .ThenInclude(i => i.Product)
                .Where(o => o.Order.OrderId == cart.OrderId)
                .ToList();

            return orderLineDetails;
        }

        public Order UpdateCartPrice(Order cart)
        {
            Order editCart = orders.FirstOrDefault(o => o.OrderId == cart.OrderId);
           
            if (editCart == null)
            {
                return null;
            }

            List<OrderLineDetails> orderLineDetails = orderLines
                .Include(o => o.Order)
                .Where(o => o.Order.OrderId == editCart.OrderId)
                .ToList();

            editCart.TotalPrice = 0;

            foreach (OrderLineDetails orderLine in orderLineDetails)
            {
                editCart.TotalPrice += orderLine.OrderDetailsPrice;
            }

            _context.SaveChanges();
            return editCart;
        }

        public void RemoveInventoryBasedOnOrder(List<OrderLineDetails> orderLines, List<Inventory> inventory)
        {
      
            foreach (Inventory i in inventory)
            {
                foreach (OrderLineDetails orderLine in orderLines)
                {
                    if (orderLine.Item.InventoryId == i.InventoryId)
                    {
                        if (i.ProductQuantity < orderLine.OrderDetailsQuantity)
                        {
                            throw new Exception($"Buying too many {i.Product.ProductName}");
                        }

                        i.ProductQuantity -= orderLine.OrderDetailsQuantity;
                    }
                }
            }
            _context.SaveChanges();
        }

        public Order CheckoutCart(Order cart)
        {
            Order checkOutOrder = orders.FirstOrDefault(o => o.OrderId == cart.OrderId);
            checkOutOrder.isCart = false;
            checkOutOrder.isOrdered = true;
            checkOutOrder.OrderTime = DateTime.Now;

            _context.SaveChanges();
            return checkOutOrder;
        }

        public Order GetCartByOrderLineId(Guid orderLineId)
        {
            OrderLineDetails orderLine = orderLines
                .Include(o => o.Order)
                .FirstOrDefault(o => o.OrderDetailsId == orderLineId);

            Order order = orders
                .Include(o => o.Store)
                .Include(o => o.Customer)
                .FirstOrDefault(o => o.OrderId == orderLine.Order.OrderId);

            return order;
        }

        public bool DeleteOrderLineById(Guid orderLineId)
        {
            OrderLineDetails orderLine = orderLines.FirstOrDefault(o => o.OrderDetailsId == orderLineId);
            var success = orderLines.Remove(orderLine);
            _context.SaveChanges();

            if (success != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }

}
