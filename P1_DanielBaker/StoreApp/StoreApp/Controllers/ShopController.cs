using System;
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
            InventoryListViewModel storeInventory = _logic.GetStoreInventory(id);
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
            InventoryListViewModel currentStoreInventory = _logic.AddNewInventory(newInventory.StoreId, newInventory.ProductName, newInventory.QuantityAdded);
            if (currentStoreInventory == null)
            {
                ModelState.AddModelError("Failure", "Product does not exist");
                return RedirectToAction("Index");
            }
            return View("Store", currentStoreInventory);
        }



    }
}
