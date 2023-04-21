using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using idn.Skycic.Inventory.Common.Attributes;

namespace idn.Skycic.Inventory.Common.Models
{
    public class DA_Mst_Customer : EntityBase
    {
        public string CustomerCode { get; set; }
        public string CustomerTypeCode { get; set; }
        public string CustomerName { get; set; }
        public string CustomerGender { get; set; }
        public string CustomerPhoneNo { get; set; }
        public string CustomerMobileNo { get; set; }
        public string CustomerAddress { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerBOD { get; set; }
        public string CustomerAvatarPath { get; set; }
        public string CustomerUserCode { get; set; }
        public string CustomerIDCardNo { get; set; }
        public string FlagActive { get; set; }
    }
}
