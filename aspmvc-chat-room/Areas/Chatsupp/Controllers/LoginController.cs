using AspMvcChatsupp.MVC.Areas.Chatsupp.Models;
using AspMvcChatsupp.MVC.Controllers;
using AspMvcChatsupp.MVC.Helpers;
using System.Linq;
using System.Web.Mvc;

namespace AspMvcChatsupp.MVC.Areas.Chatsupp.Controllers
{
    public class LoginController : BaseController
    {
        public ActionResult Login()
        {
            return View(new LoginModel());
        }

        [HttpPost]
        public ActionResult LoginResult(LoginModel model)
        {
            var usr = Rep.RepAgent.FindBy(agent => agent.Username == model.Username && agent.Password == model.Psw)
                        .FirstOrDefault();
            if (usr != null)
            {
                SessionHelper.AgentName = usr.Name;
                SessionHelper.AgentId = usr.AgenteId;
                return RedirectToAction("Index", "Admin", "Chatsupp");
            }
            else
            {
                model.MsgError = "Username o password is incorrect" ;
                return View("Login", model);
            }
                

        }

        public ActionResult Logout()
        {
            Session.Clear();
            Session.Abandon();

            return RedirectToAction("Login");
        }
    }
}