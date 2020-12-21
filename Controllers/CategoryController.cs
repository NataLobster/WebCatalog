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
        public IActionResult Edit(int id)
        {
            //получаем запись из БД по ИД
            Category categories = db.Categories.Find(id);
            // передаем запись
            return View(categories);
        }

        [HttpPost]
        public string Edit(CatalogProd catalogProd)
        {
            //добавляем данные в БД
            //db.CatalogProds.Add(catalogProd);
            //сохраняем данные
            db.SaveChanges();
            //возвращаем сообщение
            return ($"Изменения сохранены!");

        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}