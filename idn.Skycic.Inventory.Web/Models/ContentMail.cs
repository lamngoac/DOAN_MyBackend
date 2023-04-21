using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using idn.Skycic.Inventory.Common.Models;
using idn.Skycic.Inventory.Common.ModelsUI;
using idn.Skycic.Inventory.Utils;

namespace idn.Skycic.Inventory.Web.Models
{
    public class BodyEMail
    {
        public static string GetContentMailNVAT(SendMailUI mail)
        {
            var day = "";
            var year = "";
            var month = "";
            if (!CUtils.IsNullOrEmpty(mail.InvoiceDateUTC) && CUtils.IsDateTime(mail.InvoiceDateUTC))
            {
                var ngayhd = CUtils.ConvertToDateTime(mail.InvoiceDateUTC);
                day = ngayhd.Day.ToString();
                month = ngayhd.Month.ToString();
                year = ngayhd.Year.ToString();
            }
            var strTitle = @"NNT <" + mail.MST + @"> gửi hóa đơn số <" + mail.InvoiceNo + @">, mẫu <" + mail.FormNo + @">, ký hiệu: <" + mail.Sign + @"> cho <" + mail.CustomerNNTName + ">";
            var sbBodyHTML = new StringBuilder();
            sbBodyHTML.Append(@"<html lang=""en"">");
            sbBodyHTML.Append(@"<head>");
            sbBodyHTML.Append(@"<meta name=""viewport"" content =""width=device-width"" />");
            sbBodyHTML.Append(@"<meta content=""text/html; charset=utf-8"" http-equiv=""Content-Type"">");
            sbBodyHTML.Append(@"<title>" + strTitle + @"</title>");
            sbBodyHTML.Append(@"
                        <style type=""text/css"">
                            html { background: #f2f2f2;}
                            .body { width: 100%; line-height: 1.5;}
                            .div-main { overflow-x: auto; background: #ffffff; display: table;  margin: 0 auto; width: 770px; padding: 0 25px; }
                            .text-center { text-align: center; }
                            .text-transform-uppercase { text-transform: uppercase; }
                            .text-transform-capitalize { text-transform: capitalize; }
                            .width-100 { float: left; width: 100%; }
                            .width-50 { float: left; width: 50%; }
                            .width-180px { width: 180px; }
                            .width-100px { width: 100px; }
                            p { margin: 5px 0 5px; }
                            .color-blue { color: blue;}
                            .hr-line-solid { border-top: 1px solid #333333; }
                            .font-weight-bold { font-weight: bold; }
                            .font-weight-normal { font-weight: normal; }
                            .padding-top-25 { padding-top: 25px; }
                            .padding-top-15 { padding-top: 15px; }
                            .padding-left-right-15 { padding-left: 15px; padding-right: 15px; }
                            ul { padding-left: 5px; margin-top: 0px;}
                            a { text-decoration: none; }
                            .list-style-none { list-style: none;}
                        </style>
                    ");
            sbBodyHTML.Append(@"</head>");
            sbBodyHTML.Append(@"<body>");
            sbBodyHTML.Append(@"<div class=""div-main"">");
            sbBodyHTML.Append(@"<div class=""padding-left-right-15"">");
            sbBodyHTML.Append(@"<div class=""width-100"">");

            sbBodyHTML.Append(@"<div class=""width-50 text-transform-uppercase text-center"">");
            sbBodyHTML.Append(@"<p>đơn vị sử dụng dịch vụ Inbrand Clound</p>");
            sbBodyHTML.Append(@"<p>" + mail.NNTFullName + @"</p>");
            sbBodyHTML.Append(@"<hr class=""hr-line-solid width-100px"" />");
            sbBodyHTML.Append(@"</div>");

            sbBodyHTML.Append(@"<div class=""width-50 text-center"">");
            sbBodyHTML.Append(@"<p class=""text-transform-uppercase text-center"">cộng hòa xã hội chủ nghĩa việt nam</p>");
            sbBodyHTML.Append(@"<p>Độc lập - Tự do - Hạnh phúc</p>");
            sbBodyHTML.Append(@"<hr class=""hr-line-solid width-180px"" />");
            //sbBodyHTML.Append(@"<p>" + mail.mp_ProvinceName + @", ngày " + day + @" tháng " + month + @" năm " + year + @"</p>");
            sbBodyHTML.Append(@"<p>" + @"Ngày " + day + @" tháng " + month + @" năm " + year + @"</p>");
            sbBodyHTML.Append(@"</div>");

            sbBodyHTML.Append(@"</div>");

            sbBodyHTML.Append(@"<div class=""width-100""><p class=""text-transform-uppercase text-center font-weight-bold padding-top-25"">hướng dẫn nhận hóa đơn điện tử/ hóa đơn xác thực</p></div>");

            sbBodyHTML.Append(@"<div class=""width-100"">");
            sbBodyHTML.Append(@"<p class=""font-weight-bold padding-top-25"">Kính gửi: Quý <span class=""text-transform-uppercase font-weight-normal"">" + mail.CustomerNNTName + @"</span></p>");
            sbBodyHTML.Append(@"<p class=""padding-top-15"">Quý khách vui lòng xem và tải hóa đơn <a class=""font-weight-bold color-blue"" href=""https://hoadontvan.com/tracuu/"">tại đây</a> với các thông tin chi tiết sau:</p>");
            sbBodyHTML.Append(@"<p>Mẫu số: <span class=""text-transform-uppercase font-weight-bold"">" + mail.FormNo + @"</span></p>");
            sbBodyHTML.Append(@"<p>Ký hiệu: <span class=""text-transform-uppercase font-weight-bold"">" + mail.Sign + @"</span></p>");
            sbBodyHTML.Append(@"<p>Số hóa đơn: <span class=""text-transform-uppercase font-weight-bold"">" + mail.InvoiceNo + @"</span></p>");
            sbBodyHTML.Append(@"<p>Ngày hóa đơn: <span class=""text-transform-uppercase font-weight-bold"">" + mail.InvoiceDateUTC + @"</span></p>");
            sbBodyHTML.Append(@"<p>Mã tra cứu hóa đơn: <span class=""text-transform-uppercase font-weight-bold"">" + mail.InvoiceCode + @"</span></p>");
            sbBodyHTML.Append(@"<p class=""padding-top-15"">Quý khách có thể chuyển đổi sang hóa đơn giấy (vui lòng thực hiện theo luật định).</p>");
            sbBodyHTML.Append(@"<p class=""padding-top-15"">Hóa đơn điện tử này là hợp pháp, chúng tôi sử dụng chữ ký số để xác thực và mẫu hóa đơn đã được đăng ký với Cơ quan Thuế.</p>");
            sbBodyHTML.Append(@"<p class=""padding-top-15"">Chúng tôi hỗ trợ 02 cách nhận hóa đơn:</p>");
            sbBodyHTML.Append(@"<ul class=""list-style-none"">");
            sbBodyHTML.Append(@"<li>1/. Nhận hóa đơn qua email này;</li>");
            sbBodyHTML.Append(@"<li>2/. Nhận hóa đơn từ website tra cứu hóa đơn online của chúng tôi tại địa chỉ <a href=""https://hoadontvan.com/tracuu/"">https://hoadontvan.com/tracuu/</a>;</li>");
            sbBodyHTML.Append(@"</ul>");
            sbBodyHTML.Append(@"<p class=""padding-top-15"">Thông tin người liên hệ về hóa đơn điện tử:</p>");
            sbBodyHTML.Append(@"<p class=""padding-top-25"">Tên công ty XHĐ: <span class=""text-transform-uppercase font-weight-bold"">" + mail.NNTFullName + @"</span></p>");
            sbBodyHTML.Append(@"<p>Người liên hệ: <span class=""text-transform-capitalize font-weight-bold"">" + mail.ContactName + @"</span></p>");
            sbBodyHTML.Append(@"<p>Số điện thoại: <span class=""font-weight-bold"">" + mail.ContactPhone + @"</span></p>");
            sbBodyHTML.Append(@"<p>Email: <span class=""font-weight-bold color-blue"">" + mail.ContactEmail + @"</span></p>");
            sbBodyHTML.Append(@"<p class=""padding-top-15"">Chúng tôi mong muốn được tiếp tục phục vụ và hợp tác cùng Quý khách hàng trong thời gian tới.</p>");

            sbBodyHTML.Append(@"</div>");

            sbBodyHTML.Append(@"<div class=""width-100"">");
            sbBodyHTML.Append(@"<hr class=""hr-line-solid"" />");
            sbBodyHTML.Append(@"<p class=""padding-top-15"">Dear Valued Customers,</p>");
            sbBodyHTML.Append(@"<p class=""padding-top-15"">Please kindly <a class=""font-weight-bold color-blue"" href=""https://hoadontvan.com/tracuu/"">click here</a> to view and download your e-invoice with details below:</p>");
            sbBodyHTML.Append(@"<p>Form: <span class=""text-transform-uppercase font-weight-bold"">" + mail.FormNo + @"</span></p>");
            sbBodyHTML.Append(@"<p>Serial: <span class=""text-transform-uppercase font-weight-bold"">" + mail.Sign + @"</span></p>");
            sbBodyHTML.Append(@"<p>Invoice number: <span class=""text-transform-uppercase font-weight-bold"">" + mail.InvoiceNo + @"</span></p>");
            sbBodyHTML.Append(@"<p>Invoice date: <span class=""text-transform-uppercase font-weight-bold"">" + mail.InvoiceDateUTC + @"</span></p>");
            sbBodyHTML.Append(@"<p>Download code: <span class=""text-transform-uppercase font-weight-bold"">" + mail.InvoiceCode + @"</span></p>");
            sbBodyHTML.Append(@"<p class=""padding-top-15"">You can convert to hard-copy (please follow statute).</p>");
            sbBodyHTML.Append(@"<p class=""padding-top-15"">This e-invoices is legal, we use a digital signature for authentication and an invoice sample already registered with the Tax Authority.</p>");
            sbBodyHTML.Append(@"<p class=""padding-top-15"">We provide 02 ways of invoice receipt:</p>");
            sbBodyHTML.Append(@"<ul class=""list-style-none"">");
            sbBodyHTML.Append(@"<li>1/. To receive via this email;</li>");
            sbBodyHTML.Append(@"<li>2/. Check and download your invoices at <a class=""font-weight-bold color-blue"" href=""https://hoadontvan.com/tracuu/"">https://hoadontvan.com/tracuu/</a>;</li>");
            sbBodyHTML.Append(@"</ul>");
            sbBodyHTML.Append(@"<p class=""padding-top-15"">Contact information for this invoice:</p>");
            sbBodyHTML.Append(@"<p class=""padding-top-25"">Invoice issued by: <span class=""text-transform-uppercase font-weight-bold"">" + mail.NNTFullName + @"</span></p>");
            sbBodyHTML.Append(@"<p>Contact: <span class=""text-transform-capitalize font-weight-bold"">" + mail.ContactName + @"</span></p>");
            sbBodyHTML.Append(@"<p>Phone: <span class=""font-weight-bold"">" + mail.ContactPhone + @"</span></p>");
            sbBodyHTML.Append(@"<p>Email: <span class=""font-weight-bold color-blue"">" + mail.ContactEmail + @"</span></p>");
            sbBodyHTML.Append(@"<p class=""padding-top-15"">We are very appreciative of your cooperation, and we look forward to continuing this relationship.</p>");
            sbBodyHTML.Append(@"</div>");

            sbBodyHTML.Append(@"</div>");
            sbBodyHTML.Append(@"</div>");
            sbBodyHTML.Append(@"</body>");
            sbBodyHTML.Append(@"</html>");
            return sbBodyHTML.ToString();
        }

        public static string GetContentMail1VAT(SendMailUI mail)
        {
            var day = "";
            var year = "";
            var month = "";
            if (!CUtils.IsNullOrEmpty(mail.InvoiceDateUTC) && CUtils.IsDateTime(mail.InvoiceDateUTC))
            {
                var ngayhd = CUtils.ConvertToDateTime(mail.InvoiceDateUTC);
                day = ngayhd.Day.ToString();
                month = ngayhd.Month.ToString();
                year = ngayhd.Year.ToString();
            }
            var strTitle = @"NNT <" + mail.MST + @"> gửi hóa đơn số <" + mail.InvoiceNo + @">, mẫu <" + mail.FormNo + @">, ký hiệu: <" + mail.Sign + @"> cho <" + mail.CustomerNNTName + ">";
            var sbBodyHTML = new StringBuilder();
            sbBodyHTML.Append(@"<html lang=""en"">");
            sbBodyHTML.Append(@"<head>");
            sbBodyHTML.Append(@"<meta name=""viewport"" content =""width=device-width"" />");
            sbBodyHTML.Append(@"<meta content=""text/html; charset=utf-8"" http-equiv=""Content-Type"">");
            sbBodyHTML.Append(@"<title>" + strTitle + @"</title>");
            sbBodyHTML.Append(@"
                        <style type=""text/css"">
                            html { background: #f2f2f2;}
                            .body { width: 100%; line-height: 1.5;}
                            .div-main { overflow-x: auto; background: #ffffff; display: table;  margin: 0 auto; width: 770px; padding: 0 25px; }
                            .text-center { text-align: center; }
                            .text-transform-uppercase { text-transform: uppercase; }
                            .text-transform-capitalize { text-transform: capitalize; }
                            .width-100 { float: left; width: 100%; }
                            .width-50 { float: left; width: 50%; }
                            .width-180px { width: 180px; }
                            .width-100px { width: 100px; }
                            p { margin: 5px 0 5px; }
                            .color-blue { color: blue;}
                            .hr-line-solid { border-top: 1px solid #333333; }
                            .font-weight-bold { font-weight: bold; }
                            .font-weight-normal { font-weight: normal; }
                            .padding-top-25 { padding-top: 25px; }
                            .padding-top-15 { padding-top: 15px; }
                            .padding-left-right-15 { padding-left: 15px; padding-right: 15px; }
                            ul { padding-left: 5px; margin-top: 0px;}
                            a { text-decoration: none; }
                            .list-style-none { list-style: none;}
                        </style>
                    ");
            sbBodyHTML.Append(@"</head>");
            sbBodyHTML.Append(@"<body>");
            sbBodyHTML.Append(@"<div class=""div-main"">");
            sbBodyHTML.Append(@"<div class=""padding-left-right-15"">");
            sbBodyHTML.Append(@"<div class=""width-100"">");

            sbBodyHTML.Append(@"<div class=""width-50 text-transform-uppercase text-center"">");
            sbBodyHTML.Append(@"<p>đơn vị sử dụng dịch vụ Inbrand Clound</p>");
            sbBodyHTML.Append(@"<p>" + mail.NNTFullName + @"</p>");
            sbBodyHTML.Append(@"<hr class=""hr-line-solid width-100px"" />");
            sbBodyHTML.Append(@"</div>");

            sbBodyHTML.Append(@"<div class=""width-50 text-center"">");
            sbBodyHTML.Append(@"<p class=""text-transform-uppercase text-center"">cộng hòa xã hội chủ nghĩa việt nam</p>");
            sbBodyHTML.Append(@"<p>Độc lập - Tự do - Hạnh phúc</p>");
            sbBodyHTML.Append(@"<hr class=""hr-line-solid width-180px"" />");
            sbBodyHTML.Append(@"<p>" + mail.mp_ProvinceName + @", ngày " + day + @" tháng " + month + @" năm " + year + @"</p>");
            sbBodyHTML.Append(@"</div>");

            sbBodyHTML.Append(@"</div>");

            sbBodyHTML.Append(@"<div class=""width-100""><p class=""text-transform-uppercase text-center font-weight-bold padding-top-25"">hướng dẫn nhận hóa đơn điện tử/ hóa đơn xác thực</p></div>");

            sbBodyHTML.Append(@"<div class=""width-100"">");
            sbBodyHTML.Append(@"<p class=""font-weight-bold padding-top-25"">Kính gửi: Quý <span class=""text-transform-uppercase font-weight-normal"">" + mail.CustomerNNTName + @"</span></p>");
            sbBodyHTML.Append(@"<p class=""padding-top-15"">Quý khách vui lòng xem và tải hóa đơn <a class=""font-weight-bold color-blue"" href=""https://hoadontvan.com/tracuu/"">tại đây</a> với các thông tin chi tiết sau:</p>");
            sbBodyHTML.Append(@"<p>Mẫu số: <span class=""text-transform-uppercase font-weight-bold"">" + mail.FormNo + @"</span></p>");
            sbBodyHTML.Append(@"<p>Ký hiệu: <span class=""text-transform-uppercase font-weight-bold"">" + mail.Sign + @"</span></p>");
            sbBodyHTML.Append(@"<p>Số hóa đơn: <span class=""text-transform-uppercase font-weight-bold"">" + mail.InvoiceNo + @"</span></p>");
            sbBodyHTML.Append(@"<p>Ngày hóa đơn: <span class=""text-transform-uppercase font-weight-bold"">" + mail.InvoiceDateUTC + @"</span></p>");
            sbBodyHTML.Append(@"<p>Mã tra cứu hóa đơn: <span class=""text-transform-uppercase font-weight-bold"">" + mail.InvoiceCode + @"</span></p>");
            sbBodyHTML.Append(@"<p class=""padding-top-15"">Quý khách có thể chuyển đổi sang hóa đơn giấy (vui lòng thực hiện theo luật định).</p>");
            sbBodyHTML.Append(@"<p class=""padding-top-15"">Hóa đơn điện tử này là hợp pháp, chúng tôi sử dụng chữ ký số để xác thực và mẫu hóa đơn đã được đăng ký với Cơ quan Thuế.</p>");
            sbBodyHTML.Append(@"<p class=""padding-top-15"">Chúng tôi hỗ trợ 02 cách nhận hóa đơn:</p>");
            sbBodyHTML.Append(@"<ul class=""list-style-none"">");
            sbBodyHTML.Append(@"<li>1/. Nhận hóa đơn qua email này;</li>");
            sbBodyHTML.Append(@"<li>2/. Nhận hóa đơn từ website tra cứu hóa đơn online của chúng tôi tại địa chỉ <a href=""https://hoadontvan.com/tracuu/"">https://hoadontvan.com/tracuu/</a>;</li>");
            sbBodyHTML.Append(@"</ul>");
            sbBodyHTML.Append(@"<p class=""padding-top-15"">Thông tin người liên hệ về hóa đơn điện tử:</p>");
            sbBodyHTML.Append(@"<p class=""padding-top-25"">Tên công ty XHĐ: <span class=""text-transform-uppercase font-weight-bold"">" + mail.NNTFullName + @"</span></p>");
            sbBodyHTML.Append(@"<p>Người liên hệ: <span class=""text-transform-capitalize font-weight-bold"">" + mail.ContactName + @"</span></p>");
            sbBodyHTML.Append(@"<p>Số điện thoại: <span class=""font-weight-bold"">" + mail.ContactPhone + @"</span></p>");
            sbBodyHTML.Append(@"<p>Email: <span class=""font-weight-bold color-blue"">" + mail.ContactEmail + @"</span></p>");
            sbBodyHTML.Append(@"<p class=""padding-top-15"">Chúng tôi mong muốn được tiếp tục phục vụ và hợp tác cùng Quý khách hàng trong thời gian tới.</p>");

            sbBodyHTML.Append(@"</div>");

            sbBodyHTML.Append(@"<div class=""width-100"">");
            sbBodyHTML.Append(@"<hr class=""hr-line-solid"" />");
            sbBodyHTML.Append(@"<p class=""padding-top-15"">Dear Valued Customers,</p>");
            sbBodyHTML.Append(@"<p class=""padding-top-15"">Please kindly <a class=""font-weight-bold color-blue"" href=""https://hoadontvan.com/tracuu/"">click here</a> to view and download your e-invoice with details below:</p>");
            sbBodyHTML.Append(@"<p>Form: <span class=""text-transform-uppercase font-weight-bold"">" + mail.FormNo + @"</span></p>");
            sbBodyHTML.Append(@"<p>Serial: <span class=""text-transform-uppercase font-weight-bold"">" + mail.Sign + @"</span></p>");
            sbBodyHTML.Append(@"<p>Invoice number: <span class=""text-transform-uppercase font-weight-bold"">" + mail.InvoiceNo + @"</span></p>");
            sbBodyHTML.Append(@"<p>Invoice date: <span class=""text-transform-uppercase font-weight-bold"">" + mail.InvoiceDateUTC + @"</span></p>");
            sbBodyHTML.Append(@"<p>Download code: <span class=""text-transform-uppercase font-weight-bold"">" + mail.InvoiceCode + @"</span></p>");
            sbBodyHTML.Append(@"<p class=""padding-top-15"">You can convert to hard-copy (please follow statute).</p>");
            sbBodyHTML.Append(@"<p class=""padding-top-15"">This e-invoices is legal, we use a digital signature for authentication and an invoice sample already registered with the Tax Authority.</p>");
            sbBodyHTML.Append(@"<p class=""padding-top-15"">We provide 02 ways of invoice receipt:</p>");
            sbBodyHTML.Append(@"<ul class=""list-style-none"">");
            sbBodyHTML.Append(@"<li>1/. To receive via this email;</li>");
            sbBodyHTML.Append(@"<li>2/. Check and download your invoices at <a class=""font-weight-bold color-blue"" href=""https://hoadontvan.com/tracuu/"">https://hoadontvan.com/tracuu/</a>;</li>");
            sbBodyHTML.Append(@"</ul>");
            sbBodyHTML.Append(@"<p class=""padding-top-15"">Contact information for this invoice:</p>");
            sbBodyHTML.Append(@"<p class=""padding-top-25"">Invoice issued by: <span class=""text-transform-uppercase font-weight-bold"">" + mail.NNTFullName + @"</span></p>");
            sbBodyHTML.Append(@"<p>Contact: <span class=""text-transform-capitalize font-weight-bold"">" + mail.ContactName + @"</span></p>");
            sbBodyHTML.Append(@"<p>Phone: <span class=""font-weight-bold"">" + mail.ContactPhone + @"</span></p>");
            sbBodyHTML.Append(@"<p>Email: <span class=""font-weight-bold color-blue"">" + mail.ContactEmail + @"</span></p>");
            sbBodyHTML.Append(@"<p class=""padding-top-15"">We are very appreciative of your cooperation, and we look forward to continuing this relationship.</p>");
            sbBodyHTML.Append(@"</div>");

            sbBodyHTML.Append(@"</div>");
            sbBodyHTML.Append(@"</div>");
            sbBodyHTML.Append(@"</body>");
            sbBodyHTML.Append(@"</html>");
            return sbBodyHTML.ToString();
        }
        public static string GetContentMailIsRecall(SendMailUI mail, string deletedate)
        {
            //string deletedate = DateTime.Now.ToString("dd-MM-yyyy");
            var day = "";
            var year = "";
            var month = "";
            var ngayhd = CUtils.ConvertToDateTime(mail.InvoiceDateUTC);
            day = ngayhd.Day.ToString();
            month = ngayhd.Month.ToString();
            year = ngayhd.Year.ToString();
            var strTitle = @"Thông báo xác nhận về việc thu hồi/xóa hóa đơn điện tử <" + mail.InvoiceNo + @"> <" + mail.FormNo + @">";
            var sbBodyHTML = new StringBuilder();
            sbBodyHTML.Append(@"<html lang=""en"">");
            sbBodyHTML.Append(@"<head>");
            sbBodyHTML.Append(@"<meta name=""viewport"" content =""width=device-width"" />");
            sbBodyHTML.Append(@"<meta content=""text/html; charset=utf-8"" http-equiv=""Content-Type"">");
            sbBodyHTML.Append(@"<title>" + strTitle + @"</title>");
            sbBodyHTML.Append(@"
                        <style type=""text/css"">
                             html{ background: #f2f2f2;}
                            .body { width: 100%; line-height: 1.5;}
                            .div-main { overflow-x: auto; background: #ffffff; display: table;  margin: 0 auto; width: 770px; padding: 0 25px; }
                            .text-center { text-align: center; }
                            .text-transform-uppercase { text-transform: uppercase; }
                            .text-transform-capitalize { text-transform: capitalize; }
                            .width-100 { float: left; width: 100%; }
                            .width-50 { float: left; width: 50%; }
                            .width-180px { width: 180px; }
                            .width-100px { width: 100px; }
                            p { margin: 5px 0 5px; }
                            .color-blue{ color: blue;}
                            .hr-line-solid { border-top: 1px solid #333333; }
                            .font-weight-bold { font-weight: bold; }
                            .font-weight-normal { font-weight: normal; }
                            .padding-top-25{ padding-top: 25px; }
                            .padding-top-15 { padding-top: 15px; }
                            .padding-left-right-15 { padding-left: 15px; padding-right: 15px; }
                            ul{ padding-left: 5px; margin-top: 0px;}
                            a{ text-decoration: none; }
                            .list-style-none{ list-style: none;}
                            .border-test { border: 1px solid red; }
                        </style>
                    ");
            sbBodyHTML.Append(@"</head>");
            sbBodyHTML.Append(@"<body>");
            sbBodyHTML.Append(@"<div class=""div-main"">");
            sbBodyHTML.Append(@"<div class=""padding-left-right-15"">");
            sbBodyHTML.Append(@"<div class=""width-100"">");

            sbBodyHTML.Append(@"<div class=""width-50 text-transform-uppercase text-center"">");
            sbBodyHTML.Append(@"<p>đơn vị sử dụng dịch vụ Inbrand Clound</p>");
            sbBodyHTML.Append(@"<p>" + mail.NNTFullName + @"</p>");
            sbBodyHTML.Append(@"<hr class=""hr-line-solid width-100px"" />");
            sbBodyHTML.Append(@"</div>");

            sbBodyHTML.Append(@"<div class=""width-50 text-center"">");
            sbBodyHTML.Append(@"<p class=""text-transform-uppercase text-center"">cộng hòa xã hội chủ nghĩa việt nam</p>");
            sbBodyHTML.Append(@"<p>Độc lập - Tự do - Hạnh phúc</p>");
            sbBodyHTML.Append(@"<hr class=""hr-line-solid width-180px"" />");
            sbBodyHTML.Append(@"<p>" + @"Ngày " + day + @" tháng " + month + @" năm " + year + @"</p>");
            sbBodyHTML.Append(@"</div>");

            sbBodyHTML.Append(@"</div>");

            sbBodyHTML.Append(@"<div class=""width-100""><p class=""text-transform-uppercase text-center font-weight-bold padding-top-25"">thông báo về việc thu hồi/xóa hóa đơn điện tử</p></div>");

            sbBodyHTML.Append(@"<div class=""width-100"">");
            sbBodyHTML.Append(@"<p class=""font-weight-bold padding-top-25"">Kính gửi: Quý <span class=""text-transform-uppercase font-weight-normal"">" + mail.CustomerNNTName + @"</span></p>");
            sbBodyHTML.Append(@"<p class=""padding-top-15"">Hóa đơn của quý khách đã được thu hồi/xóa bỏ:</p>");
            sbBodyHTML.Append(@"<p>Mẫu số: <span class=""text-transform-uppercase font-weight-bold"">" + mail.FormNo + @"</span></p>");
            sbBodyHTML.Append(@"<p>Ký hiệu: <span class=""text-transform-uppercase font-weight-bold"">" + mail.Sign + @"</span></p>");
            sbBodyHTML.Append(@"<p>Số hóa đơn: <span class=""text-transform-uppercase font-weight-bold"">" + mail.InvoiceNo + @"</span></p>");
            sbBodyHTML.Append(@"<p>Ngày hóa đơn: <span class=""text-transform-uppercase font-weight-bold"">" + (mail.InvoiceDateUTC == null ? "" : Convert.ToDateTime(mail.InvoiceDateUTC).ToString("dd-MM-yyyy")) + @"</span></p>");
            sbBodyHTML.Append(@"<p>Mã tra cứu hóa đơn: <span class=""text-transform-uppercase font-weight-bold"">" + mail.InvoiceCode + @"</span></p>");
            sbBodyHTML.Append(@"<p>Ngày xóa: <span class=""text-transform-uppercase font-weight-bold"">" + deletedate + @"</span></p>");
            sbBodyHTML.Append(@"<p class=""padding-top-15"" > Quý khách có thể tra cứu hóa đơn tại: <a class=""font-weight-bold color-blue"" href =""https://hoadontvan.com/tracuu/"" > https://hoadontvan.com/TraCuu/</a></p>");

            sbBodyHTML.Append(@"<p class=""padding-top-15"">Thông tin người liên hệ về hóa đơn điện tử:</p>");
            sbBodyHTML.Append(@"<p class=""padding-top-25"">Tên công ty XHĐ: <span class=""text-transform-uppercase font-weight-bold"">" + mail.NNTFullName + @"</span></p>");
            sbBodyHTML.Append(@"<p>Người liên hệ: <span class=""text-transform-capitalize font-weight-bold"">" + mail.ContactName + @"</span></p>");
            sbBodyHTML.Append(@"<p>Số điện thoại: <span class=""font-weight-bold"">" + mail.ContactPhone + @"</span></p>");
            sbBodyHTML.Append(@"<p>Email: <span class=""font-weight-bold color-blue"">" + mail.ContactEmail + @"</span></p>");
            sbBodyHTML.Append(@"<p class=""padding-top-15"">Chúng tôi mong muốn được tiếp tục phục vụ và hợp tác cùng Quý khách hàng trong thời gian tới.</p>");

            sbBodyHTML.Append(@"</div>");

            sbBodyHTML.Append(@"<div class=""width-100"">");
            sbBodyHTML.Append(@"<hr class=""hr-line-solid"" />");
            sbBodyHTML.Append(@"<p class=""padding-top-15"">Dear Valued Customers,</p>");
            sbBodyHTML.Append(@"<p class=""padding-top-15"">Your e-invoice has evicted/deleted</p>");
            sbBodyHTML.Append(@"<p>Form: <span class=""text-transform-uppercase font-weight-bold"">" + mail.FormNo + @"</span></p>");
            sbBodyHTML.Append(@"<p>Serial: <span class=""text-transform-uppercase font-weight-bold"">" + mail.Sign + @"</span></p>");
            sbBodyHTML.Append(@"<p>Invoice number: <span class=""text-transform-uppercase font-weight-bold"">" + mail.InvoiceNo + @"</span></p>");
            sbBodyHTML.Append(@"<p>Invoice date: <span class=""text-transform-uppercase font-weight-bold"">" + (mail.InvoiceDateUTC == null ? "" : Convert.ToDateTime(mail.InvoiceDateUTC).ToString("dd-MM-yyyy")) + @"</span></p>");
            sbBodyHTML.Append(@"<p>Download code: <span class=""text-transform-uppercase font-weight-bold"">" + mail.InvoiceCode + @"</span></p>");
            sbBodyHTML.Append(@"<p>Deleted date: <span class=""text-transform-uppercase font-weight-bold"">" + deletedate + @"</span></p>");
            sbBodyHTML.Append(@"<p class=""padding-top-15"">You can look up your e-invoice at: <a class=""font-weight-bold color-blue"" href=""https://hoadontvan.com/tracuu/"">https://hoadontvan.com/TraCuu</a></p>");

            sbBodyHTML.Append(@"<p class=""padding-top-15"">Contact information for this invoice:</p>");
            sbBodyHTML.Append(@"<p class=""padding-top-25"">Invoice issued by: <span class=""text-transform-uppercase font-weight-bold"">" + mail.NNTFullName + @"</span></p>");
            sbBodyHTML.Append(@"<p>Contact: <span class=""text-transform-capitalize font-weight-bold"">" + mail.ContactName + @"</span></p>");
            sbBodyHTML.Append(@"<p>Phone: <span class=""font-weight-bold"">" + mail.ContactPhone + @"</span></p>");
            sbBodyHTML.Append(@"<p>Email: <span class=""font-weight-bold color-blue"">" + mail.ContactEmail + @"</span></p>");
            sbBodyHTML.Append(@"<p class=""padding-top-15"">We are very appreciative of your cooperation, and we look forward to continuing this relationship.</p>");
            sbBodyHTML.Append(@"</div>");

            sbBodyHTML.Append(@"</div>");
            sbBodyHTML.Append(@"</div>");
            sbBodyHTML.Append(@"</body>");
            sbBodyHTML.Append(@"</html>");
            return sbBodyHTML.ToString();
        }
        public static string GetContentMailIsDelete(SendMailUI mail, string deletedate)
        {
            //string deletedate = DateTime.Now.ToString("dd-MM-yyyy");
            var day = "";
            var year = "";
            var month = "";
            var ngayhd = CUtils.ConvertToDateTime(mail.InvoiceDateUTC);
            day = ngayhd.Day.ToString();
            month = ngayhd.Month.ToString();
            year = ngayhd.Year.ToString();
            var strTitle = @"Thông báo xác nhận về việc xóa hóa đơn điện tử <" + mail.InvoiceNo + @"> <" + mail.FormNo + @"> và tạo hóa đơn thay thế";
            var sbBodyHTML = new StringBuilder();
            sbBodyHTML.Append(@"<html lang=""en"">");
            sbBodyHTML.Append(@"<head>");
            sbBodyHTML.Append(@"<meta name=""viewport"" content =""width=device-width"" />");
            sbBodyHTML.Append(@"<meta content=""text/html; charset=utf-8"" http-equiv=""Content-Type"">");
            sbBodyHTML.Append(@"<title>" + strTitle + @"</title>");
            sbBodyHTML.Append(@"
                        <style type=""text/css"">
                            html{ background: #f2f2f2;}
                            .body { width: 100%; line-height: 1.5;}
                            .div-main { overflow-x: auto; background: #ffffff; display: table;  margin: 0 auto; width: 770px; padding: 0 25px; }
                            .text-center { text-align: center; }
                            .text-transform-uppercase { text-transform: uppercase; }
                            .text-transform-capitalize { text-transform: capitalize; }
                            .width-100 { float: left; width: 100%; }
                            .width-50 { float: left; width: 50%; }
                            .width-180px { width: 180px; }
                            .width-100px { width: 100px; }
                            p { margin: 5px 0 5px; }
                            .color-blue{ color: blue;}
                            .hr-line-solid { border-top: 1px solid #333333; }
                            .font-weight-bold { font-weight: bold; }
                            .font-weight-normal { font-weight: normal; }
                            .padding-top-25{ padding-top: 25px; }
                            .padding-top-15 { padding-top: 15px; }
                            .padding-left-right-15 { padding-left: 15px; padding-right: 15px; }
                            ul{ padding-left: 5px; margin-top: 0px;}
                            a{ text-decoration: none; }
                            .list-style-none{ list-style: none;}
                            .border-test { border: 1px solid red; }
                        </style>
                    ");
            sbBodyHTML.Append(@"</head>");
            sbBodyHTML.Append(@"<body>");
            sbBodyHTML.Append(@"<div class=""div-main"">");
            sbBodyHTML.Append(@"<div class=""padding-left-right-15"">");
            sbBodyHTML.Append(@"<div class=""width-100"">");

            sbBodyHTML.Append(@"<div class=""width-50 text-transform-uppercase text-center"">");
            sbBodyHTML.Append(@"<p>đơn vị sử dụng dịch vụ Inbrand Clound</p>");
            sbBodyHTML.Append(@"<p>" + mail.NNTFullName + @"</p>");
            sbBodyHTML.Append(@"<hr class=""hr-line-solid width-100px"" />");
            sbBodyHTML.Append(@"</div>");

            sbBodyHTML.Append(@"<div class=""width-50 text-center"">");
            sbBodyHTML.Append(@"<p class=""text-transform-uppercase text-center"">cộng hòa xã hội chủ nghĩa việt nam</p>");
            sbBodyHTML.Append(@"<p>Độc lập - Tự do - Hạnh phúc</p>");
            sbBodyHTML.Append(@"<hr class=""hr-line-solid width-180px"" />");
            sbBodyHTML.Append(@"<p>" + @"Ngày " + day + @" tháng " + month + @" năm " + year + @"</p>");
            sbBodyHTML.Append(@"</div>");

            sbBodyHTML.Append(@"</div>");

            sbBodyHTML.Append(@"<div class=""width-100""><p class=""text-transform-uppercase text-center font-weight-bold padding-top-25"">thông báo về việc xóa hóa đơn điện tử</p></div>");

            sbBodyHTML.Append(@"<div class=""width-100"">");
            sbBodyHTML.Append(@"<p class=""font-weight-bold padding-top-25"">Kính gửi: Quý <span class=""text-transform-uppercase font-weight-normal"">" + mail.CustomerNNTName + @"</span></p>");
            sbBodyHTML.Append(@"<p class=""padding-top-15"">Hóa đơn của quý khách đã được xóa bỏ:</p>");
            sbBodyHTML.Append(@"<p>Mẫu số: <span class=""text-transform-uppercase font-weight-bold"">" + mail.FormNo + @"</span></p>");
            sbBodyHTML.Append(@"<p>Ký hiệu: <span class=""text-transform-uppercase font-weight-bold"">" + mail.Sign + @"</span></p>");
            sbBodyHTML.Append(@"<p>Số hóa đơn: <span class=""text-transform-uppercase font-weight-bold"">" + mail.InvoiceNo + @"</span></p>");
            sbBodyHTML.Append(@"<p>Ngày hóa đơn: <span class=""text-transform-uppercase font-weight-bold"">" + mail.InvoiceDateUTC + @"</span></p>");
            sbBodyHTML.Append(@"<p>Mã tra cứu hóa đơn: <span class=""text-transform-uppercase font-weight-bold"">" + mail.InvoiceCode + @"</span></p>");
            sbBodyHTML.Append(@"<p>Ngày xóa: <span class=""text-transform-uppercase font-weight-bold"">" + deletedate + @"</span></p>");
            sbBodyHTML.Append(@"<p class=""padding-top-15"" > Quý khách có thể tra cứu hóa đơn tại: <a class=""font-weight-bold color-blue"" href =""https://hoadontvan.com/tracuu/"" > https://hoadontvan.com/TraCuu/</a></p>");
            
            sbBodyHTML.Append(@"<p class=""padding-top-15"">Thông tin người liên hệ về hóa đơn điện tử:</p>");
            sbBodyHTML.Append(@"<p class=""padding-top-25"">Tên công ty XHĐ: <span class=""text-transform-uppercase font-weight-bold"">" + mail.NNTFullName + @"</span></p>");
            sbBodyHTML.Append(@"<p>Người liên hệ: <span class=""text-transform-capitalize font-weight-bold"">" + mail.ContactName + @"</span></p>");
            sbBodyHTML.Append(@"<p>Số điện thoại: <span class=""font-weight-bold"">" + mail.ContactPhone + @"</span></p>");
            sbBodyHTML.Append(@"<p>Email: <span class=""font-weight-bold color-blue"">" + mail.ContactEmail + @"</span></p>");
            sbBodyHTML.Append(@"<p class=""padding-top-15"">Chúng tôi mong muốn được tiếp tục phục vụ và hợp tác cùng Quý khách hàng trong thời gian tới.</p>");

            sbBodyHTML.Append(@"</div>");

            sbBodyHTML.Append(@"<div class=""width-100"">");
            sbBodyHTML.Append(@"<hr class=""hr-line-solid"" />");
            sbBodyHTML.Append(@"<p class=""padding-top-15"">Dear Valued Customers,</p>");
            sbBodyHTML.Append(@"<p class=""padding-top-15"">Your e-invoice has deleted</p>");
            sbBodyHTML.Append(@"<p>Form: <span class=""text-transform-uppercase font-weight-bold"">" + mail.FormNo + @"</span></p>");
            sbBodyHTML.Append(@"<p>Serial: <span class=""text-transform-uppercase font-weight-bold"">" + mail.Sign + @"</span></p>");
            sbBodyHTML.Append(@"<p>Invoice number: <span class=""text-transform-uppercase font-weight-bold"">" + mail.InvoiceNo + @"</span></p>");
            sbBodyHTML.Append(@"<p>Invoice date: <span class=""text-transform-uppercase font-weight-bold"">" + mail.InvoiceDateUTC + @"</span></p>");
            sbBodyHTML.Append(@"<p>Download code: <span class=""text-transform-uppercase font-weight-bold"">" + mail.InvoiceCode + @"</span></p>");
            sbBodyHTML.Append(@"<p>Deleted date: <span class=""text-transform-uppercase font-weight-bold"">" + deletedate + @"</span></p>");
            sbBodyHTML.Append(@"<p class=""padding-top-15"">You can look up your e-invoice at: <a class=""font-weight-bold color-blue"" href=""https://hoadontvan.com/tracuu/"">https://hoadontvan.com/TraCuu</a></p>");

            sbBodyHTML.Append(@"<p class=""padding-top-15"">Contact information for this invoice:</p>");
            sbBodyHTML.Append(@"<p class=""padding-top-25"">Invoice issued by: <span class=""text-transform-uppercase font-weight-bold"">" + mail.NNTFullName + @"</span></p>");
            sbBodyHTML.Append(@"<p>Contact: <span class=""text-transform-capitalize font-weight-bold"">" + mail.ContactName + @"</span></p>");
            sbBodyHTML.Append(@"<p>Phone: <span class=""font-weight-bold"">" + mail.ContactPhone + @"</span></p>");
            sbBodyHTML.Append(@"<p>Email: <span class=""font-weight-bold color-blue"">" + mail.ContactEmail + @"</span></p>");
            sbBodyHTML.Append(@"<p class=""padding-top-15"">We are very appreciative of your cooperation, and we look forward to continuing this relationship.</p>");
            sbBodyHTML.Append(@"</div>");

            sbBodyHTML.Append(@"</div>");
            sbBodyHTML.Append(@"</div>");
            sbBodyHTML.Append(@"</body>");
            sbBodyHTML.Append(@"</html>");
            return sbBodyHTML.ToString();
        }

        public static string GetContentMailRegister(Mst_NNT data)
        {
            var strTitle = @"idocNet thông báo tài khoản đăng nhập và xác nhận đơn đặt hàng phần mềm Inbrand Clound của Quý khách.";
            var sbBodyHTML = new StringBuilder();
            sbBodyHTML.Append(@"<html lang=""en"">");
            sbBodyHTML.Append(@"<head>");
            sbBodyHTML.Append(@"<meta name=""viewport"" content =""width=device-width"" />");
            sbBodyHTML.Append(@"<meta content=""text/html; charset=utf-8"" http-equiv=""Content-Type"">");
            sbBodyHTML.Append(@"<title>" + strTitle + @"</title>");
            sbBodyHTML.Append(@"
                        <style type=""text/css"">
                            html{ background: #ffffff;}
                            .body { width: 100%; line-height: 1.5;}
                            .div-main { overflow-x: auto; background: #f2f2f2; display: table;  margin: 0 auto; width: 770px; color: #424242;}
                            .text-center { text-align: center!important; }
                            .text-right { text-align: right!important; }
                            .text-left { text-align: left!important; }
                            .text-transform-uppercase { text-transform: uppercase; }
                            .text-transform-capitalize { text-transform: capitalize; }
                            .width-100 { float: left; width: 100%; }
                            .width-50 { float: left; width: 50%; }
                            .width-30px { width: 30px; }
                            .width-180px { width: 180px; }
                            .width-100px { width: 100px; }
                            .width-60px { width: 60px; }
                            .width-120px { width: 120px; }
                            p { margin: 5px 0 5px; }
                            .color-blue { color: blue;}
                            .color-white { color: white;}
                            .color-red { color: red;}
                            .color-black { color: black;}
                            .hr-line-solid { border-top: 1px solid #333333; }
                            .font-weight-bold { font-weight: bold; }
                            .font-size-25 { font-size: 25px; }
                            .font-weight-normal { font-weight: normal; }
                            .font-italic { font-style: italic; }
                            .padding-top-25 { padding-top: 25px; }
                            .padding-top-15 { padding-top: 15px; }
                            .padding-top-10 { padding-top: 10px; }
                            .padding-bottom-5 { padding-bottom: 5px; }
                            .padding-top-bottom-15 { padding-top: 15px; padding-bottom: 15px; }
                            .margin-top-bottom-15 { margin-top: 15px; margin-bottom: 15px; }
                            .padding-left-right-15 { padding-left: 15px; padding-right: 15px; }
                            .padding-left-right-25 { padding-left: 25px; padding-right: 25px; }
                            .padding-left-right-40 { padding-left: 40px; padding-right: 40px; }
                            ul{ padding-left: 5px; margin-top: 0px;}
                            a{ text-decoration: none; }
                            .list-style-none{ list-style: none;}
                            .border-test { border: 1px solid red; }
                            .img-logo{ width: 244px; height: 81px}
                            .background{ background: #1f89d7; height: 80px; text-align: justify;}
                            .clear-fix{ clear: both;}
                            .div-border-radius { border: 1px solid #cacaca; border-radius: 5px; -moz-border-radius: 5px; -webkit-border-radius: 5px; -ms-border-radius: 5px; -o-border-radius: 5px; margin-top: 15px;}
                            .div-li1{ min-height: 165px; background: #ffffff;}
                            .div-li3{ min-height: 215px; background: #ffffff;}
                            .div-li4{ height: 50px; background: #ffffff;}
                            .courses-table{font-size: 14px; padding: 3px; border-collapse: collapse; border-spacing: 0;}
                            .courses-table .description{color: #505050;}
                            .courses-table td{border: 1px solid #cacaca; padding: 5px 10px; height: 25px; word-wrap: break-word;}
                            .courses-table th{border: 1px solid #cacaca; color: #FFFFFF; background-color: #0b78c8; text-align: left; padding: 0 10px; height: 40px; word-wrap: break-word;}
                            .courses-table tfoot td{border: 1px solid #cacaca; padding: 5px 10px; height: 25px; word-wrap: break-word; background: #ffffff;}
                            tr:nth-child(odd){background-color: #f2f2f2}
                            tr:nth-child(even){ background-color: #fbfbfb;}
                            .span-text-150px{display: inline-block; width: 150px; float: left;}
                        </style>
                    ");
            sbBodyHTML.Append(@"</head>");
            sbBodyHTML.Append(@"<body>");
            sbBodyHTML.Append(@"<div class=""div-main"">");
            var logoQIonvoice = "";
            if (!CUtils.IsNullOrEmpty(System.Web.HttpContext.Current.Session["LogoQInvoice"]))
            {
                logoQIonvoice = System.Web.HttpContext.Current.Session["LogoQInvoice"].ToString();
            }
            sbBodyHTML.Append(@"<div class=""padding-left-right-25"">");
            sbBodyHTML.Append(@"<div class=""width-100 padding-left-right-15"">");
            sbBodyHTML.Append(@"<img class=""img-logo"" src='" +  logoQIonvoice + "' />");
            sbBodyHTML.Append(@"</div>");
            sbBodyHTML.Append(@"</div>");

            sbBodyHTML.Append(@"<div class=""width-100 background"">");
            sbBodyHTML.Append(@"<p class=""padding-left-right-40 padding-top-10 padding-bottom-5 font-size-25 color-white""> idocNet thông báo tài khoản đăng nhập và xác nhận đơn đặt hàng phần mềm Inbrand Clound của Quý khách. </p>");
            sbBodyHTML.Append(@"</div>");

            sbBodyHTML.Append(@"<div class=""padding-left-right-25"">");
            sbBodyHTML.Append(@"<p class=""clear-fix padding-top-bottom-15 font-italic color-red"">Đây là email được gửi tự động từ hệ thống, vui lòng không phản hồi (reply) lại email này.</p>");
            sbBodyHTML.Append(@"<p class=""font-weight-bold"">Kính gửi: " + data.NNTFullName +"</p>");
            sbBodyHTML.Append(@"<p class=""padding-top-15"">Công ty Cổ phần Đầu tư và Công nghệ idocNet xin chân thành cảm ơn Quý khách đã tin tưởng, lựa chọn sử dụng phần mềm Hóa đơn điện tử Inbrand Clound.</p>");
            sbBodyHTML.Append(@"<p class=""padding-top-15"">idocNet xin thông báo tài khoản đăng nhập sử dụng dịch vụ hóa đơn điện tử và thông tin đơn đặt hàng của Quý khách như sau:</p>");
            sbBodyHTML.Append(@"<ul class=""list-style-none"">");

            sbBodyHTML.Append(@"<li>");
            sbBodyHTML.Append(@"<span class=""font-weight-bold"">1. Thông tin tài khoản kết nối với dịch vụ hóa đơn điện tử:</span>");
            sbBodyHTML.Append(@"<div class=""div-li1 div-border-radius padding-left-right-25"">");
            sbBodyHTML.Append(@"<p class=""padding-top-10""><span class=""span-text-150px""> Mã số thuế:</span> <span class=""font-weight-bold color-black"">" + data.MST + "</span></p>");
            sbBodyHTML.Append(@"<p class=""padding-top-10""><span class=""span-text-150px""> Tên đăng nhập:</span> <span class=""font-weight-bold color-black"">" + data.ContactEmail + "</span></p>");
            sbBodyHTML.Append(@"<p class=""padding-top-10""><span class=""span-text-150px""> Mật khẩu:</span> <span class=""font-weight-bold color-black"">Là mật khẩu đã khai báo khi mua phần mềm</span></p>");
            sbBodyHTML.Append(@"<p class=""padding-top-10"">Tài khoản này dùng để sử dụng dịch vụ hóa đơn điện tử Inbrand Clound tại địa chỉ <a class=""font-weight-bold color-blue"" href=""http://demo.qinvoice.vn:12071"">http://demo.qinvoice.vn:12071</a>.</p>");
            sbBodyHTML.Append(@"</div>");
            sbBodyHTML.Append(@"</li>");

            //sbBodyHTML.Append(@"<li class=""padding-top-15 clear-fix"">");
            //sbBodyHTML.Append(@"<span class=""font-weight-bold"">2. Thông tin đơn đặt hàng:</span>");
            ////sbBodyHTML.Append(@"<p class=""padding-left-right-25 font-weight-bold color-black"">Số đơn hàng: 0000000616</p>");
            //sbBodyHTML.Append(@"<div class=""width-100 padding-top-15"">");
            //sbBodyHTML.Append(@"<table class=""courses-table"">");
            //sbBodyHTML.Append(@"<thead>");
            //sbBodyHTML.Append(@"<tr>");
            //sbBodyHTML.Append(@"<th class=""width-30px text-center"">STT</th>");
            //sbBodyHTML.Append(@"<th class=""width-180px text-center"">Gói sản phẩm</th>");
            //sbBodyHTML.Append(@"<th class=""width-120px text-center"">Đơn vị tính</th>");
            //sbBodyHTML.Append(@"<th class=""width-60px text-center"">Số lượng</th>");
            //sbBodyHTML.Append(@"<th class=""width-100px text-center"">Đơn giá (VNĐ)</th>");
            //sbBodyHTML.Append(@"<th class=""width-120px text-center"">Thành tiền (VNĐ)</th>");
            //sbBodyHTML.Append(@"</tr>");
            //sbBodyHTML.Append(@"</thead>");
            //sbBodyHTML.Append(@"<tbody>");
            //sbBodyHTML.Append(@"<tr>");
            //sbBodyHTML.Append(@"<td class=""text-right"">1</td>");
            //sbBodyHTML.Append(@"<td class=""text-left"">Phí thuê bao hàng năm</td>");
            //sbBodyHTML.Append(@"<td class=""text-center"">Năm</td>");
            //sbBodyHTML.Append(@"<td class=""text-right"">1</td>");
            //sbBodyHTML.Append(@"<td class=""text-center""><span style=""font-weight: 600; color: #0c82ff"">MIỄN PHÍ</span></td>");
            //sbBodyHTML.Append(@"<td class=""text-right""><span style=""font-weight: 600;"">0</span></td>");
            //sbBodyHTML.Append(@"</tr>");

            //sbBodyHTML.Append(@"<tr>");
            //sbBodyHTML.Append(@"<td class=""text-right"">2</td>");
            //sbBodyHTML.Append(@"<td class=""text-left"">Phí sử dụng hóa đơn</td>");
            //sbBodyHTML.Append(@"<td class=""text-center"">Gói (60 hóa đơn)</td>");
            //sbBodyHTML.Append(@"<td class=""text-right"">1</td>");
            //sbBodyHTML.Append(@"<td class=""text-center""><span style=""font-weight: 600; color: #0c82ff"">MIỄN PHÍ</span></td>");
            //sbBodyHTML.Append(@"<td class=""text-right""><span style=""font-weight: 600;"">0</span></td>");
            //sbBodyHTML.Append(@"</tr>");
            //sbBodyHTML.Append(@"</tbody>");
            //sbBodyHTML.Append(@"<tfoot>");
            //sbBodyHTML.Append(@"<tr>");
            //sbBodyHTML.Append(@"<td colspan=""5"" class=""font-weight-bold text-right"">Tổng</td>");
            //sbBodyHTML.Append(@"<td class=""font-weight-bold text-right""> 0 VNĐ</td>");
            //sbBodyHTML.Append(@"</tr>");
            //sbBodyHTML.Append(@"</tfoot>");
            //sbBodyHTML.Append(@"</table>");
            //sbBodyHTML.Append(@"</div>");
            //sbBodyHTML.Append(@"</li>");

            sbBodyHTML.Append(@"<li class=""padding-top-15 clear-fix"">");
            sbBodyHTML.Append(@"<span class=""font-weight-bold"">2. Thông tin idocNet xuất hóa đơn cho Quý khách:</span>");
            sbBodyHTML.Append(@"<div class=""div-li3 div-border-radius padding-left-right-25"">");
            sbBodyHTML.Append(@"<p class=""padding-top-10""><span class=""span-text-150px""> Tên công ty:</span> <span class=""font-weight-bold color-black"">" + data.NNTFullName +"</span></p>");
            sbBodyHTML.Append(@"<p class=""padding-top-10""><span class=""span-text-150px""> Địa chỉ:</span> <span class=""font-weight-bold color-black"">" + data.NNTAddress + "</span></p>");
            sbBodyHTML.Append(@"<p class=""padding-top-10""><span class=""span-text-150px""> Mã số thuế:</span> <span class=""font-weight-bold color-black"">" + data.MST + "</span></p>");
            sbBodyHTML.Append(@"<p class=""padding-top-10""><span class=""span-text-150px""> Người mua hàng:</span> <span class=""font-weight-bold color-black"">" + data.ContactName + "</span></p>");
            sbBodyHTML.Append(@"<p class=""padding-top-10""><span class=""span-text-150px""> Email:</span> <span class=""font-weight-bold color-black"">" + data.ContactEmail + "</span></p>");
            sbBodyHTML.Append(@"<p class=""padding-top-10""><span class=""span-text-150px""> Số điện thoại:</span> <span class=""font-weight-bold color-black"">" + data.ContactPhone + "</span></p>");
            sbBodyHTML.Append(@"</div>");
            sbBodyHTML.Append(@"</li>");

            sbBodyHTML.Append(@"<li class=""padding-top-15 clear-fix"">");
            sbBodyHTML.Append(@"<span class=""font-weight-bold"">ĐƠN VỊ CUNG CẤP DỊCH VỤ HÓA ĐƠN ĐIỆN TỬ INBRAND CLOUND:</span>");
            sbBodyHTML.Append(@"<div class=""div-li3 div-border-radius padding-left-right-25"" style=""min-height: 150px;"">");
            sbBodyHTML.Append(@"<p class=""padding-top-10""><span class=""font-weight-bold color-black"">Công ty Cổ phần Đầu tư và Công nghệ idocNet</span></p>");
            sbBodyHTML.Append(@"<p class=""padding-top-10""><span class=""span-text-150px""> Email:</span> <span class=""font-weight-bold color-black"">support@idocnet.com / support@qinvoice.vn</span></p>");
            sbBodyHTML.Append(@"<p class=""padding-top-10""><span class=""span-text-150px""> Hotline:</span> <span class=""font-weight-bold color-black"">0946 23 92 92</span></p>");
            sbBodyHTML.Append(@"<p class=""padding-top-10""><span class=""span-text-150px""> Website:</span> <span class=""font-weight-bold color-black""> <a href=""https://idocnet.com/"">www.idocnet.com</a></span></p>");
            sbBodyHTML.Append(@"</div>");
            sbBodyHTML.Append(@"</li>");

            sbBodyHTML.Append(@"<li class=""padding-top-15 clear-fix"">");
            sbBodyHTML.Append(@"<p class=""padding-top-bottom-15"">Chúng tôi sẽ liên hệ với quý khách trong thời gian sớm nhất.</p>");
            sbBodyHTML.Append(@"</li>");

            sbBodyHTML.Append(@"</ul>");

            sbBodyHTML.Append(@"<p class=""clear-fix padding-top-15"">Trân trọng kính chào!</p>");
            sbBodyHTML.Append(@"<p class=""font-weight-bold color-black"">Inbrand Clound</p>");
            //sbBodyHTML.Append(@"<p class=""font-weight-bold color-black"">Công ty Cổ phần Đầu Tư và Công Nghệ idocNet</p>");
            sbBodyHTML.Append(@"</div>");



            sbBodyHTML.Append(@"</div>");
            sbBodyHTML.Append(@"</body>");
            sbBodyHTML.Append(@"</html>");
            return sbBodyHTML.ToString();
        }

        public static string GetContentMailRestPassword(string email)
        {
            var strTitle = @"idocNet thông báo yêu cầu xác nhận đổi mật khẩu của Quý khách.";
            var sbBodyHTML = new StringBuilder();
            sbBodyHTML.Append(@"<html lang=""en"">");
            sbBodyHTML.Append(@"<head>");
            sbBodyHTML.Append(@"<meta name=""viewport"" content =""width=device-width"" />");
            sbBodyHTML.Append(@"<meta content=""text/html; charset=utf-8"" http-equiv=""Content-Type"">");
            sbBodyHTML.Append(@"<title>" + strTitle + @"</title>");
            sbBodyHTML.Append(@"
                        <style type=""text/css"">
                            html{ background: #ffffff;}
                            .body { width: 100%; line-height: 1.5;}
                            .div-main { overflow-x: auto; background: #f2f2f2; display: table;  margin: 0 auto; width: 770px; color: #424242;}
                            .text-left { text-align: left!important; }
                            .width-100 { float: left; width: 100%; }
                            p { margin: 5px 0 5px; }
                            .color-black { color: black;}
                            .font-weight-bold { font-weight: bold; }
                            .padding-top-5 { padding-top: 5px; }
                            .padding-top-15 { padding-top: 15px; }
                            .padding-left-right-25 { padding-left: 25px; padding-right: 25px; }
                            a{ text-decoration: none; }
                        </style>
                    ");
            sbBodyHTML.Append(@"</head>");
            sbBodyHTML.Append(@"<body>");
            sbBodyHTML.Append(@"<div class=""div-main"">");
            sbBodyHTML.Append(@"<div class=""width-100 background"">");
            sbBodyHTML.Append(@"<div class=""padding-left-right-25"">");
            //sbBodyHTML.Append(@"<p>Xin chào " + email +",</p>");
            sbBodyHTML.Append(@"<p>Xin chào,</p>");
            sbBodyHTML.Append(@"<p class=""padding-top-5"">Bạn vừa gửi yêu cầu thiết lập lại mật khẩu cho tài khoản iNOS.</p>");
            sbBodyHTML.Append(@"<p class=""padding-top-5"">Vui lòng nhấn vào đường link phía dưới để thiết lập lại mật khẩu:</p>");
            sbBodyHTML.Append(@"<p class=""padding-top-5""><a href=""http://test.qinvoice.vn:12071/reset-password/{code}"">http://test.qinvoice.vn:12071/reset-password/{code}</a></p>");
            sbBodyHTML.Append(@"<p class=""padding-top-5""> Nếu không phải bạn là người gửi yêu cầu thiết lập lại mật khẩu thì tài khoản của bạn có thể đang bị xâm hại. Xin theo cách sau:</p>");
            sbBodyHTML.Append(@"<p class=""padding-top-5""> 1. Gửi yêu cầu đặt lại mật khẩu của bạn bằng lựa chọn Quên mật khẩu. </p>");
            sbBodyHTML.Append(@"<p class=""padding-top-5""> 2. Xem xét thông tin bảo mật của bạn. </p>");
            sbBodyHTML.Append(@"<p class=""padding-top-5""> 3. Tìm hiểu cách tăng cường bảo mật cho tài khoản của bạn. </p>");
            sbBodyHTML.Append(@"<p class=""padding-top-5""> Xin cảm ơn. </p>");
            sbBodyHTML.Append(@"<p class=""padding-top-5"">Inbrand Clound Team</p>");
            sbBodyHTML.Append(@"</div>");
            sbBodyHTML.Append(@"</div>");
            sbBodyHTML.Append(@"</div>");
            sbBodyHTML.Append(@"</body>");
            sbBodyHTML.Append(@"</html>");
            return sbBodyHTML.ToString();
        }

        public static string GetContentMailVerificationEmail(string email)
        {
            var strTitle = @"idocNet thông báo yêu cầu xác thực địa chỉ email.";
            var sbBodyHTML = new StringBuilder();
            sbBodyHTML.Append(@"<html lang=""en"">");
            sbBodyHTML.Append(@"<head>");
            sbBodyHTML.Append(@"<meta name=""viewport"" content =""width=device-width"" />");
            sbBodyHTML.Append(@"<meta content=""text/html; charset=utf-8"" http-equiv=""Content-Type"">");
            sbBodyHTML.Append(@"<title>" + strTitle + @"</title>");
            sbBodyHTML.Append(@"
                        <style type=""text/css"">
                            html{ background: #ffffff;}
                            .body { width: 100%; line-height: 1.5;}
                            .div-main { overflow-x: auto; background: #f2f2f2; display: table;  margin: 0 auto; width: 770px; color: #424242;}
                            .text-left { text-align: left!important; }
                            .width-100 { float: left; width: 100%; }
                            p { margin: 5px 0 5px; }
                            .color-black { color: black;}
                            .font-weight-bold { font-weight: bold; }
                            .padding-top-5 { padding-top: 5px; }
                            .padding-top-15 { padding-top: 15px; }
                            .padding-left-right-25 { padding-left: 25px; padding-right: 25px; }
                            a{ text-decoration: none; }
                        </style>
                    ");
            sbBodyHTML.Append(@"</head>");
            sbBodyHTML.Append(@"<body>");
            sbBodyHTML.Append(@"<div class=""div-main"">");
            sbBodyHTML.Append(@"<div class=""width-100 background"">");
            sbBodyHTML.Append(@"<div class=""padding-left-right-25"">");
            //sbBodyHTML.Append(@"<p>Xin chào " + email +",</p>");
            sbBodyHTML.Append(@"<p>Xin chào,</p>");
            sbBodyHTML.Append(@"<p class=""padding-top-5"">Cảm ơn quý khách đã đăng ký tài khoản trên hệ thống Inbrand Clound.</p>");
            sbBodyHTML.Append(@"<p class=""padding-top-5"">Mã xác thực email của quý khách là: {code}</p>");
            sbBodyHTML.Append(@"<p class=""padding-top-5"">Nếu không phải bạn là người đăng ký tài khoản Inbrand Clound. Vui lòng bỏ qua email này hoặc liên hệ với chúng tôi để được hỗ trợ: support@qinvoice.vn</p>");
            sbBodyHTML.Append(@"<p class=""padding-top-5"">Xin cảm ơn. </p>");
            sbBodyHTML.Append(@"<p class=""padding-top-5"">Inbrand Clound Team</p>");
            sbBodyHTML.Append(@"</div>");
            sbBodyHTML.Append(@"</div>");
            sbBodyHTML.Append(@"</div>");
            sbBodyHTML.Append(@"</body>");
            sbBodyHTML.Append(@"</html>");
            return sbBodyHTML.ToString();
        }
        public static string GetContentMailSysUserActive(string uuid, string networkid, string domain)
        {
            var strTitle = @"idocNet thông báo yêu cầu kích hoạt tài khoản.";
            var sbBodyHTML = new StringBuilder();
            sbBodyHTML.Append(@"<html lang=""en"">");
            sbBodyHTML.Append(@"<head>");
            sbBodyHTML.Append(@"<meta name=""viewport"" content =""width=device-width"" />");
            sbBodyHTML.Append(@"<meta content=""text/html; charset=utf-8"" http-equiv=""Content-Type"">");
            sbBodyHTML.Append(@"<title>" + strTitle + @"</title>");
            sbBodyHTML.Append(@"
                        <style type=""text/css"">
                            html{ background: #ffffff;}
                            .body { width: 100%; line-height: 1.5;}
                            .div-main { overflow-x: auto; background: #f2f2f2; display: table;  margin: 0 auto; width: 770px; color: #424242;}
                            .text-left { text-align: left!important; }
                            .width-100 { float: left; width: 100%; }
                            p { margin: 5px 0 5px; }
                            .color-black { color: black;}
                            .font-weight-bold { font-weight: bold; }
                            .padding-top-5 { padding-top: 5px; }
                            .padding-top-15 { padding-top: 15px; }
                            .padding-left-right-25 { padding-left: 25px; padding-right: 25px; }
                            a{ text-decoration: none; }
                        </style>
                    ");
            sbBodyHTML.Append(@"</head>");
            sbBodyHTML.Append(@"<body>");
            sbBodyHTML.Append(@"<div class=""div-main"">");
            sbBodyHTML.Append(@"<div class=""width-100 background"">");
            sbBodyHTML.Append(@"<div class=""padding-left-right-25"">");
            sbBodyHTML.Append(@"<p class=""font-weight-bold color-black"">Chào mừng quý khách đến với dịch vụ Xác thực nguồn gốc sản phẩm Inbrand Clound.</p>");
            sbBodyHTML.Append(@"<p class=""padding-top-15"">Vui lòng click vào đường link dưới đây để kích hoạt tài khoản:</p>");
            sbBodyHTML.Append(@"<p class=""padding-top-5""><a href=""" + domain + @"/register/activeacc?uuid=" + uuid + @"&networkid=" + networkid + @""">");
            sbBodyHTML.Append(@"" + domain + @"/register/activeacc?uuid=" + uuid + @"&networkid=" + networkid);
            sbBodyHTML.Append(@"</a></p>");
            sbBodyHTML.Append(@"<p class=""padding-top-15"">Trân trọng,</p>");
            sbBodyHTML.Append(@"<p class=""padding-top-5"">Inbrand Clound Team</p>");
            sbBodyHTML.Append(@"</div>");
            sbBodyHTML.Append(@"</div>");
            sbBodyHTML.Append(@"</div>");
            sbBodyHTML.Append(@"</body>");
            sbBodyHTML.Append(@"</html>");
            return sbBodyHTML.ToString();
        }
    }
}