using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CordobaAPI.Controllers
{
    /// <summary>
    /// home controller
    /// </summary>
    public class DefaultController : Controller
    {
        /// <summary>
        /// Index this instance.
        /// </summary>
        /// <returns>view</returns>
        /// 

        
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }


        public string GetLayoutName(string HostName)
        {
            return "localhost";
        }
     
    }
}
