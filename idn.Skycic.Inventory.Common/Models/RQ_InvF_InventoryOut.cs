using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
    public class RQ_InvF_InventoryOut : WARQBase
    {
        public string Rt_Cols_InvF_InventoryOut { get; set; }
        public string Rt_Cols_InvF_InventoryOutCover { get; set; }
        public string Rt_Cols_InvF_InventoryOutDtl { get; set; }
        public string Rt_Cols_InvF_InventoryOutInstLot { get; set; }
        public string Rt_Cols_InvF_InventoryOutInstSerial { get; set; }
        public string Rt_Cols_InvF_InventoryOutQR { get; set; }
        public string Rt_Cols_InvF_InventoryOutAttachFile { get; set; }
        public InvF_InventoryOut InvF_InventoryOut { get; set; }
        public List<InvF_InventoryOutCover> Lst_InvF_InventoryOutCover { get; set; }
        public List<InvF_InventoryOutDtl> Lst_InvF_InventoryOutDtl { get; set; }
        public List<InvF_InventoryOutInstLot> Lst_InvF_InventoryOutInstLot { get; set; }
        public List<InvF_InventoryOutInstSerial> Lst_InvF_InventoryOutInstSerial { get; set; }
        public List<InvF_InventoryOutQR> Lst_InvF_InventoryOutQR { get; set; }
        public List<InvF_InventoryOutAttachFile> Lst_InvF_InventoryOutAttachFile { get; set; }
        public object FlagIsCheckTotal { get; set; }
    }
}
