using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
    public class RptSv_Rpt_InvoiceSummary_01
    {
        public object Month { get; set; } // Tháng
        public object MST { get; set; } // Mã số thuế
        public object FormNo { get; set; } // Mẫu số
        public object Sign { get; set; } // ký hiệu
        public object InvoiceType { get; set; } // Loại Hoá đơn
        public object NNTFullName { get; set; } // Tên NNT
        public object NNTAddress { get; set; } // Địa chỉ
        public object StartInvoiceNo { get; set; } // số bắt đầu
        public object EndInvoiceNo { get; set; } // số kết thúc
        public object TotalIssued { get; set; } // Tổng Số Lượng phát hành TotalIssued = (EndInvoiceNo - StartInvoiceNo + 1)
        public object InvoiceNoFrom { get; set; } // Từ số
        public object InvoiceNoTo { get; set; } // đến số
        public object InvoiceTotal { get; set; } // Tổng số
        public object TotalUsed { get; set; } // Đã sử dụng
        public object TotalDel { get; set; } // Tổng số lượng đã xoá
        public object ListInvoiceNoDel { get; set; } // Danh sách hoá đơn đã xoá
        public object LogLUDTimeUTC { get; set; }
        public object LogLUBy { get; set; }
    }
}
