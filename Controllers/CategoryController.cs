using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
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
            //List<Category> categories = db.Categories.ToList();
            IEnumerable<Category> catt = db.Categories.FromSqlRaw("SELECT * FROM Categories ORDER BY CategoryName");
            //ViewBag.Categories = categories;
            ViewBag.Categories = catt;
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Category create_categories)
        {
            //добавление через хранимую процедуру
            var param = new Microsoft.Data.SqlClient.SqlParameter("@name", create_categories.CategoryName);
            db.Database.ExecuteSqlRaw("dbo.CategoriesAdd @name", param);
            
            //добавляем данные в БД
            //*сохраняем данные через EF
            //db.Categories.Add(create_categories);
            //db.SaveChanges();

            //возвращаем категории 
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
            //db.Categories.Update(edit_categories);
            //db.SaveChanges();

            //через хранимую процедуру
            object[] param = new object[2];
            param[0] = new Microsoft.Data.SqlClient.SqlParameter("@name", edit_categories.CategoryName);
            param[1] = new Microsoft.Data.SqlClient.SqlParameter("@id", edit_categories.Id);
            
            //возвращаем сообщение
            //return ($"Изменения {edit_categories.CategoryName} сохранены!");
            db.Database.ExecuteSqlRaw("dbo.CategoriesEdit @name, @id", param);
            return RedirectToAction("Index");

        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}