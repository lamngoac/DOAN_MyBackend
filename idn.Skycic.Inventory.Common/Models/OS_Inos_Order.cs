using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
    public class OS_Inos_Order
    {
        public object Id { get; set; }
        public object OrgId { get; set; }
        public string DiscountCode { get; set; }
        public object TotalCost { get; set; }
        public string PaymentCode { get; set; }
        public string PaymentStatusDesc { get; set; }
        public Inos_LicOrderStatuses Status { get; set; }
        public DateTime CreateDTime { get; set; }
        public DateTime ApproveDTime { get; set; }
        public object CreateUserId { get; set; }
        public string Remark { get; set; }
        public List<Inos_LicOrderDetail> DetailList { get; set; }
    }
}
