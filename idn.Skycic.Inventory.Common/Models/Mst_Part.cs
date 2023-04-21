using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
    public class Mst_Part
    {
        public object PartCode { get; set; } // Mã sản phẩm

        public object PartBarCode { get; set; } // Mã vạch

        public object PartName { get; set; } // Tên sản phẩm

        public object PartNameFS { get; set; } // Tên tiếng anh

        public object PartDesc { get; set; } // Mô tả

        public object PartType { get; set; } // Mã loại sản phẩm

        public object PMType { get; set; } // Mã nhóm vật liệu

        public object PartUnitCodeStd { get; set; }

        public object PartUnitCodeDefault { get; set; }

        public object RemarkForEffUsed { get; set; } // Thời hạn sử dụng --> text

        public object QtyMaxSt { get; set; }

        public object QtyMinSt { get; set; }

        public object QtyEffSt { get; set; }

        public object UPIn { get; set; }

        public object UPOut { get; set; }

        public object FilePath { get; set; } // File đính kèm

        public object ImagePath { get; set; }

        public object QtyEffMonth { get; set; } // Thời hạn sử dụng

        public object PartOrigin { get; set; } // Nguồn gốc

        public object PartComponents { get; set; } // Thành phần

        public object InstructionForUse { get; set; } // Cách dùng

        public object PartStorage { get; set; } // Bảo quản

        public object UrlMnfSequence { get; set; } // Link quy trình sản xuất

        public object MnfStandard { get; set; } // Tiêu chuẩn sản xuất

        public object PartStyle { get; set; } // Quy cách

        public object PartIntroduction { get; set; } // Giới thiệu sản phẩm

        public object FlagBOM { get; set; }

        public object FlagVirtual { get; set; }

        public object FlagInputLot { get; set; }

        public object FlagInputSerial { get; set; }

        public object FlagActive { get; set; }

        public object LogLUDTimeUTC { get; set; }

        public object LogLUBy { get; set; }
    }
}
