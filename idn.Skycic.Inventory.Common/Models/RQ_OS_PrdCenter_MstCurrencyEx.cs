using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
    public class RQ_OS_PrdCenter_Mst_CurrencyEx : WARQBase
    {
        public string Rt_Cols_Mst_CurrencyEx { get; set; }

        public OS_PrdCenter_Mst_CurrencyEx Mst_CurrencyEx { get; set; }
    }
}
