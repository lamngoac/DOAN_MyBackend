using idn.Skycic.Inventory.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.ModelsUI
{
    public class InvF_MoveOrdDtlUI : InvF_MoveOrdDtl
    {
        public object Idx { get; set; }
        public object Dtl_InvNameIn { get; set; }
        public object Dtl_InvNameOut { get; set; }
        public object Dtl_Remark { get; set; }
        public object ProductCodeUser { get; set; }
        public object ProductName { get; set; }
        public object QtyTotalOK { get; set; } // Tồn kho
        public List<Mst_ProductUI> Lst_Mst_ProductBase { get; set; }//Danh sách hàng cùng base
    }

    public class InvF_MoveOrdPrint
    {
        public object NNTFullName { get; set; }
        public object NNTAddress { get; set; }
        public object NNTPhone { get; set; }
        public object IF_MONo { get; set; }
        public object DatePrint { get; set; }
        public object MonthPrint { get; set; }
        public object YearPrint { get; set; }
        public object InvNameIn { get; set; }
        public object InvNameOut { get; set; }
        public object Remark { get; set; }
        public object ProductCodeUser { get; set; }
        public object ProductName { get; set; }
        public object UnitCode { get; set; }
        public object Qty { get; set; }
        public object Dtl_InvNameIn { get; set; }
        public object Dtl_InvNameOut { get; set; }
        public object Dtl_Remark { get; set; }
        public object TotalQty { get; set; }
        public object CreateUserName { get; set; }
        public object ApprUserName { get; set; }
        public object LogoFilePath { get; set; }

        public List<InvF_MoveOrdDtlUI> Lst_InvF_MoveOrdDtlUI { get; set; }
    }

    public class InvF_MoveOrdReportServer
    {
        public object NNTFullName { get; set; }
        public object NNTAddress { get; set; }
        public object NNTPhone { get; set; }
        public object IF_MONo { get; set; }
        public object DatePrint { get; set; }
        public object MonthPrint { get; set; }
        public object YearPrint { get; set; }
        public object InvNameIn { get; set; }
        public object InvNameOut { get; set; }
        public object Remark { get; set; }
        public object TotalQty { get; set; }
        public object CreateUserName { get; set; }
        public object ApprUserName { get; set; }
        public object LogoFilePath { get; set; }
        public List<InvF_MoveOrdDtlReportServer> DataTable { get; set; }
    }

    public class InvF_MoveOrdDtlReportServer
    {
        public object Idx { get; set; }
        public object ProductCodeUser { get; set; }
        public object ProductName { get; set; }
        public object UnitCode { get; set; }
        public object Qty { get; set; }
        public object Dtl_InvNameIn { get; set; }
        public object Dtl_InvNameOut { get; set; }
        public object Dtl_Remark { get; set; }
    }
}
