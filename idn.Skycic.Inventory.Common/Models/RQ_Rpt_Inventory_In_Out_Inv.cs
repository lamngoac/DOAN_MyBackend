using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
    public class RQ_Rpt_Inventory_In_Out_Inv : WARQBase
    {
        public string InvCode { get; set; }

        public string ProductCode { get; set; }

        public string ProductGrpCode { get; set; }

        public string ApprDTimeUTCFrom { get; set; }

        public string ApprDTimeUTCTo { get; set; }

        Rpt_Inventory_In_Out_Inv Rpt_Inventory_In_Out_Inv { get; set; }
    }
}
