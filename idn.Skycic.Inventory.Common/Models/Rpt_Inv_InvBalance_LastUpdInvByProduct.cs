using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
    public class Rpt_Inv_InvBalance_LastUpdInvByProduct
    {
        public string OrgID { get; set; }

        public string InvCode { get; set; }

        public string ProductCode { get; set; }

        public double QtyInv { get; set; }

        public string LogLUDTimeUTC { get; set; }
    }
}
