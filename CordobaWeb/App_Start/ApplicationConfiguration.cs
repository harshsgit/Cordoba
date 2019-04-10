using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;


namespace CordobaWeb
{
    public class ApplicationConfiguration
    {

        public static bool IsDeveloperMode
        {
            get
            {
                if (ConfigurationManager.AppSettings["IsDeveloperMode"] != null)
                {
                    return Convert.ToBoolean(ConfigurationManager.AppSettings["IsDeveloperMode"]);
                }
                return false;
            }

        }

    }
}