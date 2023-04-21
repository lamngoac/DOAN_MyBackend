using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
    public class Mst_PrintOrder
    {
        public object PrintOrdNo { get; set; }
        public object NetworkIDKH { get; set; }
        public object OrgIDKH { get; set; }
        public object OrgIDOwner { get; set; }
        public object OrgIDSup { get; set; } // Org Supplier
        public object CustomerCodeSys { get; set; } // Mã Sys Supplier
        public object ProductCode { get; set; } // mã sản phẩm
        public object CreateDTimeUTC { get; set; }
        public object CreateBy { get; set; }
        public object UpdDTimeUTC { get; set; }
        public object UpdBy { get; set; }
        public object PlanQty { get; set; }
        public object PrintedQty { get; set; }
        public object FinishedPrdQty { get; set; }
        public object FlagActive { get; set; }
        public object Remark { get; set; }
        public object Json { get; set; }
        public object LogLUDTimeUTC { get; set; }
        public object LogLUBy { get; set; }

        ////
        public object OrgIDUpd { get; set; }
        ////
        public object mpo_QtyRemain { get; set; }
        public object mc_CustomerCode { get; set; } // Mã Supplier
        public object mc_CustomerName { get; set; } // Tên Supplier
        public object mp_ProductCodeUser { get; set; }
        public object mp_ProductName { get; set; }
    }
}
