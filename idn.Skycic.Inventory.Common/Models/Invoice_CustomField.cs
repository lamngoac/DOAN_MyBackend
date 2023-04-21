using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
    public class Invoice_CustomField
    {
        public object InvoiceCustomFieldCode { get; set; }

        public object OrgID { get; set; }

        public object NetworkID { get; set; }

        public object InvoiceCustomFieldName { get; set; }

        public object DBPhysicalType { get; set; }

        public object FlagActive { get; set; }

        public object LogLUDTimeUTC { get; set; }

        public object LogLUBy { get; set; }
    }
}
