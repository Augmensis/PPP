using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PPP.Controllers
{
    [RequireHttps]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }

        public ActionResult PublicFAQ()
        {
            return View();
        }

        public ActionResult PrivateFAQ()
        {
            if (User.Identity.IsAuthenticated)
            {
                return View();
            }
            //throw new Exception("You are not signed into the service. Please open an account or log in.");
            return new HttpUnauthorizedResult("You need to be logged in to access this page.");
        }
    }
}