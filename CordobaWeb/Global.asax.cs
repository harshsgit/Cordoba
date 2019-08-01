﻿using Microsoft.Owin.Logging;
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
            string url = HttpContext.Current.Request.Url.AbsoluteUri;
            if (url.IndexOf("http://afl-rewards.co.uk") > -1 || url.IndexOf("http://www.afl-rewards.co.uk") > -1 ||
                 //url.IndexOf("http://www.pbmakeadifferencethankyou.com") > -1 || url.IndexOf("http://pbmakeadifferencethankyou.com") > -1
                 url.IndexOf("http://webapp6.cordobarewards.co.uk") > -1 ||
                url.IndexOf("http://www.rigbycapital-rewards.com") > -1 || url.IndexOf("http://rigbycapital-rewards.com") > -1 ||
                url.IndexOf("http://www.maxxiagrouprewards.co.uk") > -1 || url.IndexOf("http://maxxiagrouprewards.co.uk") > -1 ||
                url.IndexOf("http://www.money-sphererewards.co.uk") > -1 || url.IndexOf("http://money-sphererewards.co.uk") > -1 ||
                url.IndexOf("http://www.pbmakeadifferencethankyou.co.uk") > -1 || url.IndexOf("http://www.pbmakeadifferencethankyou.co.uk") > -1 ||
                url.IndexOf("http://grenkerewards.co.uk") > -1 || url.IndexOf("http://www.grenkerewards.co.uk") > -1 ||
                url.IndexOf("http://www.morechoicerewards.co.uk") > -1 || url.IndexOf("http://morechoicerewards.co.uk") > -1 ||
                url.IndexOf("http://www.kennet-rewards.co.uk") > -1 || url.IndexOf("http://kennet-rewards.co.uk") > -1 ||
                //url.IndexOf("http://fr.pbmakeadifferencethankyou.com") > -1 || url.IndexOf("http://www.fr.pbmakeadifferencethankyou.com") > -1
                url.IndexOf("http://www.vitrxrewards.co.uk") > -1 || url.IndexOf("http://vitrxrewards.co.uk") > -1 ||
                url.IndexOf("http://www.jaycityrewards.co.uk") > -1 || url.IndexOf("http://jaycityrewards.co.uk") > -1 ||
                url.IndexOf("http://www.xporewards.co.uk") > -1 || url.IndexOf("http://xporewards.co.uk") > -1 ||
                url.IndexOf("http://www.ur-rewards.co.uk") > -1 || url.IndexOf("http://ur-rewards.co.uk") > -1 ||
                url.IndexOf("http://www.clearwinnersclub.co.uk") > -1 || url.IndexOf("http://clearwinnersclub.co.uk") > -1 ||
                url.IndexOf("http://www.annodatarewards.co.uk") > -1 || url.IndexOf("http://annodatarewards.co.uk") > -1 ||
                url.IndexOf("http://www.cvdrewards.co.uk") > -1 || url.IndexOf("http://cvdrewards.co.uk") > -1 ||
                url.IndexOf("http://www.econocomrewards.com") > -1 || url.IndexOf("http://econocomrewards.com") > -1 ||
                url.IndexOf("http://www.doosanbobcatrewards.co.uk") > -1 || url.IndexOf("http://doosanbobcatrewards.co.uk") > -1 ||
                url.IndexOf("http://peac-rewards.com") > -1 || url.IndexOf("http://www.peac-rewards.com") > -1 ||
                url.IndexOf("http://doosanbobcatrewards.irish") > -1 ||
                url.IndexOf("http://smarttechrewards.co.uk") > -1 || url.IndexOf("http://www.smarttechrewards.co.uk") > -1
                )
            {
                Response.Redirect("https://" + Request.ServerVariables["HTTP_HOST"]
            + HttpContext.Current.Request.RawUrl);
            }
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
