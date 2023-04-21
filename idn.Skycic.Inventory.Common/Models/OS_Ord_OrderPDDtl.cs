using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
    public class OS_Ord_OrderPDDtl
    {
        public object OrderPDNoSys { get; set; }
        public object OrgID { get; set; }
        public object ProductCode { get; set; } // Mã sys của hàng hóa
        public object CustomerCodeSys { get; set; }  // Mã sys của khách hàng (theo sản phẩm)
        public object NetworkID { get; set; }
        public object NetworkIDKH { get; set; } // Network khách hàng
        public object OrgIDKH { get; set; }  // OrgID khách hàng

        public object OrderSRNoSys { get; set; } // Số đơn hàng bán lẻ
        public object PlanQty { get; set; } // Số lượng in kế hoạch
        public object PrintedQty { get; set; } // Số lượng đã in
        public object PlanRemainQty { get; set; } // Số lượng chưa in
        public object OffSetQty { get; set; } // Cấp bù lỗi
        public object OffSetErrQty { get; set; } // Số lượng hàng cấp bù
        public object PGInvInQty { get; set; } // số lượng đã nhập kho
        public object PGInvInRemainQty { get; set; } // Số lượng chưa nhập kho
        public object Json { get; set; }
        public object Remark { get; set; }
        public object LogLUDTimeUTC { get; set; }
        public object LogLUBy { get; set; }

        ////
        public object pd_ProductName { get; set; } // Tên khách hàng
        public object pd_ProductCodeUser { get; set; } // Tên hàng hóa user
        public object mc_CustomerCode { get; set; } // Mã khách hàng user
        public object mc_CustomerName { get; set; } // Tên khách hàng
    }
}
