using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
    public class OS_PrdCenter_Mst_SpecPrice
    {
        public object OrgID { get; set; }

        public object SpecCode { get; set; }

        public object UnitCode { get; set; }

        public object NetworkID { get; set; }

        public object BuyPrice { get; set; }

        public object SellPrice { get; set; }

        public object CurrencyCode { get; set; }

        public object DiscountVND { get; set; }

        public object VATRateCode { get; set; }

        public object EffectDTimeStart { get; set; }

        public object EffectDTimeEnd { get; set; }

        public object Remark { get; set; }

        public object FlagActive { get; set; }

        public object LogLUDTimeUTC { get; set; }

        public object LogLUBy { get; set; }
    }
}
