using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
    public class RQ_InvF_TempPrint : WARQBase
    {
        public string Rt_Cols_InvF_TempPrint { get; set; }

        public InvF_TempPrint InvF_TempPrint { get; set; }
    }
}
