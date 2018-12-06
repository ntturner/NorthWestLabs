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
    [Authorize]
    public class SeattleController : Controller
    {
        private NorthWestContext db = new NorthWestContext();

        // GET: Seattle
        public ActionResult Index()
        {
            return View();
        }

        // GET: Seattle/Request
        public ActionResult RequestForm()
        {
            List<Compound> liCompounds = db.Compounds.ToList();
            ViewBag.Compounds = liCompounds;
            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "CustomerFirstName");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RequestForm([Bind(Include = "OrderID,CustomerID,Comments")] WorkOrder workOrder)
        {           
            if (ModelState.IsValid)
            {
                db.WorkOrders.Add(workOrder);
                db.SaveChanges();

                for (int iCount = 0; iCount < 5; iCount++)
                {
                    Compound_Assay ca = new Compound_Assay();

                    ca.ClientWeight = Convert.ToDouble(Request.Form["assay_compound[" + iCount + "]['client_weight']"]);
                    ca.LTNumber = Request.Form["assay_compound[" + iCount + "]['compound']"];
                    ca.TestAll = Convert.ToInt32(Request.Form["assay_compound[" + iCount + "]['testall']"]);
                    ca.Quantity = Convert.ToDouble(Request.Form["assay_compound[" + iCount + "]['quantity']"]);
                    ca.OrderID = workOrder.OrderID;

                    db.Compound_Assays.Add(ca);
                    db.SaveChanges();
                }

                
            }
            

            return RedirectToAction("RequestForm");
        }
    }
}
