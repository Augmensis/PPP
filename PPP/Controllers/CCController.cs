using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PPP.Controllers
{
    // Command and Control Controller
    [RequireHttps]
    public class CCController : Controller
    {
        // GET: CC
        public ActionResult Overview()
        {
            if (!User.IsInRole("Controller"))
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        public ActionResult Users()
        {
            if (!User.IsInRole("Controller"))
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        public ActionResult Content()
        {
            if (!User.IsInRole("Controller"))
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
    }
}