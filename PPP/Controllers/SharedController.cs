using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Services;
using Services.Management;

namespace PPP.Controllers
{
    public class SharedController : Controller
    {
        // GET: Shared
        public ActionResult Overview(string resource = "")
        {
            var overviewContent = new Services.Management.Content.Overview().GetOverviewContent(resource);
            ViewData["Content"] = overviewContent;
            return View();
        }
    }
}