using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Services.Management;

namespace PPP.Controllers
{
    public class ProductsController : Controller
    {
        // GET: Products
        public ActionResult Details(int id)
        {
            return View(Product.FetchProduct(id));
        }
    }
}