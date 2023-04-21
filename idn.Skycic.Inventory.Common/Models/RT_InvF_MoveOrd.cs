using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
    public class RT_InvF_MoveOrd : WARTBase
    {
        public List<InvF_MoveOrd> Lst_InvF_MoveOrd { get; set; }

        public List<InvF_MoveOrdDtl> Lst_InvF_MoveOrdDtl { get; set; }

        public List<InvF_MoveOrdInstLot> Lst_InvF_MoveOrdInstLot { get; set; }

        public List<InvF_MoveOrdInstSerial> Lst_InvF_MoveOrdInstSerial { get; set; }
    }
}
