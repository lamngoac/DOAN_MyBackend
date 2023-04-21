using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
    public class RQ_Inv_GenTimesBox : WARQBase
    {
        public string Rt_Cols_Inv_GenTimesBox { get; set; }

        public Inv_GenTimesBox Inv_GenTimesBox { get; set; }
    }
}
