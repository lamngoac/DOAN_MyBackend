using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
    public class RQ_Inv_GenTimesCarton : WARQBase
    {
        public string Rt_Cols_Inv_GenTimesCarton { get; set; }

        public Inv_GenTimesCarton Inv_GenTimesCarton { get; set; }
    }
}
