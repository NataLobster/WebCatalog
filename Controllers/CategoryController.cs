using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WebCatalog.Models;

namespace WebCatalog.Controllers
{
    public class CategoryController : Controller
    {
        CatalogContext db = new CatalogContext();
        public IActionResult Index()
        {
            List<Category> categories = db.Categories.ToList();
            ViewBag.Categories = categories;
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Category edit_categories)
        {
            //добавляем данные в БД
            //db.CatalogProds.Add(catalogProd);
            //сохраняем данные
            db.Categories.Add(edit_categories);
            db.SaveChanges();
            //возвращаем каталог 
            return RedirectToAction("Index");

        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            //получаем запись из БД по ИД
            Category categories = db.Categories.Find(id);
            // передаем запись
            return View(categories);
        }

        [HttpPost]
        public IActionResult Edit(Category edit_categories)
        {
            //добавляем данные в БД
            //db.CatalogProds.Add(catalogProd);
            //сохраняем данные
            db.Categories.Update(edit_categories);
            db.SaveChanges();
            //возвращаем сообщение
            //return ($"Изменения {edit_categories.CategoryName} сохранены!");
            return RedirectToAction("Index");
                  
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}