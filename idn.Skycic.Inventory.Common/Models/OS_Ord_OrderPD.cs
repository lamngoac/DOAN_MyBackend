using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
    public class OS_Ord_OrderPD
    {
        public object OrderPDNoSys { get; set; }
        public object NetworkID { get; set; }
        public object OrgID { get; set; }
        public object OrderType { get; set; }
        public object OrderPDNo { get; set; } // Số đơn hàng user
        public object CustomerCodeSys { get; set; } // Mã sys Supplier
        public object CustomerCode { get; set; } // Mã user Supplier
        public object CustomerName { get; set; } // Tên Supplier
        public object EstimatedDeliverDate { get; set; } // Ngày giao hàng
        public object ShipperAddress { get; set; } // Địa chỉ nhận hàng
        public object CreateDTimeUTC { get; set; }
        public object CreateBy { get; set; }
        public object ApprDTimeUTC { get; set; }
        public object ApprBy { get; set; }
        public object CancelDTimeUTC { get; set; }
        public object CancelBy { get; set; }
        public object OrderStatus { get; set; } // trạng thái đơn hàng
        public object Json { get; set; }
        public object Remark { get; set; }
        public object LogLUDTimeUTC { get; set; }
        public object LogLUBy { get; set; }

        /// ct_CustomerAddress
        public object ct_CustomerAddress { get; set; } // địa chỉ nhà cung cấp
    }
}
