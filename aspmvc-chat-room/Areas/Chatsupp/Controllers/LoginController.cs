using AspMvcChatsupp.DataAccess.Domain;
using AspMvcChatsupp.MVC.Areas.Chatsupp.Models;
using AspMvcChatsupp.MVC.Controllers;
using AspMvcChatsupp.MVC.Helpers;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;

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
            var agent = RepSingleton.Rep.RepAgent.FindBy(ag => ag.Username == model.Username && ag.Password == model.Psw)
                        .FirstOrDefault();
            if (agent != null)
            {
                FormsAuthentication.SetAuthCookie(model.Username, true);
                SessionHelper.AgentName = agent.Name;
                SessionHelper.AgentId = agent.AgenteId;
                agent.CurrentConnections.Add(new CurrentConnection
                {
                    ConnectionId = model.ConnectionId
                });
                RepSingleton.Rep.RepAgent.Edit(agent);
                RepSingleton.Rep.SaveChanges();
                return RedirectToAction("Dashboard", "Admin", "Chatsupp");
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