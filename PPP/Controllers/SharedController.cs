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

        [HttpGet]
        public ActionResult Signup(string id = "")
        {
            ViewBag.ContentType = id;
            return View();
        }

        [HttpPost]
        public ActionResult Signup(Account acc)
        {
            if (acc == null)
            {
                return RedirectToAction("Signup");
            }

            var newAccount = acc;
            return RedirectToRoute(acc.AccountStatus.ToString(), newAccount);
        }
    }
}