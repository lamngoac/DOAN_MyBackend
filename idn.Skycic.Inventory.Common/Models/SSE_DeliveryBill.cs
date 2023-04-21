using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
    public class SSE_DeliveryBill //Hóa đơn xuất
    {
        public object so_ct { get; set; }
        public object ma_gd { get; set; }
        public object ma_kh { get; set; }
        public object ngay_lct { get; set; }
        public object ngay_ct { get; set; }
        public object ong_ba { get; set; }
        public object status { get; set; }
        public object dien_giai { get; set; }
        public List<SSE_DeliveryBillDtl> detail { get; set; }
        public SSE_Customer customer { get; set; }
    }
}
