using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
    public class Inos_LicOrder
    {
        public object Id { get; set; }
        public object OrgId { get; set; }
        public object DiscountCode { get; set; }
        public object TotalCost { get; set; }
        public object PaymentCode { get; set; }
        public object PaymentStatusDesc { get; set; }
        public Inos_LicOrderStatuses Status { get; set; }
        public DateTime CreateDTime { get; set; }
        public DateTime ApproveDTime { get; set; }
        public object CreateUserId { get; set; }
        public object Remark { get; set; }
        public List<Inos_LicOrderDetail> Inos_DetailList { get; set; }
    }
}
