using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
    public class Rpt_InvFInventoryInFGSum
    {

        public string InvCode { get; set; } // Mã kho

        public string DLCode { get; set; } // Mã đơn vị
        
        public string PartCode { get; set; } // Mã sản phẩm
        
        public string PartName { get; set; } // Tên sản phẩm
        
        public double TotalQtyIn { get; set; } // Tổng số lượng nhập
    }
}
