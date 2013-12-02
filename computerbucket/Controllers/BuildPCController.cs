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

        public ActionResult Index(int chosenMotherboard = 0)
        {
            ViewBag.motherboardList = db.Motherboard_tbl.OrderBy(m => new { m.motherboard_id, m.brand }).ToList();

            if (Request.IsAjaxRequest() && chosenMotherboard != 0)
            {
                ViewBag.processorList = this.ListOfPartsCompatible("Processor", chosenMotherboard);
                ViewBag.graphicCardList = this.ListOfPartsCompatible("Graphic Card", chosenMotherboard);
                ViewBag.RAMList = this.ListOfPartsCompatible("RAM", chosenMotherboard);
                //ViewBag.motherboard = db.Motherboard_tbl.Where(m => m.motherboard_id == chosenMotherboard);
                //this._MotherboardInfo(chosenMotherboard);
                return PartialView("_ViewComponents");
            }

            return View();
        }

        public ActionResult _MotherboardInfo(int chosenMotherboard)
        {
            var model = db.Motherboard_tbl.Where(m => m.motherboard_id == chosenMotherboard);
            return PartialView("_MotherboardInfo", model);
        }

        private List<SelectListItem> ListOfPartsCompatible(string category, int chosenMotherboard)
        {
            var result = new List<SelectListItem>();
            //var ctx = new YourContext();

            var items = from ct in db.Comparison_tbl.AsEnumerable()
                        where (ct.motherboard_id == chosenMotherboard)
                        join p in db.Parts_tbl.AsEnumerable() on ct.part_id equals p.part_id
                        join c in db.Category_tbl.AsEnumerable() on p.category_id equals c.category_id
                        where c.description.Equals(category)
                        select new SelectListItem
                        {
                            Text = p.title,
                            Value = p.part_id.ToString()

                        };

            foreach (var item in items)
                result.Add(item);
            return result;
        }
    }
}