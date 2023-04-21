using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
    public class DA_RQ_POW_ContactEmail : WARQBase
    {
        public string Rt_Cols_POW_ContactEmail { get; set; }

        public DA_POW_ContactEmail POW_ContactEmail { get; set; }
    }
}
