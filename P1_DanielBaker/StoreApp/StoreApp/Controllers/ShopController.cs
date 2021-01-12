﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogicLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelLayer.ViewModels;

namespace StoreApp.Controllers
{
    public class ShopController : Controller
    {
        private BusinessLogicClass _logic;

        public ShopController(BusinessLogicClass logic)
        {
            _logic = logic;
        }

        public IActionResult Index()
        {
            StoreListViewModel storeList = _logic.GetStoreList();
            return View(storeList);
        }

        public IActionResult CreateStore()
        {
            return View();
        }

        public IActionResult CreateNewStore(StoreInfoViewModel createNewStore)
        {
            StoreInfoViewModel storeCreated = _logic.CreateStore(createNewStore);

            if (storeCreated == null)
            {
                ModelState.AddModelError("Failure", "Store name already exists");
                return View("CreateStore");
            }

            return RedirectToAction("Index");
        }

        public IActionResult Details(Guid id)
        {
            StoreInfoViewModel storeInfoToView = _logic.GetStoreById(id);
            if (storeInfoToView == null)
            {
                ModelState.AddModelError("Failure", "Store does not exist");
                return View(storeInfoToView);
            }

            return View(storeInfoToView);
        }

        public IActionResult Edit(Guid id)
        {
            StoreInfoViewModel storeToEdit = _logic.GetStoreById(id);
            if (storeToEdit == null)
            {
                ModelState.AddModelError("Failure", "Store does not exist");
                return View(storeToEdit);
            }

            return View(storeToEdit);
        }

        public IActionResult EditStore(StoreInfoViewModel storeToEdit)
        {
            StoreInfoViewModel editedStore = _logic.EditStore(storeToEdit);
            if (editedStore == null)
            {
                ModelState.AddModelError("Failure", "Store does not exist");
                return View("Edit", editedStore);
            }
            return View("Details", editedStore);
        }

        public IActionResult Store(Guid id)
        {
            ShoppingListViewModel storeInventory = _logic.GetStoreInventory(id);
            return View(storeInventory);
        }

        [Route("shop/store/addinventory/{id}")]
        public IActionResult AddInventory(Guid id)
        {
            AddInventoryViewModel addInventory = _logic.AddInventory(id);
            if (addInventory == null)
            {
                ModelState.AddModelError("Failure", "Store does not exist");
                return RedirectToAction("Index");
            }
            return View(addInventory);
        }

        public IActionResult AddNewInventory(AddInventoryViewModel newInventory)
        {
            ShoppingListViewModel currentStoreInventory = _logic.AddNewInventory(newInventory.StoreId, newInventory.ProductName, newInventory.QuantityAdded);
            if (currentStoreInventory == null)
            {
                ModelState.AddModelError("Failure", "Product does not exist");
                return RedirectToAction("Index");
            }
            return View("Store", currentStoreInventory);
        }

        // handle null
        public IActionResult ViewCart(Guid StoreId)
        {
            string sessionId = HttpContext.Session.GetString("customerId");

            if (sessionId == null)
            {
                ModelState.AddModelError("Failure", "User is not logged in");
                return View("Index");
            }

            Guid customerId = new Guid(sessionId);

            CartInfoViewModel cartView = _logic.GetCustomerCartAtStore(StoreId, customerId);

            if (cartView == null)
            {
                ModelState.AddModelError("Failure", "Customer or store does not exist");
                return View("Index");
            }

            return View(cartView);
        }

        // handle null
        public IActionResult ViewCarts()
        {
            string sessionId = HttpContext.Session.GetString("customerId");

            if (sessionId == null)
            {
                ModelState.AddModelError("Failure", "User is not logged in");
                return View("Index");
            }

            Guid customerId = new Guid(sessionId);

            CartListViewModel cartList = _logic.GetUserCartList(customerId);

            if (cartList == null)
            {
                ModelState.AddModelError("Failure", "Customer does not exist");
                return View("Index");
            }

            return View(cartList);
        }

        public IActionResult AddToCart(ShoppingListViewModel cart)
        {
            string sessionId = HttpContext.Session.GetString("customerId");

            Guid customerId = new Guid(sessionId);

            CartInfoViewModel updatedCart = _logic.AddToCart(cart.ProductName, cart.QuantityAdded, cart.StoreId, customerId);


            if (updatedCart == null)
            {
                ModelState.AddModelError("Failure", "Customer, Product, Store or Inventory does not exist");
                ShoppingListViewModel storeInventory = _logic.GetStoreInventory(cart.StoreId);
                return View("Store", storeInventory);
            }

            // if you are trying too add too much quantity
            if (updatedCart.error == 1)
            {
                ModelState.AddModelError("Failure", $"{updatedCart.errorMessage}");
                ShoppingListViewModel storeInventory = _logic.GetStoreInventory(cart.StoreId);
                return View("Store", storeInventory);
            }

            return View("ViewCart", updatedCart);
        }

        public IActionResult Checkout(Guid cartId)
        {
            string sessionId = HttpContext.Session.GetString("customerId");

            if (sessionId == null)
            {
                ModelState.AddModelError("Failure", "User is not logged in");
                return View("Index");
            }

            Guid customerId = new Guid(sessionId);

            CartInfoViewModel checkedoutOrder = _logic.CheckoutCart(customerId, cartId);

            if (checkedoutOrder.error == 1)
            {
                ModelState.AddModelError("Failure", $"{checkedoutOrder.errorMessage}");
                return View("Cart", checkedoutOrder.Store.StoreLocationId);
            }

            return RedirectToAction("ViewPastOrder", checkedoutOrder.OrderId);
        }

        // handle null
        public IActionResult ViewPastOrder(Guid orderId)
        {
            string sessionId = HttpContext.Session.GetString("customerId");

            if (sessionId == null)
            {
                ModelState.AddModelError("Failure", "User is not logged in");
                return View("Index");
            }

            Guid customerId = new Guid(sessionId);

            CartInfoViewModel pastOrder = _logic.GetPastOrderById(orderId);

            if (pastOrder == null)
            {
                ModelState.AddModelError("Failure", "Customer or store does not exist");
                return View("Index");
            }

            return View(pastOrder);
        }

        // handle null
        // look at routing
        public IActionResult ViewPastOrders()
        {
            string sessionId = HttpContext.Session.GetString("customerId");

            if (sessionId == null)
            {
                ModelState.AddModelError("Failure", "User is not logged in");
                return View("Index");
            }

            Guid customerId = new Guid(sessionId);

            CartListViewModel orderList = _logic.GetUserPastOrders(customerId);

            if (orderList == null)
            {
                ModelState.AddModelError("Failure", "Customer does not exist");
                return View("Index");
            }

            return View(orderList);
        }

        public IActionResult ViewStorePastOrders(Guid StoreId)
        {
            CartListViewModel orderList = _logic.GetStorePastOrders(StoreId);
            if (orderList == null)
            {
                ModelState.AddModelError("Failure", "Store does not exist ");
                return View(orderList);
            }

            return View(orderList);
        }

    }
}
