using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

namespace mallspacium_web
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {

        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            // Check if the cookie exists
            if (Request.Cookies["Language"] != null)
            {
                // Set the culture info based on the cookie value
                string lang = Request.Cookies["Language"].Value;

                // Add more language options as needed
                switch (lang)
                {
                    case "en-US":
                        Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
                        Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");
                        break;
                    case "zh-CN":
                        Thread.CurrentThread.CurrentCulture = new CultureInfo("zh-CN");
                        Thread.CurrentThread.CurrentUICulture = new CultureInfo("zh-CN");
                        break;
                    case "ko-KR":
                        Thread.CurrentThread.CurrentCulture = new CultureInfo("ko-KR");
                        Thread.CurrentThread.CurrentUICulture = new CultureInfo("ko-KR");
                        break;
                    case "ja-JP":
                        Thread.CurrentThread.CurrentCulture = new CultureInfo("ja-JP");
                        Thread.CurrentThread.CurrentUICulture = new CultureInfo("ja-JP");
                        break;
                    case "es-ES":
                        Thread.CurrentThread.CurrentCulture = new CultureInfo("es-ES");
                        Thread.CurrentThread.CurrentUICulture = new CultureInfo("es-ES");
                        break;
                    case "fr-FR":
                        Thread.CurrentThread.CurrentCulture = new CultureInfo("fr-FR");
                        Thread.CurrentThread.CurrentUICulture = new CultureInfo("fr-FR");
                        break;
                    default:
                        // Throw an exception or set a default language option
                        throw new ApplicationException("Invalid language value in cookie.");
                }
            }
            else
            {
                // Set the default culture info to English
                Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
                Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");
            }
        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}