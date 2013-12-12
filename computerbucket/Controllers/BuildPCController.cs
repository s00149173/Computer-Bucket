using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using PagedList;

namespace computerbucket.Controllers
{
    public class BuildPCController : Controller
    {
        //
        // GET: /BuildPC/

        //Connection to the database
        private computerbucketEntities db = new computerbucketEntities();

        public ActionResult Index(int? page)
        {
            int pageNumber = page ?? 1;
            int pageSize = 5;

            //ViewBag.motherboardList = db.Motherboard_tbl.OrderBy(m => new { m.motherboard_id, m.brand }).ToPagedList(pageNumber, pageSize);
            
            
            if (Request.IsAjaxRequest())
            {
                //ViewBag.processorList = (this.ListOfPartsCompatible("Processor", chosenMotherboard)).ToPagedList(pageNumber, pageSize);
                //ViewBag.graphicCardList = (this.ListOfPartsCompatible("Graphic Card", chosenMotherboard)).ToPagedList(pageNumber, pageSize);
                //ViewBag.RAMList = (this.ListOfPartsCompatible("RAM", chosenMotherboard)).ToPagedList(pageNumber, pageSize);
                var mothbs = db.Motherboard_tbl.OrderBy(m => new { m.motherboard_id, m.brand }).ToPagedList(pageNumber, pageSize);
                return PartialView("_ViewComponents", mothbs);
            }
            var motherbs  = db.Motherboard_tbl.OrderBy(m => new { m.motherboard_id, m.brand }).ToPagedList(pageNumber, pageSize);
            return View(motherbs);
        }

        public ActionResult _MotherboardInfo(int chosenMotherboard)
        {
            var model = db.Motherboard_tbl.Where(m => m.motherboard_id == chosenMotherboard);
            return PartialView("_MotherboardInfo", model);
        }

        public ActionResult _Processors(int id, int? page)
        {
            int pageNumber = page ?? 1;
            int pageSize = 5;
            ViewBag.motherboardId = id;
            var category = db.Category_tbl.Where(c => c.description == "Processor").Select(c => c.category_id);
            int categoryID=int.Parse(category.First().ToString());

            var processors = db.selectPartCompatible(id, categoryID).ToList().ToPagedList(pageNumber, pageSize);

            return PartialView("_Processors", processors);
            
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