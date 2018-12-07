using Microsoft.AspNet.Identity;
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
            //These will all make selectable objects.
            List<Compound> liCompounds = db.Compounds.ToList();
            List<Assay> liAssays = db.Assays.ToList();
            ViewBag.Compounds = liCompounds;
            ViewBag.Assays = liAssays;
            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "CustomerFirstName");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RequestForm([Bind(Include = "OrderID,CustomerID,Comments")] Work_Order workOrder)
        {           
            if (ModelState.IsValid)
            {
                //Add new work order to database. It will have lots of null values because it is pending approval.
                var incomplete = db.Statuses.Single(x => x.StatusDescription == "Incomplete");
                workOrder.Approved = 0;
                workOrder.StatusID = incomplete.StatusID;
                db.WorkOrders.Add(workOrder);
                db.SaveChanges();

                //Get lists that will drive loops.
                List<Assay> assays = db.Assays.ToList();
                //This could be used to drive the base loop, but for now we want to limit work order to 5 compounds.
                //List<Compound> compLoop = db.Compounds.ToList();

                for (int iCount = 0; iCount < 5; iCount++)
                {
                    //Make sure that there are actually compounds on the request form.
                    if(Request.Form["assay_compound[" + iCount + "]['compound']"] != null)
                    {
                        //This will determine sequence number.
                        int counter = 1;

                        //We will check every assay we have and see if we need to create a line item if it has been selected.
                        for (int iCount2 = 0; iCount2 < assays.Count; iCount2++)
                        {
                            //Check to see if an assay was selected.
                            if (Request.Form["assay_compound[" + iCount + "]['assay'][" + assays[iCount2].AssayID + "]"] != null)
                            {
                                if (Request.Form["assay_compound[" + iCount + "]['assay'][" + assays[iCount2].AssayID + "]"] == "on")
                                {
                                    //Create the object that will be inserted in the db.
                                    Compound_Assay ca = new Compound_Assay();

                                    ca.ClientWeight = Convert.ToDecimal(Request.Form["assay_compound[" + iCount + "]['client_weight']"]);
                                    ca.LTNumber = Request.Form["assay_compound[" + iCount + "]['compound']"];

                                    //Check to see if test all is checked and set to either one or zero.
                                    if (Request.Form["assay_compound[" + iCount + "]['testall']"] != null)
                                    {
                                        if (Request.Form["assay_compound[" + iCount + "]['testall']"] == "on")
                                        {
                                            ca.TestAll = 1;
                                        }
                                        else
                                        {
                                            ca.TestAll = 0;
                                        }
                                    }
                                    else
                                    {
                                        ca.TestAll = 0;
                                    }
                                    ca.Quantity = Convert.ToDecimal(Request.Form["assay_compound[" + iCount + "]['quantity']"]);

                                    //Get ID from the work order that has already been created.
                                    ca.OrderID = workOrder.OrderID;

                                    //Set assay ID and sequence number based on where we are at in the loop.
                                    ca.AssayID = assays[iCount2].AssayID;
                                    ca.SequenceNumber = counter;
                                    counter++;

                                    //Save work order line item to the database.
                                    db.Compound_Assays.Add(ca);
                                    db.SaveChanges();
                                }
                            }
                        }
                    } 
                }

                //Verify that only 5 compounds were submitted on the work order.
                if(Request.Form["assay_compound[5]['compound']"] != null)
                {
                    //Tell user that additional compounds were not processed.
                }
            }
            

            return RedirectToAction("RequestForm");
        }

        public ActionResult Quote()
        {
            List<Assay> assays = db.Assays.ToList();
            ViewBag.Assays = assays;

            var assay_Tests = db.Assay_Tests.Include(a => a.Assay).Include(a => a.Test);

            return View(assay_Tests.ToList());
        }

        public ActionResult MyOrders()
        {
            string user_id = User.Identity.GetUserId();
            Customer cust_user = db.Customers.Single(x => x.UserID == user_id);
            if(cust_user != null)
            {
                var workOrders = db.WorkOrders.Where(w => w.CustomerID == cust_user.CustomerID).Include(w => w.Customer).Include(w => w.Status);
                return View(workOrders.ToList());
            }
            else
            {
                ViewBag.Message = "Uh oh, we were unable to find you in our customer database! Please contact support.";
                return View();
            }
        }

        [HttpGet]
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
                "WHERE C.CustomerFirstName = '" + firstname + "' AND C.CustomerLastName = '" + lastname + "' " + 
                "AND WO.StatusID=1");
            foreach (Work_Order item in work_Orders)
            {
                id = item.OrderID;
            }
            Work_Order work_Order = db.WorkOrders.Find(id);
            if (work_Orders == null)
            {
                ViewBag.search = "No work order associated with that customer was found.";
                return View();
            }
            return View("result", work_Order);
        }
        public ActionResult result()
        {
            return View();
        }

        public ActionResult Reports()
        {
            return View();
        }

        public ActionResult WorkOrder()
        {
            return View();
        }

        public ActionResult ViewAll()
        {

            return RedirectToAction("Index", "Work_Order");
        }

       public ActionResult Catalogs()
        {
            return View();
        }

        public ActionResult AssayCatalog()
        {
            return View(db.Assays.ToList());
        }

        // GET: Assays/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Assay assay = db.Assays.Find(id);
            if (assay == null)
            {
                return HttpNotFound();
            }
            return View(assay);
        }

        // POST: Assays/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AssayID,AssayName,Duration,Protocol,Description")] Assay assay)
        {
            if (ModelState.IsValid)
            {
                db.Entry(assay).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(assay);
        }

        public ActionResult MyInvoice()
        {
            return RedirectToAction("Index","Invoice");
        }
    }
}
