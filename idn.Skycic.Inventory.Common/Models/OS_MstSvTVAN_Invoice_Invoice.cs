using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
	public class OS_MstSvTVAN_Invoice_Invoice
	{
		public object InvoiceCode { get; set; } // Số tra cứu
		public object NetworkID { get; set; } // NetworkID
		public object RefNo { get; set; } // RefNo
		public object InvoiceAdjType { get; set; } // Loại điều chỉnh
		public object TInvoiceCode { get; set; } // Mẫu hóa đơn
		public object MST { get; set; } // Mã số thế
		public object InvoiceDateUTC { get; set; } // ngày hóa đơn
		public object SourceInvoiceCode { get; set; } // Nguồn hóa đơn
		public object PaymentMethodCode { get; set; } // Phương thức thanh toán
		public object CustomerNNTCode { get; set; } // Mã khách hàng NNT
		public object InvoiceNo { get; set; } // Số hóa đơn
		public object EmailSend { get; set; } // Email Send        
		public object InvoiceFileSpec { get; set; } // Nội dung hóa đơn
		public object InvoiceFilePath { get; set; } // Đường dẫn hóa đơn
		public object CreateDTimeUTC { get; set; }
		public object CreateBy { get; set; }
		public object InvoiceNoDTimeUTC { get; set; } // Ngày nhập số hóa đơn
		public object InvoiceNoBy { get; set; } // Người nhập số hóa đơn
		public object SignDTimeUTC { get; set; } // Thời gian ký hóa đơn
		public object SignBy { get; set; } // Người ký hóa đơn
		public object ApprDTimeUTC { get; set; } // thời gain duyệt hóa đơn
		public object ApprBy { get; set; } // người duyệt hóa đơn
		public object CancelDTimeUTC { get; set; } // Thời gian hủy hóa đơn
		public object CancelBy { get; set; } // Người hủy hóa đơn
		public object SendEmailDTimeUTC { get; set; } // Thời gian send email
		public object SendEmailBy { get; set; } // Người send Email
		public object IssuedDTimeUTC { get; set; } // Thời gian chấp thuận
		public object IssuedBy { get; set; } // Người chấp thuận
		public object DeleteDTimeUTC { get; set; } // Thời gian delete
		public object DeleteBy { get; set; } // Người delete
		public object LUDTimeUTC { get; set; }
		public object LUBy { get; set; }
		public object Remark { get; set; }
		public object InvoiceStatus { get; set; } // trạng thái hóa đơn
		public object FlagChange { get; set; }
		public object LogLUDTimeUTC { get; set; }
		public object LogLUBy { get; set; }

		public object mcnnt_CustomerNNTCode { get; set; }
		public object mcnnt_CustomerNNTName { get; set; }
		public object mcnnt_CustomerNNTAddress { get; set; }
		public object mcnnt_CustomerMST { get; set; }
		public object mcnnt_CustomerNNTEmail { get; set; }
		public object mcnnt_ContactEmail { get; set; }

		public object mpm_PaymentMethodCode { get; set; }
		public object mpm_PaymentMethodName { get; set; }

		public object iti_TInvoiceCode { get; set; }
		public object iti_TInvoiceName { get; set; }
		public object iti_LogoFilePath { get; set; }
		public object iti_WatermarkFilePath { get; set; }
		public object iti_InvoiceTGroupCode { get; set; }
		public object iti_InvoiceType { get; set; }

		public object itg_InvoiceTGroupCode { get; set; }
		public object itg_Spec_Prd_Type { get; set; }
	}
}
