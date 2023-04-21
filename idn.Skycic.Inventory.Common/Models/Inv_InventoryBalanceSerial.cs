using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
    public class Inv_InventoryBalanceSerial
    {
		public object OrgID { get; set; } // OrgID

		public string InvCode { get; set; } // Mã kho
        
        public string ProductCode { get; set; } // Mã sản phẩm
       
        public string SerialNo { get; set; } // Số serial
        
        public string NetworkID { get; set; } // NetworkID

		public string ProductLotNo { get; set; } 

        public string RefNo_Type { get; set; }
        
        public string RefNo_PK { get; set; } 
       
        public string BlockStatus { get; set; } 
        
        public string FlagNG { get; set; } 

        public string LogLUDTimeUTC { get; set; }
        
        public string LogLUBy { get; set; }

    }
}
