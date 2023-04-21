using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
	public class InvF_InventoryReturnSup
	{
		public object IF_InvReturnSupNo { get; set; }

		public object NetworkID { get; set; }

		public object OrgID { get; set; }

		public object InvCodeOut { get; set; }

		public object CustomerCode { get; set; }

		public object OrderNo { get; set; }

		public object OrderType { get; set; }

		public object IF_InvInNo { get; set; }

		public object TotalValReturnSup { get; set; }

		public object TotalValReturnSupDesc { get; set; }

		public object TotalValReturnSupAfterDesc { get; set; }

        public object CreateDTimeUTC { get; set; }

        public object CreateBy { get; set; }

        public object ApprDTimeUTC { get; set; }

		public object ApprBy { get; set; }

		public object CancelDTimeUTC { get; set; }

		public object CancelBy { get; set; }

		public object IF_ReturnSupStatus { get; set; }

		public object Remark { get; set; }

		public object LogLUDTimeUTC { get; set; }

		public object LogLUBy { get; set; }

        public object mct_CustomerCode { get; set; }

        public object mct_CustomerName { get; set; }

        public object su_UserCode_Create { get; set; }

        public object su_UserName_Create { get; set; }
    }
}
