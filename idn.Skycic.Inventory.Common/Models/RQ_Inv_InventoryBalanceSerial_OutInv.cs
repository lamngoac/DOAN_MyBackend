using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
    public class RQ_Inv_InventoryBalanceSerial_OutInv : WARQBase
    {
        public string MST { get; set; }

        public string FormOutType { get; set; }

        public string InvOutType { get; set; }

        public string InvCode { get; set; }

        public string PMType { get; set; }

        public string InvFOutType { get; set; }

        public string PlateNo { get; set; }

        public string MoocNo { get; set; }

        public string DriverName { get; set; }

        public string DriverPhoneNo { get; set; }

        public string AgentCode { get; set; }

        public string CustomerName { get; set; }

        public string Remark { get; set; }

        public List<Inv_InventoryBalanceSerial> Lst_Inv_InventoryBalanceSerial { get; set; }
        public List<Inv_InventoryBox> Lst_Inv_InventoryBox { get; set; }
        public List<Inv_InventoryCarton> Lst_Inv_InventoryCarton { get; set; }
    }
}
