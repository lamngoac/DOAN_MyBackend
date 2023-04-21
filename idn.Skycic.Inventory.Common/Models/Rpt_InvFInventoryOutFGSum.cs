using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
    public class Rpt_InvFInventoryOutFGSum
    {
        public string InvCode { get; set; } // Mã kho
       
        public string DLCode { get; set; } // Mã đơn vị
        
        public string AgentCode { get; set; } // Mã đại lý
        
        public string AgentName { get; set; } // Tên đại lý
        
        public string PartCode { get; set; } // Mã sản phẩm
        
        public string PartName { get; set; } // Tên sản phẩm
        
        public double TotalQtyOut { get; set; } // Tổng số lượng nhập
    }
}
