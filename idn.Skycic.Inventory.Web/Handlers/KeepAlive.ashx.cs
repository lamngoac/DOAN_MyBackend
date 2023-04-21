using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace idn.Skycic.Inventory.Web.Handlers
{
    /// <summary>
    /// Summary description for KeepAlive
    /// </summary>
    public class KeepAlive : IHttpHandler, System.Web.SessionState.IRequiresSessionState
    {
        public void ProcessRequest(HttpContext context)
        {

            context.Response.ContentType = "text/plain";

            context.Response.AddHeader("Cache-Control", "no-cache");
            context.Response.AddHeader("Pragma", "no-cache");
            
            string json = new StreamReader(context.Request.InputStream).ReadToEnd();
            string myName = context.Request.Form["firstName"];

            // simulate Microsoft XSS protection
            var wrapper = new { d = myName };
            context.Response.Write(JsonConvert.SerializeObject(wrapper));
            
            if (context.Session != null)
            {
                var UserState = System.Web.HttpContext.Current.Session["UserState"];
                System.Web.HttpContext.Current.Session["HelloCaoTo"] = "Hello cao to";
                if (context.Session["UserState"] != null)
                {

                }
            }
            
            //context.Response.Write("OK");
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}