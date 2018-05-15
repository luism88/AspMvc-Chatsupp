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

        public ActionResult VisitorInfo(int visitorId)
        {
            var visitor = RepSingleton.Rep.RepVisitor.FindBy(vis => vis.VisitorId == visitorId).FirstOrDefault();
            
            return View(visitor);
        }

        public ActionResult MyQueue(int agentId)
        {
            var myQueue = RepSingleton.Rep.RepAgent.FindBy(agent => agent.AgentId == agentId)
                                                .First().MessageHistory
                                                .GroupBy(hist => hist.Visitor)
                                                .Select(hist => new MyQueueModel
                                                {
                                                    VisitorName = hist.Key.Name,
                                                    LastMessage = hist.Key.MessageHistory.FirstOrDefault().Value
                                                }).ToList();
            return PartialView("_MyQueue", myQueue);
        }

       
    }
}