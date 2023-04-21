using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
    public class RQ_Prd_ProductID : WARQBase
    {
        public List<Prd_ProductID> Lst_Prd_ProductID { get; set; }

        public string Rt_Cols_Prd_ProductID { get; set; }

        public Prd_ProductID Prd_ProductID { get; set; }
    }
}
