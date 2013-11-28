using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace computerbucket.Controllers
{
    public class BuildPCController : Controller
    {
        //
        // GET: /BuildPC/

        //Connection to the database
        private computerbucketEntities db = new computerbucketEntities();

        public ActionResult Index()
        {
            ViewBag.motherboardList = db.Motherboard_tbl.OrderBy(m => new {m.motherboard_id, m.brand});

            var model = db.Motherboard_tbl.OrderBy(m=>m.brand);

            return View();
        }

    }
}
