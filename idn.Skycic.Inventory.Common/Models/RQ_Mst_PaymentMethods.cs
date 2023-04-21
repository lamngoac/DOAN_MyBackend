using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
    public class RQ_Mst_PaymentMethods : WARQBase
    {
        public string Rt_Cols_Mst_PaymentMethods { get; set; }

        public Mst_PaymentMethods Mst_PaymentMethods { get; set; }
    }
}
