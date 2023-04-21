using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using idn.Skycic.Inventory.Common.Models;

namespace idn.Skycic.Inventory.Common.ModelsUI
{
    public class Mst_Part_ReportServer
    {
        public string PartCode { get; set; } // Mã sản phẩm

        public string PartBarCode { get; set; } // Mã vạch

        public string PartName { get; set; } // Tên sản phẩm

        public string PartNameFS { get; set; } // Tên tiếng anh

        public string PartDesc { get; set; } // Mô tả

        public string PartType { get; set; } // Mã loại sản phẩm

        public string PMType { get; set; } // Mã nhóm vật liệu

        public string PartUnitCodeStd { get; set; }

        public string PartUnitCodeDefault { get; set; }

        public string UPIn { get; set; }

        public string UPOut { get; set; }

        public string QtyEffMonth { get; set; } // Thời hạn sử dụng

        public string PartOrigin { get; set; } // Nguồn gốc

        public string PartComponents { get; set; } // Thành phần

        public string InstructionForUse { get; set; } // Cách dùng

        public string PartStorage { get; set; } // Bảo quản

        public string UrlMnfSequence { get; set; } // Link quy trình sản xuất

        public string MnfStandard { get; set; } // Tiêu chuẩn sản xuất

        public string PartStyle { get; set; } // Quy cách

        public string PartIntroduction { get; set; } // Giới thiệu sản phẩm

        public string LogLUDTimeUTC { get; set; }

        public string LogLUBy { get; set; }
    }


    public class ObjectReportServer
    {
        public string LogoFilePath { get; set; }
        public string BackgroundFilePath { get; set; }
        public List<Mst_Part_ReportServer> DataTable { get; set; }

    }
}
