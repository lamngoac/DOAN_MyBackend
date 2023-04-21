using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
    public class RT_OS_Ord_OrderPD : WARTBase
    {
        public List<OS_Ord_OrderPD> Lst_Ord_OrderPD { get; set; } // Danh sách đơn hàng
        public List<OS_Ord_OrderPDDtl> Lst_Ord_OrderPDDtl { get; set; } // Danh sách sản phẩm (lưới detail)
    }
}
