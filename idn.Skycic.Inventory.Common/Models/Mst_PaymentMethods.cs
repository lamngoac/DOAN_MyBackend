using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
   public  class Mst_PaymentMethods
    {
        public object PaymentMethodCode { get; set; }

        public object NetworkID { get; set; }

        public object PaymentMethodName { get; set; }

        public object Remark { get; set; }

        public object FlagActive { get; set; }

        public object LogLUDTimeUTC { get; set; }

        public object LogLUBy { get; set; }
    }
}
