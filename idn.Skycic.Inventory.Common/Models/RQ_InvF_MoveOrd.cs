using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
    public class RQ_InvF_MoveOrd : WARQBase
    {
        public string Rt_Cols_InvF_MoveOrd { get; set; }
        public string Rt_Cols_InvF_MoveOrdDtl { get; set; }
        public string Rt_Cols_InvF_MoveOrdInstLot { get; set; }
        public string Rt_Cols_InvF_MoveOrdInstSerial { get; set; }
        public InvF_MoveOrd InvF_MoveOrd { get; set; }
        public List<InvF_MoveOrdDtl> Lst_InvF_MoveOrdDtl { get; set; }
        public List<InvF_MoveOrdInstLot> Lst_InvF_MoveOrdInstLot { get; set; }
        public List<InvF_MoveOrdInstSerial> Lst_InvF_MoveOrdInstSerial { get; set; }
        public object FlagIsCheckTotal { get; set; }
    }
}
