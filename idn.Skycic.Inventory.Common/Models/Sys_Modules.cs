using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
    public class Sys_Modules
    {
        public object ModuleCode { get; set; }

        public object NetworkID { get; set; }

        public object SolutionCode { get; set; }

        public object ModuleName { get; set; }

        public object Description { get; set; }

        public object QtyInvoice { get; set; }

        public object ValCapacity { get; set; }

        public object FlagActive { get; set; }

        public object LogLUDTimeUTC { get; set; }

        public object LogLUBy { get; set; }

        public object ss_SolutionCode { get; set; } // mã solution

        public object ss_SolutionName { get; set; } // tên solution
    }
}
