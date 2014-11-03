using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PPP.Controllers
{
    [RequireHttps]
    public class ChainController : Controller
    {
        // GET: Chain
        public ActionResult Start()
        {
            return View();
        }

    }
}