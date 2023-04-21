using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models.Veloca
{
    public class Ser_ROProductEngineer
    {
        public object RONoSys { get; set; }

        public object EngineerNo { get; set; }

        public object IdxPrdService { get; set; }

        public object NetworkID { get; set; }

        public object OrgID { get; set; }

        public object EngineerName { get; set; }

        public object PlanStartDTimeUTC { get; set; }

        public object PlanStartBy { get; set; }

        public object PlanEndDTimeUTC { get; set; }

        public object PlanEndBy { get; set; }

        public object ActualStartDTimeUTC { get; set; }

        public object ActualStartBy { get; set; }

        public object ActualEndDTimeUTC { get; set; }

        public object ActualEndBy { get; set; }

        public object RateInROService { get; set; }

        public object ServicesEngineerStatus { get; set; }

        public object Remark { get; set; }

        public object LogLUDTime { get; set; }

        public object LogLUBy { get; set; }

        public object sro_RONo { get; set; } // Số RO

        public object sro_RONoSys { get; set; }

        public object sro_PlateNoSys { get; set; }

        public object sro_PlateNo { get; set; } // B 

        public object sro_ProductGrpCode { get; set; }

        public object mpg_ProductGrpName { get; set; }

        public object sro_CustomerCodeSys { get; set; } // Mã khách hàng hệ thống sinh.

        public object sro_CustomerCode { get; set; } // Mã khách hàng.

        public object sro_CustomerName { get; set; } // Tên khách hàng.

        public object sro_CustomerNameEN { get; set; } // Tên khách hàng tiếng anh

        public object sro_CustomerMobilePhone { get; set; } // Số điện thoại moblie.

        public object sro_CustomerPhoneNo { get; set; } // Số điện thoại.

        public object sro_CreateBy { get; set; }

        public object su_CreateName { get; set; }

        public object sro_ROStatus { get; set; }

        public object sro_CheckInDTimeUTC { get; set; }

        public object sro_ActualDeliveryDTimeUTC { get; set; }

        public object srops_IdxPrdService { get; set; }

        public object srops_ProductCodeService { get; set; }

        public object srops_ProductCodeUserService { get; set; }

        public object srops_ProductNameService { get; set; }

        public object ValAfterVATEngineer { get; set; }
    }
}
