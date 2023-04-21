using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
    public class InvF_MoveOrd
    {
        public object IF_MONo { get; set; } // Số phiếu điều chuyển

        public object NetworkID { get; set; }

        public object OrgID { get; set; }

        public object MoveOrdType { get; set; } // Loại điều chuyển

        public object InvCodeOut { get; set; } // Kho xuất

        public object InvCodeIn { get; set; } // Kho nhập

		public object IF_InvAudNo { get; set; } // Số phiếu kiểm kê

		public object CreateDTimeUTC { get; set; } // Ngày tạo phiếu chuyển

        public object CreateBy { get; set; } // người tạo

        public object LUDTimeUTC { get; set; } // Thời gian cập nhật

        public object LUBy { get; set; } // Người cập nhật

        public object ApprDTimeUTC { get; set; } // Ngày duyệt phiếu điều chuyển

        public object ApprBy { get; set; } // Người duyệt phiếu điều chuyển

        public object CancelDTimeUTC { get; set; } // ngày hủy

        public object CancelBy { get; set; } // Người hủy

        public object IF_MOStatus { get; set; } // Trạng thái phiếu

        public object Remark { get; set; } // Remark

        public object LogLUDTimeUTC { get; set; } // Remark

        public object LogLUBy { get; set; } // Remark
        public object su_UserCode_Create { get; set; } // 20230104. Mã người tạo phiếu điều chuyển
        public object su_UserName_Create { get; set; } // 20230104. Tên người tạo phiếu điều chuyển

    }
}
