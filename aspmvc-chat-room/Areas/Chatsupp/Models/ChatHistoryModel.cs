using AspMvcChatsupp.DataAccess.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AspMvcChatsupp.MVC.Areas.Chatsupp.Models
{
    public class ChatHistoryModel
    {
        public Visitor Visitor { get; set; }
        public IList<ChatHistory> LstMessages { get; set; }
    }
}