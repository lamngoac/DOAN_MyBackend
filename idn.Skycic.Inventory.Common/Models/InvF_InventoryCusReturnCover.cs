using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
	public class InvF_InventoryCusReturnCover
	{
		public object IF_InvCusReturnNo { get; set; }

		public object InvCodeInActual { get; set; }

		public object ProductCodeRoot { get; set; }

		public object NetworkID { get; set; }

		public object Qty { get; set; }

		public object UPCusReturn { get; set; }

		public object UPCusReturnDesc { get; set; }

		public object ValCusReturn { get; set; }

		public object ValCusReturnDesc { get; set; }

		public object ValCusReturnAfterDesc { get; set; }

		public object UnitCode { get; set; }

		public object IF_InvCusReturnStatusCover { get; set; }

		public object Remark { get; set; }

		public object LogLUDTimeUTC { get; set; }

		public object LogLUBy { get; set; }


        public object mp_ProductCode { get; set; }

        public object mp_ProductCodeUser { get; set; }

        public object mp_ProductCodeBase { get; set; }

        public object mp_ProductName { get; set; }

        public object mp_FlagActive { get; set; }

        public object mp_FlagLot { get; set; }

        public object mp_FlagSerial { get; set; }

        public object mp_ProductType { get; set; }

        public object ListInvCodeInActual { get; set; }

    }
}
