using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
    public class RQ_OS_Ord_OrderPD : WARQBase
    {
        public string Rt_Cols_Ord_OrderPD { get; set; }
        public string Rt_Cols_Ord_OrderPDDtl { get; set; }

        //public OS_Ord_OrderPD OS_Ord_OrderPD { get; set; }
        public OS_Ord_OrderPD Ord_OrderPD { get; set; }
        public OS_Ord_OrderPDDtl OS_Ord_OrderPDDtl { get; set; }
        public List<OS_Ord_OrderPD> Lst_Ord_OrderPD { get; set; }
        public List<OS_Ord_OrderPDDtl> Lst_Ord_OrderPDDtl { get; set; } // Danh sách (lưới detail)
    }
}
