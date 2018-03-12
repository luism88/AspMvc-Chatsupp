using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AspMvcChatsupp.MVC.Helpers
{
    public static class SessionHelper
    {
        private static int _agentId;
        private static string _agentName;

        public static int AgentId
        {
            get
            {
                return (int)HttpContext.Current.Session["AgentId"];
            }
            set
            {
                HttpContext.Current.Session["AgentId"] = value;
            }
        }

        public static string AgentName
        {
            get
            {
                return HttpContext.Current.Session["AgentName"].ToString();
            }
            set
            {
                HttpContext.Current.Session["AgentName"] = value;
            }
        }
    }
}