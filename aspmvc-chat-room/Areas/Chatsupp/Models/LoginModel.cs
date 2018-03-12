using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AspMvcChatsupp.MVC.Areas.Chatsupp.Models
{
    public class LoginModel
    {
        public string Username { get; set; }
        public string Psw { get; set; }
        public string MsgError { get; set; }
    }
}