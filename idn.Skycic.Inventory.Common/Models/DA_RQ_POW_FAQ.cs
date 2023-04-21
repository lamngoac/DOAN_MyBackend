using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
    public class DA_RQ_POW_FAQ : WARQBase
    {
        public string Rt_Cols_POW_FAQ { get; set; }

        public DA_POW_FAQ POW_FAQ { get; set; }
    }
}
