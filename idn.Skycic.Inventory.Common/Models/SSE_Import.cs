using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
    public class SSE_Import //Phiếu nhập
    {
        public object ma_so_thue { get; set; }
        public object ma_kh { get; set; }
        public object ma_gd { get; set; }
        public object tk { get; set; }
        public object so_ct { get; set; }
        public object ngay_lct { get; set; }
        public object ngay_ct { get; set; }
        public object ma_nt { get; set; }
        public object status { get; set; }
        public object dien_giai { get; set; }
        public object ngay_ct0 { get; set; }
        public object so_seri0 { get; set; }
        public object ma_tt { get; set; }
        public object t_so_luong { get; set; }
        public object t_tien_nt0 { get; set; }
        public object t_cp_nt { get; set; }
        public object t_thue_nt { get; set; }
        public object t_tt_nt { get; set; }
        public object t_tt { get; set; }
        public object tk_thue_co { get; set; }
        public object tk_thue_no { get; set; }
        public List<SSE_ImportDtl> detail { get; set; }
        public SSE_Customer customer { get; set; }
    }
}
