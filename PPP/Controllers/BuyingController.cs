using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PPP.Controllers
{
    [RequireHttps]
    public class BuyingController : Controller
    {
        // GET: Buying
        public ActionResult Start()
        {
            return View();
        }

    }
}