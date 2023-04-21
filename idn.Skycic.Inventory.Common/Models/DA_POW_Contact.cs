using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
    public class DA_POW_Contact : EntityBase
    {
        public string ContactNo { get; set; }
        public string ContactAddress { get; set; }
        public string ContactPhoneNo { get; set; }
        public string ContactFax { get; set; }
        public string ContactEmail { get; set; }
        public string ContactMapAPI { get; set; }
        public string FlagActive { get; set; }
    }
}
