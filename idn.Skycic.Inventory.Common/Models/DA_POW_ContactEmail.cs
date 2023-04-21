using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
    public class DA_POW_ContactEmail : EntityBase
    {
        public string CENo { get; set; }
        public string InformationType { get; set; }
        public string CEName { get; set; }
        public string CEEmail { get; set; }
        public string CEMobileNo { get; set; }
        public string CECompanyName { get; set; }
        public string CETouristNumber { get; set; }
        public string CEAddress { get; set; }
        public string CETitle { get; set; }
        public string CEContent { get; set; }
        public string CreateDTime { get; set; }
        public string CreateBy { get; set; }
    }
}
