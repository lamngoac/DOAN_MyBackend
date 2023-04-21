using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
    public class RQ_OS_PrdCenter_Prd_ProductID : WARQBase
    {
        public string Rt_Cols_Prd_ProductID { get; set; }

        public OS_PrdCenter_Prd_ProductID Prd_ProductID { get; set; }

        public List<OS_PrdCenter_Prd_ProductID> Lst_Prd_ProductID { get; set; }
    }
}
