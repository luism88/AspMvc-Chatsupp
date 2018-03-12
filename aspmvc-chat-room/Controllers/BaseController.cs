using AspMvcChatsupp.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AspMvcChatsupp.MVC.Controllers
{
    public class BaseController : Controller
    {
        private RepUOW _rep = null;

        public RepUOW Rep
        {
            get
            {
                return _rep ?? (_rep = new RepUOW());
            }
        }
    }
}