using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models.ProductCentrer
{
    public class Mst_Org
    {
        public object OrgID { get; set; }
        public object NetworkID { get; set; }
        public object OrgParent { get; set; }
        public object OrgBUCode { get; set; }
        public object OrgBUPattern { get; set; }
        public object OrgLevel { get; set; }
        public object Remark { get; set; }
        public object FlagActive { get; set; }
        public object LogLUDTimeUTC { get; set; }
        public object LogLUBy { get; set; }
    }
}
