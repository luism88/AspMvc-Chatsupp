using System;
using System.Threading.Tasks;
using System.Web.Security;
using aAspMvcChatsupp.MVC.Areas.Chatsupp.Hubs;
using AspMvcChatsupp.DataAccess;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.SignalR;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Owin;

[assembly: OwinStartup(typeof(AspMvcChatsupp.MVC.Startup))]

namespace AspMvcChatsupp.MVC
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                //LoginPath = new PathString("~/Chatsupp/Login/Login"),
                CookieName = "ChatSupp_Auth"
            });
            GlobalHost.DependencyResolver.Register(
                typeof(ChatsuppHub), 
                () => new ChatsuppHub(new RepUOW()));

            app.MapSignalR();
         
            //GlobalHost.HubPipeline.RequireAuthentication();
            // Use a cookie to temporarily store information about a user logging in with a third party login provider


        }
    }
}
