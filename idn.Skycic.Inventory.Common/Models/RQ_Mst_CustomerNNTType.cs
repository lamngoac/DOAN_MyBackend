using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
    public class RQ_Mst_CustomerNNTType : WARQBase
    {
        public string Rt_Cols_Mst_CustomerNNTType { get; set; }

        public Mst_CustomerNNTType Mst_CustomerNNTType { get; set; }
    }
}
