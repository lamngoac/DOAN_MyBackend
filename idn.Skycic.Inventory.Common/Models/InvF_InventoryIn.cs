using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
    public class InvF_InventoryIn
    {
        public object IF_InvInNo { get; set; } // Mã phiếu nhập

        public object NetworkID { get; set; } // NetworkID

        public object OrgID { get; set; } // OrgID

        public object InvInType { get; set; } // Loại nhập kho

        public object InvCodeIn { get; set; } // kho nhập

		public object IF_InvAudNo { get; set; } // Số phiếu kiểm kê

		public object CustomerCode { get; set; } // Mã khách hàng <=> CustomerCodeSys

        public object InvoiceNo { get; set; } // Số hóa đơn

        public object OrderNoSys { get; set; } // Số đơn hàng hệ thống Gen

        public object OrderNo { get; set; } // Số đơn hàng

        public object OrderType { get; set; } // Loại đơn hàng

        public object RefNoSys { get; set; } // Số đơn hàng hệ thống Gen

        public object RefNo { get; set; } // Số đơn hàng

        public object RefType { get; set; } // Loại đơn hàng

        public object UserDeliver { get; set; } // Người giao hàng

        public object TotalValIn { get; set; } // Tổng tiền hàng

        public object TotalValInDesc { get; set; } // Tổng tiền giảm 

        public object TotalValInAfterDesc { get; set; } // Tổng tiền trả khách hàng

        public object CreateDTimeUTC { get; set; } // Thời gian tạo

        public object CreateBy { get; set; } // Người tạo

        public object LUDTimeUTC { get; set; } // Thời gian cập nhật cuối cùng

        public object LUBy { get; set; } // Người cập nhật cuối cùng

        public object ApprDTimeUTC { get; set; } // Thời gian duyệt

        public object ApprBy { get; set; } // Người duyệt

        public object FlagQR { get; set; } // Cờ xác thực

        public object IF_InvInStatus { get; set; } // Trạng thái phiếu nhập

        public object Remark { get; set; } // Ghi chú

        public object LogLUDTimeUTC { get; set; } // Thời điểm cập nhật cuối cùng

        public object LogLUBy { get; set; } // Người cập nhập cuối cùng

		public object mct_CustomerCode { get; set; } // Ma khach hang

		public object mct_CustomerName { get; set; } // Ten khach hang



        public object miit_InvInType { get; set; } // Loại nhập kho

        public object miit_InvInTypeName { get; set; } // Tên loại nhập kho

        public object mii_InvCode { get; set; } // Mã kho

        public object mii_InvName { get; set; } // Tên kho

        public object su_UserCode_Create { get; set; }

        public object su_UserName_Create { get; set; } // Tên người dùng tạo phiếu nhập

        public object InvFCFInCode01 { get; set; } //Số hợp đồng

        public object InvFCFInCode02 { get; set; } //Số Container

        public object InvFCFInCode03 { get; set; } //Biển số xe

        public object InvFCFInCode04 { get; set; }

        public object InvFCFInCode05 { get; set; }

        public object InvFCFInCode06 { get; set; }

        public object InvFCFInCode07 { get; set; }

        public object InvFCFInCode08 { get; set; }

        public object InvFCFInCode09 { get; set; }

        public object InvFCFInCode10 { get; set; } 
    }
}
