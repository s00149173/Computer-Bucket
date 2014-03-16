using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using computerbucket.Models;

namespace computerbucket.Controllers
{
    public class OrdersController : Controller
    {
        private ComputerBucketEntities db = new ComputerBucketEntities();

        public ActionResult Index()
        {
            //var cookie = Convert.ToInt32(Request.Cookies["OrderId"].Value);
            if (Request.Cookies["OrderId"] == null || Request.Cookies["OrderId"].Value.Equals("") || Request.Cookies["OrderId"].Value.Equals("1"))
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

        public int getNumberOfItems()
        {
            int items = 0;
            if (Request.Cookies["OrderId"] == null || Request.Cookies["OrderId"].Value.Equals("") || Request.Cookies["OrderId"].Value.Equals("1"))
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

        public ActionResult InsertBuildPc(int id)
        {
            int tempOrder = 0;
            if (Request.Cookies["OrderId"] == null || Request.Cookies["OrderId"].Value.Equals("") || Request.Cookies["OrderId"].Value.Equals("1"))
            {
                Order o = new Order { OrderDate = DateTime.Now };
                db.Orders.Add(o);
                db.SaveChanges();

                HttpCookie cookie = new HttpCookie("OrderId");
                cookie.Value = o.OrderID + "";
                tempOrder = o.OrderID;
                //cookie.Expires = DateTime.Now.AddMinutes(60);
                Response.Cookies.Add(cookie);
            }
            else
            {
                tempOrder = Int32.Parse(Request.Cookies["OrderId"].Value);
            }


            OrderItem item = new OrderItem { BuildPCID = id, OrderID = tempOrder, Discount = 0, Quantity = 1, UnitPrice = db.BuildPCs.Find(id).Price };
            db.OrderItems.Add(item);
            db.SaveChanges();



            return RedirectToAction("Index", "Orders");
        }

        public ActionResult InsertPreBuildPc(int id)
        {

            bool inserted = false;
            int tempOrder = 0;
            //int cookie = Convert.ToInt32(Request.Cookies["OrderId"].Value);
            if (Request.Cookies["OrderId"] == null || Request.Cookies["OrderId"].Value.Equals("") || Request.Cookies["OrderId"].Value.Equals("1"))
            {
                Order o = new Order { OrderDate = DateTime.Now };
                db.Orders.Add(o);
                db.SaveChanges();
                tempOrder = o.OrderID;
                HttpCookie cookie = new HttpCookie("OrderId");
                cookie.Value = o.OrderID + "";
                //cookie.Expires = DateTime.Now.AddMinutes(60);
                Response.Cookies.Add(cookie);
            }
            else
            {
                tempOrder = Int32.Parse(Request.Cookies["OrderId"].Value);
                //code to update the order quantity in case of the prebuild computer selected be already on the order
                var orders = db.OrderItems.Where(i => i.OrderID == tempOrder);

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
                OrderItem newItem = new OrderItem { PreBuildPCID = id, OrderID = tempOrder, Discount = 0, Quantity = 1, UnitPrice = db.PreBuildPCs.Find(id).Price };
                db.OrderItems.Add(newItem);
                db.SaveChanges();
            }

            inserted = false;

            return RedirectToAction("Index", "Orders");
        }

        public ActionResult InsertProduct(int id)
        {
            bool inserted = false;
            int tempOrder = 0;
            if (Request.Cookies["OrderId"] == null || Request.Cookies["OrderId"].Value.Equals("") || Request.Cookies["OrderId"].Value.Equals("1"))
            {
                Order o = new Order { OrderDate = DateTime.Now };
                db.Orders.Add(o);
                db.SaveChanges();

                HttpCookie cookie = new HttpCookie("OrderId");
                cookie.Value = o.OrderID + "";
                tempOrder = o.OrderID;
                //cookie.Expires = DateTime.Now.AddMinutes(60);

                Response.Cookies.Add(cookie);
            }
            else
            {
                tempOrder = Int32.Parse(Request.Cookies["OrderId"].Value);
            }

            var orders = db.OrderItems.Where(i => i.OrderID == tempOrder);

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
                OrderItem newItem = new OrderItem { ProductID = id, OrderID = tempOrder, Discount = 0, Quantity = 1, UnitPrice = db.Products.Find(id).UnitPrice };
                db.OrderItems.Add(newItem);
                db.SaveChanges();
            }


            return RedirectToAction("Index", "Orders");
        }


        public ActionResult _Product(OrderItem item)
        {
            return PartialView(item);
        }

        public ActionResult _PreBuildPC(OrderItem item)
        {
            var pc = db.PreBuildPCs.Find(item.PreBuildPCID);
            int pcCase = Int32.Parse(pc.ComputerCase);
            ViewBag.ImageUrl = db.Products.Find(pcCase).ImageUrl;
            ViewBag.PcType = pc.PCType.TypeName;

            return PartialView("_PreBuildPC", item);
        }

        public ActionResult _BuildPC(OrderItem item)
        {
            var pc = db.BuildPCs.Find(item.BuildPCID);
            int pcCase = Int32.Parse(pc.ComputerCase);
            ViewBag.ImageUrl = db.Products.Find(pcCase).ImageUrl;

            return PartialView("_BuildPC", item);
        }


        public ActionResult DeleteItem(int id)
        {
            var item = db.OrderItems.Find(id);
            var order = item.Order;
            db.OrderItems.Remove(item);
            db.SaveChanges();

            if (order.OrderItems.Count == 0)
            {
                db.Orders.Remove(order);
                db.SaveChanges();

                HttpCookie cookie = new HttpCookie("OrderId");
                cookie.Value = "1";
                cookie.Expires = DateTime.Now.AddMinutes(60);
                Response.Cookies.Add(cookie);
                Session.Clear();
                return RedirectToAction("Index", "Home");

            }
            return RedirectToAction("Index");
        }


        public ActionResult Checkout(int id)
        {
            ViewBag.OrderID = id;
            return View("Checkout");
        }

        public ActionResult _OrderItemsList(int id)
        {
            var items = db.Orders.Find(id).OrderItems.ToList();
            return PartialView(items);
        }

        public string GetItemName(int id)
        {
            var item = db.OrderItems.Find(id);
            if (item.BuildPCID != null)
                return "Build PC";
            if (item.PreBuildPCID != null)
                return "Pre Build PC";
            if (item.ProductID != null)
                return item.Product.ProductName;
            return "Unknown item";
        }

        public decimal CalculatePrice(decimal q, decimal up)
        {
            return q * up;
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Checkout(CustomerModel custumer)
        {
            if (ModelState.IsValid)
            {
                Customer c = new Customer
                {
                    ContactName = custumer.ContactName,
                    Address = custumer.Address,
                    City = custumer.City,
                    Region = custumer.Region,
                    PostalCode = custumer.PostalCode,
                    Country = custumer.Country,
                    Phone = custumer.Phone,
                    Email = custumer.Email
                };
                db.Customers.Add(c);
                db.SaveChanges();

                int orderID = Int32.Parse(Request.Cookies["OrderId"].Value);
                var order = db.Orders.Find(orderID);
                order.CustomerID = c.CustomerID;
                db.Orders.Attach(order);
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Payment");
            }

            return View("Checkout", custumer);
        }


        [HttpGet]
        public ActionResult Payment()
        {
            ViewBag.Months = MonthsList();
            ViewBag.Years = YearsList();
            ViewBag.OrderID = Int32.Parse(Request.Cookies["OrderId"].Value);
            return View();
        }

        public List<int> MonthsList()
        {
            List<int> list = new List<int>();
            list.Add(1);
            list.Add(2);
            list.Add(3);
            list.Add(4);
            list.Add(5);
            list.Add(6);
            list.Add(7);
            list.Add(8);
            list.Add(9);
            list.Add(10);
            list.Add(11);
            list.Add(12);
            return list;
        }

        public List<int> YearsList()
        {
            List<int> list = new List<int>();
            list.Add(2014);
            list.Add(2015);
            list.Add(2016);
            list.Add(2017);
            list.Add(2018);
            list.Add(2019);
            return list;
        }

        [HttpPost]
        public ActionResult Payment(PaymentModal payment)
        {
            if (ModelState.IsValid)
            {
                int orderID = Int32.Parse(Request.Cookies["OrderId"].Value);
                var order = db.Orders.Find(orderID);
                order.OrderStatus = "In Progress";
                db.Orders.Attach(order);
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();

                HttpCookie cookie = new HttpCookie("OrderId");
                cookie.Value = "1";
                //cookie.Expires = DateTime.Now.AddMinutes(60);
                Response.Cookies.Add(cookie);

                return RedirectToAction("Success");
            }
            ViewBag.Months = MonthsList();
            ViewBag.Years = YearsList();
            ViewBag.OrderID = Int32.Parse(Request.Cookies["OrderId"].Value);
            return View("Payment", payment);
        }

        public ActionResult Success()
        {
            return View();
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

        public ActionResult OrderStatus()
        {
            return View("OrderStatus");
        }

    
        public ActionResult StatusInfo(string email)
        {            
            var status = from c in db.Customers
                         join o in db.Orders on c.CustomerID equals o.CustomerID
                         where c.Email == email
                         select new Status()
                         {
                             OrderID = o.OrderID,
                             Name = c.ContactName,
                             StatusField = o.OrderStatus


                         };
           
                return PartialView("_StatusInfo", status);              
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}