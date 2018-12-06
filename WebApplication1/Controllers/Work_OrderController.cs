using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication1.DAL;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class Work_OrderController : Controller
    {
        private NorthWestContext db = new NorthWestContext();

        // GET: Work_Order
        public ActionResult Index()
        {
            var workOrders = db.WorkOrders.Include(w => w.Customer).Include(w => w.Status);
            return View(workOrders.ToList());
        }

        // GET: Work_Order/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Work_Order work_Order = db.WorkOrders.Find(id);
            if (work_Order == null)
            {
                return HttpNotFound();
            }
            return View(work_Order);
        }

        // GET: Work_Order/Create
        public ActionResult Create()
        {
            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "CustomerFirstName");
            ViewBag.StatusID = new SelectList(db.Statuses, "StatusID", "StatusDescription");
            return View();
        }

        // POST: Work_Order/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OrderID,CustomerID,QuotedPrice,ActualPrice,Comments,StatusID,Approved")] Work_Order work_Order)
        {
            if (ModelState.IsValid)
            {
                db.WorkOrders.Add(work_Order);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "CustomerFirstName", work_Order.CustomerID);
            ViewBag.StatusID = new SelectList(db.Statuses, "StatusID", "StatusDescription", work_Order.StatusID);
            return View(work_Order);
        }

        // GET: Work_Order/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Work_Order work_Order = db.WorkOrders.Find(id);
            if (work_Order == null)
            {
                return HttpNotFound();
            }
            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "CustomerFirstName", work_Order.CustomerID);
            ViewBag.StatusID = new SelectList(db.Statuses, "StatusID", "StatusDescription", work_Order.StatusID);
            return View(work_Order);
        }

        // POST: Work_Order/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OrderID,CustomerID,QuotedPrice,ActualPrice,Comments,StatusID,Approved")] Work_Order work_Order)
        {
            if (ModelState.IsValid)
            {
                db.Entry(work_Order).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("WorkOrder","Singapore");
            }
            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "CustomerFirstName", work_Order.CustomerID);
            ViewBag.StatusID = new SelectList(db.Statuses, "StatusID", "StatusDescription", work_Order.StatusID);
            return View(work_Order);
        }

        // GET: Work_Order/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Work_Order work_Order = db.WorkOrders.Find(id);
            if (work_Order == null)
            {
                return HttpNotFound();
            }
            return View(work_Order);
        }

        // POST: Work_Order/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Work_Order work_Order = db.WorkOrders.Find(id);
            db.WorkOrders.Remove(work_Order);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
