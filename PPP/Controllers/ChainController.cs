using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PPP.Controllers
{
    public class ChainController : Controller
    {
        // GET: Chain
        public ActionResult Index()
        {
            var shizz = new PPPServices.Service1().GetData(9001);
            
            return null;
        }
    }
}