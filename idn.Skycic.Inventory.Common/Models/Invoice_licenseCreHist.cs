
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
    public class Invoice_licenseCreHist
    {
        public object Id { get; set; }

        public object OrgID { get; set; } // mã tổ chức

        public object PackageId { get; set; } // mã package

        public object LicStatus { get; set; } // trạng thái

        public object StartDate { get; set; } // ngày hiệu lực

        public object EndDate { get; set; } // ngày hết hiệu lực
        ////
        public object osip_PackageId { get; set; }

        public object osip_PackageName { get; set; } // tên package

        public object osip_Price { get; set; } // giá

        public object osip_Subscription { get; set; } // kiểu thanh toán

        public object ModuleQtyInvoice { get; set; } //Số lượng HĐ
    }
}
