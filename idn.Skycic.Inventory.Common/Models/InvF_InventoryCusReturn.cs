using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
	public class InvF_InventoryCusReturn
	{
		public object IF_InvCusReturnNo { get; set; }

		public object NetworkID { get; set; }

		public object OrgID { get; set; }

		public object InvInType { get; set; }

		public object InvCodeIn { get; set; }

		public object CustomerCode { get; set; }

        public object OrderNoSys { get; set; } // Số đơn hàng hệ thống Gen

        public object OrderNo { get; set; }

        public object InvoiceNo { get; set; }

        public object OrderType { get; set; }

        public object RefNoSys { get; set; } // Số đơn hàng hệ thống Gen

        public object RefNo { get; set; } // Số đơn hàng

        public object RefType { get; set; } // Loại đơn hàng

        public object TotalValCusReturn { get; set; }

		public object TotalValCusReturnDesc { get; set; }

		public object TotalValCusReturnAfterDesc { get; set; }

		public object CreateDTimeUTC { get; set; }

		public object CreateBy { get; set; }

		public object LUDTimeUTC { get; set; }

		public object LUBy { get; set; }

		public object ApprDTimeUTC { get; set; }

		public object ApprBy { get; set; }

		public object CancelDTimeUTC { get; set; }

		public object CancelBy { get; set; }

		public object IF_InvCusReturnStatus { get; set; }

		public object Remark { get; set; }

		public object LogLUDTimeUTC { get; set; }

		public object LogLUBy { get; set; }

        public object mct_CustomerCodeSys { get; set; }

        public object mct_CustomerCode { get; set; }

        public object mct_CustomerName { get; set; }

        public object su_UserCode_Create { get; set; }

        public object su_UserName_Create { get; set; }

    }
}
