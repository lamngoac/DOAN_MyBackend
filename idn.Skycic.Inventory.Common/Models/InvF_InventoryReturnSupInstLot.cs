using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
	public class InvF_InventoryReturnSupInstLot
	{
		public object IF_InvReturnSupNo { get; set; }

		public object InvCodeOutActual { get; set; }

		public object ProductCode { get; set; }

		public object ProductLotNo { get; set; }

		public object NetworkID { get; set; }

		public object Qty { get; set; }

		public object IF_ReturnSupILStatus { get; set; }

		public object LogLUDTimeUTC { get; set; }

		public object LogLUBy { get; set; }

        public object mp_ProductCode { get; set; } // Ma san pham

        public object mp_ProductName { get; set; } // Ten san pham 

        public object mp_ProductCodeUser { get; set; }

        public object mp_ProductCodeBase { get; set; } // Ma san pham Base

        public object mp_FlagLot { get; set; } // Co Lot

        public object mp_FlagSerial { get; set; } // Co Serial

        public object mp_ValConvert { get; set; } // Co Serial
    }
}
