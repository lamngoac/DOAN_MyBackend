using idn.Skycic.Inventory.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.ModelsUI
{
    public class Rpt_Inv_InventoryBalance_MinimumUI : Rpt_Inv_InventoryBalance_Minimum
    {
        public List<Rpt_Inv_InventoryBalance_MinimumUI> ListRpt_Inv_InventoryBalance_MinimumUIChild { get; set; }
    }

    public class Rpt_Inv_InventoryBalance_MinimumPrint
    {
        public object NNTFullName { get; set; }
        public object NNTAddress { get; set; }
        public object NNTPhone { get; set; }
        public object InvName { get; set; }
        public object DatePrint { get; set; }
        public object MonthPrint { get; set; }
        public object YearPrint { get; set; }
        public object Idx { get; set; }
        public object CreateUserName { get; set; }
        public object LogoFilePath { get; set; }
        public List<Rpt_Inv_InventoryBalance_MinimumUI> Lst_Rpt_Inv_InventoryBalance_MinimumUI { get; set; }
    }

    public class Rpt_Inv_InventoryBalance_MinimumReportServer
    {
        public object NNTFullName { get; set; }
        public object NNTAddress { get; set; }
        public object NNTPhone { get; set; }
        public object DatePrint { get; set; }
        public object MonthPrint { get; set; }
        public object YearPrint { get; set; }
        public object InvName { get; set; }
        public object CreateUserName { get; set; }
        public object ApprUserName { get; set; }
        public object LogoFilePath { get; set; }
        public List<Rpt_Inv_InventoryBalance_MinimumDtlReportServer> DataTable { get; set; }
    }

    public class Rpt_Inv_InventoryBalance_MinimumDtlReportServer
    {
        public object Idx { get; set; }
        public object ProductCodeUser { get; set; }
        public object ProductName { get; set; }
        public object UnitCode { get; set; }
        public object ProductGrpName { get; set; }
        public object QtyMinSt { get; set; }
        public object QtyTotalOK { get; set; }
    }
}
