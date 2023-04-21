using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
    public class RQ_OS_InBrand_Inv_InventoryVerifiedID_OutInv : WARQBase
    {
        public string IF_InvOutNo { get; set; }

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

        public string OrgID_Customer { get; set; }

        public string CustomerCode { get; set; }

        public string CustomerAddress { get; set; }

        public string UserKCS { get; set; }

        public List<OS_InBrand_Inv_InventoryVerifiedID> Lst_Inv_InventoryVerifiedID { get; set; }
        public List<OS_InBrand_Inv_InventoryGenBox> Lst_Inv_InventoryGenBox { get; set; }
        public List<OS_InBrand_Inv_InventoryGenCarton> Lst_Inv_InventoryGenCarton { get; set; }
    }
}
