using idn.Skycic.Inventory.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.ModelsUI
{
    public class Prd_BOMUI: Prd_BOM
    {
        public List<TonKhoVT> lstTonKhoVT { get; set; }
        public object InvCodeMax { get; set; }
        public object QtyTotalOKMax { get; set; }
        public Prd_BOMUI()
        {
            lstTonKhoVT = new List<TonKhoVT>();
            QtyTotalOKMax = "0";
        }
    }

    public class TonKhoVT
    {
        public object InvCode { get; set; } // Vị trí
        public object QtyTotalOK { get; set; } // Tồn kho
        public object ProductCode { get; set; } // Mã hàng hoá
    }
}
