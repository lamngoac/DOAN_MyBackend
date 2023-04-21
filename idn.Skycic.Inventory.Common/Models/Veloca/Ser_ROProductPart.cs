using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models.Veloca
{
    public class Ser_ROProductPart
    {
        public object RONoSys { get; set; }

        public object IdxPrdPart { get; set; }

        public object NetworkID { get; set; }

        public object OrgID { get; set; }

        public object ProductCodePart { get; set; }

        public object ProductCodeUserPart { get; set; }

        public object ProductNamePart { get; set; }

        public object UnitCode { get; set; }

        public object RepairType { get; set; }

        public object ObjectType { get; set; }

        public object DiscountRate { get; set; } // Chiết khấu

        public object UP { get; set; }

        public object VATRateCode { get; set; } // VAT

        public object VAT { get; set; } // VAT

        public object Qty { get; set; }

        public object QtyOut { get; set; } // Số lượng xuất

        public object QtyReturn { get; set; } // Số lượng trả lại

        public object ValPart { get; set; } // Tổng giá trị phụ tùng trước chiết khấu và trước VAT

        public object ValBeforeVATPart { get; set; } // Tổng giá trị phụ tùng trước chiết khấu

        public object ValVATPart { get; set; } // Tổng giá trị VAT

        public object ValAfterVATPart { get; set; } // TỔng giá trị sau VAT

        public object ValDisAfterVATPart { get; set; } // Tổng giảm giá sau VAT

        public object ValInsAfterVATPart { get; set; } // Tổng giá bảo hiểm thanh toán sau VAT

        public object PRMCode { get; set; } // Mã chương trình khuyến mại

        public object FlagAccrual { get; set; }

        public object ROStatusPart { get; set; }

        public object Remark { get; set; }

        public object LogLUDTimeUTC { get; set; }

        public object LogLUBy { get; set; }

        public object motp_ObjectType { get; set; } //  

        public object motp_ObjectTypeName { get; set; } //  

        public object sro_RONo { get; set; } //  Số báo giá

        public object sro_RONoSys { get; set; } //  

        public object sro_PlateNoSys { get; set; } //  

        public object sro_PlateNo { get; set; } //  Biển số

        public object sro_ProductGrpCode { get; set; } //  Model

        public object mpg_ProductGrpName { get; set; } //  Tên Model

        public object sro_CreateBy { get; set; } //  

        public object su_CreateName { get; set; } //  

        public object Sro_CustomerCodeSys { get; set; } //  

        public object Sro_CustomerCode { get; set; } //  

        public object Sro_CustomerName { get; set; } //  

        public object Sro_CustomerNameEN { get; set; } //  

        public object Sro_CustomerMobilePhone { get; set; } //  

        public object Sro_CustomerPhoneNo { get; set; } // 

        public object sro_ROStatus { get; set; } // Trạng thái báo giá

        public object sro_CheckInDTimeUTC { get; set; } //  Ngày vào xưởng

        public object sro_ActualDeliveryDTimeUTC { get; set; } // Ngày giap xe thực tế

        public object QtyOutRemain { get; set; } // Số lượng còn lại có thể xuất tiếp

        public object QtyReturnRemain { get; set; } // Số lượng còn lại có thể trả lại tiếp
    }
}
