using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
    public class InvF_InventoryOutHistInstSerial
    {
        public object IF_InvOutHistNo { get; set; }

        public object PartCode { get; set; }

        public object SerialNo { get; set; }

        public object NetworkID { get; set; }

        public object IF_InvOutHistPrevNo { get; set; }

        public object FlagInitBase { get; set; }

        public object FlagCurrent { get; set; }

        public object IF_InvOutHistISStatus { get; set; }

        public object LogLUDTimeUTC { get; set; }

        public object LogLUBy { get; set; }
    }
}
