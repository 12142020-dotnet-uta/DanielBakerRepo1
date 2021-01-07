using System;
using System.Collections.Generic;
using ModelLayer.Models;
using ModelLayer.ViewModels;
using RepositoryLayer;

namespace BusinessLogicLayer
{
    public class BusinessLogicClass
    {
        private readonly Repository _repo;
        private readonly MapperClass _mapper;

        public BusinessLogicClass(Repository repo, MapperClass mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public CustomerInfoViewModel LoginUser(LoginViewModel loginPlayer)
        {
            Customer customer = new Customer()
            {
                CustomerUserName = loginPlayer.CustomerUserName,
                CustomerPassword = loginPlayer.CustomerPassword
            };

            // if null????
            Customer c = _repo.LoginUser(customer);

            if (c == null)
            {
                return null;
            }

            CustomerInfoViewModel currentCustomerViewModel = _mapper.ConvertCustomerToCustomerInfoViewModel(c);

            return currentCustomerViewModel;
            
        }

        public CustomerInfoViewModel CreateUser(CreateCustomerViewModel createdPlayer)
        {
            Customer customer = new Customer()
            {
                CustomerUserName = createdPlayer.CustomerUserName,
                CustomerPassword = createdPlayer.CustomerPassword,
                CustomerFName = createdPlayer.CustomerFName,
                CustomerLName = createdPlayer.CustomerLName,
                CustomerAge = createdPlayer.CustomerAge,
                CustomerBirthday = createdPlayer.CustomerBirthday
            };

            // if null????
            Customer newCustomer = _repo.CreateUser(customer);
            if (newCustomer == null)
            {
                return null;
            }

            CustomerInfoViewModel newCustomerViewModel = _mapper.ConvertCustomerToCustomerInfoViewModel(newCustomer);

            return newCustomerViewModel;
        }

        public CustomerInfoViewModel GetCustomerById(Guid id)
        {
            Customer customer = _repo.GetCustomerById(id);

            CustomerInfoViewModel customerInfo = _mapper.ConvertCustomerToCustomerInfoViewModel(customer);

            return customerInfo;
        }

        public CustomerListViewModel GetCustomerList()
        {
            CustomerListViewModel customerList = new CustomerListViewModel();
            List<CustomerInfoViewModel> customerViewModels = new List<CustomerInfoViewModel>();

            List<Customer> customers = _repo.GetCustomerList();


            foreach (Customer c in customers)
            {
                customerViewModels.Add(_mapper.ConvertCustomerToCustomerInfoViewModel(c));
            }

            customerList.Customers = customerViewModels;

            return customerList;
        }


    }
}
