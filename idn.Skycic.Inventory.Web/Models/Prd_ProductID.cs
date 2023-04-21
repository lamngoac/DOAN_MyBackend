using idn.Skycic.Inventory.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace idn.Skycic.Inventory.Web.Models
{
    public class Prd_ProductID : OS_PrdCenter_Mst_Spec
    {
        public string BrandCode { get; set; }
        public string BrandName { get; set; }
        public string SellPrice { get; set; }
    }
}