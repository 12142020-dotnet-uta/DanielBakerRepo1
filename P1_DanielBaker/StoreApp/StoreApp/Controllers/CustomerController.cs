using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogicLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelLayer.Models;
using ModelLayer.ViewModels;

namespace StoreApp.Controllers
{
    public class CustomerController : Controller
    {
        private BusinessLogicClass _logic;

        public CustomerController(BusinessLogicClass logic)
        {
            _logic = logic;
        }

        // GET: CustomerController
        public ActionResult Index(CustomerInfoViewModel loggedInView)
        {
            return View(loggedInView);
        }

        // GET: CustomerController/Details/5
        public ActionResult Details(Guid id)
        {
            CustomerInfoViewModel custonerInfo = _logic.GetCustomerById(id);
            return View(custonerInfo);
        }

        public IActionResult GetAllCustomers()
        {
            CustomerListViewModel customerList = _logic.GetCustomerList();
            return View(customerList);
        }


        // GET: CustomerController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CustomerController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CustomerController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CustomerController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
