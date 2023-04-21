using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace idn.Skycic.Inventory.Website.Extensions
{
    public static class StringExtensions
    {
        public static string Titleize(this string text)
        {
            return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(text).ToSentenceCase();
        }

        public static string ToSentenceCase(this string str)
        {
            return Regex.Replace(str, "[a-z][A-Z]", m => m.Value[0] + " " + char.ToLower(m.Value[1]));
        }
        public static string LanguageDesc(this string code)
        {
            if (string.IsNullOrEmpty(code)) return string.Empty;

            if (code == "vi") return "Tiếng Việt";
            else if (code == "en") return "English";
            else return "";
        }

        public static string LanguageFlag(this string code)
        {
            if (string.IsNullOrEmpty(code)) return string.Empty;

            if (code == "vi") return "flag-icon-vn";
            else if (code == "en") return "flag-icon-us";
            else return "";
        }

        public static string HtmlItemString(this string defaultVal, string itemCode = "", string fieldName = "")
        {

            //HtmlItemFieldTypes fieldType = HtmlItemFieldTypes.TEXT;
            //var ret = defaultVal.HtmlItemObject(fieldType, itemCode, fieldName);
            //var ret = fieldName;
            var ret = defaultVal;
            return ret;
        }

        public static HtmlString EditHtmlItem(this string itemCode, string fieldName = "")
        {
            return new HtmlString(string.Format("rel=\"htmlitemeditor\" item-code=\"{0}\" item-field=\"{1}\"", itemCode, fieldName));
        }
    }
}