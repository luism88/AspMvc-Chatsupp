using AspMvcChatsupp.MVC.Areas.Chatsupp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AspMvcChatsupp.MVC.Areas.Chatsupp.Controllers
{
    public class VisitorController : Controller
    {
        // GET: Chatsupp/Chat
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ChatBox(string title)
        {
            return View(new ChatBoxModel { Title = title });
        }

        public ActionResult ChatBoxVisitor()
        {
            return View(new ChatBoxModel { Title = "Send us a messaage" });
        }
    }
}