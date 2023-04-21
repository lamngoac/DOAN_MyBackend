using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
    public class RQ_InvF_CustomFieldIn : WARQBase
    {
        public string Rt_Cols_InvF_CustomFieldIn { get; set; }

        public InvF_CustomFieldIn InvF_CustomFieldIn { get; set; }
    }
}
