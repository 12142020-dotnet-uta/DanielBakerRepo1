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

            if (customer == null)
            {
                return null;
            }

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

        public CustomerInfoViewModel EditCustomer(CustomerInfoViewModel customerToEdit)
        {
            Customer customer = _repo.GetCustomerById(customerToEdit.CustomerID);

            customer.CustomerFName = customerToEdit.CustomerFName;
            customer.CustomerLName = customerToEdit.CustomerLName;
            customer.CustomerAge = customerToEdit.CustomerAge;
            customer.CustomerBirthday = customerToEdit.CustomerBirthday;

            Customer editedCustomer = _repo.EditCustomer(customer);

            CustomerInfoViewModel editedCustomerViewModel = _mapper.ConvertCustomerToCustomerInfoViewModel(editedCustomer);

            return editedCustomerViewModel;
        }

        public StoreInfoViewModel CreateStore(StoreInfoViewModel createdStore)
        {
            StoreLocation store = new StoreLocation()
            {
                StoreLocationName = createdStore.StoreLocationName,
                StoreLocationAddress = createdStore.StoreLocationAddress
            };

            // if null???? 
            StoreLocation newStore = _repo.CreateStore(store);

            if (newStore == null)
            {
                return null;
            }

            StoreInfoViewModel newStoreViewModel = _mapper.ConvertStoreToStoreInfoViewModel(newStore);

            return newStoreViewModel;
        }

        public StoreListViewModel GetStoreList()
        {
            StoreListViewModel storeList = new StoreListViewModel();
            List<StoreInfoViewModel> storeViewModels = new List<StoreInfoViewModel>();
            List<StoreLocation> stores = _repo.GetStoreList();


            foreach (StoreLocation s in stores)
            {
                storeViewModels.Add(_mapper.ConvertStoreToStoreInfoViewModel(s));
            }

            storeList.StoreLoactions = storeViewModels;

            return storeList;
        }


        public StoreInfoViewModel GetStoreById(Guid id)
        {
            StoreLocation storeToEdit = _repo.GetStoreById(id);

            if (storeToEdit == null)
            {
                return null;
            }

            StoreInfoViewModel storeViewToEdit = _mapper.ConvertStoreToStoreInfoViewModel(storeToEdit);
            
            return storeViewToEdit;
        }

        public StoreInfoViewModel EditStore(StoreInfoViewModel storeToEdit)
        {
            StoreLocation store = _repo.GetStoreById(storeToEdit.StoreLocationId);

            store.StoreLocationName = storeToEdit.StoreLocationName;
            store.StoreLocationAddress = storeToEdit.StoreLocationAddress;

            StoreLocation editedStore = _repo.EditStore(store);

            StoreInfoViewModel editedStoreIfoViewModel = _mapper.ConvertStoreToStoreInfoViewModel(editedStore);

            return editedStoreIfoViewModel;
        }

        public ProductListViewModel GetProductList()
        {
            ProductListViewModel productList = new ProductListViewModel();
            List<Product> products = _repo.GetProductList();
            List<ProductInfoViewModel> productsInfo = new List<ProductInfoViewModel>();

            foreach (Product p in products)
            {
                productsInfo.Add(_mapper.ConvertProductToProductInfoViewModel(p));
            }

            productList.Products = productsInfo;

            return productList;
        }

        public ProductInfoViewModel CreateProduct(ProductInfoViewModel newProduct)
        {
            Product product = new Product()
            {
                ProductName = newProduct.ProductName,
                ProductDesc = newProduct.ProductDesc,
                ProductPrice = newProduct.ProductPrice,
                IsAgeRestricted = newProduct.IsAgeRestricted
            };


            Product createProduct = _repo.CreateProduct(product);

            if (createProduct == null)
            {
                return null;
            }

            ProductInfoViewModel newProductInfoViewModel = _mapper.ConvertProductToProductInfoViewModel(createProduct);

            return newProductInfoViewModel;
        }

    }
}
