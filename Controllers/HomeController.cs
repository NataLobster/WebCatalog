using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WebCatalog.Models;

namespace WebCatalog.Controllers
{
    public class HomeController : Controller
    {
        CatalogContext db = new CatalogContext();
        public IActionResult Index()
        {
            List<CatalogProd> prods = db.CatalogProds.ToList();
            List<Category> categories = db.Categories.ToList();

            List<CatalogProdView> prodView = new List<CatalogProdView>();

            foreach (var item in prods)
            {
                prodView.Add(new CatalogProdView
                {
                    Id = item.Id,
                    //CategoryName = db.Categories.Where(x => x.Id == item.IdCategory).Select(x => x.CategoryName).FirstOrDefault(),
                    ProdName = item.ProdName,
                    DescriptionProd = item.DescriptionProd,
                    Price = item.Price,
                    Remark = item.Remark,
                    SpecialRemark = item.SpecialRemark,
                    Category = categories.FirstOrDefault(c => c.Id == item.IdCategory)

            });
            } 
            
            ViewBag.CatalogProds = prodView;
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            //List<Category> categories = db.Categories.ToList();
            SelectList categories = new SelectList(db.Categories,"Id", "CategoryName");
            ViewBag.CategoryProd = categories;
            return View ();
        }

        [HttpPost]
        public IActionResult Create(CatalogProd create_prods)
        {
                      
            if (ModelState.IsValid)
            {
                //добавляем данные в БД
                //db.CatalogProds.Add(create_prods);
                //db.SaveChanges();

                //через хранимую процедуру
                Microsoft.Data.SqlClient.SqlParameter[] param = new Microsoft.Data.SqlClient.SqlParameter[6];
                param[0] = new Microsoft.Data.SqlClient.SqlParameter("@idCategory", create_prods.IdCategory);
                param[1] = new Microsoft.Data.SqlClient.SqlParameter("@prodName", create_prods.ProdName);
                param[2] = new Microsoft.Data.SqlClient.SqlParameter("@description", create_prods.DescriptionProd);
                param[3] = new Microsoft.Data.SqlClient.SqlParameter("@price", create_prods.Price);
                param[4] = new Microsoft.Data.SqlClient.SqlParameter("@remark", create_prods.Remark);
                if (param[4].Value == null) param[4].Value = DBNull.Value;
                param[5] = new Microsoft.Data.SqlClient.SqlParameter("@specialRemark", create_prods.SpecialRemark);
                if (param[5].Value == null) param[5].Value = DBNull.Value;

                //возвращаем сообщение
                //return ($"Изменения {edit_categories.CategoryName} сохранены!");
                db.Database.ExecuteSqlRaw("dbo.CatalogProdsAdd @idCategory, @prodName, @description, @price, @remark, @specialRemark", param);

                //возвращаем каталог 
                return RedirectToAction("Index");
            }
            // строчечка, которая позволяет увидеть ошибки валидации, если она не проходит (надо только на следующую точку останова поставить и watch-ить)
            //var errors = ModelState.Values.SelectMany(v => v.Errors);
            SelectList categories = new SelectList(db.Categories, "Id", "CategoryName");
            ViewBag.CategoryProd = categories;
            return View("Create");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            //получаем запись из БД по ИД
            SelectList categories = new SelectList(db.Categories, "Id", "CategoryName");
            ViewBag.CategoryProd = categories;
            CatalogProd prods = db.CatalogProds.Find(id);
            // передаем запись
            return View(prods);
        }

        [HttpPost]
        public IActionResult Edit(CatalogProd edit_prods)
        {
            //добавляем данные в БД
            //db.CatalogProds.Add(catalogProd);
            //сохраняем данные
            db.CatalogProds.Update(edit_prods);
            db.SaveChanges();
            //возвращаем каталог 
            return RedirectToAction("Index");

        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            //получаем запись из БД по ИД
            CatalogProd prods = db.CatalogProds.Find(id);
            // удаляем запись
            db.CatalogProds.Remove(prods);
            db.SaveChanges();
            //возвращаем каталог 
            return RedirectToAction("Index");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
