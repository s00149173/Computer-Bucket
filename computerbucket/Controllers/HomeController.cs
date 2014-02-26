using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
            var query = _db.Products.OrderBy(p => p.ProductName).Where(pr => searchTerm == null || pr.ProductName.Contains(searchTerm));
            return View(query);
        }

        public ActionResult About()
        {
            

            return View();
        }

        public ActionResult Contact()
        {
            

            return View();
        }

        public ActionResult PreBuildComputers(int typeId)
        {
            var query = _db.PreBuildPCs.Where(p => p.PCTypeID == typeId).ToList();

            return View(query);
        }

        
    }
}
