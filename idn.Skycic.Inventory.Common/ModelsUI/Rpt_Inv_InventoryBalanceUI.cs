using idn.Skycic.Inventory.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.ModelsUI
{
    public class Rpt_Inv_InventoryBalanceUI : Rpt_Inv_InventoryBalance
    {
        public object ListInvCode { get; set; }

        public List<Rpt_Inv_InventoryBalanceUI> ListRpt_Inv_InventoryBalanceUIChild { get; set; }
    }

    public class Rpt_Inv_InventoryBalancePrint
    {
        public object NNTFullName { get; set; }
        public object NNTAddress { get; set; }
        public object NNTPhone { get; set; }
        public object InvName { get; set; }
        public object DatePrint { get; set; }
        public object MonthPrint { get; set; }
        public object YearPrint { get; set; }
        public object Idx { get; set; }
        public object ProductCodeUser { get; set; }
        public object ProductName { get; set; }
        public object UnitCode { get; set; }
        public object TotalVal { get; set; }
        public object Dtl_InvCode { get; set; }
        public object CreateUserName { get; set; }
        public object LogoFilePath { get; set; }
        public List<Rpt_Inv_InventoryBalanceUI> Lst_Rpt_Inv_InventoryBalanceUI { get; set; }
    }

    public class Rpt_Inv_InventoryBalanceReportServer
    {
        public object NNTFullName { get; set; }
        public object NNTAddress { get; set; }
        public object NNTPhone { get; set; }
        public object DatePrint { get; set; }
        public object MonthPrint { get; set; }
        public object YearPrint { get; set; }
        public object InvName { get; set; }
        public object CreateUserCode { get; set; }
        public object CreateUserName { get; set; }
        public object ApprUserName { get; set; }
        public object LogoFilePath { get; set; }
        public List<Rpt_Inv_InventoryBalanceDtlReportServer> DataTable { get; set; }
    }

    public class Rpt_Inv_InventoryBalanceDtlReportServer
    {
        public object Idx { get; set; }
        public object ProductCodeUser { get; set; }
        public object ProductName { get; set; }
        public object UnitCode { get; set; }
        public object Qty { get; set; }
        public object TotalVal { get; set; }
        public object Dtl_InvCode { get; set; }
        // 2023-01-05
        public object QtyTotalOK { get; set; }
        public object QtyAvailOK { get; set; }
        public object QtyBlockOK { get; set; }
        public object TotalValInv { get; set; }
        public object UPInv { get; set; }
    }
}
