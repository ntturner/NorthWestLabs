using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.DAL;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private NorthWestContext db = new NorthWestContext();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        [HttpGet]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpPost]
        public ActionResult Contact(Contact contact)
        {
            if (ModelState.IsValid)
            {
                return View("Confirmation",contact);
            }
            else
            {
                return View(contact);
            }
        }

        public ActionResult Confirmation(Contact contact)
        {
            return View(contact);
        }

        public ActionResult AssayCatalog()
        {
            return View(db.Assays.ToList());
        }
    }
}