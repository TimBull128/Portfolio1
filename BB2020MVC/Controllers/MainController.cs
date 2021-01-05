using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BB2020MVC.Controllers
{
    public class MainController : Controller
    {
        // GET: Splash
        public ActionResult Index()
        {
            return View();
        }
        //public ActionResult About()
        //{
        //    return View();
        //}
        //public ActionResult Contact()
        //{
        //    return View();
        //}
        public ActionResult ToRules()
        {
            return RedirectToAction("Index", controllerName: "BaseRules");
        }
        public ActionResult ToBaseTeams()
        {
            return RedirectToAction("Index", controllerName: "BaseTeams");
        }
        public ActionResult ToRosters()
        {
            return RedirectToAction("Index", controllerName: "Rosters");
        }
        public ActionResult Navbar()
        {
            return PartialView("PartNavbar");
        }
        public ActionResult Test()
        {
            return View("Test");
        }
    }
}