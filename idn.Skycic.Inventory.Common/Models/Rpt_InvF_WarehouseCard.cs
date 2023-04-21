using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
    public class Rpt_InvF_WarehouseCard
    {
        public string DocNo { get; set; } // Số chứng từ

        public string InvCode { get; set; } // Kho tồn

        public string InvName { get; set; } // Tên kho tồn

        public string OrgID { get; set; } // OrgID

        public string InventoryAction { get; set; } // InventoryAction

        public string ListInvCode { get; set; } // List vị trí

        public string ListInvName { get; set; } // List tên vị trí

        public string InvCodeActual { get; set; } // InvCodeActual

        public string InventoryActionDesc { get; set; } // Mô tả

        public string ProductCodeBase { get; set; } // ProductCodeBase

        public string ProductCode { get; set; } // ProductCode

        public string mp_ProductCodeUser { get; set; } // ProductCodeUser

        public string mp_ProductName { get; set; } // ProductName

        public string mp_ProductNameEN { get; set; } // ProductNameEN

        public string DateDoc { get; set; } // DateDoc

        public string IN_TotalQty { get; set; } // IN_TotalQty

        public string CUSRETURN_TotalQty { get; set; } // CUSRETURN_TotalQty

        public string AUDITIN_TotalQty { get; set; } // AUDITIN_TotalQty

        public string MOVE_TotalQty { get; set; } // MOVE_TotalQty

        public string OUT_TotalQty { get; set; } // Out_TotalQty

        public string RETURNSUP_TotalQty { get; set; } // RETURNSUP_TotalQty

        public string AUDITOUT_TotalQty { get; set; } // AUDITOUT_TotalQty

        public string TotalQtyInv { get; set; } // AUDITOUT_TotalQty

        public string ApprDTimeUTC { get; set; } // Thời gian

        public object QtyTrans { get; set; }

        public object InvFCFInCode01 { get; set; }

        public object InvFCFInCode02 { get; set; }

        public object InvFCFInCode03 { get; set; }

        public object InvFCFInCode04 { get; set; }

        public object InvFCFInCode05 { get; set; }

        public object InvFCFInCode06 { get; set; }

        public object InvFCFInCode07 { get; set; }

        public object InvFCFInCode08 { get; set; }

        public object InvFCFInCode09 { get; set; }

        public object InvFCFInCode10 { get; set; }

        public object InvFCFOutCode01 { get; set; }

        public object InvFCFOutCode02 { get; set; }

        public object InvFCFOutCode03 { get; set; }

        public object InvFCFOutCode04 { get; set; }

        public object InvFCFOutCode05 { get; set; }

        public object InvFCFOutCode06 { get; set; }

        public object InvFCFOutCode07 { get; set; }

        public object InvFCFOutCode08 { get; set; }

        public object InvFCFOutCode09 { get; set; }

        public object InvFCFOutCode10 { get; set; }

    }
}
