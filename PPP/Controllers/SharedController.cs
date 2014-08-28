using System.Web.Mvc;
using Services.Management;

namespace PPP.Controllers
{
    public class SharedController : Controller
    {
        // GET: Shared
        public ActionResult Overview(string id = "")
        {
            return View(ViewData["content"] = new Content.Overview().GetOverview(id));
        }

        public ActionResult Signup(string id = "")
        {
            ViewBag.ContentType = id;
            return View();
        }
    }
}