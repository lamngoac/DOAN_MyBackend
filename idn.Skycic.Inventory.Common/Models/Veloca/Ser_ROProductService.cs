using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models.Veloca
{
    public class Ser_ROProductService
    {
        public object RONoSys { get; set; }

        public object IdxPrdService { get; set; }

        public object NetworkID { get; set; }

        public object OrgID { get; set; }

        public object ProductCodeService { get; set; }

        public object ProductCodeUserService { get; set; }

        public object ProductNameService { get; set; }

        public object UnitCode { get; set; }

        public object RepairType { get; set; }

        public object ObjectType { get; set; }

        public object DiscountRate { get; set; } // Chiết khấu

        public object UP { get; set; }

        public object VATRateCode { get; set; } // VAT

        public object VAT { get; set; } // VAT

        public object ValService { get; set; } // giá trị công việc trước báo giá và trước VAT

        public object ValBeforeVATService { get; set; } // Giá trị công việc trước VAT

        public object ValVATService { get; set; } // Giá trị VAT 

        public object ValAfterVATService { get; set; } // Giá trị sau VAT

        public object ValDisAfterVATService { get; set; } // Giá trị giảm sau VAT

        public object ValInsAfterVATService { get; set; } // Bảo hiểm thanh toán sau VAT

        public object RateInRO { get; set; } // Tỷ trọng trong báo giá

        public object PRMCode { get; set; } // Mã chương trình khuyển mại

        public object FlagAccrual { get; set; }

        public object ROStatusService { get; set; }

        public object ServicesStatus { get; set; } // trạng thái chi tiết công việc

        public object Remark { get; set; }

        public object LogLUDTimeUTC { get; set; }

        public object LogLUBy { get; set; }

        public object PlanStartDTimeUTC { get; set; } // Thời gian dự kiến bắt đầu

        public object PlanEndDTimeUTC { get; set; } // Thời gian dự kiến kết thúc

        public object ActualStartDTimeUTC { get; set; } // Thời gian dự thực tế bắt đầu

        public object ActualEndDTimeUTC { get; set; } //  Thời gian thực tế kết thúc

        public object ListEngineerNo { get; set; } //  

        public object ListEngineerName { get; set; } //  

        public object motp_ObjectType { get; set; } //  

        public object motp_ObjectTypeName { get; set; } //  
    }
}
