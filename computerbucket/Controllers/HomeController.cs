using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;

namespace computerbucket.Controllers
{
    public class HomeController : Controller
    {
        private readonly ComputerBucketEntities _db = new ComputerBucketEntities();

        public ActionResult Index()
        {

            return View();
        }

        public ActionResult SearchProducts(string searchTerm)
        {
            if (searchTerm != null && searchTerm != "")
            {
                ViewBag.searchTerm = " results for " + searchTerm;
            }
            else
            {
                ViewBag.searchTerm = " products";
            }
            
            var query = _db.Products.OrderBy(p => p.ProductName).Where(pr => searchTerm == null || pr.ProductName.Contains(searchTerm));
            //Hack
            ViewBag.jpg = ".jpg";
            return View(query);
        }

        public ActionResult PreBuildComputers(int typeId)
        {
            var query = _db.PreBuildPCs.Where(p => p.PCTypeID == typeId).ToList();

            return View(query);
        }

        public ActionResult PreBuildComputer(int id)
        {
            var computer = _db.PreBuildPCs.Find(id);
            ViewBag.Image = _db.Products.Find(int.Parse(computer.ComputerCase)).ProductID;
            ViewBag.jpg = ".jpg";
            ViewBag.PreBuildPCID = computer.PreBuildPCID;

            var computerParts = getPreBuildPCPartsList(computer);

            computer.Price = ComputerPrice(computerParts);
            _db.Entry(computer).State = System.Data.EntityState.Modified;
            _db.SaveChanges();

            if (computer.Price != null && computer.Price != 0)
                ViewBag.price = computer.Price;
            else
                ViewBag.price = "Not Defined!";

            return PartialView("_PreBuildComputer", computerParts);
        }

        public List<Product> getPreBuildPCPartsList(PreBuildPC computer)
        {
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

            return computerParts;
        
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
