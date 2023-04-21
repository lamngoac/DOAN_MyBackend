using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
    public class RQ_Rpt_Inv_InvBalance_LastUpdInvByProduct : WARQBase
    {
        public object InvCode { get; set; }

        public List<Rpt_Inv_InvBalance_LastUpdInvByProduct> Lst_Rpt_Inv_InvBalance_LastUpdInvByProduct { get; set; }
    }
}
