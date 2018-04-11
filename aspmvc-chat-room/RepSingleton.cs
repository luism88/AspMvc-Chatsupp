using AspMvcChatsupp.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AspMvcChatsupp.MVC
{
    public class RepSingleton
    {
        const string REPKEY = "Rep";
        
        public static RepUOW Rep 
        {
            get
            {
                if (HttpContext.Current != null)
                {
                    if (HttpContext.Current.Items[REPKEY] == null)
                    {
                        // store in the container
                        HttpContext.Current.Items[REPKEY] = new RepUOW();
                    }

                    return (RepUOW)HttpContext.Current.Items[REPKEY];
                }
                else
                    return null;
               
            }
            
        }
    }
}