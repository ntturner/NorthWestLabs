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
    public class Test_MaterialsController : Controller
    {
        private NorthWestContext db = new NorthWestContext();

        // GET: Test_Materials
        public ActionResult Index()
        {
            var test_Materials = db.Test_Materials.Include(t => t.Materials).Include(t => t.Test);
            return View(test_Materials.ToList());
        }

        // GET: Test_Materials/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Test_Materials test_Materials = db.Test_Materials.Find(id);
            if (test_Materials == null)
            {
                return HttpNotFound();
            }
            return View(test_Materials);
        }

        // GET: Test_Materials/Create
        public ActionResult Create()
        {
            ViewBag.MaterialID = new SelectList(db.Materials, "MaterialID", "MaterialDescription");
            ViewBag.TestID = new SelectList(db.Tests, "TestID", "TestDescription");
            return View();
        }

        // POST: Test_Materials/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TestID,MaterialID,QuantityUsed,MaterialsCost")] Test_Materials test_Materials)
        {
            if (ModelState.IsValid)
            {
                db.Test_Materials.Add(test_Materials);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaterialID = new SelectList(db.Materials, "MaterialID", "MaterialDescription", test_Materials.MaterialID);
            ViewBag.TestID = new SelectList(db.Tests, "TestID", "TestDescription", test_Materials.TestID);
            return View(test_Materials);
        }

        // GET: Test_Materials/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Test_Materials test_Materials = db.Test_Materials.Find(id);
            if (test_Materials == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaterialID = new SelectList(db.Materials, "MaterialID", "MaterialDescription", test_Materials.MaterialID);
            ViewBag.TestID = new SelectList(db.Tests, "TestID", "TestDescription", test_Materials.TestID);
            return View(test_Materials);
        }

        // POST: Test_Materials/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TestID,MaterialID,QuantityUsed,MaterialsCost")] Test_Materials test_Materials)
        {
            if (ModelState.IsValid)
            {
                db.Entry(test_Materials).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaterialID = new SelectList(db.Materials, "MaterialID", "MaterialDescription", test_Materials.MaterialID);
            ViewBag.TestID = new SelectList(db.Tests, "TestID", "TestDescription", test_Materials.TestID);
            return View(test_Materials);
        }

        // GET: Test_Materials/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Test_Materials test_Materials = db.Test_Materials.Find(id);
            if (test_Materials == null)
            {
                return HttpNotFound();
            }
            return View(test_Materials);
        }

        // POST: Test_Materials/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Test_Materials test_Materials = db.Test_Materials.Find(id);
            db.Test_Materials.Remove(test_Materials);
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
