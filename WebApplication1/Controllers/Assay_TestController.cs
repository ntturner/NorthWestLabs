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
    public class Assay_TestController : Controller
    {
        private NorthWestContext db = new NorthWestContext();

        // GET: Assay_Test
        public ActionResult Index()
        {
            var assay_Tests = db.Assay_Tests.Include(a => a.Assay).Include(a => a.Test);
            return View(assay_Tests.ToList());
        }

        // GET: Assay_Test/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Assay_Test assay_Test = db.Assay_Tests.Find(id);
            if (assay_Test == null)
            {
                return HttpNotFound();
            }
            return View(assay_Test);
        }

        // GET: Assay_Test/Create
        public ActionResult Create()
        {
            ViewBag.AssayID = new SelectList(db.Assays, "AssayID", "AssayName");
            ViewBag.TestID = new SelectList(db.Tests, "TestID", "TestDescription");
            return View();
        }

        // POST: Assay_Test/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AssayID,TestID,TestRequired")] Assay_Test assay_Test)
        {
            if (ModelState.IsValid)
            {
                db.Assay_Tests.Add(assay_Test);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AssayID = new SelectList(db.Assays, "AssayID", "AssayName", assay_Test.AssayID);
            ViewBag.TestID = new SelectList(db.Tests, "TestID", "TestDescription", assay_Test.TestID);
            return View(assay_Test);
        }

        // GET: Assay_Test/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Assay_Test assay_Test = db.Assay_Tests.Find(id);
            if (assay_Test == null)
            {
                return HttpNotFound();
            }
            ViewBag.AssayID = new SelectList(db.Assays, "AssayID", "AssayName", assay_Test.AssayID);
            ViewBag.TestID = new SelectList(db.Tests, "TestID", "TestDescription", assay_Test.TestID);
            return View(assay_Test);
        }

        // POST: Assay_Test/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AssayID,TestID,TestRequired")] Assay_Test assay_Test)
        {
            if (ModelState.IsValid)
            {
                db.Entry(assay_Test).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AssayID = new SelectList(db.Assays, "AssayID", "AssayName", assay_Test.AssayID);
            ViewBag.TestID = new SelectList(db.Tests, "TestID", "TestDescription", assay_Test.TestID);
            return View(assay_Test);
        }

        // GET: Assay_Test/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Assay_Test assay_Test = db.Assay_Tests.Find(id);
            if (assay_Test == null)
            {
                return HttpNotFound();
            }
            return View(assay_Test);
        }

        // POST: Assay_Test/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Assay_Test assay_Test = db.Assay_Tests.Find(id);
            db.Assay_Tests.Remove(assay_Test);
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
