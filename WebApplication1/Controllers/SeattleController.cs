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
            Customer cust_user = db.Customers.Single(x => x.UserID == User.Identity.GetUserId());
            List<Work_Order> workOrders = db.WorkOrders.Where(x => x.CustomerID == cust_user.CustomerID).ToList();
            return View(workOrders);
        }
    }
}
