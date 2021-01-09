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
                ProductName = product.ProductName,
                ProductDesc = product.ProductDesc,
                ProductPrice = product.ProductPrice
            };

            return productInfoViewModel;
        }



    }
}
