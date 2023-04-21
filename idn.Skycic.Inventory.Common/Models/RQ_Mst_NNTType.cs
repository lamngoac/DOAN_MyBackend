using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
    public class RQ_Mst_NNTType : WARQBase
    {
        public string Rt_Cols_Mst_NNTType { get; set; }

        public Mst_NNTType Mst_NNTType { get; set; }
    }
}
