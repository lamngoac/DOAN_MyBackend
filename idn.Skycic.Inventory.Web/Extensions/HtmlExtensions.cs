using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;
using System.Web.UI;

namespace idn.Skycic.Inventory.Web.Extensions
{
    public static class HtmlExtensions
    {

        public static string ResourceVersion = "";

        public static string GetModelAttemptedValue(this HtmlHelper htmlHelper, string key)
        {
            ModelState state;
            if (htmlHelper.ViewData.ModelState.TryGetValue(key, out state))
            {
                return state.Value.AttemptedValue;
            }
            return null;
        }

        public static string EvalString(this HtmlHelper htmlHelper, string key)
        {
            return Convert.ToString(htmlHelper.ViewData.Eval(key), CultureInfo.CurrentCulture);
        }

        public static MvcHtmlString MetaDescription(this HtmlHelper htmlHelper, string content)
        {
            if (content != null)
            {
                TagBuilder builder = new TagBuilder("meta");
                builder.Attributes.Add("name", "description");
                content = Regex.Replace(content, "\r|\n", "");
                content = Regex.Replace(content, "(&nbsp;)+|(\t+)", " ");
                builder.Attributes.Add("content", SecurityElement.Escape(content));
                return MvcHtmlString.Create(builder.ToString(TagRenderMode.SelfClosing));
            }
            else
            {
                return null;
            }
        }


        /// <summary>
        /// Hop-PC
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="name"></param>
        /// <param name="selectList"></param>
        /// <param name="defaultValue"></param>
        /// <param name="defaultText"></param>
        /// <returns></returns>
        public static SelectList ToSelectList<TEnum>(this TEnum enumObj)
        {
            var values = from TEnum e in Enum.GetValues(typeof(TEnum))
                         select new { Id = (int)Enum.Parse(typeof(TEnum), e.ToString()), Name = e.ToString() };

            return new SelectList(values, "Id", "Name", (int)Enum.Parse(typeof(TEnum), enumObj.ToString()));
        }

        public static MvcHtmlString DropDownListDefault(this HtmlHelper htmlHelper, string name, IEnumerable<SelectListItem> selectList, object defaultValue, string defaultText)
        {
            List<SelectListItem> list = new List<SelectListItem>(selectList);
            SelectListItem item = new SelectListItem();
            item.Text = defaultText;
            item.Value = defaultValue.ToString();
            list.Insert(0, item);
            return htmlHelper.DropDownList(name, list);
        }
        public static MvcHtmlString DropDownListDefault(this HtmlHelper htmlHelper, string name, IEnumerable<SelectListItem> selectList, object defaultValue, string defaultText, object htmAttribute = null)
        {
            List<SelectListItem> list = new List<SelectListItem>(selectList);
            SelectListItem item = new SelectListItem();
            item.Text = defaultText;
            item.Value = defaultValue.ToString();
            list.Insert(0, item);
            return htmlHelper.DropDownList(name, list, htmAttribute);
        }
        public static T GetStateValue<T>(this HtmlHelper htmlHelper, string key)
        {
            T value = default(T);
            ModelState state;
            if (htmlHelper.ViewData.ModelState.TryGetValue(key, out state))
            {
                value = (T)state.Value.ConvertTo(typeof(T), null);
            }
            else
            {
                value = (T)htmlHelper.ViewData.Eval(key);
            }
            return value;
        }

        public static string ToTitleOnTable(this string value, int length)
        {
            string temp = "";
            if (value.Length > length)
            {
                temp = value.Substring(0, length) + "...";
            }
            else
            {
                temp = value;
            }
            return temp;
        }

        #region Link
        public static MvcHtmlString Link(this HtmlHelper htmlHelper, string url)
        {
            return htmlHelper.Link(url, null);
        }

        public static MvcHtmlString Link(this HtmlHelper htmlHelper, string url, object htmlAttributes)
        {
            return htmlHelper.Link(null, url, htmlAttributes);
        }

        public static MvcHtmlString Link(this HtmlHelper htmlHelper, string innerHtml, string url)
        {
            return htmlHelper.Link(innerHtml, url, null);
        }

        public static MvcHtmlString Link(this HtmlHelper htmlHelper, string innerHtml, string url, object htmlAttributes)
        {
            IDictionary<string, object> htmlAttributesDictionay = ((IDictionary<string, object>)new RouteValueDictionary(htmlAttributes));
            TagBuilder builder = new TagBuilder("a");
            builder.MergeAttributes<string, object>(htmlAttributesDictionay);
            if (innerHtml == null)
            {
                builder.InnerHtml = url.Replace("http://", "");
            }
            else
            {
                builder.InnerHtml = innerHtml;
            }
            builder.Attributes.Add("href", url);
            return MvcHtmlString.Create(builder.ToString(TagRenderMode.Normal));
        }


        #endregion

        public static MvcHtmlString CheckBoxBit<T>(this HtmlHelper htmlHelper, string name, T value, T expectedBit) where T : struct, IConvertible
        {
            if (!typeof(T).IsEnum)
            {
                throw new ArgumentException("T must be an enumerated type");
            }

            return htmlHelper.CheckBox(name, (Convert.ToInt32(value) & Convert.ToInt32(expectedBit)) > 0, new { value = Convert.ToInt32(expectedBit) });
        }




        #region GetUrl
        /// <summary>
        /// Generates a url based on the routeValues
        /// </summary>
        public static string GetUrl(this HtmlHelper htmlHelper, string actionName, string controllerName, object routeValues)
        {
            return UrlHelper.GenerateUrl(null, actionName, controllerName, new RouteValueDictionary(routeValues), htmlHelper.RouteCollection, htmlHelper.ViewContext.RequestContext, true);
        }
        #endregion


        #region Script, style sheets and images
        /// <summary>
        /// Script tag, enforces to be app relative
        /// </summary>
        public static MvcHtmlString Script(this HtmlHelper htmlHelper, string url)
        {
            ValidateApplicationUrl(url);
            if (url[0] == '~')
            {
                url = url.ToLower();
            }

            if (!string.IsNullOrEmpty(ResourceVersion))
            {
                if (url.Contains("?"))
                {
                    url = string.Format("{0}&rv={1}", url, ResourceVersion);
                }
                else
                {
                    url = string.Format("{0}?rv={1}", url, ResourceVersion);
                }
            }

            TagBuilder builder = new TagBuilder("script");
            builder.Attributes.Add("type", "text/javascript");
            builder.Attributes.Add("src", UrlHelper.GenerateContentUrl(url, htmlHelper.ViewContext.HttpContext));
            return MvcHtmlString.Create(builder.ToString(TagRenderMode.Normal));
        }


        /// <summary>
        /// Add a link tag referencing the stylesheet, enforcing the url to be app relative.
        /// </summary>
        public static MvcHtmlString Stylesheet(this HtmlHelper htmlHelper, string url)
        {

            ValidateApplicationUrl(url);
            if (url[0] == '~')
            {
                url = url.ToLower();
            }

            if (!string.IsNullOrEmpty(ResourceVersion))
            {
                if (url.Contains("?"))
                {
                    url = string.Format("{0}&rv={1}", url, ResourceVersion);
                }
                else
                {
                    url = string.Format("{0}?rv={1}", url, ResourceVersion);
                }
            }

            TagBuilder builder = new TagBuilder("link");
            builder.Attributes.Add("type", "text/css");
            builder.Attributes.Add("rel", "stylesheet");
            builder.Attributes.Add("href", UrlHelper.GenerateContentUrl(url, htmlHelper.ViewContext.HttpContext));
            return MvcHtmlString.Create(builder.ToString(TagRenderMode.SelfClosing));
        }

        /// <summary>
        /// Add a link tag referencing the stylesheet, enforcing the url to be app relative.
        /// </summary>
        public static MvcHtmlString Img(this HtmlHelper htmlHelper, string url, string alt)
        {
            ValidateApplicationUrl(url);
            if (url[0] == '~')
            {
                url = url.ToLower();
            }

            TagBuilder builder = new TagBuilder("img");
            builder.Attributes.Add("alt", alt);
            builder.Attributes.Add("src", UrlHelper.GenerateContentUrl(url, htmlHelper.ViewContext.HttpContext));
            return MvcHtmlString.Create(builder.ToString(TagRenderMode.SelfClosing));
        }

        /// <summary>
        /// Validates that the url is absolute or starts with tilde (~) char.
        /// </summary>
        /// <param name="url"></param>
        private static void ValidateApplicationUrl(string url)
        {
            if (String.IsNullOrEmpty(url))
            {
                throw new ArgumentNullException("url");
            }
            if ((!url.StartsWith("http://")) && (!url.StartsWith("https://")) && url[0] != '~')
            {
                throw new ArgumentException("Url must start tilde character '~' or be absolute.", "url");
            }
        }
        #endregion

        public static MvcHtmlString Nl2Br(this HtmlHelper htmlHelper, string text)
        {
            if (string.IsNullOrEmpty(text))
                return MvcHtmlString.Create(text);
            else
            {
                StringBuilder builder = new StringBuilder();
                string[] lines = text.Split('\n');
                for (int i = 0; i < lines.Length; i++)
                {
                    if (i > 0)
                        builder.Append("<br/>");
                    builder.Append(System.Web.HttpUtility.HtmlEncode(lines[i]));
                }
                return MvcHtmlString.Create(builder.ToString());
            }
        }


        #region VIews
        public static string RenderPartialViewToString(Controller controller, string viewName, object model)
        {

            controller.ViewData.Model = model;
            try
            {
                using (StringWriter sw = new StringWriter())
                {
                    ViewEngineResult viewResult = ViewEngines.Engines.FindPartialView(controller.ControllerContext, viewName);
                    ViewContext viewContext = new ViewContext(controller.ControllerContext, viewResult.View, controller.ViewData, controller.TempData, sw);
                    viewResult.View.Render(viewContext, sw);

                    return sw.GetStringBuilder().ToString();
                }
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

        public static string RenderPartialViewToString(ControllerContext context, string viewName, object model, ViewDataDictionary viewdata)
        {
            try
            {
                using (StringWriter sw = new StringWriter())
                {
                    ViewEngineResult viewResult = ViewEngines.Engines.FindPartialView(context, viewName);
                    ViewContext viewContext = new ViewContext(context, viewResult.View, viewdata, context.Controller.TempData, sw);
                    viewResult.View.Render(viewContext, sw);
                    var strRender = sw.GetStringBuilder().ToString();
                    return strRender;
                }
            }
            catch (Exception ex)
            {
                var strEx = ex.ToString();
                return strEx;
            }
        }

        #endregion


        #region Maps
        public static ExampleConfigurator Configurator(this HtmlHelper instance, string title)
        {
            return new ExampleConfigurator(instance).Title(title);
        }
        #endregion

    }


    public class ExampleConfigurator : IDisposable
    {
        public const string CssClass = "configurator";

        private HtmlTextWriter writer;
        private HtmlHelper htmlHelper;
        private string title;
        private MvcForm form;

        public ExampleConfigurator(HtmlHelper htmlHelper)
        {
            this.htmlHelper = htmlHelper;
            this.writer = new HtmlTextWriter(htmlHelper.ViewContext.Writer);
        }

        public ExampleConfigurator Title(string title)
        {
            this.title = title;

            return this;
        }

        public ExampleConfigurator Begin()
        {
            this.writer.AddAttribute(HtmlTextWriterAttribute.Class, CssClass);
            this.writer.RenderBeginTag(HtmlTextWriterTag.Div);

            this.writer.AddAttribute(HtmlTextWriterAttribute.Class, CssClass + "-legend");
            this.writer.RenderBeginTag(HtmlTextWriterTag.H3);
            this.writer.Write(this.title);
            this.writer.RenderEndTag();

            return this;
        }

        public ExampleConfigurator End()
        {
            this.writer.RenderEndTag();

            if (this.form != null)
            {
                this.form.EndForm();
            }

            return this;
        }

        public void Dispose()
        {
            this.End();
        }

        public ExampleConfigurator PostTo(string action, string controller)
        {
            form = htmlHelper.BeginForm(action, controller);
            return this;
        }


    }
}