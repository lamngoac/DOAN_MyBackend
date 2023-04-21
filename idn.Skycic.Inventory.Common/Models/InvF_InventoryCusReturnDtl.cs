using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
	public class InvF_InventoryCusReturnDtl
	{
		public object IF_InvCusReturnNo { get; set; }

		public object InvCodeInActual { get; set; }

		public object ProductCodeRoot { get; set; }

		public object ProductCode { get; set; }

		public object NetworkID { get; set; }

		public object Qty { get; set; }

		public object UnitCode { get; set; }

		public object IF_InvCusReturnStatusDtl { get; set; }

		public object Remark { get; set; }

		public object LogLUDTimeUTC { get; set; }

		public object LogLUBy { get; set; }

        public object mp_ProductCode { get; set; }

        public object mp_ProductName { get; set; }

        public object mp_ProductCodeUser { get; set; }

        public object mp_ProductCodeBase { get; set; } // Ma san pham Base

        public object mp_FlagLot { get; set; } // Co Lot

        public object mp_FlagSerial { get; set; } // Co Serial

        public object ificrc_UPCusReturn { get; set; }

        public object ificrc_UPCusReturnDesc { get; set; }
    }
}
