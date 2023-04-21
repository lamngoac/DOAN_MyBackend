using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
    public class Rpt_MapDeliveryOrder_ByInvFIOut
    {
        public object AreaCode { get; set; } // Mã khu vực hệ thống sinh

        public object AreaCodeUser { get; set; } // Mã khu vực 

        public object AreaName { get; set; } // Tên khu vực

        public object CustomerCodeSys { get; set; } // Mã khách hàng hệ thống sinh

        public object OrgID_CTM { get; set; } // OrgID khách hàng 

        public object CustomerCode { get; set; } // Mã khách hàng 

        public object CustomerName { get; set; } // Tên khách hàng 

        public object IF_InvOutNo { get; set; } // Số phiếu xuất 

        public object ProductCode { get; set; } // Mã sản phẩm hệ thống sinh

        public object OrgID_PD { get; set; } // OrgID sản phẩm

        public object ProductCodeUser { get; set; } // Mã sản phẩm 

        public object ProductName { get; set; } // Tên sản phẩm

        public object UnitCode { get; set; } // Đơn vị sản phẩm

        public object Qty { get; set; } // Số lượng sản phẩm trong phiếu xuất 

        public object IF_InvOutStatus { get; set; } // Trạng thái phiếu xuất

        public object ProfileStatus { get; set; } // Trạng thái hồ sơ

        public object CreateDTimeUTC { get; set; } // Thời gian tạo phiếu xuất

        public object Rtp_Date { get; set; } // Ngày 
    }
}
