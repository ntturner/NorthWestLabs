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
    public class Compound_AssayController : Controller
    {
        private NorthWestContext db = new NorthWestContext();

        // GET: Compound_Assay
        public ActionResult Index()
        {
            var compound_Assays = db.Compound_Assays.Include(c => c.Assay).Include(c => c.Compound).Include(c => c.Status).Include(c => c.WorkOrder);
            return View(compound_Assays.ToList());
        }

        // GET: Compound_Assay/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Compound_Assay compound_Assay = db.Compound_Assays.Find(id);
            if (compound_Assay == null)
            {
                return HttpNotFound();
            }
            return View(compound_Assay);
        }

        // GET: Compound_Assay/Create
        public ActionResult Create()
        {
            ViewBag.AssayID = new SelectList(db.Assays, "AssayID", "AssayName");
            ViewBag.LTNumber = new SelectList(db.Compounds, "LTNumber", "CompoundName");
            ViewBag.StatusID = new SelectList(db.Statuses, "StatusID", "StatusDescription");
            ViewBag.OrderID = new SelectList(db.WorkOrders, "OrderID", "Comments");
            return View();
        }

        // POST: Compound_Assay/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "LTNumber,SequenceNumber,OrderID,AssayID,TestAll,Quantity,DateArrived,ReceivedBy,DateDue,Appearance,ActualWeight,ClientWeight,MolecularMass,MTD,StatusID")] Compound_Assay compound_Assay)
        {
            if (ModelState.IsValid)
            {
                db.Compound_Assays.Add(compound_Assay);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AssayID = new SelectList(db.Assays, "AssayID", "AssayName", compound_Assay.AssayID);
            ViewBag.LTNumber = new SelectList(db.Compounds, "LTNumber", "CompoundName", compound_Assay.LTNumber);
            ViewBag.StatusID = new SelectList(db.Statuses, "StatusID", "StatusDescription", compound_Assay.StatusID);
            ViewBag.OrderID = new SelectList(db.WorkOrders, "OrderID", "Comments", compound_Assay.OrderID);
            return View(compound_Assay);
        }

        // GET: Compound_Assay/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Compound_Assay compound_Assay = db.Compound_Assays.Find(id);
            if (compound_Assay == null)
            {
                return HttpNotFound();
            }
            ViewBag.AssayID = new SelectList(db.Assays, "AssayID", "AssayName", compound_Assay.AssayID);
            ViewBag.LTNumber = new SelectList(db.Compounds, "LTNumber", "CompoundName", compound_Assay.LTNumber);
            ViewBag.StatusID = new SelectList(db.Statuses, "StatusID", "StatusDescription", compound_Assay.StatusID);
            ViewBag.OrderID = new SelectList(db.WorkOrders, "OrderID", "Comments", compound_Assay.OrderID);
            return View(compound_Assay);
        }

        // POST: Compound_Assay/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "LTNumber,SequenceNumber,OrderID,AssayID,TestAll,Quantity,DateArrived,ReceivedBy,DateDue,Appearance,ActualWeight,ClientWeight,MolecularMass,MTD,StatusID")] Compound_Assay compound_Assay)
        {
            if (ModelState.IsValid)
            {
                db.Entry(compound_Assay).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AssayID = new SelectList(db.Assays, "AssayID", "AssayName", compound_Assay.AssayID);
            ViewBag.LTNumber = new SelectList(db.Compounds, "LTNumber", "CompoundName", compound_Assay.LTNumber);
            ViewBag.StatusID = new SelectList(db.Statuses, "StatusID", "StatusDescription", compound_Assay.StatusID);
            ViewBag.OrderID = new SelectList(db.WorkOrders, "OrderID", "Comments", compound_Assay.OrderID);
            return View(compound_Assay);
        }

        // GET: Compound_Assay/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Compound_Assay compound_Assay = db.Compound_Assays.Find(id);
            if (compound_Assay == null)
            {
                return HttpNotFound();
            }
            return View(compound_Assay);
        }

        // POST: Compound_Assay/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Compound_Assay compound_Assay = db.Compound_Assays.Find(id);
            db.Compound_Assays.Remove(compound_Assay);
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
