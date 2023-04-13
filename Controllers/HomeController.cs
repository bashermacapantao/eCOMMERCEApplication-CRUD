using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using eCOMMERCEApplication.Models;

namespace eCOMMERCEApplication.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            using (ProductDBEntities db = new ProductDBEntities())
            {
                List<tblProduct> products = (from data in db.tblProducts select data).ToList();

                return View(products);
            }
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}