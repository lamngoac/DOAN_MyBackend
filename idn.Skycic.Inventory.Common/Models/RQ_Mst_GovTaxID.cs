using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace idn.Skycic.Inventory.Common.Models
{
    public class RQ_Mst_GovTaxID : WARQBase
    {
        public string Rt_Cols_Mst_GovTaxID { get; set; }

        public Mst_GovTaxID Mst_GovTaxID { get; set; }
    }
}
