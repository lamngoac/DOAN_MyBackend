using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.ModelsUI
{
    public class InvF_InventoryOutReportServer
    {
        public string NNTFullName { get; set; }
        public string NNTAddress { get; set; }
        public string NNTPhone { get; set; }
        public string IF_InvOutNo { get; set; }
        public string DatePrint { get; set; }
        public string MonthPrint { get; set; }
        public string YearPrint { get; set; }
        public string CustomerName { get; set; }
        public string OrderNo { get; set; }
        public string InvOutTypeName { get; set; }
        public string Remark { get; set; }
        public string TotalQty { get; set; }
        public string TotalValOut { get; set; }
        public string TotalValOutDesc { get; set; }
        public string TotalValOutAfterDesc { get; set; }
        public string CreateUserCode { get; set; }
        public string CreateUserName { get; set; }
        public string LogoFilePath { get; set; }

        public List<InvF_InventoryOutDtlReportServer> DataTable { get; set; }
    }

    public class InvF_InventoryOutDtlReportServer
    {
        public string Idx { get; set; }
        public string ProductCodeUser { get; set; }
        public string ProductName { get; set; }
        public string UnitCode { get; set; }
        public string InvName { get; set; }
        public string Qty { get; set; }
        public string UPOut { get; set; }
        public string UPOutDesc { get; set; }
        public string ValOut { get; set; }
        public string ValOutDesc { get; set; }
        public string ValOutAfterDesc { get; set; }
    }
}
