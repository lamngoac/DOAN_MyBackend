using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
    public class SSE_Customer
    {
        public object ma_kh { get; set; } //Mã khách hàng
        public object ten_kh { get; set; } //Tên khách hàng
        public object nh_kh1 { get; set; } //Nhóm khách hàng 1
        public object nh_kh2 { get; set; } //Nhóm khách hàng 2
        public object nh_kh3 { get; set; } //Nhóm khách hàng 3
        public object ma_so_thue { get; set; } //Mã số thuế
        public object dien_thoai { get; set; } //Điện thoại
        public object e_mail { get; set; } //Email
        public object doi_tac { get; set; } //Người liên hệ chính
        public object kh_yn { get; set; } //Khách hàng Yes/No
        public object ncc_yn { get; set; } //Nhà cung cấp Yes/No
        public object tk_nh { get; set; } //Số tài khoản
        public object ngan_hang { get; set; } //Ngân hàng
        public object ghi_chu { get; set; } //Ghi chú
        //public object so_gttt { get; set; } //Số Giấy Tờ Tùy Thân
        //public object Loai_gttt { get; set; } //Loại giấy tờ tùy than
        //public object Ngay_cap { get; set; } //Ngày cấp
        //public object Noi_cap { get; set; } //Nơi cấp 
        //public object ngay_het_han { get; set; } //Ghi chú
    }
}
