using idn.Skycic.Inventory.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.ModelsUI
{

    public class Rpt_InvF_WarehouseCardUI : Rpt_InvF_WarehouseCard
    {
        public string ListInvCodeActual { get; set; }
    }

    public class Rpt_InvF_WarehouseCardPrint
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
        public object DocNo { get; set; }
        public object DateDoc { get; set; }
        public object InventoryActionDesc { get; set; }
        public object IN_TotalQty { get; set; }
        public object CUSRETURN_TotalQty { get; set; }
        public object AUDITIN_TotalQty { get; set; }
        public object MOVE_TotalQty { get; set; }
        public object OUT_TotalQty { get; set; }
        public object RETURNSUP_TotalQty { get; set; }
        public object AUDITOUT_TotalQty { get; set; }
        public object TotalQtyInv { get; set; }
        public object Dtl_InvName { get; set; }
        public object CreateUserName { get; set; }
        public object LogoFilePath { get; set; }
        public List<Rpt_InvF_WarehouseCardUI> Lst_Rpt_InvF_WarehouseCardUI { get; set; }
    }

    public class Rpt_Inv_InvF_WarehouseCardReportServer
    {
        public object NNTFullName { get; set; }
        public object NNTAddress { get; set; }
        public object NNTPhone { get; set; }
        public object InvName { get; set; }
        public object DatePrint { get; set; }
        public object MonthPrint { get; set; }
        public object YearPrint { get; set; }
        public object ProductName { get; set; }
        public object ProductCodeUser { get; set; }
        public object UnitCode { get; set; }
        public object CreateUserName { get; set; }
        public object ApprUserName { get; set; }
        public object LogoFilePath { get; set; }
        public List<Rpt_Inv_InvF_WarehouseCardDtlReportServer> DataTable { get; set; }
    }

    public class Rpt_Inv_InvF_WarehouseCardDtlReportServer
    {
        public object Idx { get; set; }
        public object DocNo { get; set; }
        public object DateDoc { get; set; }
        public object InventoryActionDesc { get; set; }
        public object IN_TotalQty { get; set; }
        public object CUSRETURN_TotalQty { get; set; }
        public object AUDITIN_TotalQty { get; set; }
        public object MOVE_TotalQty { get; set; }
        public object OUT_TotalQty { get; set; }
        public object RETURNSUP_TotalQty { get; set; }
        public object AUDITOUT_TotalQty { get; set; }
        public object TotalQtyInv { get; set; }
        public object Dtl_InvName { get; set; }
    }
}
