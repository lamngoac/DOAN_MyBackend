using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;

namespace idn.Skycic.Inventory.WebAdmin.Extensions.Pager
{
    public class Pager
    {
        private HtmlHelper _htmlHelper;
        private readonly int currentPage;
        private readonly RouteValueDictionary linkWithoutPageValuesDictionary;
        private readonly int pageSize;
        private readonly int totalItemCount;

        public Pager()
        {
            this.LabelNext = "Next >>";
            this.LabelPrevious = "<< Previous";
            this.LabelFirst = "First";
            this.LabelLast = "Last";
        }

        public Pager(HtmlHelper htmlHelper, int pageSize, int currentPage, int totalItemCount, RouteValueDictionary valuesDictionary, string labelPrevious, string labelNext, string labelFirst, string labelLast)
            : this()
        {
            this._htmlHelper = htmlHelper;
            this.pageSize = pageSize;
            this.currentPage = currentPage;
            this.totalItemCount = totalItemCount;
            this.linkWithoutPageValuesDictionary = valuesDictionary;
            if (labelPrevious != null)
            {
                this.LabelPrevious = labelPrevious;
            }
            if (labelNext != null)
            {
                this.LabelNext = labelNext;
            }
            if (labelFirst != null)
            {
                this.LabelFirst = labelFirst;
            }
            if (labelLast != null)
            {
                this.LabelLast = labelLast;
            }
        }

        private MvcHtmlString GeneratePageLink(string linkText, string culture, int pageIndex, object htmlAttributes)
        {
            RouteValueDictionary routeValues = new RouteValueDictionary(this._htmlHelper.ViewContext.RouteData.Values);
            routeValues.Remove("action");
            routeValues.Remove("controller");
            routeValues["culturename"] = culture;
            routeValues["page"] = pageIndex;
            foreach (KeyValuePair<string, object> pair in this.linkWithoutPageValuesDictionary)
            {
                if (!routeValues.ContainsKey(pair.Key))
                {
                    routeValues.Add(pair.Key, pair.Value);
                }
            }
            return this._htmlHelper.ActionLink(linkText, this._htmlHelper.ViewContext.RouteData.Values["action"].ToString(), this._htmlHelper.ViewContext.RouteData.Values["controller"].ToString(), routeValues, new RouteValueDictionary(htmlAttributes));
        }

        public MvcHtmlString RenderHtml()
        {
            int num9;
            string culture = (string)this._htmlHelper.ViewContext.RouteData.Values["CultureName"];
            int num = (int)Math.Ceiling((double)(((double)this.totalItemCount) / ((double)this.pageSize)));
            if (num <= 1)
            {
                return null;
            }
            int num2 = 10;
            StringBuilder builder = new StringBuilder();
            if (this.currentPage > 0)
            {
                builder.Append(this.GeneratePageLink(this.LabelFirst, culture, 0, new { @class = "previous", @title = "trước", rel = "prev" }));
                builder.Append(this.GeneratePageLink(this.LabelPrevious, culture, this.currentPage - 1, new { @class = "previous", @title = "trước", rel = "prev" }));
            }
            else
            {
                builder.Append("<span class=\"disabled previous\">" + this._htmlHelper.Encode(this.LabelFirst) + "</span> &nbsp;");
                builder.Append("<span class=\"disabled previous\">" + this._htmlHelper.Encode(this.LabelPrevious) + "</span>");
            }
            int num3 = 0;
            int num4 = num;
            if (num > num2)
            {
                int num5 = ((int)Math.Ceiling((double)(((double)num2) / 2.0))) - 1;
                int num6 = this.currentPage - num5;
                int num7 = this.currentPage + num5;
                if (num6 < 4)
                {
                    num7 = num2;
                    num6 = 0;
                }
                else if (num7 > (num - 4))
                {
                    num7 = num;
                    num6 = num - num2;
                }
                num3 = num6;
                num4 = num7;
            }
            if (num3 > 3)
            {
                builder.Append(this.GeneratePageLink("1", culture, 0, null));
                builder.Append(this.GeneratePageLink("2", culture, 1, null));
                builder.Append("<span class=\"more\">...</span>");
            }
            for (int i = num3; i < num4; i++)
            {
                if (i == this.currentPage)
                {
                    builder.AppendFormat("<span class=\"current\">{0}</span>", i + 1);
                }
                else
                {
                    num9 = i + 1;
                    builder.Append(this.GeneratePageLink(num9.ToString(), culture, i, null));
                }
            }
            if (num4 < (num - 3))
            {
                builder.Append("<span class=\"more\">...</span>");
                num9 = num - 1;
                builder.Append(this.GeneratePageLink(num9.ToString(), culture, num - 2, null));
                builder.Append(this.GeneratePageLink(num.ToString(), culture, num - 1, null));
            }
            if (this.currentPage < (num - 1))
            {
                builder.Append(this.GeneratePageLink(this.LabelNext, culture, this.currentPage + 1, new { @class = "next", @title = "sau", rel = "next" }));
                builder.Append(this.GeneratePageLink(this.LabelLast, culture, num - 1, new { @class = "next", @title = "sau", rel = "next" }));
            }
            else
            {
                builder.Append("<span class=\"disabled next\">" + this._htmlHelper.Encode(this.LabelNext) + "</span>");
                builder.Append("<span class=\"disabled next\">" + this._htmlHelper.Encode(this.LabelLast) + "</span>");
            }
            builder.Insert(0, "<div class=\"pager\">");
            builder.Append("</div>");
            return MvcHtmlString.Create(builder.ToString());
        }

        public MvcHtmlString RenderHtmlBootstrap1()
        {
            var pageCount = 0;
            int num9;
            var currentPageReal = 0;
            if (this.currentPage >= 0)
            {
                currentPageReal = this.currentPage + 1;
            }
            var culture = (string)this._htmlHelper.ViewContext.RouteData.Values["CultureName"];
            var num = (int)Math.Ceiling((double)(((double)this.totalItemCount) / ((double)this.pageSize)));
            if ((int)this.totalItemCount % ((int)this.pageSize) == 0)
            {
                pageCount = (int)this.totalItemCount / ((int)this.pageSize);
            }
            else
            {
                pageCount = (int)this.totalItemCount / ((int)this.pageSize) + 1;
            }
            if (num <= 1)
            {
                return null;
            }
            int num2 = 10;
            var builder = new StringBuilder();
            int num3 = 0;
            int num4 = num;
            if (num > num2)
            {
                int num5 = ((int)Math.Ceiling((double)(((double)num2) / 2.0))) - 1;
                int num6 = currentPageReal - num5;
                int num7 = currentPageReal + num5;
                if (num6 < 4)
                {
                    num7 = num2;
                    num6 = 0;
                }
                else if (num7 > (num - 4))
                {
                    num7 = num;
                    num6 = num - num2;
                }
                num3 = num6;
                num4 = num7;
            }
            builder.Append("<ul class=\"pagination pull-right\">");
            if (currentPageReal > 0)
            {
                builder.Append("<li>" + this.GeneratePageLink(this.LabelFirst, culture, 0, new { @class = "first", @title = "đầu tiên", rel = "first" }) + "</li>");
                builder.Append("<li>" + this.GeneratePageLink(this.LabelPrevious, culture, currentPageReal - 1, new { @class = "previous", @title = "trước", rel = "previous" }) + "</li>");
                if ((currentPageReal - num2 == 0) || (currentPageReal + 1 - num2 == 0))
                {
                    builder.Append("<li>" + this.GeneratePageLink("1", culture, 0, null) + "</li>");
                    builder.Append("<li class=\"disabled\"><a href=\"#\">...</a></li>");
                }
            }
            else
            {
                builder.Append("<li class=\"disabled\"><a href=\"#\">" + this.LabelFirst + "</a></li>");
                builder.Append("<li class=\"disabled\"><a href=\"#\">" + this.LabelPrevious + "</a></li>");
            }
            if (num3 > 3)
            {
                builder.Append("<li>" + this.GeneratePageLink("1", culture, 0, null) + "</li>");
                builder.Append("<li>" + this.GeneratePageLink("2", culture, 1, null) + "</li>");
                builder.Append("<li class=\"disabled\"><a href=\"#\">...</a></li>");
            }
            for (int i = num3; i < num4; i++)
            {
                if (i == currentPageReal)
                {
                    builder.AppendFormat("<li class=\"active\"><a href=\"#\">{0}</a></li>", i + 1);
                }
                else
                {
                    num9 = i + 1;
                    builder.Append("<li>" + this.GeneratePageLink(num9.ToString(CultureInfo.InvariantCulture), culture, i, null) + "</li>");
                }
            }
            if (num4 <= (num - 3))
            {
                builder.Append("<li class=\"disabled\"><a href=\"#\">...</a></li>");
                num9 = num - 1;
                builder.Append("<li>" + this.GeneratePageLink(num9.ToString(CultureInfo.InvariantCulture), culture, num - 2, null) + "</li>");
                builder.Append("<li>" + this.GeneratePageLink(num.ToString(CultureInfo.InvariantCulture), culture, num - 1, null) + "</li>");
            }
            if (currentPageReal < (num - 1))
            {
                if (pageCount - num2 > 0)
                {
                    if ((currentPageReal + 3) == (num - 1))
                    {
                        builder.Append("<li class=\"disabled\"><a href=\"#\">...</a></li>");
                        builder.Append("<li>" + this.GeneratePageLink(num.ToString(CultureInfo.InvariantCulture), culture, num - 1, null) + "</li>");
                    }
                }

                builder.Append("<li>" + this.GeneratePageLink(this.LabelNext, culture, currentPageReal + 1, new { @class = "next", @title = "sau", rel = "next" }) + "</li>");
                builder.Append("<li>" + this.GeneratePageLink(this.LabelLast, culture, (num - 1), new { @class = "last", @title = "cuối cùng", rel = "last" }) + "</li>");
            }
            else
            {
                builder.Append("<li class=\"disabled\"><a href=\"#\">" + this.LabelNext + "</a></li>");
                builder.Append("<li class=\"disabled\"><a href=\"#\">" + this.LabelLast + "</a></li>");
            }
            builder.Append("</ul>");
            return MvcHtmlString.Create(builder.ToString());
        }

        public MvcHtmlString RenderHtmlBootstrap()
        {
            var builder = new StringBuilder();
            var pageCount = 0;
            int pageShow = GetConfigPageShow();
            var currentPageReal = 0;
            if (this.currentPage >= 0)
            {
                currentPageReal = this.currentPage + 1;
            }
            var culture = (string)this._htmlHelper.ViewContext.RouteData.Values["CultureName"];
            pageCount = (int)Math.Ceiling((double)(((double)this.totalItemCount) / ((double)this.pageSize)));
            if (pageCount <= 1)
            {
                return null;
            }
            builder.Append("<ul class=\"pagination pull-right\">");

            #region[""]
            if (currentPageReal < 1)
            {
                builder.Append("<li class=\"disabled\"><a href=\"#\">" + this.LabelFirst + "</a></li>");
                builder.Append("<li class=\"disabled\"><a href=\"#\">" + this.LabelPrevious + "</a></li>");
            }
            else
            {
                builder.Append("<li>" + this.GeneratePageLink(this.LabelFirst, culture, 0, new { @class = "first", @title = "đầu tiên", rel = "first" }) + "</li>");
                builder.Append("<li>" + this.GeneratePageLink(this.LabelPrevious, culture, currentPageReal - 1, new { @class = "previous", @title = "trước", rel = "previous" }));

            }
            #region["Phân đoạn số trang hiển thị"]
            //var checkShowPage = true;
            if (pageCount <= pageShow)
            {
                #region[""]
                for (var i = 0; i < pageCount; i++)
                {
                    if (currentPageReal == i)
                    {
                        builder.AppendFormat("<li class=\"active\"><a href=\"#\">{0}</a></li>", i + 1);
                    }
                    else
                    {
                        builder.Append("<li>" + this.GeneratePageLink((i + 1).ToString(CultureInfo.InvariantCulture), culture, i, null) + "</li>");
                    }
                }

                #endregion
            }
            else
            {
                //var num = (currentPageReal + 1) / pageShow;
                var num = currentPageReal / pageShow;
                if (num == 0)
                {
                    #region[""]
                    if (currentPageReal >= pageShow / 2)
                    {
                        builder.Append("<li>" + this.GeneratePageLink("1", culture, 0, null) + "</li>");
                        builder.Append("<li>" + this.GeneratePageLink("2", culture, 1, null) + "</li>");
                        builder.Append("<li class=\"disabled\"><a href=\"#\">...</a></li>");

                        var _pageShow = ((pageShow / 2) * (num + 1)) + 7;
                        if (_pageShow < pageCount)
                        {
                            #region[""]
                            for (int i = (pageShow / 2); i <= _pageShow; i++)
                            {
                                if ((currentPageReal + 1) == i)
                                {
                                    builder.AppendFormat("<li class=\"active\"><a href=\"#\">{0}</a></li>", i);
                                }
                                else
                                {
                                    builder.Append("<li>" + this.GeneratePageLink((i).ToString(CultureInfo.InvariantCulture), culture, (i - 1), null) + "</li>");
                                }
                            }
                            if (pageCount - 2 >= _pageShow)
                            {
                                builder.Append("<li class=\"disabled\"><a href=\"#\">...</a></li>");
                                builder.Append("<li>" + this.GeneratePageLink((pageCount - 2).ToString(CultureInfo.InvariantCulture), culture, (pageCount - 2 - 1), null) + "</li>");
                            }
                            if (pageCount - 1 >= _pageShow)
                            {
                                builder.Append("<li>" + this.GeneratePageLink((pageCount - 1).ToString(CultureInfo.InvariantCulture), culture, (pageCount - 1 - 1), null) + "</li>");
                            }
                            #endregion
                        }
                        else
                        {
                            #region[""]
                            for (int i = (pageShow / 2); i < pageCount; i++)
                            {
                                if (currentPageReal == i)
                                {
                                    builder.AppendFormat("<li class=\"active\"><a href=\"#\">{0}</a></li>", i + 1);
                                }
                                else
                                {
                                    builder.Append("<li>" + this.GeneratePageLink((i + 1).ToString(CultureInfo.InvariantCulture), culture, (i), null) + "</li>");
                                }
                            }
                            #endregion
                        }
                    }
                    else
                    {
                        #region[""]
                        for (var i = 0; i < pageShow; i++)
                        {
                            if (currentPageReal == i)
                            {
                                builder.AppendFormat("<li class=\"active\"><a href=\"#\">{0}</a></li>", i + 1);
                            }
                            else
                            {
                                builder.Append("<li>" + this.GeneratePageLink((i + 1).ToString(CultureInfo.InvariantCulture), culture, i, null) + "</li>");
                            }
                        }
                        //if (pageCount - 2 >= pageShow)
                        //{
                        //    builder.Append("<li class=\"disabled\"><a href=\"#\">...</a></li>");
                        //    builder.Append("<li>" + this.GeneratePageLink((pageCount - 2).ToString(CultureInfo.InvariantCulture), culture, (pageCount - 2 - 1), null) + "</li>");
                        //}
                        //if (pageCount - 1 >= pageShow)
                        //{
                        //    builder.Append("<li>" + this.GeneratePageLink((pageCount - 1).ToString(CultureInfo.InvariantCulture), culture, (pageCount - 1 - 1), null) + "</li>");
                        //}
                        if (pageCount - 2 > pageShow)
                        {
                            builder.Append("<li class=\"disabled\"><a href=\"#\">...</a></li>");
                            builder.Append("<li>" + this.GeneratePageLink((pageCount - 2).ToString(CultureInfo.InvariantCulture), culture, (pageCount - 2 - 1), null) + "</li>");
                        }
                        if (pageCount - 1 > pageShow)
                        {
                            builder.Append("<li>" + this.GeneratePageLink((pageCount - 1).ToString(CultureInfo.InvariantCulture), culture, (pageCount - 1 - 1), null) + "</li>");
                        }
                        #endregion
                    }
                    #endregion
                }
                else
                {
                    #region[""]
                    if (currentPageReal >= ((pageShow / 2) + (pageShow * num)))
                    {
                        builder.Append("<li>" + this.GeneratePageLink(((pageShow + 1) + (pageShow * num) - pageShow).ToString(CultureInfo.InvariantCulture), culture, ((pageShow + 1) + (pageShow * num) - pageShow) - 1, null) + "</li>");
                        builder.Append("<li>" + this.GeneratePageLink(((pageShow + 1) + (pageShow * num) - pageShow + 1).ToString(CultureInfo.InvariantCulture), culture, ((pageShow + 1) + (pageShow * num) - pageShow), null) + "</li>");
                        builder.Append("<li class=\"disabled\"><a href=\"#\">...</a></li>");

                        var _pageShow = ((pageShow / 2) + (pageShow * num)) + 7;
                        if (_pageShow < pageCount)
                        {
                            #region[""]
                            for (int i = ((pageShow / 2) + (pageShow * num)); i <= _pageShow; i++)
                            {
                                if ((currentPageReal + 1) == i)
                                {
                                    builder.AppendFormat("<li class=\"active\"><a href=\"#\">{0}</a></li>", i);
                                }
                                else
                                {
                                    builder.Append("<li>" + this.GeneratePageLink((i).ToString(CultureInfo.InvariantCulture), culture, (i - 1), null) + "</li>");
                                }
                            }
                            if (pageCount - 2 > _pageShow)
                            {
                                builder.Append("<li class=\"disabled\"><a href=\"#\">...</a></li>");
                                builder.Append("<li>" + this.GeneratePageLink((pageCount - 2).ToString(CultureInfo.InvariantCulture), culture, (pageCount - 2 - 1), null) + "</li>");
                            }
                            if (pageCount - 1 > _pageShow)
                            {
                                builder.Append("<li>" + this.GeneratePageLink((pageCount - 1).ToString(CultureInfo.InvariantCulture), culture, (pageCount - 1 - 1), null) + "</li>");
                            }
                            #endregion
                        }
                        else
                        {
                            #region[""]
                            for (int i = ((pageShow / 2) + (pageShow * num)); i <= pageCount; i++)
                            {
                                if ((currentPageReal + 1) == i)
                                {
                                    builder.AppendFormat("<li class=\"active\"><a href=\"#\">{0}</a></li>", i);
                                }
                                else
                                {
                                    builder.Append("<li>" + this.GeneratePageLink((i).ToString(CultureInfo.InvariantCulture), culture, (i - 1), null) + "</li>");
                                }
                            }
                            #endregion
                        }
                    }
                    else
                    {
                        builder.Append("<li>" + this.GeneratePageLink(((pageShow / 2) + (pageShow * num) - pageShow).ToString(CultureInfo.InvariantCulture), culture, ((pageShow / 2) + (pageShow * num) - pageShow) - 1, null) + "</li>");
                        builder.Append("<li>" + this.GeneratePageLink(((pageShow / 2) + (pageShow * num) - pageShow + 1).ToString(CultureInfo.InvariantCulture), culture, ((pageShow / 2) + (pageShow * num) - pageShow), null) + "</li>");
                        builder.Append("<li class=\"disabled\"><a href=\"#\">...</a></li>");

                        var _pageShow = (pageShow * num) + 7;
                        #region[""]
                        if (_pageShow < pageCount)
                        {
                            #region[""]
                            for (int i = (pageShow * num); i <= _pageShow; i++)
                            {
                                if ((currentPageReal + 1) == i)
                                {
                                    builder.AppendFormat("<li class=\"active\"><a href=\"#\">{0}</a></li>", i);
                                }
                                else
                                {
                                    builder.Append("<li>" + this.GeneratePageLink((i).ToString(CultureInfo.InvariantCulture), culture, (i - 1), null) + "</li>");
                                }
                            }
                            if (pageCount - 2 > _pageShow)
                            {
                                builder.Append("<li class=\"disabled\"><a href=\"#\">...</a></li>");
                                builder.Append("<li>" + this.GeneratePageLink((pageCount - 2).ToString(CultureInfo.InvariantCulture), culture, (pageCount - 2 - 1), null) + "</li>");
                            }
                            if (pageCount - 1 > _pageShow)
                            {
                                builder.Append("<li>" + this.GeneratePageLink((pageCount - 1).ToString(CultureInfo.InvariantCulture), culture, (pageCount - 1 - 1), null) + "</li>");
                            }
                            #endregion
                        }
                        else
                        {
                            #region[""]
                            for (int i = (pageShow * num); i <= pageCount; i++)
                            {
                                if ((currentPageReal + 1) == i)
                                {
                                    builder.AppendFormat("<li class=\"active\"><a href=\"#\">{0}</a></li>", i);
                                }
                                else
                                {
                                    builder.Append("<li>" + this.GeneratePageLink((i).ToString(CultureInfo.InvariantCulture), culture, (i - 1), null) + "</li>");
                                }
                            }
                            #endregion
                        }
                        #endregion

                    }
                    #endregion
                }

            }
            #endregion
            if (currentPageReal == pageCount - 1)
            {
                builder.Append("<li class=\"disabled\"><a href=\"#\">" + this.LabelNext + "</a></li>");
                builder.Append("<li class=\"disabled\"><a href=\"#\">" + this.LabelLast + "</a></li>");
            }
            else
            {
                builder.Append("<li>" + this.GeneratePageLink(this.LabelNext, culture, currentPageReal + 1, new { @class = "next", @title = "sau", rel = "next" }) + "</li>");
                builder.Append("<li>" + this.GeneratePageLink(this.LabelLast, culture, (pageCount - 1), new { @class = "last", @title = "cuối cùng", rel = "last" }) + "</li>");
            }
            #endregion

            builder.Append("</ul>");
            return MvcHtmlString.Create(builder.ToString());
        }

        public string LabelFirst { get; set; }

        public string LabelLast { get; set; }

        public string LabelNext { get; set; }

        public string LabelPrevious { get; set; }

        public int GetConfigPageShow()
        {
            var pageShow = 10;
            if (ConfigurationManager.AppSettings != null)
            {
                var keyValue = System.Configuration.ConfigurationManager.AppSettings["PageShow"];
                if (keyValue != null)
                {
                    pageShow = Convert.ToInt32(keyValue.ToString(CultureInfo.InvariantCulture).Trim());
                }
            }
            return pageShow;
        }
    }
}