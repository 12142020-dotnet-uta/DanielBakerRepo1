using System;
using System.Collections.Generic;
using ModelLayer.Models;
using ModelLayer.ViewModels;
using RepositoryLayer;
using System.Linq;


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
            StoreLocation favoriteStore = _repo.GetStoreByName(createdPlayer.StoreNameChosen);

            Customer customer = new Customer()
            {
                CustomerUserName = createdPlayer.CustomerUserName,
                CustomerPassword = createdPlayer.CustomerPassword,
                CustomerFName = createdPlayer.CustomerFName,
                CustomerLName = createdPlayer.CustomerLName,
                CustomerAge = createdPlayer.CustomerAge,
                CustomerBirthday = createdPlayer.CustomerBirthday,
                PerferedStore = favoriteStore
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

            StoreLocation favoriteStore = _repo.GetStoreByName(customerToEdit.StoreNameChosen);

            customer.CustomerFName = customerToEdit.CustomerFName;
            customer.CustomerLName = customerToEdit.CustomerLName;
            customer.CustomerAge = customerToEdit.CustomerAge;
            customer.CustomerBirthday = customerToEdit.CustomerBirthday;
            customer.PerferedStore = favoriteStore;

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

            storeList.StoreLocations = storeViewModels;

            return storeList;
        }

        public List<string> GetStoreNames()
        {
            List<string> storeNames = _repo.GetStoreNames();
            return storeNames;
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

        public StoreLocation GetNewStoreById(Guid id)
        {
            StoreLocation store = _repo.GetStoreById(id);
            return store;
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

        public ProductInfoViewModel GetProductById(Guid id)
        {
            Product product = _repo.GetProductById(id);

            if (product == null)
            {
                return null;
            }

            ProductInfoViewModel productDetails = _mapper.ConvertProductToProductInfoViewModel(product);
            return productDetails;
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


        public ProductInfoViewModel EditProduct(ProductInfoViewModel productToEdit)
        {
            Product product = _repo.GetProductById(productToEdit.ProductID);

            if (product == null)
            {
                return null;
            }

            product.ProductName = productToEdit.ProductName;
            product.ProductDesc = productToEdit.ProductDesc;
            product.ProductPrice = productToEdit.ProductPrice;
            product.IsAgeRestricted = productToEdit.IsAgeRestricted;

            Product editedProduct = _repo.EditProduct(product);

            ProductInfoViewModel editedProductView = _mapper.ConvertProductToProductInfoViewModel(editedProduct);

            return editedProductView;
        }

        public ShoppingListViewModel GetStoreInventory(Guid storeId)
        {
            StoreLocation store = _repo.GetStoreById(storeId);

            // TODO: handle this in controller
            if (store == null)
            {
                return null;
            }

            ShoppingListViewModel storeInventory = new ShoppingListViewModel();

            List<InventoryInfoViewModel> inventoryViewModels = new List<InventoryInfoViewModel>();

            List<Inventory> inventories = _repo.GetStoreInventory(store);

            foreach (Inventory i in inventories)
            {
                inventoryViewModels.Add(_mapper.ConvertInventoryToInventoryInfoViewModel(i));
            }

            storeInventory.StoreLocation = _mapper.ConvertStoreToStoreInfoViewModel(store);
            storeInventory.Inventories = inventoryViewModels;

            return storeInventory;
        }

        public AddInventoryViewModel AddInventory(Guid storeId)
        {
            AddInventoryViewModel addInventoryViewModel = new AddInventoryViewModel();

            StoreLocation store = _repo.GetStoreById(storeId);

            // TODO: handle this in controller
            if (store == null)
            {
                return null;
            }

            //handing products
            List<Product> products = _repo.GetProductList();

            List<ProductInfoViewModel> productInfoViews = new List<ProductInfoViewModel>();

            foreach (Product p in products)
            {
                productInfoViews.Add(_mapper.ConvertProductToProductInfoViewModel(p));
            }

            //handling inventory for the store
            List<Inventory> inventories = _repo.GetStoreInventory(store);

            List<InventoryInfoViewModel> inventoryViewModels = new List<InventoryInfoViewModel>();
            foreach (Inventory i in inventories)
            {
                inventoryViewModels.Add(_mapper.ConvertInventoryToInventoryInfoViewModel(i));
            }

            // filling AddInventoryViewModel
            // TODO: Mapper this
            addInventoryViewModel.Store = _mapper.ConvertStoreToStoreInfoViewModel(store);
            addInventoryViewModel.Products = productInfoViews;
            addInventoryViewModel.Inventory = inventoryViewModels;

            return addInventoryViewModel;
        }

        public ShoppingListViewModel AddNewInventory(Guid inventoryStore, string productName, int qantityAdded)
        {
            ShoppingListViewModel inventoryList = new ShoppingListViewModel();

            Product product = _repo.GetProductByName(productName);

            if (product == null)
            {
                return null;
            }

            StoreLocation store = _repo.GetStoreById(inventoryStore);

            Inventory inventory = new Inventory()
            {
                Product = product,
                StoreLocation = store,
                ProductQuantity = qantityAdded
            };

            Inventory newInventory = _repo.AddNewInventory(inventory);

            inventoryList = GetStoreInventory(newInventory.StoreLocation.StoreLocationId);

            return inventoryList;
        }

        public CartInfoViewModel GetCustomerCartAtStore(Guid storeId, Guid customerId)
        {
            StoreLocation store = _repo.GetStoreById(storeId);
            Customer customer = _repo.GetCustomerById(customerId);

            if (store == null || customer == null)
            {
                return null;
            }

            Order cart = _repo.GetOrderByStoreAndCustomer(store.StoreLocationId, customer.CustomerID);

            // TODO: creating new order if order doesnt exist. need to abstract out
            if (cart == null)
            {
                Order newCart = new Order()
                {
                    Store = store,
                    Customer = customer,
                    isCart = true
                };

                cart = _repo.CreateOrder(newCart);

                if (cart == null)
                {
                    return null;
                }
            }


            List<OrderLineDetails> orderLineDetails = _repo.GetOrderLineListByCart(cart);

            cart.ProductsInOrder = orderLineDetails;

            CartInfoViewModel cartInfo = _mapper.ConvertOrderToCartInfoViewModel(cart);


            return cartInfo;


        }

        public CartListViewModel GetUserCartList(Guid customerId)
        {
            CartListViewModel cartList = new CartListViewModel();

            List<CartInfoViewModel> carts = new List<CartInfoViewModel>();

            Customer customer = _repo.GetCustomerById(customerId);

            if (customer == null)
            {
                return null;
            }

            List<Order> customerCarts = _repo.GetCartsByCustomerId(customer.CustomerID);

            foreach (Order cart in customerCarts)
            {
                List<OrderLineDetails> orderLineDetails = _repo.GetOrderLineListByCart(cart);
                cart.ProductsInOrder = orderLineDetails;
                carts.Add(_mapper.ConvertOrderToCartInfoViewModel(cart));
            }

            cartList.CurrentCarts = carts;

            return cartList;
        }

        public CartInfoViewModel AddToCart(string productName, int quantity, Guid storeId, Guid customerId)
        {

            StoreLocation store = _repo.GetStoreById(storeId);
            Customer customer = _repo.GetCustomerById(customerId);
            Order cart = _repo.GetOrderByStoreAndCustomer(store.StoreLocationId, customer.CustomerID);

            // TODO: creating new order if order doesnt exist. need to abstract out
            if (cart == null)
            {
                Order newCart = new Order()
                {
                    Store = store,
                    Customer = customer,
                    isCart = true
                };

                cart = _repo.CreateOrder(newCart);

                if (cart == null)
                {
                    return null;
                }
            }

            Product product = _repo.GetProductByName(productName);
            
            Inventory inventory = _repo.GetInventoryByStoreAndName(store, product);

            if (store == null || customer == null || product == null || inventory == null)
            {
                return null;
            }

            try
            {
                OrderLineDetails orderLine = _repo.AddOrderLineDetail(inventory, cart, quantity);
            }
            catch (Exception e)
            {
                CartInfoViewModel badQuantity = new CartInfoViewModel()
                {
                    error = 1,
                    errorMessage = e.Message
                };
                return badQuantity;
            }
           

            cart = _repo.UpdateCartPrice(cart);

            List<OrderLineDetails> orderLineDetails = _repo.GetOrderLineListByCart(cart);

            cart.ProductsInOrder = orderLineDetails;


            CartInfoViewModel cartInfo = _mapper.ConvertOrderToCartInfoViewModel(cart);

            return cartInfo;
        }


        public CartInfoViewModel CheckoutCart(Guid customerId, Guid cartId)
        {
            // should validate customer somewhere
            Order order = _repo.GetOrderById(cartId);
            StoreLocation store = order.Store;
            List<OrderLineDetails> orderLineDetails = _repo.GetOrderLineListByCart(order);
            List<Inventory> inventory = _repo.GetStoreInventory(store);

            try
            {
                _repo.RemoveInventoryBasedOnOrder(orderLineDetails, inventory);
            }
            catch (Exception e)
            {
                CartInfoViewModel badQuantity = new CartInfoViewModel()
                {
                    Store = _mapper.ConvertStoreToStoreInfoViewModel(store),
                    error = 1,
                    errorMessage = e.Message
                };
                return badQuantity;
            }
           

            order.ProductsInOrder = orderLineDetails;

            order = _repo.CheckoutCart(order);

            CartInfoViewModel orderInfo = _mapper.ConvertOrderToCartInfoViewModel(order);

            return orderInfo;
        }


        public CartInfoViewModel GetPastOrderById(Guid orderId)
        {
            Order order = _repo.GetOrderById(orderId);

            if (order == null)
            {
                return null;
            }

            List<OrderLineDetails> orderLineDetails = _repo.GetOrderLineListByCart(order);

            order.ProductsInOrder = orderLineDetails;

            CartInfoViewModel orderInfo = _mapper.ConvertOrderToCartInfoViewModel(order);

            return orderInfo;
        }

        public CartListViewModel GetUserPastOrders(Guid customerId)
        {
            CartListViewModel orderList = new CartListViewModel();

            List<CartInfoViewModel> orders = new List<CartInfoViewModel>();

            Customer customer = _repo.GetCustomerById(customerId);

            if (customer == null)
            {
                return null;
            }

            List<Order> customerOrders = _repo.GetOrdersByCustomerId(customer.CustomerID);

            foreach (Order order in customerOrders)
            {
                List<OrderLineDetails> orderLineDetails = _repo.GetOrderLineListByCart(order);
                order.ProductsInOrder = orderLineDetails;
                orders.Add(_mapper.ConvertOrderToCartInfoViewModel(order));
            }

            orderList.CurrentCarts = orders;

            return orderList;
        }

        public CartListViewModel GetStorePastOrders(Guid StoreId)
        {
            CartListViewModel orderList = new CartListViewModel();

            List<CartInfoViewModel> orders = new List<CartInfoViewModel>();

            StoreLocation store = _repo.GetStoreById(StoreId);

            if (store == null)
            {
                return null;
            }

            List<Order> storeOrders = _repo.GetOrdersByStoreId(store.StoreLocationId);

            foreach (Order order in storeOrders)
            {
                List<OrderLineDetails> orderLineDetails = _repo.GetOrderLineListByCart(order);
                order.ProductsInOrder = orderLineDetails;

                orders.Add(_mapper.ConvertOrderToCartInfoViewModel(order));
            }

            orderList.CurrentCarts = orders;

            return orderList;
        }



        public CartInfoViewModel DeleteOrderLineItem(Guid orderLineId)
        {
            Order order = _repo.GetCartByOrderLineId(orderLineId);

            if (order == null)
            {
                return null;
            }

            bool isDeleted = _repo.DeleteOrderLineById(orderLineId);

            if (isDeleted == false)
            {
                return null;
            }

            List<OrderLineDetails> orderLineDetails = _repo.GetOrderLineListByCart(order);

            order.ProductsInOrder = orderLineDetails;

            order = _repo.UpdateCartPrice(order);

            CartInfoViewModel orderInfo = _mapper.ConvertOrderToCartInfoViewModel(order);

            return orderInfo;
        }




    }
}
