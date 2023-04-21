using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
    public class Invoice_TempGroupField
    {
        public object InvoiceTGroupCode { get; set; }

        public object DBFieldName { get; set; }

        public object NetworkID { get; set; }

        public object TCFType { get; set; }

        public object FlagActive { get; set; }

        public object LogLUDTimeUTC { get; set; }

        public object LogLUBy { get; set; }

        public object DBPhysicalType { get; set; }
        //// //
        public object icf_InvoiceCustomFieldCode { get; set; }

        public object icf_InvoiceCustomFieldName { get; set; }

        public object idcf_InvoiceDtlCustomFieldCode { get; set; }

        public object idcf_InvoiceDtlCustomFieldName { get; set; }
    }
}
