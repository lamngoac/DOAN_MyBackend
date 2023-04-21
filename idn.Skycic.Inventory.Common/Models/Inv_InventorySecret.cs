using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
    public class Inv_InventorySecret
    {
        public object SerialNo { get; set; }

        public object NetworkID { get; set; }

        public object MST { get; set; }

        public object OrgID { get; set; }

        public object GenTimesNo { get; set; }

        public object SecretNo { get; set; }

        public object QR_SerialNo { get; set; }

        public object FlagMap { get; set; }

        public object FlagUsed { get; set; }

        public object Remark { get; set; }

        public object LogLUDTimeUTC { get; set; }

        public object LogLUBy { get; set; }
    }
}
