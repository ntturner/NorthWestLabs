using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.DAL;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class SingaporeController : Controller
    {
        private NorthWestContext db = new NorthWestContext();
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
            ViewBag.begin = begindate;
            ViewBag.end = enddate;
            //List<Compound_Assay> lcompound_Assays = new List<Compound_Assay>();
            //var compound_Assays = db.Database.SqlQuery<Compound_Assay>("USE NorthWestLabs SELECT CA.OrderID, CA.LTNumber, CA.SequenceNumber, CA.AssayID, CA.TestAll, CA.DateDue " +
            //    "FROM Work_Order WO INNER JOIN Compound_Assay CA" +"    ON WO.OrderID = CA.OrderID " +
            //    "WHERE DateDue BETWEEN CAST('"+ begindate +"' as datetime) AND CAST('"+ enddate +"' as datetime)");
            //foreach (var item in compound_Assays)
            //{
            //    lcompound_Assays.Add(new Compound_Assay(item.OrderID, item.LTNumber, item.SequenceNumber, item.Assay, item.TestAll, item.DateDue));
            //}
            //ViewBag.compound = lcompound_Assays;
            var compound_Assays = db.Compound_Assays;
            return View("WeeklyTest", compound_Assays.ToList());
        }

        public ActionResult WeeklyTest()
        {
            return View();
        }

        public ActionResult Test()
        {
            return View();
        }
    }
}