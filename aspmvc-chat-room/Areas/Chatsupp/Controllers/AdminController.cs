using AspMvcChatsupp.DataAccess.Domain;
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
            var lstConnectedVisitors = RepSingleton.Rep.RepVisitor
                                                    .FindBy(vis => vis.StateId == EnumState.Connected 
                                                                || vis.StateId == EnumState.WaitingAnswer
                                                    ).ToList();
            if (Request.IsAjaxRequest())
                return PartialView("_VisitorList", lstConnectedVisitors);
            else
                return View(lstConnectedVisitors);
        }

        public ActionResult ChatBoxAgent(string title)
        {
            return View(new ChatBoxModel { Title = title });
        }

    }
}