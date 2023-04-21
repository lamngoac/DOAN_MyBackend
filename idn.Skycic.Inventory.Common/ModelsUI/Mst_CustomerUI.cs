using idn.Skycic.Inventory.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.ModelsUI
{
    public class Object_Common
    {
        public List<Mst_Province> Lst_Mst_Province { get; set; }
        public List<Mst_District> Lst_Mst_District { get; set; }
        public List<Mst_Ward> Lst_Mst_Ward { get; set; }
        public List<Mst_Area> Lst_Mst_Area { get; set; }
        public List<Mst_CustomerGroup> Lst_Mst_CustomerGroup { get; set; }
        public List<Mst_CustomerSource> Lst_Mst_CustomerSource { get; set; }
        public List<Mst_CustomerType> Lst_Mst_CustomerType { get; set; }
        public List<Mst_GovIDType> Lst_Mst_GovIDType { get; set; }
    }

    public class Mst_CustomerUI : Mst_Customer
    {
        public string FlagRedirect { get; set; } // 1: Reload page; 0: Redirect page
        public string FlagCustomerExsist { get; set; } // 1: đã tồn tại và Redirect sang màn hình update; 0: chưa tồn tại và ở màn hình tạo mới
    }

    public class Mst_Customer_Check
    {
        public bool Success { get; set; }
        public string Messages { get; set; }
        public string Action { get; set; } // 1: hiển thị cảnh báo; 0: gọi hàm lưu luôn
        public string CustomerCode { get; set; }
    }
}
