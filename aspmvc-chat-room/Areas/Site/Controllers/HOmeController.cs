using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AspMvcChatsupp.MVC.Areas.Site.Controllers
{
    public class HomeController : Controller
    {
        // GET: Site/HOme
        public ActionResult Index()
        {
            return View();
        }
    }
}