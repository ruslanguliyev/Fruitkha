﻿using Bussines.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Areas.Dashboard.Controllers
{
    [Area("Dashboard")]
    public class CategoryController : Controller
    {
        private readonly ICategoryManager _categoryManager;

        public CategoryController(ICategoryManager categoryManager)
        {
            _categoryManager = categoryManager;
        }

        public IActionResult Index()
        {
            var categories = _categoryManager.GetAll();
            return View(categories);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Category category)
        {
            
            try
            {
                var categoryName = _categoryManager.RepeatName(category);   
                if (categoryName != null)
                {
                    return View();  
                }
                else
                {
                    ModelState.AddModelError("Name", "category eyni ola bilmez");
                    return View(category);
                }
                category.SeoUrl = "test";
                _categoryManager.Add(category);
                return RedirectToAction("Index");
               

            }
            catch (Exception)
            {

                return View();
            }


        }
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var category = _categoryManager.GetByCategoryId(id);
            _categoryManager.Remove(category);
            return View();
        }

    }
}
