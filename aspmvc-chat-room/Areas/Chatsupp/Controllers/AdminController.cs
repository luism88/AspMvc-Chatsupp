using AspMvcChatsupp.MVC.Areas.Chatsupp.Models;
using AspMvcChatsupp.MVC.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AspMvcChatsupp.MVC.Areas.Chatsupp.Controllers
{
    public class AdminController : BaseController
    {
        // GET: Chatsupp/Admin
        public ActionResult Dashboard()
        {
            var lstConnectedClientes = RepSingleton.Rep.RepConnectionInfo.FindBy(conn => conn.StateId == 1).ToList();
            if (Request.IsAjaxRequest())
                return PartialView("_VisitorList", lstConnectedClientes);
            else
                return View(lstConnectedClientes);
        }

        public ActionResult ChatBoxAgent(string title)
        {
            return View(new ChatBoxModel { Title = title });
        }

    }
}