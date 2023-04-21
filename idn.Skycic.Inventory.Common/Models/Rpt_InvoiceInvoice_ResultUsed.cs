using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace idn.Skycic.Inventory.Common.Models
{
    public class Rpt_InvoiceInvoice_ResultUsed
    {
        public object TInvoiceCode { get; set; }

        public object InvoiceType { get; set; }

        public object InvoiceTypeName { get; set; }

        public object FormNo { get; set; }

        public object Sign { get; set; }

        public object K1_TongSo { get; set; }

        public object K1_BeginPeriod_Start { get; set; }

        public object K1_BeginPeriod_End { get; set; }

        public object K1_InPeriod_Start { get; set; } // phát sinh

        public object K1_InPeriod_End { get; set; }

        public object K2_TongSo_Start { get; set; }

        public object K2_TongSo_End { get; set; }

        public object K2_Total { get; set; }

        public object K2_TotalUsed { get; set; }

        public object K2_TotalDel { get; set; }

        public object K2_ListInvoiceNoDel { get; set; }

        public object K3_EndPeriod_Start { get; set; }

        public object K3_EndPeriod_End { get; set; }

        public object K3_EndPeriod_Remain { get; set; }
    }
}
