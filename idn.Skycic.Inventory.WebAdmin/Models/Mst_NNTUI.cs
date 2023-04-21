using idn.Skycic.Inventory.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace idn.Skycic.Inventory.WebAdmin.Models
{
    public class Mst_NNTUI : Mst_NNT
    {
        public string FromDate { get; set; }
        public string ToDate { get; set; }
    }
}