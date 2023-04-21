using idn.Skycic.Inventory.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.ModelsUI
{
    public class Rpt_Inventory_In_Out_InvUI : Rpt_Inventory_In_Out_Inv
    {
        public object ListInvCode { get; set; }
        public List<Rpt_Inventory_In_Out_InvUI> ListRpt_Inventory_In_Out_InvUIChild { get; set; }
    }

    public class Rpt_Inventory_In_Out_InvPrint
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
        public object BeginPeriod_Inv_QtyBase { get; set; }
        public object BeginPeriod_Val { get; set; }
        public object InPeriod_In_QtyBase { get; set; }
        public object InPeriod_In_Val { get; set; }
        public object InPeriod_Out_QtyBase { get; set; }
        public object InPeriod_Out_Val { get; set; }
        public object EndPeriod_Inv_QtyBase { get; set; }
        public object EndPeriod_Val { get; set; }
        public object CreateUserName { get; set; }
        public object ApprUserName { get; set; }
        public object LogoFilePath { get; set; }
        public List<Rpt_Inventory_In_Out_InvUI> Lst_Rpt_Inventory_In_Out_InvUI { get; set; }
    }

    public class Rpt_Inventory_In_Out_InvReportServer
    {
        public object NNTFullName { get; set; }
        public object NNTAddress { get; set; }
        public object NNTPhone { get; set; }
        public object InvName { get; set; }
        public object DatePrint { get; set; }
        public object MonthPrint { get; set; }
        public object YearPrint { get; set; }
        public object CreateUserName { get; set; }
        public object ApprUserName { get; set; }
        public object LogoFilePath { get; set; }
        public List<Rpt_Inventory_In_Out_InvDtlReportServer> DataTable { get; set; }
    }

    public class Rpt_Inventory_In_Out_InvDtlReportServer
    {
        public object Idx { get; set; }
        public object ProductName { get; set; }
        public object ProductCodeUser { get; set; }
        public object UnitCode { get; set; }
        public object BeginPeriod_Inv_QtyBase { get; set; }
        public object BeginPeriod_Val { get; set; }
        public object InPeriod_In_QtyBase { get; set; }
        public object InPeriod_In_Val { get; set; }
        public object InPeriod_Out_QtyBase { get; set; }
        public object InPeriod_Out_Val { get; set; }
        public object EndPeriod_Inv_QtyBase { get; set; }
        public object EndPeriod_Val { get; set; }
        public object Dtl_InvName { get; set; }
    }
}
