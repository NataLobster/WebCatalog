﻿using Microsoft.AspNetCore.Mvc;
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
            ViewBag.CatalogProds = prods;
            return View();
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            //получаем запись из БД по ИД
            CatalogProd prods = db.CatalogProds.Find(id);
            // передаем запись
            return View(prods);
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
