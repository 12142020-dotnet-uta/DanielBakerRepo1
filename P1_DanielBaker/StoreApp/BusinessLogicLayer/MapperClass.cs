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
    }
}
