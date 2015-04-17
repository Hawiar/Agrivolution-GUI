using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;
using System.Data.Entity;
using Agrivolution.Models;

namespace Agrivolution
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            //Initalize the MCU database
            Database.SetInitializer(new MCUDatabaseInit());
        }

        //Should check if user is logged in, otherwise redirect them to login page
        //protected void Application_AuthenticateRequest(Object sender, EventArgs e)
        //{
        //    // This is the page
        //    string cTheFile = HttpContext.Current.Request.Path;

        //    // Check if I am all ready on login page to avoid crash
        //    if (!cTheFile.EndsWith("login.aspx") || !cTheFile.EndsWith("register.aspx"))
        //    {
        //        // Extract the form's authentication cookie
        //        string cookieName = FormsAuthentication.FormsCookieName;
        //        HttpCookie authCookie = Context.Request.Cookies[cookieName];

        //        // If not logged in
        //        if (null == authCookie)
        //        // Alternative way of checking:
        //        //if (HttpContext.Current.User == null || HttpContext.Current.User.Identity == null || !HttpContext.Current.User.Identity.IsAuthenticated)
        //        {
        //            Response.Redirect("/login.aspx", true);
        //            Response.End();
        //            return;
        //        }
        //    }
        //}
    }
}