using System.Web.Mvc;

namespace AspMvcChatsupp.MVC.Areas.Chatsupp
{
    public class ChatsuppAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Chatsupp";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Chatsupp_default",
                "Chatsupp/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}