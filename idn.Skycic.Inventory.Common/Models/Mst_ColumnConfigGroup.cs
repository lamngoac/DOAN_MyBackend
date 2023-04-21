using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
    public class Mst_ColumnConfigGroup
    {
        public object ColumnConfigGrpCode { get; set; }
        public object OrgID { get; set; }
        public object NetworkID { get; set; }
        public object ColumnGrpName { get; set; }
        public object ColumnGrpFormat { get; set; }
        public object ColumnGrpDesc { get; set; }
        public object FlagActive { get; set; }
        public object LogLUDTimeUTC { get; set; }
        public object LogLUBy { get; set; }
    }
}
