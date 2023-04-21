using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
    public class Inos_DiscountCode
    {
        public string Code { get; set; }
        public int RemainQty { get; set; }
        public Inos_DiscountCodeTypes DiscountType { get; set; }
        public object DiscountAmount { get; set; }
        public string Description { get; set; }
        public bool Enabled { get; set; }
        public DateTime EffectDateFrom { get; set; }
        public DateTime EffectDateTo { get; set; }
    }
}
