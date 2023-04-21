using idn.Skycic.Inventory.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.ModelsUI
{
    public class ProductID_UI: OS_PrdCenter_Prd_ProductID
    {
        public object SellPrice { get; set; }
        public string UnitCode { get; set; }
    }
}
