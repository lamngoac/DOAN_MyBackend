using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
    public class SSE_Product
    {
        public object ma_vt { get; set; } //Mã spec
        public object ten_vt { get; set; } //Mô tả xe
        public object dvt { get; set; } // Đơn vị tính
        public object loai_vt { get; set; } //Loại vật tư default: 64
        public object nh_vt1 { get; set; } //Hiệu xe
        public object nh_vt2 { get; set; } //Mã model
        public object nh_vt3 { get; set; } //null
        public object s8 { get; set; } //Năm sx
        public object nuoc_sx { get; set; } //Xuất xứ
        public object xcolor { get; set; } //Màu sắc
    }
}
