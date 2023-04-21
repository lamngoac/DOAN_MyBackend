using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using idn.Skycic.Inventory.Common.Attributes;

namespace idn.Skycic.Inventory.Common.Models
{
    public class DA_Mst_CustomerType : EntityBase
    {
        public string CustomerTypeCode { get; set; }
        public string CustomerTypeName { get; set; }
        public string CustomerTypeDesc { get; set; }
        public string FlagActive { get; set; }
    }
}
