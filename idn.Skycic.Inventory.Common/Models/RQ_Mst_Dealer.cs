using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
    public class RQ_Mst_Dealer : WARQBase
    {
        public string Rt_Cols_Mst_Dealer { get; set; }

        public Mst_Dealer Mst_Dealer { get; set; }
    }
}
