using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
    public class Prd_ProductID
    {
        public object OrgID { get; set; }

        public object ProductID { get; set; }

        public object SpecCode { get; set; }

        public object PrdCustomFieldCode { get; set; }

        public object ProductionDate { get; set; }

        public object LOTNo { get; set; }

        public object BuyDate { get; set; }

        public object SecretNo { get; set; }

        public object WarrantyStartDate { get; set; }

        public object WarrantyExpiredDate { get; set; }

        public object WarrantyDuration { get; set; }

        public object RefNo1 { get; set; }

        public object RefBiz1 { get; set; }

        public object RefNo2 { get; set; }

        public object RefBiz2 { get; set; }

        public object RefNo3 { get; set; }

        public object RefBiz3 { get; set; }

        public object Buyer { get; set; }

		public object NetworkProductIDCode { get; set; }

		public object ProductIDStatus { get; set; }

        public object FlagExist { get; set; }

        public object CustomField1 { get; set; }

        public object CustomField2 { get; set; }

        public object CustomField3 { get; set; }

        public object CustomField4 { get; set; }

        public object CustomField5 { get; set; }

        public object LogLUDTimeUTC { get; set; }

        public object LogLUBy { get; set; }

        //// 
        public object ms_SpecCode { get; set; }
        public object ms_SpecName { get; set; }
        public object ms_SpecDesc { get; set; }
        ////
        public object mm_ModelCode { get; set; }
        public object mm_ModelName { get; set; }
        ////
        public object mb_BrandCode { get; set; }
        public object mb_BrandName { get; set; }

        public object ppidc_PrdCustomFieldCode { get; set; }

        public object ppidc_PrdCustomFieldName { get; set; }

        public object msp_UnitCode { get; set; }

        public object ms_CustomField1 { get; set; }

        public object msp_UnitName { get; set; }
    }
}
