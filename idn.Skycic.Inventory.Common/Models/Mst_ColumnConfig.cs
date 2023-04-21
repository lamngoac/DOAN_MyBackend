using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
    public class Mst_ColumnConfig
    {
        public object AutoId { get; set; }
        public object OrgID { get; set; }
        public object NetworkID { get; set; }
        public object ColumnConfigGrpCode { get; set; } // Nhóm Cột
        public object TableName { get; set; }
        public object ColumnName { get; set; }
        public object ColumnFormat { get; set; }
        public object ColumnDesc { get; set; }
        public object FlagActive { get; set; }
        public object LogLUDTimeUTC { get; set; }
        public object LogLUBy { get; set; }
    }
}
