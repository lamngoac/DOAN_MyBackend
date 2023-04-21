using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace idn.Skycic.Inventory.Web.Models
{
    public static class clsDocTien
    {
        public static string[] arrSo = { "không", "một", "hai", "ba", "bốn", "năm", "sáu", "bảy", "tám", "chín" };

        private static string Doc3So(string so, bool isDau)
        {
            Int16 str1So, str2So, str3So;
            try
            {
                string ketQua = string.Empty;
                if (so == "000") return string.Empty;
                str1So = Convert.ToInt16(so.Substring(0, 1));
                str2So = Convert.ToInt16(so.Substring(1, 1));
                str3So = Convert.ToInt16(so.Substring(2, 1));
                if (isDau != true || str1So != 0) ketQua = arrSo[str1So] + " trăm";
                if (str2So != 0)
                {
                    if (str2So == 1) ketQua += " mười";
                    else ketQua += " " + arrSo[str2So] + " mươi";
                }
                else if (isDau != true || str1So != 0) if (str3So != 0) ketQua += " linh";
                if (str3So == 1)
                {
                    if (str2So == 0 || str2So == 1) ketQua += " một";
                    else ketQua += " mốt";
                }
                else if (str3So == 4)
                {
                    if (str2So == 0 || str2So == 1) ketQua += " bốn";
                    else ketQua += " tư";
                }
                else if (str3So == 5)
                {
                    if (str2So == 0 /*|| str2So == 1*/) ketQua += " năm";
                    else ketQua += " lăm";
                }
                else
                {
                    if (str3So != 0)
                    {
                        if (str2So == 0 || str2So == 1) ketQua += " " + arrSo[str3So];
                        else ketQua += " " + arrSo[str3So];
                    }
                }
                return ketQua.Trim();
            }
            catch
            {
                return string.Empty;
            }
        }

        private static string Doc9So(string so, bool isDau)
        {
            try
            {
                string ketQua = string.Empty;
                string ketQuaTempt = string.Empty;
                ketQuaTempt = Doc3So(so.Substring(0, 3), isDau);
                if (ketQuaTempt != string.Empty)
                {
                    ketQua = ketQuaTempt + " triệu";
                    isDau = false;
                }
                ketQuaTempt = Doc3So(so.Substring(3, 3), isDau);
                if (ketQuaTempt != string.Empty)
                {
                    ketQua += " " + ketQuaTempt + " nghìn";
                    isDau = false;
                }
                ketQua += " " + Doc3So(so.Substring(6, 3), isDau);
                return ketQua.Trim();
            }
            catch
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Đọc số thành chữ với giá trị truyền vào kiểu string
        /// </summary>
        /// <param name="so">Giá trị truyền vào</param>
        /// <returns></returns>
        public static string DocSo(string so)
        {
            if (Convert.ToDecimal(so) == 0) return "không";
            so = Math.Round(Convert.ToDecimal(so), 0).ToString();
            string ketQua = string.Empty;
            string ketQuaTempt = string.Empty;
            while (so.Length % 9 != 0) so = "0" + so;
            int slTy = so.Length / 9;
            for (int i = 0; i < slTy; i++)
            {
                if (i == 0) ketQuaTempt = Doc9So(so.Substring(i * 9, 9), true);
                else ketQuaTempt = Doc9So(so.Substring(i * 9, 9), false);
                if (ketQuaTempt.Length > 0)
                {
                    ketQua += " " + ketQuaTempt;
                    for (int j = slTy - 1; j > i; j--) ketQua += " tỷ";
                }
            }
            return ketQua.Trim();
        }


        /// <summary>
        /// Đọc số thành chữ với giá trị truyền vào kiểu long integer
        /// </summary>
        /// <param name="so">Giá trị truyền vào</param>
        /// <returns></returns>
        public static string DocSo(long so)
        {
            if (so == 0) return "không";
            return DocSo(so.ToString());
        }
    }
}