using Microsoft.Owin.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace CordobaWeb
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();           
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_BeginRequest(Object sender, EventArgs e)
        {
            //string[] MVC_URL = new string[] { "/", "/Home/GetAdminUserDetail", "/Home/GetStoreDetail", "/Home/GetAdminUserDetail", "/Admin/Login", "/Home/accessdenied" };
            //var RquestedUrl = Convert.ToString(Request.Url.AbsolutePath);
            //var aa=!((Array.IndexOf(MVC_URL, RquestedUrl) >= 0));
            //var sca = !(RquestedUrl.Contains(".cshtml"));
            //var cac = !(RquestedUrl.Contains(".html"));

            //if (!(Array.IndexOf(MVC_URL, RquestedUrl) >= 0) && !(RquestedUrl.Contains(".cshtml")) && !(RquestedUrl.Contains(".html")))
            //{
            //    var PathAndQuery =Convert.ToString(Request.Url.PathAndQuery);
            //    var Authority =Convert.ToString(Request.Url.Authority);
            //    var Scheme = Convert.ToString(Request.Url.Scheme);

            //    Response.Redirect(Scheme + "://" + Authority + "/#/" + PathAndQuery);
                
            //}
                
        }
   
    }
}
