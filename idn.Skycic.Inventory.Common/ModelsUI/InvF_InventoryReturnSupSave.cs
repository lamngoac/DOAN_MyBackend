using idn.Skycic.Inventory.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.ModelsUI
{
    public class InvF_InventoryReturnSupSave
    {
        public InvF_InventoryReturnSup InvF_InventoryReturnSup { get; set; }
        public List<InvF_InventoryReturnSupDtl> Lst_InvF_InventoryReturnSupDtl { get; set; }
        public List<InvF_InventoryReturnSupInstLot> Lst_InvF_InventoryReturnSupInstLot { get; set; }
        public List<InvF_InventoryReturnSupInstSerial> Lst_InvF_InventoryReturnSupInstSerial { get; set; }
        public string FlagRedirect { get; set; } // 1: Reload page; 0: Redirect page
        public string FlagIsDelete { get; set; }
    }

    public class InvF_InventoryReturnSupPrint
    {
        public object NNTFullName { get; set; }
        public object NNTAddress { get; set; }
        public object NNTPhone { get; set; }
        public object IF_InvCusReturnNo { get; set; } //Số phiếu (Sai từ file mẫu)
        public object DatePrint { get; set; }
        public object MonthPrint { get; set; }
        public object YearPrint { get; set; }
        public object CustomerName { get; set; }
        public object OrderNo { get; set; }
        public object InvNameOut { get; set; } //Kho xuất
        public object Remark { get; set; }
        public object TotalQty { get; set; }
        public object TotalValReturnSup { get; set; }
        public object TotalValReturnSupDesc { get; set; }
        public object TotalValReturnSupAfterDesc { get; set; }
        public object CreateUserName { get; set; }
        public object LogoFilePath { get; set; }

        public List<InvF_InventoryReturnSupDtlUI> Lst_InvF_InventoryReturnSupDtlUI { get; set; }
    }

    public class InvF_InventoryReturnSupReportServer
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
        public string TotalValReturnSup { get; set; }
        public string TotalValReturnSupDesc { get; set; }
        public string TotalValReturnSupAfterDesc { get; set; }
        public string CreateUserName { get; set; }
        public string LogoFilePath { get; set; }

        public List<InvF_InventoryReturnSupDtlReportServer> DataTable { get; set; }
    }

    public class InvF_InventoryReturnSupDtlReportServer
    {
        public string Idx { get; set; }
        public string ProductCodeUser { get; set; }
        public string ProductName { get; set; }
        public string UnitCode { get; set; }
        public string Dtl_InvOutName { get; set; }
        public string Qty { get; set; }
        public string UPReturnSup { get; set; }
        public string ValReturnSup { get; set; }
    }
}
