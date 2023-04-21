using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
    public class Rpt_Inventory_In_Out_Inv
    {
        public object OrgID { get; set; }

        public object InvCodeActual { get; set; }

        public object ProductCode { get; set; }

        public object mp_ProductCodeUser { get; set; }

        public object mp_ProductCodeRoot { get; set; }

        public object mp_ProductCodeBase { get; set; }

        public object mp_ProductName { get; set; }

        public object mp_UnitCode { get; set; }

        public object mp_FlagLot { get; set; }

        public object mp_FlagSerial { get; set; }

        public object mp_ProductType { get; set; }

        public object mp_ValConvert { get; set; }

        public object BeginPeriod_In_QtyBase { get; set; }

        public object BeginPeriod_Out_QtyBase { get; set; }

        public object BeginPeriod_Inv_QtyBase { get; set; } // Số lượng Tồn đầu

        public object BeginPeriod_UP { get; set; } // Giá lastest

        public object BeginPeriod_Val { get; set; } // Giá trị đầu

        public object InPeriod_In_QtyBase { get; set; } // Số lượng Nhập trong kỳ 

        public object InPeriod_Out_QtyBase { get; set; } // Số lượng Xuất trong kỳ 

        public object InPeriod_In_Val { get; set; } // Giá trị nhập trong kỳ 

        public object InPeriod_Out_Val { get; set; } // Giá trị xuất trong kỳ 

        public object EndPeriod_UP { get; set; } // 

        public object EndPeriod_Inv_QtyBase { get; set; } // Số lượng tồn cuối kỳ 

        public object EndPeriod_Val { get; set; } // Giá trị tồn cuối kỳ 
    }
}
