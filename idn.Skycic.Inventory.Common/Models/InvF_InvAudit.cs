using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
    public class InvF_InvAudit
    {
        public object IF_InvAudNo { get; set; } // Mã phiếu kiểm kê

        public object NetworkID { get; set; } // 

        public object OrgID { get; set; } // Mã org

        public object InvCodeAudit { get; set; } //Kho kiểm kê

        public object CreateDTimeUTC { get; set; } // Ngày kiểm kê

        public object CreateBy { get; set; } // người kiểm kê

        public object LUDTimeUTC { get; set; }

        public object LUBy { get; set; }

        public object ApprDTimeUTC { get; set; }

        public object ApprBy { get; set; }

        public object FinishDTimeUTC { get; set; }

        public object FinishBy { get; set; }

        public object CancelDTimeUTC { get; set; }

        public object CancelBy { get; set; }

        public object Remark { get; set; }

        public object IF_InvAuditStatus { get; set; }

        public object LogLUDTimeUTC { get; set; }

        public object LogLUBy { get; set; }

        public object su_UserCode_Create { get; set; } // 20230104. Mã người tạo phiếu kiểm kê
        public object su_UserName_Create { get; set; } // 20230104. Tên người tạo phiếu kiểm kê

    }
}
