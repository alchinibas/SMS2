using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StudentManagement.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "About Project";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Find Me";

            return View();
        }
    }
}