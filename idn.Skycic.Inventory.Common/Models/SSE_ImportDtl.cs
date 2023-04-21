using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
    public class SSE_ImportDtl //Chi tiết phiếu nhập
    {
        public object ma_vt { get; set; }
        public object dvt { get; set; }
        public object ma_kho { get; set; }
        public object ma_vi_tri { get; set; }
        public object ma_lo { get; set; }
        public object gc_td1 { get; set; }
        public object s9 { get; set; }
        public object so_luong { get; set; }
        public object gia_nt0 { get; set; }
        public object gia0 { get; set; }
        public object tien0 { get; set; }
        public object tien_nt0 { get; set; }
        public object cp { get; set; }
        public object cp_nt { get; set; }
        public object tk_vt { get; set; }
        public object pn_so { get; set; }
        public object ma_vv { get; set; }
        public object ma_bp { get; set; }
        public object dh_so { get; set; }
        public object ma_hd { get; set; }
        public SSE_Product product { get; set; }

    }
}
