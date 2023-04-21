using idn.Skycic.Inventory.Common.Models;
using System.Collections.Generic;

namespace idn.Skycic.Inventory.Common.ModelsUI
{
    public class InvF_InventoryInSave
    {
        public InvF_InventoryIn Obj_InvF_InventoryIn { get; set; }
        public List<InvF_InventoryInDtl> Lst_InvF_InventoryInDtl { get; set; }
        public List<InvF_InventoryInInstLot> Lst_InvF_InventoryInInstLot { get; set; }
        public List<InvF_InventoryInInstSerial> Lst_InvF_InventoryInInstSerial { get; set; }
        public List<InvF_InventoryInQR> Lst_InvF_InventoryInQR { get; set; }
        public string FlagRedirect { get; set; } // 1: Reload page; 0: Redirect page
    }

    public class InvF_InventoryInPrint
    {
        public object NNTFullName { get; set; }
        public object NNTAddress { get; set; }
        public object NNTPhone { get; set; }
        public object IF_InvInNo { get; set; }
        public object DatePrint { get; set; }
        public object MonthPrint { get; set; }
        public object YearPrint { get; set; }
        public object CustomerName { get; set; }
        public object OrderNo { get; set; }
        public object InvInTypeName { get; set; }
        public object Remark { get; set; }
        public object TotalQty { get; set; }
        public object TotalValIn { get; set; }
        public object TotalValInDesc { get; set; }
        public object TotalValInAfterDesc { get; set; }
        public object CreateUserName { get; set; }
        public object LogoFilePath { get; set; }        

        public List<InvF_InventoryInDtlUI> Lst_InvF_InventoryInDtlUI { get; set; }        
    }

    public class InvF_InventoryInReportServer
    {
        public string NNTFullName { get; set; }
        public string NNTAddress { get; set; }
        public string NNTPhone { get; set; }
        public string IF_InvInNo { get; set; }
        public string DatePrint { get; set; }
        public string MonthPrint { get; set; }
        public string YearPrint { get; set; }
        public string CustomerName { get; set; }
        public string OrderNo { get; set; }
        public string InvInTypeName { get; set; }
        public string Remark { get; set; }
        public string TotalQty { get; set; }
        public string TotalValIn { get; set; }
        public string TotalValInDesc { get; set; }
        public string TotalValInAfterDesc { get; set; }
        public string CreateUserName { get; set; }
        public string LogoFilePath { get; set; }

        public List<InvF_InventoryInDtlReportServer> DataTable { get; set; }
    }

    public class InvF_InventoryInDtlReportServer
    {
        public string Idx { get; set; }
        public string ProductCodeUser { get; set; }
        public string ProductName { get; set; }
        public string UnitCode { get; set; }
        public string InvName { get; set; }
        public string Qty { get; set; }
        public string UPIn { get; set; }
        public string UPInDesc { get; set; }
        public string ValInvIn { get; set; }
    }
}
