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

        [HttpGet]
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
            ViewBag.ComputerCases = _db.Products.Select(m => new { brand = m.ProductName, id = m.ProductID, category = m.CategoryID }).Where(c => c.category == 14);

            return View();
        }

        [HttpPost]
        public ActionResult Index(BuildPCModel pc)
        {
            BuildPC aux = new BuildPC { Motherboad = pc.Motherboard, Processor = pc.Processor, GraphicCard = pc.GraphicCard, RAM = pc.GraphicCard, Hard_Drive = pc.HardDrive, SSD = pc.Ssd, PowerSupply = pc.PowerSupply, CPUCooling = pc.CpuCooling, ThermalPaste = pc.ThermalPaste, InternalDrive = pc.InternalDrive, OperatingSystem = pc.Os, ComputerCase = pc.ComputerCase };
            _db.BuildPCs.Add(aux);
            _db.SaveChanges();

            var computer = _db.BuildPCs.Find(aux.BuildPCID);
            var computerParts = new List<Product>()
            {
                _db.Products.Find(int.Parse(computer.Motherboad)),
                _db.Products.Find(int.Parse(computer.Processor)),
                _db.Products.Find(int.Parse(computer.GraphicCard)),
                _db.Products.Find(int.Parse(computer.RAM)),
                _db.Products.Find(int.Parse(computer.Hard_Drive)),
                _db.Products.Find(int.Parse(computer.SSD)),
                _db.Products.Find(int.Parse(computer.PowerSupply)),
                _db.Products.Find(int.Parse(computer.CPUCooling)),
                _db.Products.Find(int.Parse(computer.ThermalPaste)),
                _db.Products.Find(int.Parse(computer.InternalDrive)),
                _db.Products.Find(int.Parse(computer.OperatingSystem)),
                _db.Products.Find(int.Parse(computer.ComputerCase))
            };

            computer.Price = ComputerPrice(computerParts);
            _db.Entry(computer).State = System.Data.EntityState.Modified;
            _db.SaveChanges();

            return RedirectToAction("InsertBuildPc", "Orders", new { id = aux.BuildPCID });
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

        public decimal ComputerPrice(List<Product> products)
        {
            decimal finalPrice = 0.0m;
            foreach (var prod in products)
            {
                finalPrice += (decimal)prod.UnitPrice;
            }
            return finalPrice;
        }

      
    }
}