﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogicLayer;
using Microsoft.AspNetCore.Mvc;
using ModelLayer.ViewModels;

namespace StoreApp.Controllers
{
    public class ProductController : Controller
    {
  
        private BusinessLogicClass _logic;

        public ProductController(BusinessLogicClass logic)
        {
            _logic = logic;
        }

        public IActionResult Index()
        {
            ProductListViewModel productList = _logic.GetProductList();
            return View(productList);
        }

        public IActionResult CreateProduct()
        {
            return View();
        }

        public IActionResult CreateNewProduct(ProductInfoViewModel newProduct)
        {

            ProductInfoViewModel productCreated = _logic.CreateProduct(newProduct);

            if (productCreated == null)
            {
                ModelState.AddModelError("Failure", "Product already exists");
                return View("CreateProduct");
            }

            return RedirectToAction("Index");
        }




    }
}