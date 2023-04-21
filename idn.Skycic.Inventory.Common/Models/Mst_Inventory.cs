using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace idn.Skycic.Inventory.Common.Models
{
    public class Mst_Inventory
    {
		public object OrgID { get; set; }

		public object InvCode { get; set; }

		public object NetworkID { get; set; }

		public object InvCodeParent { get; set; }

        public object InvBUCode { get; set; }

        public object InvBUPattern { get; set; }

        public object InvLevel { get; set; }

        public object InvLevelType { get; set; }

        public object InvType { get; set; }

        public object InvName { get; set; }

        public object InvAddress { get; set; }

        public object InvContactName { get; set; }

        public object InvContactPhone { get; set; }

        public object InvContactEmail { get; set; }

		public object FlagIn_Out { get; set; }

		public object FlagActive { get; set; }

		public object Remark { get; set; }

        public object LogLUDTimeUTC { get; set; }

        public object LogLUBy { get; set; }
        public object GLNCode { get; set; } // 20220219. Mã địa chỉ kho
        public object mgln_GLNName { get; set; }
        public object mgln_GPSLat { get; set; }
        public object mgln_GPSLong { get; set; }
    }
}
