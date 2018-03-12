using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AspMvcChatsupp.MVC.Areas.Chatsupp.Models
{
    public class ChatBoxModel
    {
        public string Title { get; set; }
        public string Action { get; set; }
        public int ConnectionInfoId { get; set; }
    }
}