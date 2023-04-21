using idn.Skycic.Inventory.Common.Models;
using System.Collections.Generic;

namespace idn.Skycic.Inventory.Common.ModelsUI
{
    public class InvF_InventoryCusReturnSave
    {
        public InvF_InventoryCusReturn Obj_InvF_InventoryCusReturn { get; set; }
        public List<InvF_InventoryCusReturnCover> Lst_InvF_InventoryCusReturnCover { get; set; }
        public List<InvF_InventoryCusReturnDtl> Lst_InvF_InventoryCusReturnDtl { get; set; }
        public List<InvF_InventoryCusReturnInstLot> Lst_InvF_InventoryCusReturnInstLot { get; set; }
        public List<InvF_InventoryCusReturnInstSerial> Lst_InvF_InventoryCusReturnInstSerial { get; set; }
        public string FlagRedirect { get; set; } // 1: Reload page; 0: Redirect page
    }

    public class InvF_InventoryCusReturnPrint
    {
        public object NNTFullName { get; set; }
        public object NNTAddress { get; set; }
        public object NNTPhone { get; set; }
        public object IF_InvCusReturnNo { get; set; }
        public object DatePrint { get; set; }
        public object MonthPrint { get; set; }
        public object YearPrint { get; set; }
        public object CustomerName { get; set; }
        public object OrderNo { get; set; }
        public object InvNameOut { get; set; } //Kho nhập
        public object Remark { get; set; }
        public object TotalQty { get; set; }
        public object TotalValCusReturn { get; set; }
        public object TotalValCusReturnDesc { get; set; }
        public object TotalValCusReturnAfterDesc { get; set; }
        public object CreateUserName { get; set; }
        public object LogoFilePath { get; set; }

        public List<InvF_InventoryCusReturnCoverUI> Lst_InvF_InventoryCusReturnCoverUI { get; set; }
    }

    public class InvF_InventoryCusReturnReportServer
    {
        public string NNTFullName { get; set; }
        public string NNTAddress { get; set; }
        public string NNTPhone { get; set; }
        public string IF_InvCusReturnNo { get; set; }
        public string DatePrint { get; set; }
        public string MonthPrint { get; set; }
        public string YearPrint { get; set; }
        public string CustomerName { get; set; }
        public string OrderNo { get; set; }
        public string InvNameOut { get; set; }
        public string Remark { get; set; }
        public string TotalQty { get; set; }
        public string TotalValCusReturn { get; set; }
        public string TotalValCusReturnDesc { get; set; }
        public string TotalValCusReturnAfterDesc { get; set; }
        public string CreateUserName { get; set; }
        public string LogoFilePath { get; set; }

        public List<InvF_InventoryCusReturnCoverReportServer> DataTable { get; set; }
    }

    public class InvF_InventoryCusReturnCoverReportServer
    {
        public string Idx { get; set; }
        public string ProductCodeUser { get; set; }
        public string ProductName { get; set; }
        public string UnitCode { get; set; }
        public string InvName { get; set; }
        public string Qty { get; set; }
        public string UPCusReturn { get; set; }
        public string UPCusReturnDesc { get; set; }
        public string ValCusReturn { get; set; }
    }
}
