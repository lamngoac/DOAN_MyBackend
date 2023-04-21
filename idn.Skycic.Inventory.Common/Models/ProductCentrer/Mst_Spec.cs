using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models.ProductCentrer
{
    public class Mst_Spec
    {
        public object OrgID { get; set; }

        public object SpecCode { get; set; }

        public object NetworkID { get; set; }

        public object SpecName { get; set; }

        public object SpecDesc { get; set; }

        public object ModelCode { get; set; }

        public object SpecType1 { get; set; }

        public object SpecType2 { get; set; }

        public object Color { get; set; }

        public object FlagHasSerial { get; set; }

        public object FlagHasLOT { get; set; }

        public object DefaultUnitCode { get; set; }

        public object StandardUnitCode { get; set; }

        public object NetworkSpecCode { get; set; }

        public object Remark { get; set; }

        public object FlagActive { get; set; }

        public object FlagExist { get; set; }

        public object CustomField1 { get; set; }

        public object CustomField2 { get; set; }

        public object CustomField3 { get; set; }

        public object CustomField4 { get; set; }

        public object CustomField5 { get; set; }

        public object CustomField6 { get; set; }

        public object CustomField7 { get; set; }

        public object CustomField8 { get; set; }

        public object CustomField9 { get; set; }

        public object CustomField10 { get; set; }

        public object LogLUDTimeUTC { get; set; }

        public object LogLUBy { get; set; }

        //// //
        public object mst1_SpecType1 { get; set; }

        public object mst1_SpecType1Name { get; set; }

        public object mst2_SpecType2 { get; set; }

        public object mst2_SpecType2Name { get; set; }

        public object msc_SpecCustomFieldCode { get; set; }

        public object msc_SpecCustomFieldName { get; set; }

        public object mm_ModelCode { get; set; }

        public object mb_BrandCode { get; set; }

        public object mb_BrandName { get; set; }

        public object msp_UnitCode { get; set; }

        public object msp_BuyPrice { get; set; }

        public object msp_SellPrice { get; set; }

        public object msp_UnitName { get; set; } // Tên đơn vị

    }
}
