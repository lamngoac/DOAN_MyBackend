using idn.Skycic.Inventory.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace idn.Skycic.Inventory.Web.Models
{
    public class OS_Inos_OrgLicenseUI : OS_Inos_OrgLicense
    {
        public string FromDate { get; set; }
        public string ToDate { get; set; }
    }
}