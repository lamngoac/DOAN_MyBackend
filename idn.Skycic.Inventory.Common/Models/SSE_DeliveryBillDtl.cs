using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
    public class SSE_DeliveryBillDtl //Chi tiết hóa đơn xuất
    {
        public object ma_vt { get; set; }
        public object dvt { get; set; }
        public object ma_bp { get; set; }
        public object ma_hd { get; set; }
        public object ma_kho { get; set; }
        public object ma_lo { get; set; }
        public object ma_vi_tri { get; set; }
        public object ma_vv { get; set; }
        public SSE_Product product { get; set; }
        public object so_luong { get; set; }
    }
}
