using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelLayer.Models;
using ModelLayer.ViewModels;

namespace BusinessLogicLayer
{
    public class MapperClass
    {
  
        public CustomerInfoViewModel ConvertCustomerToCustomerInfoViewModel(Customer customer)
        {
            CustomerInfoViewModel customerInfoViewModel = new CustomerInfoViewModel()
            {
                CustomerID = customer.CustomerID,
                CustomerUserName = customer.CustomerUserName,
                CustomerFName = customer.CustomerFName,
                CustomerLName = customer.CustomerLName,
                CustomerAge = customer.CustomerAge
            };

            return customerInfoViewModel;
        }


        public StoreInfoViewModel ConvertStoreToStoreInfoViewModel(StoreLocation store)
        {
            StoreInfoViewModel storeInfoViewModel = new StoreInfoViewModel()
            {
                StoreLocationId = store.StoreLocationId,
                StoreLocationName = store.StoreLocationName,
                StoreLocationAddress = store.StoreLocationAddress,
                Inventory = store.Inventory
            };

            return storeInfoViewModel;
        }

        public ProductInfoViewModel ConvertProductToProductInfoViewModel(Product product)
        {
            ProductInfoViewModel productInfoViewModel = new ProductInfoViewModel()
            {
                ProductID = product.ProductID,
                ProductName = product.ProductName,
                ProductDesc = product.ProductDesc,
                ProductPrice = product.ProductPrice,
                IsAgeRestricted = product.IsAgeRestricted
            };

            return productInfoViewModel;
        }

        public InventoryInfoViewModel ConvertInventoryToInventoryInfoViewModel(Inventory inventory)
        {
            InventoryInfoViewModel infoViewModel = new InventoryInfoViewModel()
            {
                InventoryId = inventory.InventoryId,
                Product = inventory.Product,
                StoreLocation = inventory.StoreLocation,
                ProductQuantity = inventory.ProductQuantity
            };

            return infoViewModel;
        }

        public CartInfoViewModel ConvertOrderToCartInfoViewModel(Order cart)
        {
            CartInfoViewModel cartInfoViewModel = new CartInfoViewModel()
            {
                OrderId = cart.OrderId,
                Customer = ConvertCustomerToCustomerInfoViewModel(cart.Customer),
                Store = ConvertStoreToStoreInfoViewModel(cart.Store),
                TotalPrice = cart.TotalPrice,
                isCart = cart.isCart,
                isOrdered = cart.isOrdered
            };

            return cartInfoViewModel;

        }



    }
}
