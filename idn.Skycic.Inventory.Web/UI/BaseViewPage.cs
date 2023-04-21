using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using idn.Skycic.Inventory.Common.Models;
using idn.Skycic.Inventory.Web.Extensions;
using idn.Skycic.Inventory.Web.State;

namespace idn.Skycic.Inventory.Web.UI
{
    public class BaseViewPage<TModel> : WebViewPage<TModel> where TModel : class // add dll idocNet.Client.Core
    {
        #region Initialization
        public BaseViewPage()
        {

        }

        protected override void InitializePage()
        {
            base.InitializePage();
        }
        #endregion

        #region Properties
        private SessionWrapper _session;
        public new SessionWrapper Session
        {
            get
            {
                if (_session == null && Context != null)
                {
                    _session = new SessionWrapper(Context.Session);
                }
                return _session;
            }
            set
            {
                _session = value;
            }
        }


        public string ControllerName
        {
            get
            {
                return ViewContext.RouteData.GetRequiredString("controller");
            }
        }

        public string ActionName
        {
            get
            {
                return ViewContext.RouteData.GetRequiredString("action");
            }
        }



        #region Domain

        public string Domain
        {
            get
            {
                if (this.ViewContext != null)
                {
                    var url = this.ViewContext.HttpContext.Request.Url;
                    if (url != null)
                    {
                        return url.Scheme + Uri.SchemeDelimiter + url.Host;
                    }

                }
                return "http://www.idocnet.com";
            }
        }
        #endregion

        /// <summary>
        /// Gets the current action name
        /// </summary>

        /// <summary>
        /// Determines if it is the current actionName
        /// </summary>
        /// <param name="actionName"></param>
        /// <returns></returns>
        public bool IsAction(string actionName)
        {
            return this.ActionName.ToUpper() == actionName.ToUpper();
        }
        #endregion

        #region Methods

        public ViewDataDictionary CreateViewData(object values)
        {
            return ViewDataExtensions.CreateViewData(values);
        }

        public override void Execute()
        {

        }

        public string GetRandomClientId()
        {
            return "cl_" + Guid.NewGuid().ToString("N").Substring(0, 6);
        }

        #endregion

        #region security

        #endregion


        #region helpers

        public HtmlString RenderMainMenuItem(string functionName, string text, string actionName, string controllerName = null, object routeValues = null, object htmlAtts = null)
        {

            string format = ""; // @"<li><a href=""{URL}"" {ATTR}>{TEXT}</a></li>";
            format += @"<li class=''>";
            format += @"<a href=""{URL}"" {ATTR} ><span class='menu-text'>{TEXT}</span></a>";
            format += @"</li>";

            if (ControllerName.Equals(controllerName, StringComparison.InvariantCultureIgnoreCase))
            {
                return RenderItemFormat(functionName, format, text, actionName, controllerName, routeValues, htmlAtts, "active");
            }

            var renderItemHtml = RenderItemFormat(functionName, format, text, actionName, controllerName, routeValues, htmlAtts);
            return renderItemHtml;
        }

        public HtmlString RenderMainMenu(string functionName, string text, string actionName, string controllerName = null, object routeValues = null, object htmlAtts = null)
        {

            string format = ""; // @"<li><a href=""{URL}"" {ATTR}>{TEXT}</a></li>";
            format += @"<li class=''>";
            format += @"<a href=""{URL}"" {ATTR} ><span class='menu-text'>{TEXT}</span></a>";
            format += @"</li>";

            if (ControllerName.Equals(controllerName, StringComparison.InvariantCultureIgnoreCase))
            {
                return RenderItemFormat(functionName, format, text, actionName, controllerName, routeValues, htmlAtts, "active");
            }

            var renderItemHtml = RenderItemFormat(functionName, format, text, actionName, controllerName, routeValues, htmlAtts);
            return renderItemHtml;
        }



        public HtmlString RenderSubMenuItem(string functionName, string text, string actionName, string controllerName = null, object routeValues = null, object htmlAtts = null)
        {
            string format = "";
            format += @"<li class=''>";
            //format += @"<a href=""{URL}"" {ATTR} >{TEXT}</a>";
            //format += @"<a href=""{URL}"" {ATTR} ><i class='menu-icon fa fa-caret-right'></i>{TEXT}</a>";
            format += @"<a href=""{URL}"" {ATTR} >{TEXT}</a>";
            //format += @"<b class='arrow'></b>";
            format += @"</li>";

            if (ControllerName.Equals(controllerName, StringComparison.InvariantCultureIgnoreCase) && ActionName.Equals(actionName, StringComparison.InvariantCultureIgnoreCase))
            {
                return RenderItemFormat(functionName, format, text, actionName, controllerName, routeValues, htmlAtts, "active");
            }

            return RenderItemFormat(functionName, format, text, actionName, controllerName, routeValues, htmlAtts);
        }

        public HtmlString RenderSubMenuItemNoIcon(string functionName, string text, string actionName, string controllerName = null, object routeValues = null, object htmlAtts = null)
        {
            string format = "";
            format += @"<li class=''>";
            //format += @"<a href=""{URL}"" {ATTR} >{TEXT}</a>";
            format += @"<a href=""{URL}"" {ATTR} >{TEXT}</a>";
            //format += @"<b class='arrow'></b>";
            format += @"</li>";

            if (ControllerName.Equals(controllerName, StringComparison.InvariantCultureIgnoreCase) && ActionName.Equals(actionName, StringComparison.InvariantCultureIgnoreCase))
            {
                return RenderItemFormat(functionName, format, text, actionName, controllerName, routeValues, htmlAtts, "active");
            }

            return RenderItemFormat(functionName, format, text, actionName, controllerName, routeValues, htmlAtts);
        }

        public HtmlString RenderLink(string functionName, string text, string actionName, string controllerName = null, object routeValues = null, object htmlAtts = null)
        {
            string format = @"<a href=""{URL}"" {ATTR}>{TEXT}</a>";
            return RenderItemFormat(functionName, format, text, actionName, controllerName, routeValues, htmlAtts);
        }


        public HtmlString RenderItemFormat(string functionName, string format, string text = null, string actionName = null, string controllerName = null, object routeValues = null, object htmlAtts = null)
        {

            return RenderItemFormat(functionName, format, text, actionName, controllerName, routeValues, htmlAtts, "");
            //return ret;
        }



        private HtmlString RenderItemFormat(string functionName, string format, string text, string actionName, string controllerName, object routeValues, object htmlAtts, string className)
        {
            string ret = "";


            if (!string.IsNullOrEmpty(functionName))
            {
                var functions = functionName.Split(',');
                if (!CheckAccess(functions))
                {
                    return new HtmlString(ret);
                }
            }
            string atts = "";


            if (htmlAtts != null)
            {

                foreach (System.Reflection.PropertyInfo fi in htmlAtts.GetType().GetProperties())
                {
                    string attName = fi.Name;
                    object attVal = fi.GetValue(htmlAtts, null);

                    if (attName.Equals("class", StringComparison.InvariantCultureIgnoreCase))
                    {
                        className = string.Format("{0} {1}", attVal, className);
                    }
                    else
                    {
                        atts += string.Format(" {0}=\"{1}\"", attName, attVal);
                    }
                }
            }


            atts = string.Format("class=\"{0}\"{1}", className, atts);
            string url = Url.Action(actionName, controllerName, routeValues);
            string linkText = string.IsNullOrEmpty(text) ? url : text;
            ret = format.Replace(@"{URL}", url).Replace(@"{ATTR}", atts).Replace(@"{TEXT}", linkText);




            return new HtmlString(ret);
            //return ret;
        }

        public string View7So(object so)
        {
            var strSo = so.ToString();
            var len = strSo.Length;
            var num0 = 0;
            var soht = "";
            if (len < 7)
            {
                num0 = 7 - len;
                for (var i = 0; i < num0; i++)
                {
                    soht += "0";
                }
            }
            soht += strSo;
            return soht;
        }

        //public string DocSoThanhChu(long so)
        //{
        //    var tienbangchu = Models.clsDocTien.DocSo(so.ToString());
        //    var b = tienbangchu.Substring(0, 1).ToUpper();
        //    var c = b + tienbangchu.Substring(1) + " đồng ./.";
        //    return c;
        //}

        public string DocSoThanhChu(string so)
        {
            var tienbangchu = Models.clsDocTien.DocSo(so.ToString());
            var b = tienbangchu.Substring(0, 1).ToUpper();
            var c = b + tienbangchu.Substring(1) + " đồng ./.";
            return c;
        }

        public string GetValueCustomFieldInvoice(int vitri, Invoice_Invoice invoice)
        {
            var value = "";
            switch (vitri)
            {
                case 1:
                    {
                        value = invoice.InvoiceCF1 == null ? "" : invoice.InvoiceCF1.ToString();
                        break;
                    }
                case 2:
                    {
                        value = invoice.InvoiceCF2 == null ? "" : invoice.InvoiceCF2.ToString();
                        break;
                    }
                case 3:
                    {
                        value = invoice.InvoiceCF3 == null ? "" : invoice.InvoiceCF3.ToString();
                        break;
                    }
                case 4:
                    {
                        value = invoice.InvoiceCF4 == null ? "" : invoice.InvoiceCF4.ToString();
                        break;
                    }
                case 5:
                    {
                        value = invoice.InvoiceCF5 == null ? "" : invoice.InvoiceCF5.ToString();
                        break;
                    }
                case 6:
                    {
                        value = invoice.InvoiceCF6 == null ? "" : invoice.InvoiceCF6.ToString();
                        break;
                    }
                case 7:
                    {
                        value = invoice.InvoiceCF7 == null ? "" : invoice.InvoiceCF7.ToString();
                        break;
                    }
                case 8:
                    {
                        value = invoice.InvoiceCF8 == null ? "" : invoice.InvoiceCF8.ToString();
                        break;
                    }
                case 9:
                    {
                        value = invoice.InvoiceCF9 == null ? "" : invoice.InvoiceCF9.ToString();
                        break;
                    }
                case 10:
                    {
                        value = invoice.InvoiceCF10 == null ? "" : invoice.InvoiceCF10.ToString();
                        break;
                    }
            }
            return value;
        }
        public string GetValueCustomFielDtl(string DBFieldName, Invoice_InvoiceDtl invoiceDtl)
        {
            var value = "";
            switch (DBFieldName)
            {
                case "CUSTOMFIELD_DTL1":
                    {
                        value = invoiceDtl.InvoiceDCF1 == null ? "" : invoiceDtl.InvoiceDCF1.ToString();
                        break;
                    }
                case "CUSTOMFIELD_DTL2":
                    {
                        value = invoiceDtl.InvoiceDCF2 == null ? "" : invoiceDtl.InvoiceDCF2.ToString();
                        break;
                    }
                case "CUSTOMFIELD_DTL3":
                    {
                        value = invoiceDtl.InvoiceDCF3 == null ? "" : invoiceDtl.InvoiceDCF3.ToString();
                        break;
                    }
                case "CUSTOMFIELD_DTL4":
                    {
                        value = invoiceDtl.InvoiceDCF4 == null ? "" : invoiceDtl.InvoiceDCF4.ToString();
                        break;
                    }
                case "CUSTOMFIELD_DTL5":
                    {
                        value = invoiceDtl.InvoiceDCF5 == null ? "" : invoiceDtl.InvoiceDCF5.ToString();
                        break;
                    }
                case "INVDCF1":
                    {
                        value = invoiceDtl.InvoiceDCF1 == null ? "" : invoiceDtl.InvoiceDCF1.ToString();
                        break;
                    }
                case "INVDCF2":
                    {
                        value = invoiceDtl.InvoiceDCF2 == null ? "" : invoiceDtl.InvoiceDCF2.ToString();
                        break;
                    }
                case "INVDCF3":
                    {
                        value = invoiceDtl.InvoiceDCF3 == null ? "" : invoiceDtl.InvoiceDCF3.ToString();
                        break;
                    }
                case "INVDCF4":
                    {
                        value = invoiceDtl.InvoiceDCF4 == null ? "" : invoiceDtl.InvoiceDCF4.ToString();
                        break;
                    }
                case "INVDCF5":
                    {
                        value = invoiceDtl.InvoiceDCF5 == null ? "" : invoiceDtl.InvoiceDCF5.ToString();
                        break;
                    }
            }
            return value;
        }

        public static string LHCDecompress(string data)
        {
            byte[] gzBuffer = Convert.FromBase64String(data);
            using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
            {
                int msgLen = BitConverter.ToInt32(gzBuffer, 0);
                ms.Write(gzBuffer, 4, gzBuffer.Length - 4);
                byte[] buffer = new byte[msgLen - 1];
                ms.Position = 0;
                using (System.IO.Compression.GZipStream zip = new System.IO.Compression.GZipStream(ms, System.IO.Compression.CompressionMode.Decompress))
                {
                    zip.Read(buffer, 0, buffer.Length);
                }
                return System.Text.Encoding.UTF8.GetString(buffer);
            }
        }
        #endregion

        #region["check access"]
        public bool CheckAccess(params string[] functionNames)
        {
            var checkAccess = false;
            //if (functionNames == null || functionNames.Length == 0 || (functionNames.Length == 1 && string.IsNullOrEmpty(functionNames[0]))) return true;
            if (Session != null && Session.UserState != null)
            {
                if (Session.UserState.IsSysAdmin)
                {
                    return true;
                }
                else
                {
                    if (Session.UserState.ListSysAccess != null)
                    {
                        foreach (string f in functionNames)
                        {
                            if (Session.UserState.ListSysAccess.Any(item => f.Trim().ToUpper() == item.ObjectCode.Trim().ToUpper()))
                            {
                                checkAccess = true;
                                break;
                            }
                            //foreach (var item in Session.UserState.ListSysAccess)
                            //{
                            //    if (item.ObjectCode.Trim().ToUpper().Equals(f.Trim().ToUpper()))
                            //    {
                            //        checkAccess = true;
                            //        break;
                            //    }
                            //}
                        }
                    }
                }
            }

            return checkAccess;
        }
        #endregion
    }

    public class BaseViewPage : BaseViewPage<object>
    {

    }
}