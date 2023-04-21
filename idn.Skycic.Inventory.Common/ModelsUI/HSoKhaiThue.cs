using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace idn.Skycic.Inventory.Common.ModelsUI
{
    [XmlRoot(Namespace = "http://kekhaithue.gdt.gov.vn/TKhaiThue",
        ElementName = "HSoThueDTu",
        DataType = "string",
        IsNullable = true)]
    public class HSoThueDTu
    {
        public HSoKhaiThue HSoKhaiThue { get; set; }
    }
    public class HSoKhaiThue
    {
        public TTinChung TTinChung { get; set; }
        //public CTieuTKhaiChinh CTieuTKhaiChinh { get; set; }
        //public PLuc PLuc { get; set; }

    }
    public class TTinChung
    {
        public TTinDVu TTinDVu { get; set; }
        public TTinTKhaiThue TTinTKhaiThue { get; set; }
    }

    public class TTinDVu
    {
        public string maDVu { get; set; }
        public string tenDVu { get; set; }
        public string pbanDVu { get; set; }
        public string ttinNhaCCapDVu { get; set; }
    }

    public class TTinTKhaiThue
    {
        public TKhaiThue TKhaiThue { get; set; }
        public NNT NNT { get; set; }
        //public DLyThue DLyThue { get; set; }
    }

    public class TKhaiThue
    {
        public string maTKhai { get; set; }
        public string tenTKhai { get; set; }
        public string moTaBMau { get; set; }
        public string pbanTKhaiXML { get; set; }
        public string loaiTKhai { get; set; }
        public string soLan { get; set; }

        public KyKKhaiThue KyKKhaiThue { get; set; }

        public string maCQTNoiNop { get; set; }
        public string tenCQTNoiNop { get; set; }
        public string ngayLapTKhai { get; set; }

        public GiaHan GiaHan { get; set; }

        public string nguoiKy { get; set; }
        public string ngayKy { get; set; }
        public string nganhNgheKD { get; set; }        
    }

    public class KyKKhaiThue
    {
        public string kieuKy { get; set; }
        public string kyKKhai { get; set; }
        public string kyKKhaiTuNgay { get; set; }
        public string kyKKhaiDenNgay { get; set; }
        public string kyKKhaiTuThang { get; set; }
        public string kyKKhaiDenThang { get; set; }
    }

    public class GiaHan
    {
        public string maLyDoGiaHan { get; set; }
        public string lyDoGiaHan { get; set; }
    }
    public class NNT
    {
        public string mst { get; set; }
        public string tenNNT { get; set; }
        public string dchiNNT { get; set; }
        public string phuongXa { get; set; }
        public string maHuyenNNT { get; set; }
        public string tenHuyenNNT { get; set; }
        public string maTinhNNT { get; set; }
        public string tenTinhNNT { get; set; }
        public string dthoaiNNT { get; set; }
        public string faxNNT { get; set; }
        public string emailNNT { get; set; }
    }

    public class DLyThue
    {

    }

    public class CTieuTKhaiChinh
    {
    }

    public class PLuc
    {

    }
}
