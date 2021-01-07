using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogicLayer;
using Microsoft.AspNetCore.Mvc;
using ModelLayer.ViewModels;

namespace StoreApp.Controllers
{
    public class LoginController : Controller
    {
        private BusinessLogicClass _logic;

        public LoginController(BusinessLogicClass logic)
        {
            _logic = logic;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult LoginCustomer(LoginViewModel loginView)
        {
            CustomerInfoViewModel customerLoggingIn = _logic.LoginUser(loginView);
            if (customerLoggingIn == null)
            {
                return RedirectToAction("PlayerDoesNotExist");
            }
            return RedirectToAction("Index", "Customer", customerLoggingIn);
        }

        [ActionName("NewCustomer")]
        public IActionResult LoginCustomer(CustomerInfoViewModel newUser)
        {
            if (newUser == null)
            {
                return RedirectToAction("PlayerDoesNotExist");
            }
            return RedirectToAction("Index", "Customer", newUser);
        }

        public IActionResult CreateCustomer()
        {
            return View();
        }

        public IActionResult CreateNewCustomer(CreateCustomerViewModel signUpView)
        {
            CustomerInfoViewModel playerCreated = _logic.CreateUser(signUpView);

            if (playerCreated == null)
            {
                return RedirectToAction("PlayerAlreadyExists");
            }

            return RedirectToAction("NewCustomer", playerCreated);
        }

        public IActionResult PlayerDoesNotExist()
        {
            return View();
        }

        public IActionResult PlayerAlreadyExists()
        {
            return View();
        }
    }
}
