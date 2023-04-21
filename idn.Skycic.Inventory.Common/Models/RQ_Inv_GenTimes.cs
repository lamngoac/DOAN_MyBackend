using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
    public class RQ_Inv_GenTimes : WARQBase
    {
        public string Rt_Cols_Inv_GenTimes { get; set; }

        public Inv_GenTimes Inv_GenTimes { get; set; }
    }
}
