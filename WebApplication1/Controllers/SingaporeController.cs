using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication1.DAL;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class SingaporeController : Controller
    {
        private NorthWestContext db = new NorthWestContext();
        public static DateTime begin;
        public static DateTime end;
        // GET: Singapore
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Approve()
        {
            var workOrders = db.WorkOrders;
            return View(workOrders.ToList());
        }

        public ActionResult Approved(int id)
        {
            db.Database.ExecuteSqlCommand("USE NorthWestLabs Update Work_Order SET Work_Order.Approved = 1 WHERE Work_Order.OrderID = " + id);
            db.SaveChanges();
            var workOrders = db.WorkOrders;
            return View("Approve",workOrders.ToList());
        }

        [HttpGet]
        public ActionResult Schedule()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Schedule(DateTime begindate, DateTime enddate)
        {
            begin = begindate;
            end = enddate;
            ViewBag.begin = begin;
            ViewBag.end = end;
            var compound_Assays = db.Compound_Assays;
            return View("WeeklyTest", compound_Assays.ToList());
        }

        public ActionResult WeeklyTest()
        {
            return View();
        }

        public ActionResult Test()
        {
            ViewBag.begin = begin;
            ViewBag.end = end;
            var compound_Assays = db.Compound_Assays;
            return View(compound_Assays.ToList());
        }

        public ActionResult Details(int? id)
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

        // GET: Compound_Assay/Edit/5
        public ActionResult Edit(int? id)
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
        public ActionResult Edit([Bind(Include = "ListID,LTNumber,SequenceNumber,OrderID,AssayID,TestAll,Quantity,DateArrived,ReceivedBy,DateDue,Appearance,ActualWeight,ClientWeight,MolecularMass,MTD,StatusID,Results")] Compound_Assay compound_Assay)
        {
            if (ModelState.IsValid)
            {
                db.Entry(compound_Assay).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Test");
            }
            ViewBag.AssayID = new SelectList(db.Assays, "AssayID", "AssayName", compound_Assay.AssayID);
            ViewBag.LTNumber = new SelectList(db.Compounds, "LTNumber", "CompoundName", compound_Assay.LTNumber);
            ViewBag.StatusID = new SelectList(db.Statuses, "StatusID", "StatusDescription", compound_Assay.StatusID);
            ViewBag.OrderID = new SelectList(db.WorkOrders, "OrderID", "Comments", compound_Assay.OrderID);
            return View(compound_Assay);
        }

        public ActionResult MaterialsInventory()
        {
            return View(db.Materials.ToList());
        }

        public ActionResult OrderMaterials(int? id)
        {
            Materials materials = db.Materials.Find(id);
            return View(materials);
        }

        public ActionResult WorkOrder()
        {
            return View();
        }

        public ActionResult ViewAll()
        {

            return RedirectToAction("Index", "Work_Order");
        }

        public ActionResult Search()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Search(string firstname, string lastname)
        {
            var id = 0;
            IEnumerable<Work_Order> work_Orders = db.Database.SqlQuery<Work_Order>("SELECT * " +
                "FROM Work_Order WO inner join Customer C ON Wo.CustomerID = C.CustomerID " +
                "WHERE C.CustomerFirstName = '" + firstname + "' AND C.CustomerLastName = '" + lastname+"' " +
                "AND WO.StatusID = 1");
            foreach (Work_Order item in work_Orders)
            {
                id = item.OrderID;
            }
            Work_Order work_Order = db.WorkOrders.Find(id);
            if (work_Orders==null)
            {
                ViewBag.search = "No work order associated with that customer was found.";
                return View();
            }
            return View("result",work_Order);
        }
        public ActionResult result()
        {
            return View();
        }

        public ActionResult Archived()
        {
            return View();
        }
    }
}