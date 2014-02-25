using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using computerbucket.Models;
using PagedList;

namespace computerbucket.Controllers
{
    public class BuildPCController : Controller
    {       
        private readonly ComputerBucketEntities _db = new ComputerBucketEntities();

        public ActionResult Index()
        {
            ViewBag.motherboardList = _db.Products.Select(m => new { brand = m.ProductName, id = m.ProductID, category = m.CategoryID }).Where(c => c.category == 1);
            ViewBag.Processors = _db.Products.Select(m => new { brand = m.ProductName, id = m.ProductID, category = m.CategoryID }).Where(c => c.category == 2);
            ViewBag.RAMs = _db.Products.Select(m => new { brand = m.ProductName, id = m.ProductID, category = m.CategoryID }).Where(c => c.category == 3);
            ViewBag.HardDrives = _db.Products.Select(m => new { brand = m.ProductName, id = m.ProductID, category = m.CategoryID }).Where(c => c.category == 4);
            ViewBag.SSDs = _db.Products.Select(m => new { brand = m.ProductName, id = m.ProductID, category = m.CategoryID }).Where(c => c.category == 5);
            ViewBag.PowerSupplies = _db.Products.Select(m => new { brand = m.ProductName, id = m.ProductID, category = m.CategoryID }).Where(c => c.category == 6);
            ViewBag.CPUCooling = _db.Products.Select(m => new { brand = m.ProductName, id = m.ProductID, category = m.CategoryID }).Where(c => c.category == 7);
            ViewBag.ThermalPaste = _db.Products.Select(m => new { brand = m.ProductName, id = m.ProductID, category = m.CategoryID }).Where(c => c.category == 8);
            ViewBag.InternalDrives = _db.Products.Select(m => new { brand = m.ProductName, id = m.ProductID, category = m.CategoryID }).Where(c => c.category == 9);
            ViewBag.OperatingSystems = _db.Products.Select(m => new { brand = m.ProductName, id = m.ProductID, category = m.CategoryID }).Where(c => c.category == 12);
            ViewBag.GraphicCards = _db.Products.Select(m => new { brand = m.ProductName, id = m.ProductID, category = m.CategoryID }).Where(c => c.category == 13);

            return View();
        }

        public ActionResult ProdDetails(int id)
        {         
            var prod = from e in _db.Products.Where(e => e.ProductID == id)
                      select new ProductModel()
                      {
                           ProductId = e.ProductID,
                           ProductName = e.ProductName,
                           UnitPrice = (decimal) e.UnitPrice,
                           UnitsInStock = (int) e.UnitsInStock,
                           ImageUrl = e.ImageUrl                                                                                                
                      };          
            return PartialView("_prodDetails", prod);
           
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Order order)
        {
            return View();
        }

      
    }
}