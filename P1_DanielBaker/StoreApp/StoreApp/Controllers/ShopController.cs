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

            return View(storeToEdit);
        }

        public IActionResult EditStore(StoreInfoViewModel storeToEdit)
        {
            StoreInfoViewModel editedStore = _logic.EditStore(storeToEdit);
            return View("Details", editedStore);
        }

        public IActionResult Store(Guid id)
        {
            return View();
        }

        
    }
}
