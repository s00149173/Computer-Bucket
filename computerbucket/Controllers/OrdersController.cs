using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace computerbucket.Controllers
{
    public class OrdersController : Controller
    {
        private ComputerBucketEntities db = new ComputerBucketEntities();

        public ActionResult Index()
        {


            //var cookie = Convert.ToInt32(Request.Cookies["OrderId"].Value);
            if (Request.Cookies["OrderId"] == null)
            {
                ViewBag.OrderStatus = "You have no items for the moment!";
                var order = db.Orders.Find(1);
                return View(order);
            }
            else
            {
                var query = db.Orders.Find(Int32.Parse(Request.Cookies["OrderId"].Value));
                return View(query);

            }
        }

        public ActionResult InsertBuildPc(int id)
        {
            //int cookie = Convert.ToInt32(Request.Cookies["OrderId"].Value);
            if (Request.Cookies["OrderId"] == null)
            {
                Order o = new Order { OrderDate = DateTime.Now };
                db.Orders.Add(o);
                db.SaveChanges();

                HttpCookie cookie = new HttpCookie("OrderId");
                cookie.Value = o.OrderID + "";
                //cookie.Expires = DateTime.Now.AddMinutes(60);
                Response.Cookies.Add(cookie);
            }

            OrderItem item = new OrderItem { BuildPCID = id, OrderID = Int32.Parse(Request.Cookies["OrderId"].Value), Discount = 0, Quantity = 1 };
            db.OrderItems.Add(item);
            db.SaveChanges();



            return RedirectToAction("Index", "Orders");
        }

        public ActionResult InsertPreBuildPc(int id)
        {

            bool inserted = false;
            //int cookie = Convert.ToInt32(Request.Cookies["OrderId"].Value);
            if (Request.Cookies["OrderId"] == null)
            {
                Order o = new Order { OrderDate = DateTime.Now };
                db.Orders.Add(o);
                db.SaveChanges();

                HttpCookie cookie = new HttpCookie("OrderId");
                cookie.Value = o.OrderID + "";
                //cookie.Expires = DateTime.Now.AddMinutes(60);
                Response.Cookies.Add(cookie);
            }
            else
            {
                //code to update the order quantity in case of the prebuild computer selected be already on the order
                int orderID = Int32.Parse(Request.Cookies["OrderId"].Value);
                var orders = db.OrderItems.Where(i => i.OrderID == orderID);

                foreach (var item in orders.ToList())
                {
                    if (item.PreBuildPCID == id)
                    {
                        item.Quantity += 1;
                        db.OrderItems.Attach(item);
                        db.Entry(item).State = EntityState.Modified;
                        db.SaveChanges();
                        inserted = true;
                    }
                }


            }

            if (!inserted)
            {
                OrderItem newItem = new OrderItem { PreBuildPCID = id, OrderID = Int32.Parse(Request.Cookies["OrderId"].Value), Discount = 0, Quantity = 1 };
                db.OrderItems.Add(newItem);
                db.SaveChanges();
            }


            return RedirectToAction("Index", "Orders");
        }

        public ActionResult InsertProduct(int id)
        {
            bool inserted = false;
            if (Request.Cookies["OrderId"] == null)
            {
                Order o = new Order { OrderDate = DateTime.Now };
                db.Orders.Add(o);
                db.SaveChanges();

                HttpCookie cookie = new HttpCookie("OrderId");
                cookie.Value = o.OrderID + "";
                //cookie.Expires = DateTime.Now.AddMinutes(60);
                Response.Cookies.Add(cookie);
            }

            //code to update the order quantity in case of the prebuild computer selected be already on the order
            int orderID = Int32.Parse(Request.Cookies["OrderId"].Value);
            var orders = db.OrderItems.Where(i => i.OrderID == orderID);

            foreach (var item in orders.ToList())
            {
                if (item.ProductID == id)
                {
                    item.Quantity += 1;
                    db.OrderItems.Attach(item);
                    db.Entry(item).State = EntityState.Modified;
                    db.SaveChanges();
                    inserted = true;
                }
            }

            if (!inserted)
            {
                OrderItem newItem = new OrderItem { ProductID = id, OrderID = Int32.Parse(Request.Cookies["OrderId"].Value), Discount = 0, Quantity = 1 };
                db.OrderItems.Add(newItem);
                db.SaveChanges();
            }


            return RedirectToAction("Index", "Orders");
        }

        public int getNumberOfItems()
        {
            int items = 0;
            if (Request.Cookies["OrderId"] == null)
            {
                return 0;
            }
            else
            {
                int orderID = Int32.Parse(Request.Cookies["OrderId"].Value);
                var order = db.Orders.Find(orderID);
                items = order.OrderItems.Count;
            }

            return items;
        }


        public ActionResult Checkout(int id)
        {
            return View("Checkout");
        }

        //
        // GET: /Orders/Details/5

        public ActionResult Details(int id = 0)
        {
            var orders = db.Orders.Find(id);
            if (orders == null)
            {
                return HttpNotFound();
            }
            return View(orders);
        }

        //
        // GET: /Orders/Create

        public ActionResult Create()
        {
            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "ContactName");
            return View();
        }

        //
        // POST: /Orders/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Order orders)
        {
            if (ModelState.IsValid)
            {
                db.Orders.Add(orders);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "ContactName", orders.CustomerID);
            return View(orders);
        }

        //
        // GET: /Orders/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Order orders = db.Orders.Find(id);
            if (orders == null)
            {
                return HttpNotFound();
            }
            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "ContactName", orders.CustomerID);
            return View(orders);
        }

        //
        // POST: /Orders/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Order orders)
        {
            if (ModelState.IsValid)
            {
                db.Entry(orders).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "ContactName", orders.CustomerID);
            return View(orders);
        }

        //
        // GET: /Orders/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Order orders = db.Orders.Find(id);
            if (orders == null)
            {
                return HttpNotFound();
            }
            return View(orders);
        }

        //
        // POST: /Orders/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Order orders = db.Orders.Find(id);
            db.Orders.Remove(orders);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}