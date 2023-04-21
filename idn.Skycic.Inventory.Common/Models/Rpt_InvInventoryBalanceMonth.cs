using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
    public class Rpt_InvInventoryBalanceMonth
    {
        public string InvCode { get; set; }

        public string PartCode { get; set; }

        public string PartName { get; set; }

        public string PartType { get; set; }

        public string PartTypeName { get; set; }

        public double TotalQtyInvBegin { get; set; } // Tồn đầu kỳ

        public double TotalQtyIn { get; set; } // Số lượng nhập

        public double TotalQtyOut { get; set; } // Số lượng xuất

        public double TotalQtyInvEnd { get; set; } // Tồn cuối kỳ
    }
}
