using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Data;
using System.Net;
using System.Collections;
using System.Xml;
using System.Linq;
using System.Threading;
//using System.Xml.Linq;

using CmUtils = CommonUtils;
using TDAL = EzDAL.MyDB;
using TDALUtils = EzDAL.Utils;
using TConst = idn.Skycic.Inventory.Constants;
//using TUtils = idn.Skycic.Inventory.Utils;
using TError = idn.Skycic.Inventory.Errors;
using System.Text.RegularExpressions;
using System.Reflection;
using System.ComponentModel;
using System.IO;
using System.Runtime.Serialization;
using System.Security.AccessControl;
using idn.Skycic.Inventory.Common.Models;

////
using RestSharp;
using RestSharp.Authenticators;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Web;

namespace idn.Skycic.Inventory.Utils
{
    public class CUtils
    {
        public static string TidNext(object strTidOrg, ref int n)
        {
            return string.Format("{0}.{1}", strTidOrg, n++);
        }
        public static string StdParam(object objParam)
        {
            if (objParam == null || objParam == DBNull.Value) return null;
            string str = Convert.ToString(objParam).Trim().ToUpper();
            if (str.Length <= 0) return null;
            return str;
        }
        public static string StdStr(object objParam)
        {
            if (objParam == null || objParam == DBNull.Value) return null;
            string str = Convert.ToString(objParam).Trim();
            if (str.Length <= 0) return null;
            return str;
        }
        public static object StdInt(object objParam)
        {
            if (objParam == null || objParam == DBNull.Value) return null;
            return Convert.ToInt32(objParam);
        }
        public static object StdDouble(object objParam)
        {
            if (objParam == null || objParam == DBNull.Value) return null;
            return Convert.ToDouble(objParam);
        }
        public static string StdDate(object objDate)
        {
            if (objDate == null || objDate == DBNull.Value || objDate == "") return null;
            DateTime dtime = Convert.ToDateTime(objDate);
            if (dtime < Convert.ToDateTime("1900-01-01 00:00:00") || dtime > Convert.ToDateTime("2100-01-01 23:59:59"))
                throw new Exception("MyBiz.DateTimeOutOfRange");
            return dtime.ToString("yyyy-MM-dd");
        }
        public static string StdDateT24(object objDate)
        {
            if (objDate == null || objDate == DBNull.Value) return null;
            DateTime dtime = Convert.ToDateTime(objDate);
            if (dtime < Convert.ToDateTime("1900-01-01 00:00:00") || dtime > Convert.ToDateTime("2100-01-01 23:59:59"))
                throw new Exception("MyBiz.DateTimeOutOfRange");
            return dtime.ToString("yyyyMMdd");
        }
        public static string StdT24Date(object objDate)
        {
            ////
            DateTimeFormatInfo dtfi = new DateTimeFormatInfo();
            dtfi.ShortDatePattern = "yyyy-MM-dd";
            dtfi.DateSeparator = "-";
            string strT24format = "yyyyMMdd";
            ////
            if (objDate == null || objDate == DBNull.Value || string.IsNullOrEmpty(Convert.ToString(objDate))) return null;
            DateTime dtime = DateTime.ParseExact(Convert.ToString(objDate), strT24format, dtfi);
            if (dtime < Convert.ToDateTime("1900-01-01 00:00:00") || dtime > Convert.ToDateTime("2100-01-01 23:59:59"))
                throw new Exception("MyBiz.DateTimeOutOfRange");
            return dtime.ToString("yyyy-MM-dd");
        }
        public static string StdDTime(object objDTime)
        {
            if (objDTime == null || objDTime == DBNull.Value || objDTime.ToString().Length == 0) return null;
            DateTime dtime = Convert.ToDateTime(objDTime);
            if (dtime < Convert.ToDateTime("1900-01-01 00:00:00") || dtime > Convert.ToDateTime("2100-01-01 23:59:59"))
                throw new Exception("MyBiz.DateTimeOutOfRange");
            return dtime.ToString("yyyy-MM-dd HH:mm:ss");
        }
        public static string StdT24DTime(object objDTime)
        {
            ////
            DateTimeFormatInfo dtfi = new DateTimeFormatInfo();
            dtfi.ShortDatePattern = "yyyy-MM-dd HH:mm:ss";
            dtfi.DateSeparator = "-";
            string strT24format = "yyyyMMddHHmmss";
            ////
            if (objDTime == null || objDTime == DBNull.Value || objDTime.ToString().Length == 0) return null;
            DateTime dtime = DateTime.ParseExact(Convert.ToString(objDTime), strT24format, dtfi);
            if (dtime < Convert.ToDateTime("1900-01-01 00:00:00") || dtime > Convert.ToDateTime("2100-01-01 23:59:59"))
                throw new Exception("MyBiz.DateTimeOutOfRange");
            return dtime.ToString("yyyy-MM-dd HH:mm:ss");
        }
        public static string StdDTimeBeginDay(object objDTime)
        {
            string strDTime = StdDate(objDTime);
            if (strDTime == null) return null;
            return string.Format("{0} 00:00:00", strDTime);
        }
        public static string StdDTimeEndDay(object objDTime)
        {
            string strDTime = StdDate(objDTime);
            if (strDTime == null) return null;
            return string.Format("{0} 23:59:59", strDTime);
        }
        public static string StdDateBeginOfYear(object objYear)
        {
            if (objYear == null || objYear == DBNull.Value) return null;
            string strDate = string.Format("{0}-01-01 00:00:00", objYear);
            DateTime dtime = Convert.ToDateTime(strDate);
            if (dtime < Convert.ToDateTime("1900-01-01 00:00:00") || dtime > Convert.ToDateTime("2100-01-01 23:59:59"))
                throw new Exception("MyBiz.DateTimeOutOfRange");
            return string.Format("{0}-01-01", objYear);
        }
        public static string StdDateEndOfYear(object objYear)
        {
            if (objYear == null || objYear == DBNull.Value) return null;
            string strDate = string.Format("{0}-12-31 23:59:59", objYear);
            DateTime dtime = Convert.ToDateTime(strDate);
            if (dtime < Convert.ToDateTime("1900-01-01 00:00:00") || dtime > Convert.ToDateTime("2100-01-01 23:59:59"))
                throw new Exception("MyBiz.DateTimeOutOfRange");
            return string.Format("{0}-12-31", objYear);
        }
        public static string GetDateToSearch(object objDate)
        {
            //int nHeuristicPriviousDays = -10; // days
            DateTime dtime = Convert.ToDateTime(objDate);
            //dtime = dtime.AddDays(nHeuristicPriviousDays);
            //dtime = new DateTime(dtime.Year, dtime.Month, 1);
            dtime = new DateTime(dtime.Year, dtime.Month, 1);
            return dtime.ToString("yyyy-MM-dd");
        }
        public static string GetDateToSearch(object objDate, string dateTimeFormat)
        {
            int nHeuristicPriviousDays = -10; // days
            DateTime dtime = Convert.ToDateTime(objDate);
            dtime = dtime.AddDays(nHeuristicPriviousDays);
            dtime = new DateTime(dtime.Year, dtime.Month, 1);
            return dtime.ToString(dateTimeFormat);
        }


        /// <summary>
        /// Uri.CheckHostName("127.0.0.1"); //=> IPv4
        /// Uri.CheckHostName("www.google.com"); //=> Dns
        /// Uri.CheckHostName("localhost"); //=> Dns
        /// Uri.CheckHostName("2000:2000:2000:2000::"); //=> IPv6
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsIPAddress(string input)
        {
            var hostNameType = Uri.CheckHostName(input);

            return hostNameType == UriHostNameType.IPv4 || hostNameType == UriHostNameType.IPv6;
        }

        //Is datetime

        public static bool IsDateTime(object Object)
        {
            string strDate = Object.ToString();
            try
            {

                DateTime dt;
                DateTime.TryParse(strDate,
                    System.Globalization.CultureInfo.CurrentCulture,
                    System.Globalization.DateTimeStyles.None, out dt);

                if (dt > DateTime.MinValue && dt < DateTime.MaxValue)
                    return true;
                return false;
            }
            catch
            {
                return false;
            }
        }

        //Check Datetime đúng định dạng
        public static bool IsDateTimeFormat(object Object)
        {
            string strDate = Object.ToString();
            try
            {
                DateTime dt;
                string[] formats = { "yyyy-MM-dd" };
                if (!DateTime.TryParseExact(strDate, formats,
                                System.Globalization.CultureInfo.InvariantCulture,
                                DateTimeStyles.None, out dt))
                {
                    return false;
                }
                else if (dt > DateTime.MinValue && dt < DateTime.MaxValue) {
                    return true;
                }
                else { return false; }
            }
            catch
            {
                return false;
            }
        }

        // Convert từ giờ UTC => Giờ local
        // Giờ UTC: 2019-11-12 07:00:00
        // => Giờ local: 2019-11-12 08:00:00 (offset = +7)
        public static DateTime ConvertingUTCToLocalTime(object objDate)
        {
            var offset = TimeZoneInfo.Local.GetUtcOffset(DateTime.UtcNow).TotalHours;
            var localTime = DateTimeOffset.Parse(objDate.ToString().Trim()).AddHours(1 * offset).DateTime;
            return localTime;
        }

        public static DateTime ConvertingUTCToLocalTime(object objDate, double offset)
        {
            var localTime = DateTimeOffset.Parse(objDate.ToString().Trim()).AddHours(1 * offset).DateTime;
            return localTime;
        }

        // Convert từ giờ local sang giờ UTC
        // Giờ local: 2019-11-12 08:00:00 (offset = +7)
        // => Giờ UTC: 2019-11-12 07:00:00
        public static DateTime ConvertingLocalTimeToUTC(object objDate)
        {
            var offset = TimeZoneInfo.Local.GetUtcOffset(DateTime.UtcNow).TotalHours;
            var utcTime = DateTimeOffset.Parse(objDate.ToString().Trim()).AddHours(-1 * offset).DateTime;
            return utcTime;
        }

        public static DateTime ConvertingLocalTimeToUTC(object objDate, double offset)
        {
            var utcTime = DateTimeOffset.Parse(objDate.ToString().Trim()).AddHours(-1 * offset).DateTime;
            return utcTime;
        }

        public static string ReturnDateTimeUtcToLocalTime(string datetime, string format, double offset)
        {
            var strDateTime = "";
            if (!CUtils.IsNullOrEmpty(datetime) && CUtils.IsDateTime(datetime))
            {
                var dDateTime = CUtils.ConvertingUTCToLocalTime(datetime, offset);
                strDateTime = dDateTime.ToString(format);
            }
            return strDateTime;
        }

        public static string FormatDate(object objDate)
        {
            try
            {
                return DateTime.Parse(objDate.ToString()).ToString("MM/dd/yyyy");
            }
            catch
            {
                return DateTime.MinValue.ToString("MM/dd/yyyy");
            }
        }

        public static string FormatDate(object objDate, string strFormat)
        {
            try
            {
                return DateTime.Parse(objDate.ToString()).ToString(strFormat);
            }
            catch
            {
                return DateTime.MinValue.ToString(strFormat);
            }
        }

        public static DateTime ConvertToDateTime(string strDateTime)
        {
            DateTime dt;
            DateTime.TryParse(strDateTime,
                System.Globalization.CultureInfo.CurrentCulture,
                System.Globalization.DateTimeStyles.None, out dt);
            return dt;
        }

        //Isnumeric
        public static bool IsNumeric(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            else
            {
                double OutValue;
                return double.TryParse(obj.ToString().Trim(),
                 System.Globalization.NumberStyles.Any,
                   System.Globalization.CultureInfo.CurrentCulture,
                   out OutValue);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool IsInteger(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            else
            {
                int outValue;
                string strValue = Convert.ToString(obj).Trim();
                return Int32.TryParse(strValue, out outValue);
            }
        }

        public static bool IsInteger(string pText)
        {
            Regex regex = new Regex(@"^[0-9]");
            var check = regex.IsMatch(pText);
            return check;
        }

        public static Int32 ConvertToInt32(object obj)
        {
            return Convert.ToInt32(obj);
        }

        public static double ConvertToDouble(object obj)
        {
            return Convert.ToDouble(obj);
        }
        public static decimal ConvertToDecimal(object obj)
        {
            return Convert.ToDecimal(obj);
        }
        public static double LamTronSo(object number)
        {
            var numbercur = Math.Round(Convert.ToDouble(number));
            return numbercur;
        }
        //Math.Round(10.232258,3); //kết quả: 10.232
        //Math.Round(10.232558,3); //kết quả: 10.233
        public static double LamTronSo(object number, int digits)
        {
            var numbercur = Math.Round(Convert.ToDouble(number), digits);
            return numbercur;
        }

        /// <summary>
        /// return value: 1 - 10 - 100 - 1,000
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="strformat"></param>
        /// <returns></returns>
        public static string formatInteger(int obj, string strformat)
        {
            var strNumber = "";
            if (obj < 0)
            {
                obj = obj * (-1);
                strNumber = String.Format(CultureInfo.InvariantCulture, strformat, obj);
                if (obj < 10)
                {

                    strNumber = '-' + strNumber.Substring(1, strNumber.Length - 1);
                }
                else
                {
                    strNumber = '-' + strNumber;
                }
            }
            else
            {
                strNumber = String.Format(CultureInfo.InvariantCulture, strformat, obj);
                if (obj < 10)
                {

                    strNumber = strNumber.Substring(1, strNumber.Length - 1);
                }
            }

            //if(obj < 10)
            //{
            //    if(obj < 0)
            //    {
            //        
            //        if()
            //    }
            //    else
            //    {
            //        strNumber = strNumber.Substring(1, strNumber.Length - 1);
            //    }
            //}
            return strNumber;
        }
        /// <summary>
        /// return value: 1.00 - 10.00 - 100.00 - 1,000.00
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="strformat"></param>
        /// <returns></returns>
        public static string formatNumeric(double obj, string strformat)
        {
            var strNumber = "";
            strNumber = String.Format(CultureInfo.InvariantCulture, strformat, obj);
            if (obj < 0)
            {
                obj = obj * (-1);
                strNumber = String.Format(CultureInfo.InvariantCulture, strformat, obj);
                if (obj < 10)
                {

                    strNumber = '-' + strNumber.Substring(1, strNumber.Length - 1);
                }
                else
                {
                    strNumber = '-' + strNumber;
                }
            }
            else
            {
                strNumber = String.Format(CultureInfo.InvariantCulture, strformat, obj);
                if (obj < 10)
                {
                    strNumber = strNumber.Substring(1, strNumber.Length - 1);
                }
            }

            return strNumber;
        }

        public static TimeSpan TimeSpan(DateTime startdate, DateTime enddate)
        {
            TimeSpan timeSpan = startdate - enddate;
            return timeSpan;
        }

        public static TimeSpan DateTimeSubtract(DateTime startdate, DateTime enddate)
        {
            TimeSpan timeSpan = startdate.Subtract(enddate);
            return timeSpan;
        }

        public static DateTime MinDateTime(DateTime startdate, DateTime enddate)
        {
            int compare = DateTime.Compare(startdate, enddate);
            if (compare < 0)
            {
                return startdate;
            }
            else
            {
                return enddate;
            }
        }

        public static DateTime MaxDateTime(DateTime startdate, DateTime enddate)
        {
            int compare = DateTime.Compare(startdate, enddate);
            if (compare < 0)
            {
                return enddate;
            }
            else
            {
                return startdate;
            }
        }

        public static int TotalMonths(DateTime enddate, DateTime startdate)
        {
            var totalMonths = ((enddate.Year - startdate.Year) * 12) + enddate.Month - startdate.Month;
            return totalMonths;
        }

        public static string FormatDateTime(string strDateTime, string strformat)
        {
            var _strDateTime = "";
            if (!string.IsNullOrEmpty(strDateTime))
            {
                strDateTime = strDateTime.Replace('/', '-');
                var culture = CultureInfo.InvariantCulture;
                DateTime dt = DateTime.ParseExact(strDateTime, strformat, culture);
                _strDateTime = dt.ToString(strformat);
            }
            return _strDateTime;
        }


        /// <summary>
        /// strDateTime: 2019-03-21 => strformat: yyyy-MM-dd
        /// strDateTime: 21-03-2019 => strformat: dd-MM-yyyy
        /// </summary>
        /// <param name="strDateTime"></param>
        /// <param name="strformat"></param>
        /// <returns></returns>
        public static DateTime ConvertToDateTime(string strDateTime, string strformat)
        {
            //System.Threading.Thread.CurrentThread.CurrentCulture = new CultureInfo("en-CA");
            //DateTime dt = DateTime.Parse(strDateTime);

            //// Or
            //DateTime dt1 = DateTime.ParseExact(strDateTime, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            if (!string.IsNullOrEmpty(strDateTime))
            {
                //strDateTime = strDateTime.Replace('/', '-');
                var culture = CultureInfo.InvariantCulture;
                var dateTime = DateTime.ParseExact(strDateTime, strformat, culture);
                return dateTime;
            }
            return DateTime.MinValue;

            //DateTime dt;
            //DateTime.TryParse(strDateTime,
            //    System.Globalization.CultureInfo.CurrentCulture,
            //    System.Globalization.DateTimeStyles.None, out dt);
            //return dt;
        }

        public static DateTime ConvertToDateTimeAddDay(string strDateTime, int addDay, string strformat)
        {
            if (!string.IsNullOrEmpty(strDateTime))
            {
                strDateTime = strDateTime.Replace('/', '-');
                var culture = CultureInfo.InvariantCulture;
                var dateTime = DateTime.ParseExact(strDateTime, strformat, culture).AddDays(addDay);
                return dateTime;
            }
            return DateTime.MinValue;
        }

        public static string strConvertToDateTimeAddDay(string strDateTime, double addDay, string strformat)
        {
            if (!string.IsNullOrEmpty(strDateTime))
            {
                strDateTime = strDateTime.Replace('/', '-');
                var culture = CultureInfo.InvariantCulture;
                var dateTime = DateTime.ParseExact(strDateTime, strformat, culture).AddDays(addDay).ToString(strformat);
                return dateTime;
            }
            return "";
        }

        public static string StretchListString(List<string> lstStr)
        {
            StringBuilder strBuider = new StringBuilder();
            foreach (string str in lstStr)
            {
                strBuider.Append(str).Append(",");
            }
            string strResult = strBuider.ToString();
            if (strResult.Length > 0)
                strResult = strResult.Substring(0, strResult.Length - 1);
            return strResult;
        }

        #region["Email"]
        public static bool IsValidEmail(string email)
        {
            if (!IsNullOrEmpty(email))
            {
                try
                {
                    var addr = new System.Net.Mail.MailAddress(email);
                    return addr.Address == email;
                }
                catch
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public static bool IsEmail(string email)
        {
            if (!IsNullOrEmpty(email))
            {
                return Regex.IsMatch(email, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region["Phone"]
        //public static bool IsPhoneNumber(string number)
        //{
        //    //return Regex.Match(number, @"^([0-9]{12})$").Success;
        //    if (!IsNullOrEmpty(number))
        //    {
        //        if (number.Trim().Length == 10)
        //        {
        //            return Regex.Match(number, @"^(\[0-9]{9})$").Success;
        //            //return Regex.Match(number, @"^(\+[0-9]{9})$").Success;
        //        }
        //        else
        //        {
        //            return Regex.Match(number, @"^(\+[0-9]{10})$").Success;
        //            //return Regex.Match(number, @"^(\+[0-9]{10})$").Success;
        //        }
        //    }
        //    return false;
        //}
        public static bool IsNumber(string number)
        {
            foreach (Char c in number)
            {
                if (!Char.IsDigit(c))
                    return false;
            }
            return true;
        }
        #endregion

        public static string StrReplaceUrl(string strOld, string strNew, string newchar)
        {
            if(!CUtils.IsNullOrEmpty(strOld)&& !CUtils.IsNullOrEmpty(strNew))
            {
                var str = strOld.ToString().Replace(strNew, newchar).Trim();
                return str;
            }
            return null;
        }

        public static bool IsNullOrEmpty(object obj)
        {
            if (obj != null && obj.ToString().Trim().Length > 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static string StrValueOrNull(object obj)
        {
            if (obj != null && obj.ToString().Trim().Length > 0)
            {
                return obj.ToString().Trim();
            }
            else
            {
                return null;
            }
        }

        public static string StrValue(object obj)
        {
            if (obj != null && obj.ToString().Trim().Length > 0)
            {
                return obj.ToString().Trim();
            }
            else
            {
                return "";
            }
        }
        public static string StrValueNew(object obj)
        {
            if (obj != null && obj.ToString().Trim().Length > 0)
            {
                return obj.ToString().Trim();
            }
            else
            {
                return "";
            }
        }
        public static string StrReplace(string chuoi, string oldChar, string newchar)
        {
            var strChuoi = "";
            if (!IsNullOrEmpty(chuoi))
            {
                strChuoi = chuoi.Replace(oldChar, newchar).Trim();
            }

            return strChuoi;
        }
        public static string StrValueFormatInteger(object obj)
        {
            if (obj != null && obj.ToString().Trim().Length > 0)
            {
                obj = obj.ToString().Trim();
                if (IsInteger(obj))
                {
                    obj = formatNumeric(Convert.ToDouble(obj), TConst.Nonsense.INTEGER_DB_FORMAT);
                }
                return obj.ToString();
            }
            else
            {
                return "";
            }
        }

        public static string StrValueFormatNumber(object obj, string strformat)
        {
            if (obj != null && obj.ToString().Trim().Length > 0)
            {
                string strvalue = obj.ToString().Trim();
                var check = IsInteger(strvalue);
                if (check)
                {
                    obj = formatNumeric(Convert.ToDouble(obj), strformat);
                }
                return obj.ToString();
            }
            else
            {
                return "";
            }
        }

        public static Mst_ColumnConfig SearchColumnConfig(Mst_ColumnConfig data, List<Mst_ColumnConfig> listMst_ColumnConfig)
        {
            if (data != null && listMst_ColumnConfig != null && listMst_ColumnConfig.Count > 0)
            {
                var objMst_ColumnConfig = listMst_ColumnConfig.Where(it =>
                    !IsNullOrEmpty(it.TableName) && !IsNullOrEmpty(data.TableName) &&
                    !IsNullOrEmpty(it.ColumnName) && !IsNullOrEmpty(data.ColumnName) &&
                    it.TableName.ToString().Trim().Equals(data.TableName.ToString().Trim()) &&
                    it.ColumnName.ToString().Trim().Equals(data.ColumnName.ToString().Trim())).FirstOrDefault();
                return objMst_ColumnConfig;
            }
            else
            {
                return null;
            }
        }

        public static int ReturnValueColumnFormat(Mst_ColumnConfig data, List<Mst_ColumnConfig> listMst_ColumnConfig)
        {
            var columnFormat = 2;
            var objMst_ColumnConfig = SearchColumnConfig(data, listMst_ColumnConfig);
            if (objMst_ColumnConfig != null)
            {
                var strColumnFormat = StrValue(objMst_ColumnConfig.ColumnFormat);
                if (!IsNullOrEmpty(strColumnFormat))
                {
                    columnFormat = Convert.ToInt32(strColumnFormat);
                }
            }
            return columnFormat;
        }
        public static string ReturnNumericFormat(string strformat)
        {
            var numericFormat = "{0:0,0}";
            switch (strformat)
            {
                case "0":
                    {
                        numericFormat = "{0:0,0}";
                        break;
                    }
                case "1":
                    {
                        numericFormat = "{0:0,0.0}";
                        break;
                    }
                case "2":
                    {
                        numericFormat = "{0:0,0.00}";
                        break;
                    }
                case "3":
                    {
                        numericFormat = "{0:0,0.000}";
                        break;
                    }
                case "4":
                    {
                        numericFormat = "{0:0,0.0000}";
                        break;
                    }
                case "5":
                    {
                        numericFormat = "{0:0,0.00000}";
                        break;
                    }
                case "6":
                    {
                        numericFormat = "{0:0,0.000000}";
                        break;
                    }
                case "7":
                    {
                        numericFormat = "{0:0,0.0000000}";
                        break;
                    }
                case "8":
                    {
                        numericFormat = "{0:0,0.00000000}";
                        break;
                    }
                case "9":
                    {
                        numericFormat = "{0:0,0.000000000}";
                        break;
                    }
            }
            return numericFormat;
        }

        /// <summary>
        /// "   &quot;              idn_quot_idn
        /// '   &apos;              idn_apos_idn
        /// <!-- <  &lt; -->        idn_lt_idn
        /// >   &gt;                idn_gt_idn
        /// &   &amp;               idn_amp_idn
        /// </summary>
        /// <param name="xmlString"></param>
        /// <returns></returns>
        public static string XML_HttpUtility_HtmlEncode(string xmlString)
        {
            if (IsNullOrEmpty(xmlString)) return null;
            xmlString = Regex.Replace(xmlString, @"<[^>]+?>", m => HttpUtility.HtmlEncode(m.Value))
                .Replace("&lt;", "idn_lt_idn")
                .Replace("&gt;", "idn_gt_idn")
                .Replace("&quot;", "idn_quot_idn")

                .Replace("&", "idn__amp__idn")
                .Replace(">", "idn__gt__idn")
                .Replace("<", "idn__lt__idn")
                .Replace("\"", "idn__quot__idn")
                .Replace("'", "idn__apos__idn");
            return xmlString;
        }

        /// <summary>
        /// "   &quot;              idn_quot_idn
        /// '   &apos;              idn_apos_idn
        /// <!-- <  &lt; -->        idn_lt_idn
        /// >   &gt;                idn_gt_idn
        /// &   &amp;               idn_amp_idn
        /// </summary>
        /// <param name="xmlString"></param>
        /// <returns></returns>
        public static string XML_HttpUtility_HtmlDecode(string xmlString)
        {
            if (IsNullOrEmpty(xmlString)) return null;
            xmlString = HttpUtility.HtmlDecode(xmlString)
                .Replace("idn_lt_idn", "<")
                .Replace("idn_gt_idn", ">")
                .Replace("idn_quot_idn", "\"")

                .Replace("idn__lt__idn", "&lt;")
                .Replace("idn__gt__idn", "&gt;")
                .Replace("idn__quot__idn", "&quot;")
                .Replace("idn__apos__idn", "&apos;")
                .Replace("idn__amp__idn", "&amp;");

            return xmlString;
        }

        public static string XML_Unescape_Value(string xmlString)
        {
            if (IsNullOrEmpty(xmlString)) return null;
            xmlString = xmlString.Replace("&apos;", "'")
                .Replace("&quot;", "\"")
                .Replace("&gt;", ">")
                .Replace("&lt;", "<")
                .Replace("&amp;", "&");
            return xmlString;
        }

        public static string XML_Escape_Value(string xmlString)
        {
            if (IsNullOrEmpty(xmlString)) return null;
            xmlString = xmlString.Replace("'", "&apos;")
                .Replace("\"", "&quot;")
                .Replace(">", "&gt;")
                .Replace("<", "&lt;")
                .Replace("&", "&amp;");
            return xmlString;
        }

        /// <summary>
        /// Compiled regular expression for performance.
        /// </summary>
        static Regex _htmlRegex = new Regex("<.*?>", RegexOptions.Compiled);

        /// <summary>
        /// Remove HTML from string with compiled Regex.
        /// </summary>
        public static string StripTagsRegexCompiled(string source)
        {
            return _htmlRegex.Replace(source, string.Empty);
        }

        public static List<T> RemoveDuplicatesSet<T>(List<T> items)
        {
            // Use HashSet to remember items seen.
            var result = new List<T>();
            var set = new HashSet<T>();
            for (int i = 0; i < items.Count; i++)
            {
                // Add if needed.
                if (!set.Contains(items[i]))
                {
                    result.Add(items[i]);
                    set.Add(items[i]);
                }
            }
            return result;
        }

        public static string CatChuoi(string chuoi, int soluongkytu)
        {
            var subchuoi = "";
            if (!IsNullOrEmpty(chuoi))
            {
                chuoi = chuoi.Trim();
                if (chuoi.Trim().Length <= soluongkytu)
                {
                    subchuoi = chuoi;
                }
                else
                {
                    var indexOf = chuoi.LastIndexOf(" ", soluongkytu, StringComparison.Ordinal);
                    if (indexOf > 0)
                    {
                        subchuoi = chuoi.Substring(0, indexOf).Trim() + "...";
                    }
                    else
                    {
                        subchuoi = chuoi.Substring(0, soluongkytu).Trim() + "...";
                    }
                }

            }


            return subchuoi;
        }
        public static int MathMod(int a, int b)
        {
            return (Math.Abs(a * b) + a) % b;
        }
        /// <summary>
        /// data
        /// separator = new[] { "hello" }
        /// </summary>
        /// <param name="data"></param>
        /// <param name="separator"></param>
        /// <returns></returns>
        public static string[] StrSplit(string data, string[] separator)
        {
            return data.Split(separator, StringSplitOptions.None);
        }

        /// <summary>
        /// var ordersOrderedByDate = CUtils.OrderByAsc(objRT_Inv_InventoryBalance.Lst_Inv_InventoryBalance, x => x.QtyTotalOK);
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="P"></typeparam>
        /// <param name="collection"></param>
        /// <param name="propertySelector"></param>
        /// <returns></returns>
        public static List<T> OrderByAsc<T, P>(IEnumerable<T> collection, Func<T, P> propertySelector)
        {
            return (from item in collection
                    orderby propertySelector(item)
                    select item).ToList();
        }
        /// <summary>
        /// var ordersOrderedByDate = CUtils.OrderByDesc(objRT_Inv_InventoryBalance.Lst_Inv_InventoryBalance, x => x.QtyTotalOK);
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="P"></typeparam>
        /// <param name="collection"></param>
        /// <param name="propertySelector"></param>
        /// <returns></returns>
        public static List<T> OrderByDesc<T, P>(IEnumerable<T> collection, Func<T, P> propertySelector)
        {
            return (from item in collection
                    orderby propertySelector(item) descending
                    select item).ToList();
        }
        public static string StdFlag(object strFlagRaw)
        {
            return (CmUtils.StringUtils.StringEqual(strFlagRaw, TConst.Flag.Active) ? TConst.Flag.Active : TConst.Flag.Inactive);
        }
        public static string StdMonth(object objMonth)
        {
            if (objMonth == null || objMonth == DBNull.Value) return null;
            DateTime dtime = Convert.ToDateTime(objMonth);
            if (dtime < Convert.ToDateTime("1900-01-01 00:00:00") || dtime > Convert.ToDateTime("2100-01-01 23:59:59"))
                throw new Exception("MyBiz.DateTimeOutOfRange");
            return (string.Format("{0}-01", dtime.ToString("yyyy-MM")));
        }

        public static void EnsureDirectoryExists(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }

        public static DataTable StdDataInTable(DataTable dtData, params string[] arrstrCouple)
        {
            // Check:
            if (dtData == null
                || dtData.Rows.Count <= 0
                || arrstrCouple == null
                || arrstrCouple.Length <= 0
                )
                return dtData;

            // Init:
            CmUtils.DataTableUtils.DataTableCore_BeginUpdate(dtData);
            foreach (DataRow drScan in dtData.Rows)
            {
                for (int nScan = 0; nScan < arrstrCouple.Length; nScan += 2)
                {
                    if (CmUtils.StringUtils.StringEqualIgnoreCase(arrstrCouple[nScan], "StdParam"))
                    {
                        drScan[arrstrCouple[nScan + 1]] = StdParam(drScan[arrstrCouple[nScan + 1]]);
                    }
                    else if (CmUtils.StringUtils.StringEqualIgnoreCase(arrstrCouple[nScan], "StdDate"))
                    {
                        drScan[arrstrCouple[nScan + 1]] = StdDate(drScan[arrstrCouple[nScan + 1]]);
                    }
                    else if (CmUtils.StringUtils.StringEqualIgnoreCase(arrstrCouple[nScan], "StdDTime"))
                    {
                        drScan[arrstrCouple[nScan + 1]] = StdDTime(drScan[arrstrCouple[nScan + 1]]);
                    }
                    else if (CmUtils.StringUtils.StringEqualIgnoreCase(arrstrCouple[nScan], "StdFlag"))
                    {
                        drScan[arrstrCouple[nScan + 1]] = StdFlag(drScan[arrstrCouple[nScan + 1]]);
                    }
                    else if (CmUtils.StringUtils.StringEqualIgnoreCase(arrstrCouple[nScan], "StdMonth"))
                    {
                        drScan[arrstrCouple[nScan + 1]] = StdMonth(drScan[arrstrCouple[nScan + 1]]);
                    }
                    else if (CmUtils.StringUtils.StringEqualIgnoreCase(arrstrCouple[nScan], "StdInt"))
                    {
                        drScan[arrstrCouple[nScan + 1]] = StdInt(drScan[arrstrCouple[nScan + 1]]);
                    }
                    else if (CmUtils.StringUtils.StringEqualIgnoreCase(arrstrCouple[nScan], "StdDouble"))
                    {
                        drScan[arrstrCouple[nScan + 1]] = StdDouble(drScan[arrstrCouple[nScan + 1]]);
                    }
                }
            }
            CmUtils.DataTableUtils.DataTableCore_EndUpdate(dtData);

            // Return Good:
            return dtData;
        }
        public static DataSet StdDS(object[] arrobjDS)
        {
            try
            {
                return CmUtils.ConvertUtils.Array2DataSet(arrobjDS);
            }
            catch (Exception exc)
            {
                throw new Exception("MyBiz.DataSetRawInvalidFormat", exc);
            }
        }
        public static object IsNullSql(object objParam)
        {
            return (objParam == null ? DBNull.Value : objParam);
        }

        public static Hashtable MyBuildHTSupportedColumns(
            EzSql.IDBEngine db
            , ref Hashtable htSupportedColumns
            , string strTableNameDB
            , string strPrefixStd
            , string strPrefixAlias
            , params object[] arrstrExcept
            )
        {
            DataTable dt = EzDAL.Utils.DBUtils.GetSchema(db, strTableNameDB).Tables[0];
            return CmUtils.SqlUtils.BuildSupportedColumnsInfo(
                ref htSupportedColumns // htSupportedColumns
                , dt // dtSource
                , strPrefixStd // strPrefixStd
                , strPrefixAlias // strPrefixAlias
                , arrstrExcept // arrstrExcept
                );
        }
        public static string MyBuildSql_GetTempTable(string zzzzClauseTable, string zzzzClauseColumn)
        {
            // Init:
            string strSql_TempTbl = CmUtils.StringUtils.Replace(@"
							select 
								identity(bigint, 0, 1) MyIdxSeq
								, zzzzClauseColumn
							into #tbl_zzzzClauseTable
							from zzzzClauseTable t --//[mylock]
							where (0=1)
							;
							select * from #tbl_zzzzClauseTable t --//[mylock]
							;
						"
                        , "zzzzClauseTable", zzzzClauseTable
                        , "zzzzClauseColumn", zzzzClauseColumn
                        );

            // Return Good:
            return strSql_TempTbl;
        }
        public static DataTable MyBuildDBDT_CreateTableParam(EzSql.IDBEngine db, string zzzzClauseColumn, string zzzzClauseTable)
        {
            // Init:
            string strSql_TempTbl = CmUtils.StringUtils.Replace(@"
					select 
						zzzzClauseColumn
					into zzzzClauseTable
					where (0=1)
					;
					select * from zzzzClauseTable t --//[mylock]
					;
				"
                , "zzzzClauseColumn", zzzzClauseColumn
                , "zzzzClauseTable", zzzzClauseTable
                );
            DataTable dtDB = db.ExecQuery(strSql_TempTbl).Tables[0];

            // Return Good:
            return dtDB;
        }
        public static DataTable MyBuildDBDT_CreateTableParam(EzSql.IDBEngine db)
        {
            // Init:
            string zzzzClauseColumn = @"
					Cast(null as nvarchar(200)) PCODE0, Cast(null as nvarchar(400)) PVAL0
					, Cast(null as nvarchar(200)) PCODE1, Cast(null as nvarchar(400)) PVAL1
					, Cast(null as nvarchar(200)) PCODE2, Cast(null as nvarchar(400)) PVAL2
					, Cast(null as nvarchar(200)) PCODE3, Cast(null as nvarchar(400)) PVAL3
					, Cast(null as nvarchar(200)) PCODE4, Cast(null as nvarchar(400)) PVAL4
					, Cast(null as nvarchar(200)) PCODE5, Cast(null as nvarchar(400)) PVAL5
				";
            string zzzzClauseTable = "#tbl_Param";
            DataTable dtDB = MyBuildDBDT_CreateTableParam(db, zzzzClauseColumn, zzzzClauseTable);

            // Return Good:
            return dtDB;
        }
        public static DataTable MyBuildDBDT_Common(
            EzSql.IDBEngine db
            , string strTableName
            , object strDefaultType
            , object[] arrSingleStructure
            , object[] arrArrParamValue
            )
        {
            // Init:
            ArrayList alCoupleStructure = new ArrayList();
            foreach (object objScan in arrSingleStructure)
            {
                alCoupleStructure.Add(objScan);
                alCoupleStructure.Add(strDefaultType);
            }

            // Return Good:
            return MyBuildDBDT_Common(db, strTableName, alCoupleStructure.ToArray(), arrArrParamValue);
        }
        public static DataTable MyBuildDBDT_Common(
            EzSql.IDBEngine db
            , string strTableName
            , object strDefaultType
            , object[] arrSingleStructure
            , DataTable dtData
            )
        {
            // Init:
            ArrayList alCoupleStructure = new ArrayList();
            foreach (object objScan in arrSingleStructure)
            {
                alCoupleStructure.Add(objScan);
                alCoupleStructure.Add(strDefaultType);
            }

            // Return Good:
            return MyBuildDBDT_Common(db, strTableName, alCoupleStructure.ToArray(), dtData);
        }
        public static DataTable MyBuildDBDT_Common(
            EzSql.IDBEngine db
            , string strTableName
            , object[] arrCoupleStructure
            , object[] arrArrParamValue
            )
        {
            #region // Build Structure:
            DataTable dtDB = null;
            {
                // Init:
                StringBuilder strbdTemp = new StringBuilder(1000);
                string strMySeparator = ", ";

                // Scan:
                for (int nScan = 0; nScan < arrCoupleStructure.Length; nScan += 2)
                {
                    // Ex: ", Cast(null as nvarchar(200)) PCODE1"
                    strbdTemp.AppendFormat(
                        "{0}Cast(null as {1}) {2}"
                        , strMySeparator // {0}
                        , arrCoupleStructure[nScan + 1] // {1}
                        , Convert.ToString(arrCoupleStructure[nScan]).ToUpper() // {2}
                        );
                }
                string zzzzClauseColumn = strbdTemp.ToString(strMySeparator.Length, strbdTemp.Length - strMySeparator.Length);

                // Build Table:
                string strSql_TempTbl = CmUtils.StringUtils.Replace(@"
						select 
							zzzzClauseColumn
						into zzzzClauseTable
						where (0=1)
						;
						select * from zzzzClauseTable t --//[mylock]
						;
					"
                    , "zzzzClauseColumn", zzzzClauseColumn
                    , "zzzzClauseTable", strTableName
                    );
                dtDB = db.ExecQuery(strSql_TempTbl).Tables[0];
            }
            #endregion

            #region // ProcessData:
            if (arrArrParamValue == null || arrArrParamValue.Length <= 0)
            {
                return dtDB;
            }
            else
            {
                // Fill Data:
                for (int nScan = 0; nScan < arrArrParamValue.Length; nScan++)
                {
                    object[] arrItems = (object[])arrArrParamValue[nScan];
                    dtDB.Rows.Add(arrItems);
                }
                dtDB.AcceptChanges();

                // Save to DB:
                db.InsertHuge(strTableName, dtDB);
            }
            #endregion

            // Return Good:
            return dtDB;
        }
        public static DataTable MyBuildDBDT_Common(
            EzSql.IDBEngine db
            , string strTableName
            , object[] arrCoupleStructure
            , DataTable dtData
            )
        {
            ////
            ArrayList alCols = new ArrayList();
            for (int nScan = 0; nScan < arrCoupleStructure.Length; nScan += 2)
            {
                alCols.Add(Convert.ToString(arrCoupleStructure[nScan]).ToUpper());
            }
            ////
            ArrayList alArrParamValue = new ArrayList();
            if (dtData != null && dtData.Rows.Count > 0)
            {
                foreach (DataRow drScan in dtData.Rows)
                {
                    ArrayList alItems = new ArrayList();
                    foreach (string strCol in alCols)
                    {
                        alItems.Add(drScan[strCol]);
                    }
                    alArrParamValue.Add(alItems.ToArray());
                }
            }
            ////

            // Return Good:
            return MyBuildDBDT_Common(db, strTableName, arrCoupleStructure, alArrParamValue.ToArray());
        }
        public static void MyBuildDBDT_InsertTableParam(EzSql.IDBEngine db, DataTable dtDB_Param_Template, object[] arrArrayParams)
        {
            // Init:
            DataTable dtDB_Param = dtDB_Param_Template.Clone();
            CmUtils.DataTableUtils.DataTableCore_BeginUpdate(dtDB_Param);
            foreach (object[] arrItems in arrArrayParams)
            {
                DataRow drScan = dtDB_Param.NewRow();
                for (int nScan = 0; nScan < arrItems.Length; nScan++)
                {
                    drScan[nScan] = arrItems[nScan];
                }
                dtDB_Param.Rows.Add(drScan);
            }
            CmUtils.DataTableUtils.DataTableCore_EndUpdate(dtDB_Param);

            List<string> lstMapFN = new List<string>();
            for (int nScan = 0; nScan < dtDB_Param.Columns.Count; nScan++)
            {
                lstMapFN.AddRange(new string[] { dtDB_Param.Columns[nScan].ColumnName, dtDB_Param.Columns[nScan].ColumnName });
            }

            // Save:
            db.InsertHuge(
                "#tbl_Param" // strTableName
                , dtDB_Param // dtData
                , lstMapFN.ToArray() // arrstrColumnsCouple
                );
        }
        public static void MyBuildDBDT_InsertTableParam(EzSql.IDBEngine db, object[] arrArrayParams)
        {
            // Init:
            DataTable dtDB_Param_Template = new DataTable("dtDB_Param");
            dtDB_Param_Template.Columns.Add("PCODE0", typeof(string)); dtDB_Param_Template.Columns.Add("PVAL0", typeof(string));
            dtDB_Param_Template.Columns.Add("PCODE1", typeof(string)); dtDB_Param_Template.Columns.Add("PVAL1", typeof(string));
            dtDB_Param_Template.Columns.Add("PCODE2", typeof(string)); dtDB_Param_Template.Columns.Add("PVAL2", typeof(string));
            dtDB_Param_Template.Columns.Add("PCODE3", typeof(string)); dtDB_Param_Template.Columns.Add("PVAL3", typeof(string));
            dtDB_Param_Template.Columns.Add("PCODE4", typeof(string)); dtDB_Param_Template.Columns.Add("PVAL4", typeof(string));
            dtDB_Param_Template.Columns.Add("PCODE5", typeof(string)); dtDB_Param_Template.Columns.Add("PVAL5", typeof(string));

            // Return Good:
            MyBuildDBDT_InsertTableParam(db, dtDB_Param_Template, arrArrayParams);
        }

        public static DataTable MyForceNewColumn(ref DataTable dt, string strColumnName, Type t)
        {
            if (dt == null) return dt;
            if (dt.Columns.Contains(strColumnName)) dt.Columns.Remove(strColumnName);
            dt.Columns.Add(strColumnName, t);
            return dt;
        }

        public static void ProcessMyDSError(
           ref DataSet mdsFinal
           , DataSet mdsSource
           )
        {
            string strErrorCode = Convert.ToString(CmUtils.CMyDataSet.GetErrorCode(mdsSource));
            object[] arrErrorParamsCouple = CmUtils.CMyDataSet.GetErrorParams(mdsSource);
            ArrayList alParamsCoupleError = new ArrayList(new object[] { "ErrorCode", strErrorCode });
            alParamsCoupleError.AddRange(arrErrorParamsCouple);
            CmUtils.CMyDataSet.AppendErrorParams(
                ref mdsFinal
                , alParamsCoupleError.ToArray()
                );
        }

        public static void ProcessMyDS(DataSet mdsFinal)
        {
            ServiceException svException = GenServiceException(mdsFinal);

            if (svException.HasError())
            {
                //MessageBox.Show(svException.ErrorMessage);
                //CommonForms.CFormInputParam.ShowMessage(svException.ErrorCode, svException.ErrorDetail);
                throw svException;
            }
        }

        public static DataSet MyDSDecode(DataSet mds)
        {
            // Check:
            if (mds == null || !CmUtils.CMyDataSet.IsCompress(mds)) return mds;

            // Init:
            byte[] arrbyteCompressedData = (byte[])mds.Tables["dtCompressedData"].Rows[0][0];
            byte[] arrbytePlain = CmUtils.CompressUtils.DecompressGZip(arrbyteCompressedData);
            string strPlain = CmUtils.StringUtils.ArrayBytes2String(Encoding.Unicode, arrbytePlain);
            DataSet mdsNew = CmUtils.XmlUtils.Xml2DataSet(strPlain);

            // Return Good:
            return mdsNew;
        }
        public static void ProcessMyDSWarning(
            ref DataSet mdsFinal
            , DataSet mdsSource
            )
        {
            string strErrorCode = Convert.ToString(CmUtils.CMyDataSet.GetErrorCode(mdsSource));
            object[] arrErrorParamsCouple = CmUtils.CMyDataSet.GetErrorParams(mdsSource);
            ArrayList alParamsWarningCoupleError = new ArrayList(new object[] { "ErrorCode", strErrorCode });
            alParamsWarningCoupleError.AddRange(arrErrorParamsCouple);
            CmUtils.CMyDataSet.AppendWarningParams(
                ref mdsFinal
                , alParamsWarningCoupleError.ToArray()
                );
        }

        public static ServiceException GenServiceException(DataSet ds)
        {
            ServiceException ex = new ServiceException();
            if (ds == null)
            {
                ex.ErrorCode = "ERR0001";
                ex.ErrorMessage = "Null dataset return";
                ex.ErrorDetail = "Null dataset return";
                ex.Tag = "";
                return ex;
            }
            //
            string errorCode = Convert.ToString(CmUtils.CMyDataSet.GetErrorCode(ds));
            string errorMessage = Convert.ToString(CmUtils.CMyDataSet.GetErrorCode(ds));
            StringBuilder sbDetail = new StringBuilder();

            sbDetail.Append(string.Format("Error Code: {0}", errorCode));
            sbDetail.Append("<br/>--------------------------------------------------------<br/>");
            object[] arrObj = CmUtils.CMyDataSet.GetErrorParams(ds);
            if (arrObj != null && arrObj.Length > 1)
            {
                for (int i = 0; i < arrObj.Length; i++)
                {
                    string val = Convert.ToString(arrObj[i]);

                    val = System.Net.WebUtility.HtmlEncode(val);
                    sbDetail.Append(val.Replace("\n", "<br/>"));
                    if (i % 2 == 0)
                        sbDetail.Append(" = ");
                    else
                        sbDetail.Append("<br/>--------------------------------------------------------<br/>");
                }
            }
            ex.ErrorCode = errorMessage;
            ex.ErrorMessage = errorCode;

            //if (ErrorCodes.DIC_ERROR_CODES.ContainsKey(errorCode))
            //{
            //    ex.ErrorMessage = ErrorCodes.DIC_ERROR_CODES[errorCode];
            //}
            if (!string.IsNullOrEmpty(errorCode))
            {
                ex.ErrorMessage = Error.GetErrorMessage(errorCode);
            }
            var strDetailErr = "";
            strDetailErr += "<div class='dtlErr'><a href='javascript:void' onclick='showDetailErr(); return false;'>Detail</a></div>";
            strDetailErr += "<div id='showDtlErr' class='dtlErr dtlErr-hide'>";
            strDetailErr += sbDetail.ToString();
            strDetailErr += "</div>";
            ex.ErrorDetail = strDetailErr;
            ex.Tag = "";
            return ex;
        }

        public static ServiceException BizGenServiceException(DataSet ds)
        {
            ServiceException ex = new ServiceException();
            if (ds == null)
            {
                ex.ErrorCode = "ERR0001";
                ex.ErrorMessage = "Null dataset return";
                ex.ErrorDetail = "Null dataset return";
                ex.Tag = "";
                return ex;
            }
            //
            string errorCode = Convert.ToString(CmUtils.CMyDataSet.GetErrorCode(ds));
            string errorMessage = Convert.ToString(CmUtils.CMyDataSet.GetErrorCode(ds));
            StringBuilder sbDetail = new StringBuilder();

            sbDetail.Append(string.Format("Error Code: {0}", errorCode));
            sbDetail.Append("<br/>--------------------------------------------------------<br/>");
            object[] arrObj = CmUtils.CMyDataSet.GetErrorParams(ds);
            if (arrObj != null && arrObj.Length > 1)
            {
                for (int i = 0; i < arrObj.Length; i++)
                {
                    string val = Convert.ToString(arrObj[i]);

                    //val = System.Net.WebUtility.HtmlEncode(val);
                    sbDetail.Append(val.Replace("\n", "<br/>"));
                    if (i % 2 == 0)
                        sbDetail.Append(" = ");
                    else
                        sbDetail.Append("<br/>--------------------------------------------------------<br/>");
                }
            }
            ex.ErrorCode = errorMessage;
            ex.ErrorMessage = errorCode;

            //if (ErrorCodes.DIC_ERROR_CODES.ContainsKey(errorCode))
            //{
            //    ex.ErrorMessage = ErrorCodes.DIC_ERROR_CODES[errorCode];
            //}
            if (!string.IsNullOrEmpty(errorCode))
            {
                ex.ErrorMessage = Error.GetErrorMessage(errorCode);
            }
            var strDetailErr = "";
            //strDetailErr += "<div class='dtlErr'><a href='javascript:void' onclick='showDetailErr(); return false;'>Detail</a></div>";
            //strDetailErr += "<div id='showDtlErr' class='dtlErr dtlErr-hide'>";
            strDetailErr += sbDetail.ToString();
            strDetailErr += "</div>";
            ex.ErrorDetail = strDetailErr;
            ex.Tag = "";
            return ex;
        }

        public static string GetSimpleHash(string strData)
        {
            // //
            if (string.IsNullOrEmpty(strData)) return null;
            // //
            byte[] arrbyData = GetBytes(strData);
            int[] arrintResult = new int[] { arrbyData.Length, arrbyData.Length, arrbyData.Length, arrbyData.Length, arrbyData.Length };
            int nSize = arrintResult.Length;
            // //
            for (int nScan = 0; nScan < arrbyData.Length; nScan++)
            {
                int nIdx = nScan % nSize;
                arrintResult[nIdx] += (nScan + arrbyData[nScan]);
            }
            // //
            StringBuilder strbd = new StringBuilder();
            foreach (int nScan in arrintResult)
            {
                strbd.Append((nScan % 100) / 10).Append(nScan % 10);
            }
            // //
            return strbd.ToString();
        }
        private static Encoding s_e = Encoding.UTF8;
        public static byte[] GetBytes(string strData)
        {
            if (string.IsNullOrEmpty(strData)) return null;
            return s_e.GetBytes(strData);
        }

        public static string ConvertFileToBase64String(string filePath)
        {
            byte[] fileContent = System.IO.File.ReadAllBytes(filePath);
            return Convert.ToBase64String(fileContent);
        }

        public static byte[] ConvertFileFromBase64String(string fileBase64)
        {
            byte[] fileContent = Convert.FromBase64String(fileBase64);
            return fileContent;

        }

        public static string Base64_Encode(string content)
        {
            return Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(content));
        }

        public static string Base64_Decode(string content)
        {
            return System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(content));
        }

        public static DataTable DataTableRemoveColumn(ref DataTable dt, string strColumnName)
        {
            if (dt == null) return dt;
            if (dt.Columns.Contains(strColumnName))
            {
                dt.Columns.Remove(strColumnName);
            }
            return dt;
        }

        #region["Encrypt - Decrypt - tuyenba"]
        private static byte[] key = new byte[8] { 3, 2, 1, 4, 5, 6, 7, 8 };
        private static byte[] iv = new byte[8] { 8, 2, 3, 4, 5, 6, 7, 1 };

        /// <summary>
        /// Hàm mã hóa (tuyenba)
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string Encrypt(string text)
        {
            SymmetricAlgorithm algorithm = DES.Create();
            ICryptoTransform transform = algorithm.CreateEncryptor(key, iv);
            byte[] inputbuffer = Encoding.Unicode.GetBytes(text);
            byte[] outputBuffer = transform.TransformFinalBlock(inputbuffer, 0, inputbuffer.Length);
            return Convert.ToBase64String(outputBuffer);
        }

        /// <summary>
        /// Hàm giải mã (tuyenba)
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string Decrypt(string text)
        {
            SymmetricAlgorithm algorithm = DES.Create();
            ICryptoTransform transform = algorithm.CreateDecryptor(key, iv);
            byte[] inputbuffer = Convert.FromBase64String(text);
            byte[] outputBuffer = transform.TransformFinalBlock(inputbuffer, 0, inputbuffer.Length);
            return Encoding.Unicode.GetString(outputBuffer);
        }
        #endregion
        public static string ReverseString(string s)
        {
            char[] arr = s.ToCharArray();
            Array.Reverse(arr);
            return new string(arr);
        }
        public static string GetEncodedHash(string password)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] digest = md5.ComputeHash(Encoding.UTF8.GetBytes(password + IdocNetSalt));
            string base64digest = Convert.ToBase64String(digest, 0, digest.Length);
            return base64digest.Substring(0, base64digest.Length - 2);
        }

        public const string IdocNetSalt = "idocNet";

        #region["Encrypt - Decrypt"]

        public const string myKey = "idocNet.com";

        /// <summary>
        /// Encrypt a string using dual encryption method. Return a encrypted cipher Text
        /// </summary>
        /// <param name="toEncrypt">string to be encrypted</param>
        /// <param name="useHashing">use hashing? send to for extra secirity</param>
        /// <returns></returns>
        public static string Encrypt(string toEncrypt, bool useHashing)
        {
            byte[] keyArray;
            byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(toEncrypt);
            // Get the key from config file
            string key = myKey;
            //System.Windows.Forms.MessageBox.Show(key);
            if (useHashing)
            {
                MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                hashmd5.Clear();
            }
            else
                keyArray = UTF8Encoding.UTF8.GetBytes(key);

            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            tdes.Key = keyArray;
            tdes.Mode = CipherMode.ECB;
            tdes.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = tdes.CreateEncryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
            tdes.Clear();
            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }
        /// <summary>
        /// DeCrypt a string using dual encryption method. Return a DeCrypted clear string
        /// </summary>
        /// <param name="cipherString">encrypted string</param>
        /// <param name="useHashing">Did you use hashing to encrypt this data? pass true is yes</param>
        /// <returns></returns>
        public static string Decrypt(string cipherString, bool useHashing)
        {
            byte[] keyArray;
            byte[] toEncryptArray = Convert.FromBase64String(cipherString);
            //Get your key from config file to open the lock!
            string key = myKey;

            if (useHashing)
            {
                MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                hashmd5.Clear();
            }
            else
                keyArray = UTF8Encoding.UTF8.GetBytes(key);

            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            tdes.Key = keyArray;
            tdes.Mode = CipherMode.ECB;
            tdes.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = tdes.CreateDecryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

            tdes.Clear();
            return UTF8Encoding.UTF8.GetString(resultArray);
        }
        #endregion

        #region["Check File and Folder"]
        public static void GrantAccess(string fullPath)
        {
            var dInfo = new DirectoryInfo(fullPath);
            var dSecurity = dInfo.GetAccessControl();
            dSecurity.AddAccessRule(new FileSystemAccessRule(new SecurityIdentifier(WellKnownSidType.WorldSid, null), FileSystemRights.FullControl, InheritanceFlags.ObjectInherit | InheritanceFlags.ContainerInherit, PropagationFlags.NoPropagateInherit, AccessControlType.Allow));
            dInfo.SetAccessControl(dSecurity);
        }

        public static string PathFolder(string root, string folder)
        {

            if (CUtils.IsNullOrEmpty(folder))
            {
                folder = DateTime.Now.ToString("yyyyMMdd");
            }
            // root: "C:\"
            if (CUtils.IsNullOrEmpty(folder))
            {
                //root = @"C:";
                root = Path.GetPathRoot(Environment.SystemDirectory);
            }

            var path = root + @"\" + folder;
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            CUtils.GrantAccess(path);
            return path;
        }

        public static string PathFile(string root, string folder, string filename)
        {
            var pathfolder = PathFolder(root, folder);
            var pathfile = pathfolder + @"\" + filename;
            if (!File.Exists(pathfile))
            {
                File.Create(pathfile).Dispose();
            }
            return pathfile;
        }

        #endregion

        #region["StringBuilder Append"]

        public static StringBuilder StrAppend(string value)
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append(!CUtils.IsNullOrEmpty(value) ? value : "");
            stringBuilder.Append(Environment.NewLine);
            return stringBuilder;
        }

        public static StringBuilder StrAppend(StringBuilder stringBuilder, string value)
        {
            if (stringBuilder == null)
            {
                stringBuilder = new StringBuilder();
            }
            stringBuilder.Append(!CUtils.IsNullOrEmpty(value) ? value : "");
            stringBuilder.Append(Environment.NewLine);
            return stringBuilder;
        }

        #endregion

        ///// ThomPTT
        public static void AddParamServiceException(
            ref ArrayList alParamsCoupleError
            , DataSet ds
            )
        {
            if (ds == null)
            {
                alParamsCoupleError.AddRange(new object[]{
                    "OutSite.ErrorCode", "ERR0001"
                    , "OutSite.ErrorMessage", "Null dataset return"
                    , "OutSite.ErrorDetail", "Null dataset return"
                    });
            }
            else if (ds != null)
            {
                string errorCode = Convert.ToString(CmUtils.CMyDataSet.GetErrorCode(ds));
                string errorMessage = Convert.ToString(CmUtils.CMyDataSet.GetErrorCode(ds));
                /////
                alParamsCoupleError.AddRange(new object[]{
                    "OutSite.ErrorCode", errorCode
                    , "OutSite.ErrorMessage", errorMessage
                    });
                /////
                object[] arrObj = CmUtils.CMyDataSet.GetErrorParams(ds);
                ////
                int index = 0;
                while (index < arrObj.Length)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        arrObj[index], arrObj[index + 1]
                        });
                    index += 2;
                }
                ////
            }
        }
    }

    public class CMyBase36
    {
        // //
        private static Dictionary<int, char> s_dicTo36 = null;
        private static Dictionary<char, int> s_dicTo10 = null;
        private static int s_nBaseSize = 36;
        static CMyBase36()
        {
            // //
            s_dicTo36 = new Dictionary<int, char>();
            s_dicTo10 = new Dictionary<char, int>();
            // //
            for (char chScan = '0'; chScan <= '9'; chScan++)
            {
                int nScan = chScan - '0';
                s_dicTo36[nScan] = chScan;
                s_dicTo10[chScan] = nScan;
            }
            // //
            for (char chScan = 'A'; chScan <= 'Z'; chScan++)
            {
                int nScan = chScan - 'A' + 10;
                s_dicTo36[nScan] = chScan;
                s_dicTo10[chScan] = nScan;
            }
            // //
        }
        // //
        public static string To36(int nNumber)
        {
            // //
            if (nNumber <= 0) return "0";
            // //
            StringBuilder strbdBuffPublic = new StringBuilder();
            // //
            for (int nThuong = nNumber; nThuong > 0; nThuong = nThuong / s_nBaseSize)
            {
                int nDu = nThuong % s_nBaseSize;
                strbdBuffPublic.Insert(0, s_dicTo36[nDu]);
            }
            // //
            return Convert.ToString(strbdBuffPublic);
        }
        public static int To10(string strNumber36)
        {
            // //
            int nResult = 0;
            strNumber36 = strNumber36.Trim().ToUpper();
            int nLength = strNumber36.Length;
            // //
            int nAccum = 1;
            for (int nScan = 0; nScan < nLength; nScan++)
            {
                nResult += nAccum * s_dicTo10[strNumber36[nLength - nScan - 1]];
                nAccum *= s_nBaseSize;
            }
            // //
            return nResult;
        }
        // //
        public static string To36(int nNumber, int nLengthExact, char chFormat = '0')
        {
            // //
            string strResult = new string(chFormat, nLengthExact) + To36(nNumber);
            strResult = strResult.Substring(strResult.Length - nLengthExact);
            return strResult;
            // //
        }
        // //
    }

    #region // DungND:
    public class MyStringUtils
    {
        private static Hashtable htStdCode = (Hashtable)null;
        private static Hashtable htStdCodeLetter = (Hashtable)null;
        private static Hashtable htStdCodeNumber = (Hashtable)null;

        static MyStringUtils()
        {
            MyStringUtils.FillhtStdCode();
            MyStringUtils.FillhtStdCodeLetter();
            MyStringUtils.FillhtStdCodeNumber();
        }

        private static void FillhtStdCode()
        {
            MyStringUtils.htStdCode = new Hashtable(200);
            MyStringUtils.htStdCode.Add((object)'A', (object)'A');
            MyStringUtils.htStdCode.Add((object)'B', (object)'B');
            MyStringUtils.htStdCode.Add((object)'C', (object)'C');
            MyStringUtils.htStdCode.Add((object)'D', (object)'D');
            MyStringUtils.htStdCode.Add((object)'Đ', (object)'Đ');
            MyStringUtils.htStdCode.Add((object)'E', (object)'E');
            MyStringUtils.htStdCode.Add((object)'F', (object)'F');
            MyStringUtils.htStdCode.Add((object)'G', (object)'G');
            MyStringUtils.htStdCode.Add((object)'H', (object)'H');
            MyStringUtils.htStdCode.Add((object)'I', (object)'I');
            MyStringUtils.htStdCode.Add((object)'J', (object)'J');
            MyStringUtils.htStdCode.Add((object)'K', (object)'K');
            MyStringUtils.htStdCode.Add((object)'L', (object)'L');
            MyStringUtils.htStdCode.Add((object)'M', (object)'M');
            MyStringUtils.htStdCode.Add((object)'N', (object)'N');
            MyStringUtils.htStdCode.Add((object)'O', (object)'O');
            MyStringUtils.htStdCode.Add((object)'P', (object)'P');
            MyStringUtils.htStdCode.Add((object)'Q', (object)'Q');
            MyStringUtils.htStdCode.Add((object)'R', (object)'R');
            MyStringUtils.htStdCode.Add((object)'S', (object)'S');
            MyStringUtils.htStdCode.Add((object)'T', (object)'T');
            MyStringUtils.htStdCode.Add((object)'U', (object)'U');
            MyStringUtils.htStdCode.Add((object)'V', (object)'V');
            MyStringUtils.htStdCode.Add((object)'X', (object)'X');
            MyStringUtils.htStdCode.Add((object)'Y', (object)'Y');
            MyStringUtils.htStdCode.Add((object)'Z', (object)'Z');
            MyStringUtils.htStdCode.Add((object)'W', (object)'W');
            MyStringUtils.htStdCode.Add((object)'0', (object)'0');
            MyStringUtils.htStdCode.Add((object)'1', (object)'1');
            MyStringUtils.htStdCode.Add((object)'2', (object)'2');
            MyStringUtils.htStdCode.Add((object)'3', (object)'3');
            MyStringUtils.htStdCode.Add((object)'4', (object)'4');
            MyStringUtils.htStdCode.Add((object)'5', (object)'5');
            MyStringUtils.htStdCode.Add((object)'6', (object)'6');
            MyStringUtils.htStdCode.Add((object)'7', (object)'7');
            MyStringUtils.htStdCode.Add((object)'8', (object)'8');
            MyStringUtils.htStdCode.Add((object)'9', (object)'9');
            MyStringUtils.htStdCode.Add((object)'.', (object)'.');
            MyStringUtils.htStdCode.Add((object)'@', (object)'@');
            MyStringUtils.htStdCode.Add((object)'_', (object)'_');
            MyStringUtils.htStdCode.Add((object)'-', (object)'-');
            MyStringUtils.htStdCode.Add((object)' ', (object)' ');
            MyStringUtils.htStdCode.Add((object)'/', (object)'/');
        }

        private static void FillhtStdCodeLetter()
        {
            MyStringUtils.htStdCodeLetter = new Hashtable(200);
            MyStringUtils.htStdCodeLetter.Add((object)'A', (object)'A');
            MyStringUtils.htStdCodeLetter.Add((object)'B', (object)'B');
            MyStringUtils.htStdCodeLetter.Add((object)'C', (object)'C');
            MyStringUtils.htStdCodeLetter.Add((object)'D', (object)'D');
            MyStringUtils.htStdCodeLetter.Add((object)'Đ', (object)'Đ');
            MyStringUtils.htStdCodeLetter.Add((object)'E', (object)'E');
            MyStringUtils.htStdCodeLetter.Add((object)'F', (object)'F');
            MyStringUtils.htStdCodeLetter.Add((object)'G', (object)'G');
            MyStringUtils.htStdCodeLetter.Add((object)'H', (object)'H');
            MyStringUtils.htStdCodeLetter.Add((object)'I', (object)'I');
            MyStringUtils.htStdCodeLetter.Add((object)'J', (object)'J');
            MyStringUtils.htStdCodeLetter.Add((object)'K', (object)'K');
            MyStringUtils.htStdCodeLetter.Add((object)'L', (object)'L');
            MyStringUtils.htStdCodeLetter.Add((object)'M', (object)'M');
            MyStringUtils.htStdCodeLetter.Add((object)'N', (object)'N');
            MyStringUtils.htStdCodeLetter.Add((object)'O', (object)'O');
            MyStringUtils.htStdCodeLetter.Add((object)'P', (object)'P');
            MyStringUtils.htStdCodeLetter.Add((object)'Q', (object)'Q');
            MyStringUtils.htStdCodeLetter.Add((object)'R', (object)'R');
            MyStringUtils.htStdCodeLetter.Add((object)'S', (object)'S');
            MyStringUtils.htStdCodeLetter.Add((object)'T', (object)'T');
            MyStringUtils.htStdCodeLetter.Add((object)'U', (object)'U');
            MyStringUtils.htStdCodeLetter.Add((object)'V', (object)'V');
            MyStringUtils.htStdCodeLetter.Add((object)'X', (object)'X');
            MyStringUtils.htStdCodeLetter.Add((object)'Y', (object)'Y');
            MyStringUtils.htStdCodeLetter.Add((object)'Z', (object)'Z');
            MyStringUtils.htStdCodeLetter.Add((object)'W', (object)'W');
            MyStringUtils.htStdCodeLetter.Add((object)'.', (object)'.');
            MyStringUtils.htStdCodeLetter.Add((object)'@', (object)'@');
            MyStringUtils.htStdCodeLetter.Add((object)'_', (object)'_');
            MyStringUtils.htStdCodeLetter.Add((object)'-', (object)'-');
            //MyStringUtils.htStdCodeLetter.Add((object)' ', (object)' ');
            MyStringUtils.htStdCodeLetter.Add((object)'/', (object)'/');
        }

        private static void FillhtStdCodeNumber()
        {
            MyStringUtils.htStdCodeNumber = new Hashtable(200);
            MyStringUtils.htStdCodeNumber.Add((object)'0', (object)'0');
            MyStringUtils.htStdCodeNumber.Add((object)'1', (object)'1');
            MyStringUtils.htStdCodeNumber.Add((object)'2', (object)'2');
            MyStringUtils.htStdCodeNumber.Add((object)'3', (object)'3');
            MyStringUtils.htStdCodeNumber.Add((object)'4', (object)'4');
            MyStringUtils.htStdCodeNumber.Add((object)'5', (object)'5');
            MyStringUtils.htStdCodeNumber.Add((object)'6', (object)'6');
            MyStringUtils.htStdCodeNumber.Add((object)'7', (object)'7');
            MyStringUtils.htStdCodeNumber.Add((object)'8', (object)'8');
            MyStringUtils.htStdCodeNumber.Add((object)'9', (object)'9');
        }

        public static void CheckStdCode(
            ref ArrayList alParamsCoupleError
            , ref string strSource
            ////
            , bool bCheckStdCode
            , bool bCheckDoubleSpaces
            )
        {
            ////
            if (bCheckStdCode)
            {
                for (int i = 0; i < strSource.Length; i++)
                {
                    object obj = (object)strSource[i];

                    if (!MyStringUtils.htStdCode.ContainsKey(obj))
                    {
                        alParamsCoupleError.AddRange(new object[]{
                            "Check.strSource", strSource
                            , "Check.Char", obj
                            });
                        throw CmUtils.CMyException.Raise(
                            TError.ErridnInventory.CmSys_InvalidStdCode
                            , null
                            , alParamsCoupleError.ToArray()
                            );
                    }
                }
            }
            ////
            if (bCheckDoubleSpaces)
            {
                if (strSource.Contains("  "))
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.strSource", strSource
                        , "Check.Condition.RaiseError", "Double Spaces"
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.CmSys_InvalidStdCode
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
            }
        }

        public static bool IsValid(string strPassword)
        {
            ////
            int Minimum_Length = 8;
            int nLetter = 0;
            int nNumber = 0;

            //// Check Lenth
            if (strPassword.Length < Minimum_Length)
            {
                return false;
            }
            //// Check Letter:
            for (int i = 0; i < strPassword.Length; i++)
            {
                object objLetter = (object)strPassword[i];

                if (MyStringUtils.htStdCodeLetter.ContainsKey(objLetter))
                {
                    nLetter++;
                }
            }
            //// Check Number:
            for (int i = 0; i < strPassword.Length; i++)
            {
                object objLetter = (object)strPassword[i];

                if (MyStringUtils.htStdCodeNumber.ContainsKey(objLetter))
                {
                    nNumber++;
                }
            }

            ////
            if (nLetter == 0 || nNumber == 0)
            {
                return false;
            }

            return true;
        }

        public static string To10Mask(long nNumber, int nLengthExact)
        {
            ////
            char chFormat = '0';
            string strResult = new string(chFormat, nLengthExact) + nNumber.ToString();
            strResult = strResult.Substring(strResult.Length - nLengthExact);
            return strResult;
            ////
        }

        public static string SubStringBetweenElement(string STR, string FirstString, string LastString)
        {
            string FinalString;
            int Pos1 = STR.IndexOf(FirstString);
            int Pos2 = STR.IndexOf(LastString) + LastString.Length;
            FinalString = STR.Substring(Pos1, Pos2 - Pos1);
            return FinalString;
        }
    }
    public class PasswordPolicy
    {
        private static int Minimum_Length = 8;
        private static int Upper_Case_length = 1;
        //private static int Lower_Case_length = 1;
        //private static int NonAlpha_length = 1;
        //private static int Numeric_length = 1;

        public static bool IsValid(string Password)
        {
            if (Password.Length < Minimum_Length)
                return false;
            if (UpperCaseCount(Password) < Upper_Case_length)
                return false;
            //if (LowerCaseCount(Password) < Lower_Case_length)
            //	return false;
            if (NumericCount(Password) < 1)
                return false;
            //if (NonAlphaCount(Password) < NonAlpha_length)
            //	return false;
            return true;
        }

        private static int UpperCaseCount(string Password)
        {
            return Regex.Matches(Password, "[A-Z]").Count;
        }

        private static int LowerCaseCount(string Password)
        {
            return Regex.Matches(Password, "[a-z]").Count;
        }
        private static int NumericCount(string Password)
        {
            return Regex.Matches(Password, "[0-9]").Count;
        }
        private static int NonAlphaCount(string Password)
        {
            return Regex.Matches(Password, @"[^0-9a-zA-Z\._]").Count;
        }
    }

    public class DataTableCmUtils
    {
        public static List<T> ToListof<T>(DataTable dt)
        {
            const BindingFlags flags = BindingFlags.Public | BindingFlags.Instance;
            var columnNames = dt.Columns.Cast<DataColumn>()
                .Select(c => c.ColumnName.ToUpper())
                .ToList();
            var objectProperties = typeof(T).GetProperties(flags);
            var targetList = dt.AsEnumerable().Select(dataRow =>
            {
                var instanceOfT = Activator.CreateInstance<T>();

                foreach (var properties in objectProperties.Where(properties => columnNames.Contains(properties.Name.ToUpper()) && dataRow[properties.Name.ToUpper()] != DBNull.Value))
                {
                    properties.SetValue(instanceOfT, dataRow[properties.Name], null);
                }
                return instanceOfT;
            }).ToList();

            return targetList;
        }

        public static List<T> DtToList<T>(DataTable dt) where T : class, new()
        {
            List<T> lst = new List<T>();

            foreach (var row in dt.AsEnumerable())
            {
                T obj = new T();

                foreach (var prop in obj.GetType().GetProperties())
                {
                    PropertyInfo propertyInfo = obj.GetType().GetProperty(prop.Name);
                    propertyInfo.SetValue(obj, Convert.ChangeType(row[prop.Name], propertyInfo.PropertyType), null);
                }

                lst.Add(obj);
            }

            return lst;
        }

        public static DataTable ToDataTable<T>(IList<T> data, string dtName)
        {
            PropertyDescriptorCollection properties =
                TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable(dtName);
            foreach (PropertyDescriptor prop in properties)
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            foreach (T item in data)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                table.Rows.Add(row);
            }
            return table;
        }
    }
    #endregion

    #region // MailGun:
    public static class MailGunCommon
    {
        public static void SendMail(SendMail sendMail)
        {
            RestClient client = new RestClient();
            client.BaseUrl = @"https://api.mailgun.net/v3";
            client.Authenticator =
                new HttpBasicAuthenticator(sendMail.MG_HttpBasicAuthUserName,
                                            sendMail.MG_HttpBasicAuthUserPwd);

            //client.Authenticator =
            //	new HttpBasicAuthenticator("api",
            //								"00000");
            RestRequest request = new RestRequest();
            request.AddParameter("domain", sendMail.MG_Domain, ParameterType.UrlSegment);
            request.Resource = "{domain}/messages";
            request.AddParameter("from", sendMail.MG_From);
            request.AddParameter("subject", sendMail.Subject);
            request.AddParameter("html", sendMail.Body);
            request.Method = Method.POST;
            // //
            var multiToMail = sendMail.ToMail;
            if (multiToMail != null && multiToMail.Count > 0)
            {
                foreach (var item in multiToMail)
                {
                    request.AddParameter("to", item.Trim());
                }
            }
            // //
            var multiCcMail = sendMail.CcMail;
            if (multiCcMail != null && multiCcMail.Count > 0)
            {
                foreach (var item in multiCcMail)
                {
                    request.AddParameter("cc", item.Trim());
                }
            }
            // //
            var multiBccMail = sendMail.BccMail;
            if (multiBccMail != null && multiBccMail.Count > 0)
            {
                foreach (var item in multiBccMail)
                {
                    request.AddParameter("bcc", item.Trim());
                }
            }

            var multiAttachmentFile = sendMail.AttachmentFile;
            if (multiAttachmentFile != null && multiAttachmentFile.Count > 0)
            {
                foreach (var attachmentFilePath in multiAttachmentFile)
                {
                    request.AddFile("attachment", attachmentFilePath.Trim());
                }

            }

            IRestResponse restResponse = client.Execute(request);
            if (restResponse.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception(restResponse.StatusCode.ToString());
            }

        }
    }
    #endregion

    public class CProcessExc
    {
        private const string c_strKey_DataSetName_CMyDataSet = "c_K_DS";
        private const string c_strKey_DataTableName_SysInfo = "c_K_DT_SysInfo";
        private const string c_strKey_DataTableName_SysError = "c_K_DT_SysError";
        private const string c_strKey_DataTableName_SysWarning = "c_K_DT_SysWarning";
        private const string c_strKey_ColumnName_Tid = "Tid";
        private const string c_strKey_ColumnName_DigitalSignature = "DigitalSignature";
        private const string c_strKey_ColumnName_ErrorCode = "ErrorCode";
        private const string c_strKey_ColumnName_FlagWarning = "FlagWarning";
        private const string c_strKey_ColumnName_Remark = "Remark";
        private const string c_strKey_ColumnName_FlagCompress = "FlagCompress";
        private const string c_strKey_ColumnName_FlagEncrypt = "FlagEncrypt";
        private const string c_strKey_ColumnName_ParamCode = "PCode";
        private const string c_strKey_ColumnName_ParamValue = "PVal";
        private const string c_strKey_ColumnName_WarningCode = "WCode";
        private const string c_strKey_ColumnName_WarningValue = "WVal";

        public static DataSet Process(
            ref DataSet mdsFinal
            , Exception exc
            , string strErrorCode
            , params object[] arrobjErrorParams
            )
        {
            // Process:
            if (CmUtils.CMyException.IsMyException(exc))
            {
                CmUtils.CMyDataSet.SetErrorCode(ref mdsFinal, CmUtils.CMyException.GetErrorCode(exc));
                CmUtils.CMyDataSet.AppendErrorParams(ref mdsFinal, CmUtils.CMyException.GetErrorParams(exc));
            }
            else
            {
                CmUtils.CMyDataSet.SetErrorCode(ref mdsFinal, strErrorCode);
                CmUtils.CMyDataSet.AppendErrorParams(ref mdsFinal, arrobjErrorParams);
                if (exc.Data != null)
                {
                    foreach (object objDataKey in exc.Data.Keys)
                    {
                        CmUtils.CMyDataSet.AppendErrorParams(ref mdsFinal, string.Format("ExcData.{0}", objDataKey), string.Format("{0}", exc.Data[objDataKey]));
                    }
                }
                CmUtils.CMyDataSet.AppendErrorParams(
                    ref mdsFinal
                    , "ExceptionContents", string.Format("{0}\r\n{1}", exc.Message, exc.StackTrace)
                    );
            }

            // Always return Bad:
            mdsFinal.AcceptChanges();
            return mdsFinal;
        }

        public static DataSet Process1(
            ref DataSet mdsFinal
            , Exception exc
            , string strErrorCode
            , params object[] arrobjErrorParams
            )
        {
            // Process:
            if (CmUtils.CMyException.IsMyException(exc))
            {
                CmUtils.CMyDataSet.SetErrorCode(ref mdsFinal, CmUtils.CMyException.GetErrorCode(exc));
                AppendErrorParams(ref mdsFinal, CmUtils.CMyException.GetErrorParams(exc));
            }
            else
            {
                CmUtils.CMyDataSet.SetErrorCode(ref mdsFinal, strErrorCode);
                CmUtils.CMyDataSet.AppendErrorParams(ref mdsFinal, arrobjErrorParams);
                if (exc.Data != null)
                {
                    foreach (object objDataKey in exc.Data.Keys)
                    {
                        CmUtils.CMyDataSet.AppendErrorParams(ref mdsFinal, string.Format("ExcData.{0}", objDataKey), string.Format("{0}", exc.Data[objDataKey]));
                    }
                }
                CmUtils.CMyDataSet.AppendErrorParams(
                    ref mdsFinal
                    , "ExceptionContents", string.Format("{0}\r\n{1}", exc.Message, exc.StackTrace)
                    );
            }

            // Always return Bad:
            mdsFinal.AcceptChanges();
            return mdsFinal;
        }


        public static void AddParamServiceException(
            ref ArrayList alParamsCoupleError
            , DataSet ds
            )
        {
            ServiceException ex = new ServiceException();
            if (ds == null)
            {
                ex.ErrorCode = "ERR0001";
                ex.ErrorMessage = "Null dataset return";
                ex.ErrorDetail = "Null dataset return";
                ex.Tag = "";
            }

            string errorCode = Convert.ToString(CmUtils.CMyDataSet.GetErrorCode(ds));
            string errorMessage = Convert.ToString(CmUtils.CMyDataSet.GetErrorCode(ds));
            StringBuilder sbDetail = new StringBuilder();

            sbDetail.Append(string.Format("Error Code: {0}", errorCode));
            sbDetail.Append("<br/>--------------------------------------------------------<br/>");
            object[] arrObj = CmUtils.CMyDataSet.GetErrorParams(ds);
            if (arrObj != null && arrObj.Length > 1)
            {
                for (int i = 0; i < arrObj.Length; i++)
                {
                    string val = Convert.ToString(arrObj[i]);

                    val = System.Net.WebUtility.HtmlEncode(val);
                    sbDetail.Append(val.Replace("\n", "<br/>"));
                    if (i % 2 == 0)
                        sbDetail.Append(" = ");
                    else
                        sbDetail.Append("<br/>--------------------------------------------------------<br/>");
                }
            }
            ex.ErrorCode = errorMessage;
            ex.ErrorMessage = errorCode;
            ex.ErrorDetail = sbDetail.ToString();
            ex.Tag = "";

            alParamsCoupleError.AddRange(new object[]{
                "Check.OutSite.ErrorCode", ex.ErrorCode
                , "Check.OutSite.ErrorMessage", ex.ErrorMessage
                , "Check.OutSite.ErrorDetail", ex.ErrorDetail
                 });
        }

        public static void AppendErrorParams(ref DataSet mds, params object[] arrobjParamsCouple)
        {
            if (arrobjParamsCouple == null || arrobjParamsCouple.Length == 0) return;
            DataTable dt = mds.Tables[c_strKey_DataTableName_SysError];
            for (int i = 0; i < arrobjParamsCouple.Length; i += 2)
            {
                dt.Rows.Add(arrobjParamsCouple[i], arrobjParamsCouple[i + 1]);
            }
        }


        public static void BizShowException(
            ref ArrayList alParamsCoupleError
            , Exception ex
            )
        {
            DataSet dsGetData = new DataSet();
            if (ex != null)
            {
                if (ex is Common.Models.ClientException)
                {
                    var cex = ex as ClientException;

                    var c_K_DT_Sys = cex.c_K_DT_Sys;
                    var exception = cex.Exception;


                    if (exception != null)
                    {
                        var strStackTrace = "";
                        var strMessage = "";

                        alParamsCoupleError.AddRange(new object[]{
                            "Check.OutSite.Exception", ""
                            });
                        if (!CUtils.IsNullOrEmpty(exception.Message))
                        {
                            strMessage = exception.Message.Trim();
                            alParamsCoupleError.AddRange(new object[]{
                               "Check.OutSite.Message", strMessage
                                 });
                            ////
                        }
                        if (!CUtils.IsNullOrEmpty(exception.StackTrace))
                        {
                            strStackTrace = exception.StackTrace.Trim();

                            alParamsCoupleError.AddRange(new object[]{
                                "Check.OutSite.StackTrace", strStackTrace
                                });
                            ////
                        }
                        if (!CUtils.IsNullOrEmpty(strStackTrace))
                        {
                            alParamsCoupleError.AddRange(new object[]{
                                "Check.OutSite.Exception", ""
                                , "Check.OutSite.Message", strMessage
                                , "Check.OutSite.StackTrace", strStackTrace
                                 });
                            ////
                        }

                    }
                    if (c_K_DT_Sys != null)
                    {
                        #region["Lst_c_K_DT_SysInfo"]
                        if (c_K_DT_Sys.Lst_c_K_DT_SysInfo != null && c_K_DT_Sys.Lst_c_K_DT_SysInfo.Count > 0)
                        {
                            DataTable dt_c_K_DT_SysInfo = new DataTable();
                            dt_c_K_DT_SysInfo = DataTableCmUtils.ToDataTable<c_K_DT_SysInfo>(c_K_DT_Sys.Lst_c_K_DT_SysInfo, "c_K_DT_SysInfo");
                            dsGetData.Tables.Add(dt_c_K_DT_SysInfo.Copy());
                        }
                        #endregion

                        #region["Lst_c_K_DT_SysError"]
                        if (c_K_DT_Sys.Lst_c_K_DT_SysError != null && c_K_DT_Sys.Lst_c_K_DT_SysError.Count > 0)
                        {
                            DataTable dt_c_K_DT_SysError = new DataTable();
                            dt_c_K_DT_SysError = DataTableCmUtils.ToDataTable<c_K_DT_SysError>(c_K_DT_Sys.Lst_c_K_DT_SysError, "c_K_DT_SysError");
                            dsGetData.Tables.Add(dt_c_K_DT_SysError.Copy());
                            /////
                        }
                        #endregion

                        #region["Lst_c_K_DT_SysWarning"]
                        if (c_K_DT_Sys.Lst_c_K_DT_SysWarning != null && c_K_DT_Sys.Lst_c_K_DT_SysWarning.Count > 0)
                        {
                            DataTable dt_c_K_DT_SysWarning = new DataTable();
                            dt_c_K_DT_SysWarning = DataTableCmUtils.ToDataTable<c_K_DT_SysWarning>(c_K_DT_Sys.Lst_c_K_DT_SysWarning, "c_K_DT_SysWarning");
                            dsGetData.Tables.Add(dt_c_K_DT_SysWarning.Copy());
                            /////
                        }
                        #endregion
                        /////
                        CProcessExc.AddParamServiceException(
                            ref alParamsCoupleError // alParamsCoupleError
                            , dsGetData // ds
                            );
                        ////
                    }
                }
                else
                {
                    var strStackTrace = "";
                    var strMessage = "";
                    if (!CUtils.IsNullOrEmpty(ex.Message))
                    {
                        strMessage = ex.Message.Trim();

                        alParamsCoupleError.AddRange(new object[]{
                            "Check.OutSite.Message", strMessage
                            });
                        ////
                    }
                    if (!CUtils.IsNullOrEmpty(ex.StackTrace))
                    {
                        strStackTrace = ex.StackTrace.Trim();

                        alParamsCoupleError.AddRange(new object[]{
                            "Check.OutSite.StackTrace", strStackTrace
                            });
                        ////
                    }
                    if (!CUtils.IsNullOrEmpty(strStackTrace))
                    {

                        alParamsCoupleError.AddRange(new object[]{
                            "Check.OutSite.Message", strMessage
                            , "Check.OutSite.StackTrace", strStackTrace
                            });
                        ////
                    }
                }

            }
        }

        public static void BizShowException(
            ref ArrayList alParamsCoupleError
            , Exception ex
            , out string strErrorCodeOS
            )
        {
            DataSet dsGetData = new DataSet();
            strErrorCodeOS = null;
            if (ex != null)
            {
                if (ex is Common.Models.ClientException)
                {
                    var cex = ex as ClientException;

                    var c_K_DT_Sys = cex.c_K_DT_Sys;
                    var exception = cex.Exception;


                    if (exception != null)
                    {
                        var strStackTrace = "";
                        var strMessage = "";

                        alParamsCoupleError.AddRange(new object[]{
                            "Check.OutSite.Exception", ""
                            });
                        if (!CUtils.IsNullOrEmpty(exception.Message))
                        {
                            strMessage = exception.Message.Trim();
                            alParamsCoupleError.AddRange(new object[]{
                               "Check.OutSite.Message", strMessage
                                 });
                            ////
                        }
                        if (!CUtils.IsNullOrEmpty(exception.StackTrace))
                        {
                            strStackTrace = exception.StackTrace.Trim();

                            alParamsCoupleError.AddRange(new object[]{
                                "Check.OutSite.StackTrace", strStackTrace
                                });
                            ////
                        }
                        if (!CUtils.IsNullOrEmpty(strStackTrace))
                        {
                            alParamsCoupleError.AddRange(new object[]{
                                "Check.OutSite.Exception", ""
                                , "Check.OutSite.Message", strMessage
                                , "Check.OutSite.StackTrace", strStackTrace
                                 });
                            ////
                        }

                    }
                    if (c_K_DT_Sys != null)
                    {
                        #region["Lst_c_K_DT_SysInfo"]
                        if (c_K_DT_Sys.Lst_c_K_DT_SysInfo != null && c_K_DT_Sys.Lst_c_K_DT_SysInfo.Count > 0)
                        {
                            DataTable dt_c_K_DT_SysInfo = new DataTable();
                            dt_c_K_DT_SysInfo = DataTableCmUtils.ToDataTable<c_K_DT_SysInfo>(c_K_DT_Sys.Lst_c_K_DT_SysInfo, "c_K_DT_SysInfo");
                            strErrorCodeOS = Convert.ToString(dt_c_K_DT_SysInfo.Rows[0]["ErrorCode"]);
                            dsGetData.Tables.Add(dt_c_K_DT_SysInfo.Copy());

                        }
                        #endregion

                        #region["Lst_c_K_DT_SysError"]
                        if (c_K_DT_Sys.Lst_c_K_DT_SysError != null && c_K_DT_Sys.Lst_c_K_DT_SysError.Count > 0)
                        {
                            DataTable dt_c_K_DT_SysError = new DataTable();
                            dt_c_K_DT_SysError = DataTableCmUtils.ToDataTable<c_K_DT_SysError>(c_K_DT_Sys.Lst_c_K_DT_SysError, "c_K_DT_SysError");
                            dsGetData.Tables.Add(dt_c_K_DT_SysError.Copy());
                            /////
                        }
                        #endregion

                        #region["Lst_c_K_DT_SysWarning"]
                        if (c_K_DT_Sys.Lst_c_K_DT_SysWarning != null && c_K_DT_Sys.Lst_c_K_DT_SysWarning.Count > 0)
                        {
                            DataTable dt_c_K_DT_SysWarning = new DataTable();
                            dt_c_K_DT_SysWarning = DataTableCmUtils.ToDataTable<c_K_DT_SysWarning>(c_K_DT_Sys.Lst_c_K_DT_SysWarning, "c_K_DT_SysWarning");
                            dsGetData.Tables.Add(dt_c_K_DT_SysWarning.Copy());
                            /////
                        }
                        #endregion
                        /////
                        CProcessExc.AddParamServiceException(
                            ref alParamsCoupleError // alParamsCoupleError
                            , dsGetData // ds
                            );
                        ////
                    }
                }
                else
                {
                    var strStackTrace = "";
                    var strMessage = "";
                    if (!CUtils.IsNullOrEmpty(ex.Message))
                    {
                        strMessage = ex.Message.Trim();

                        alParamsCoupleError.AddRange(new object[]{
                            "Check.OutSite.Message", strMessage
                            });
                        ////
                    }
                    if (!CUtils.IsNullOrEmpty(ex.StackTrace))
                    {
                        strStackTrace = ex.StackTrace.Trim();

                        alParamsCoupleError.AddRange(new object[]{
                            "Check.OutSite.StackTrace", strStackTrace
                            });
                        ////
                    }
                    if (!CUtils.IsNullOrEmpty(strStackTrace))
                    {

                        alParamsCoupleError.AddRange(new object[]{
                            "Check.OutSite.Message", strMessage
                            , "Check.OutSite.StackTrace", strStackTrace
                            });
                        ////
                    }
                }

            }
        }
    }

    public class CConnectionManager
    {
        #region // Utils:
        private static string s_strNameTimeoutMillisecond = "Biz_TimeoutMS";
        private static bool myCheckExpired(
            System.Collections.Specialized.NameValueCollection nvcParams
            , DataRow drCheck
            )
        {
            bool bExpired = ((DateTime.Now - Convert.ToDateTime(drCheck["DateTimeLastAccess"])).TotalMilliseconds > Convert.ToDouble(nvcParams[s_strNameTimeoutMillisecond]));
            return bExpired;
        }
        #endregion

        #region // Public members:
        public static void CheckGatewayAuthentication(
            System.Collections.Specialized.NameValueCollection nvcParams
            , ref ArrayList alParamsCoupleError
            , string strGwUserCode
            , string strGwPassword
            )
        {
            // Check GatewayAuthentication:
            if (!CmUtils.StringUtils.StringEqualIgnoreCase(strGwUserCode, nvcParams["Biz_GwUserCode"])
                || !CmUtils.StringUtils.StringEqual(strGwPassword, nvcParams["Biz_GwPassword"])
                )
            {
                throw CmUtils.CMyException.Raise(
                    TError.ErridnInventory.CmSys_GatewayAuthenticateFailed
                    , null
                    , alParamsCoupleError.ToArray()
                    );
            }
        }
        public static void CleanSessionExpired(
            System.Collections.Specialized.NameValueCollection nvcParams
            , TSession.Core.CSession sess
            )
        {
            sess.CleanExpired(
                true // bLazyMode
                , (DateTime.Now.AddMilliseconds(-Convert.ToDouble(nvcParams[s_strNameTimeoutMillisecond]))) // objDateTimeExpiredForLastAccess
                );
        }
        public static DataRow CheckSessionInfo(
            TSession.Core.CSession sess
            , ref ArrayList alParamsCoupleError
            , string strSessionId
            )
        {
            DataRow drSession = sess.GetFromId(strSessionId);
            if (drSession == null)
            {
                alParamsCoupleError.AddRange(new object[]{
                    "Check.SessionId", strSessionId
                    });
                throw CmUtils.CMyException.Raise(
                    TError.ErridnInventory.CmSys_SessionNotFound
                    , null
                    , alParamsCoupleError.ToArray()
                    );
            }
            return drSession;
        }
        public static void RaiseSessionExpired(
            System.Collections.Specialized.NameValueCollection nvcParams
            , ref ArrayList alParamsCoupleError
            , DataRow drSession
            )
        {
            // Check ExpiredSession:
            alParamsCoupleError.AddRange(new object[]{
                "Check.DateTimeLastAccess", drSession["DateTimeLastAccess"]
                , "Check.DateTimeNow", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                , "Check.TotalMilliseconds", (DateTime.Now - Convert.ToDateTime(drSession["DateTimeLastAccess"])).TotalMilliseconds
                , "Check.TimeoutMillisecond", nvcParams[s_strNameTimeoutMillisecond]
                });
            throw CmUtils.CMyException.Raise(
                TError.ErridnInventory.CmSys_SessionExpired
                , null
                , alParamsCoupleError.ToArray()
                );
        }
        public static void CheckAllCondition(
            System.Collections.Specialized.NameValueCollection nvcParams
            , TSession.Core.CSession sess
            , ref ArrayList alParamsCoupleError
            , string strGwUserCode
            , string strGwPassword
            , string strSessionId
            , out DataRow drSession
            )
        {
            // Init:
            drSession = null;

            // Check GatewayAuthentication:
            CheckGatewayAuthentication(
                nvcParams // nvcParams
                , ref alParamsCoupleError // alParamsCoupleError
                , strGwUserCode // strGwUserCode
                , strGwPassword // strGwPassword
                );

            // Check Session:
            drSession = CheckSessionInfo(
                sess // sess
                , ref alParamsCoupleError // alParamsCoupleError
                , strSessionId // strSessionId
            );

            // Check ExpiredSession:
            if (myCheckExpired(nvcParams, drSession))
            {
                CleanSessionExpired(nvcParams, sess);
                RaiseSessionExpired(nvcParams, ref alParamsCoupleError, drSession);
            }

            // Reset LastAccess:
            sess.UpdateLastAccess(
                false // bLazyMode
                , strSessionId // strSessionId
                , DateTime.Now // objDateTimeLastAccess
                );
        }

        #endregion
    }



    public class ServiceException : Exception
    {
        //svn://192.168.1.199:8035/idocNet/2015.2.DMSChain/Dev/V20/idn.Skycic.Inventory.Utils/ErrorCodes.cs
        public static string ERROR_CODE_CLIENT = "ERROR_CLIENT";
        //
        private string _errorMessage;
        private string _errorDetail;
        private string _errorCode;
        //
        private object _tag;
        //
        public Boolean isWarning = false;

        public string ErrorDetail
        {
            set { _errorDetail = value; }
            get { return _errorDetail; }
        }

        public string ErrorCode
        {
            set { _errorCode = value; }
            get { return _errorCode; }
        }

        public string ErrorMessage
        {
            set { _errorMessage = value; }
            get { return _errorMessage; }
        }

        public object Tag
        {
            set { _tag = value; }
            get { return _tag; }
        }

        public bool HasError()
        {
            return (this.ErrorCode != null && this.ErrorCode.Length > 0);
        }
    }

    public class Serialization
    {
        public static void SerializeSettings<T>(T o, string filePath)
        {
            Serialize(o, filePath);
        }
        public static T DeserializeSettings<T>(string filePath)
        {
            return Deserialize<T>(filePath);
        }
        public static void Serialize<T>(T o, string filePath)
        {
            DataContractSerializer ser = new DataContractSerializer(typeof(T));
            string folderPath = Path.GetDirectoryName(filePath);
            EnsureDirectoryExists(folderPath);
            using (FileStream stream = new FileStream(filePath, FileMode.Create))
            {
                ser.WriteObject(stream, o);
            }
        }
        public static T Deserialize<T>(string filePath)
        {
            DataContractSerializer ser = new DataContractSerializer(typeof(T));
            return (T)Deserialize(typeof(T), new[] { typeof(T) }, filePath);
        }
        public static object Deserialize(Type type, IEnumerable<Type> knownTypes, string filePath)
        {
            DataContractSerializer ser = new DataContractSerializer(type, knownTypes);
            using (FileStream stream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                return ser.ReadObject(stream);
            }
        }

        private static void EnsureDirectoryExists(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }
    }
}
