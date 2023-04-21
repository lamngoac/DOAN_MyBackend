using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
    public class InvF_InventoryOut
    {
        public object IF_InvOutNo { get; set; } // Số phiếu xuất

        public object NetworkID { get; set; } // NetworkID

        public object OrgID { get; set; } // OrgID

        public object InvOutType { get; set; } // Loại nhập kho

        public object InvCodeOut { get; set; } // Mã kho xuất

		public object IF_InvAudNo { get; set; } // Số phiếu kiểm kê

		public object CustomerCode { get; set; } // Mã khách hàng

        public object OrderNoSys { get; set; } // Số đơn hàng hệ thống Gen

        public object OrderNo { get; set; } // Số đơn hàng

        public object OrderType { get; set; } // Loại đơn hàng

        public object RefNoSys { get; set; } // Số đơn hàng hệ thống Gen
                                             // = Số đơn hàng hệ thống sinh: Khi xuất kho qua đơn hàng từ hệ thống Skycic.DMS
                                             // = Số báo giá: Khi xuất kho từ Báo giá của Veloca
                                             // = Số phiếu xuất từ hệ thống kho ngoài (Lâm Thao):Khi Dùng cho nghiệp vụ xuất kho tích hợp với hệ thống kho khác (Lâm Thao)
        public object RefNo { get; set; } // Số đơn hàng
                                          // = Số đơn hàng hệ thống sinh: Khi xuất kho qua đơn hàng từ hệ thống Skycic.DMS
                                          // = Số báo giá: Khi xuất kho từ Báo giá của Veloca
                                          // = Số phiếu xuất từ hệ thống kho ngoài (Lâm Thao):Khi Dùng cho nghiệp vụ xuất kho tích hợp với hệ thống kho khác (Lâm Thao)
        public object RefType { get; set; } // Loại đơn hàng 
                                            // = ORDERDL/ ORDERSO/ ORDERSR: Khi xuất kho qua đơn hàng từ hệ thống Skycic.DMS
                                            // = RO : Khi xuất kho từ Báo giá của Veloca
                                            // = INVOUT : Khi Dùng cho nghiệp vụ xuất kho tích hợp với hệ thống kho khác (Lâm Thao)
        public object UseReceive { get; set; } // Người nhận hàng

        public object TotalValOut { get; set; } // Tổng tiền hàng

        public object TotalValOutDesc { get; set; } // Tổng tiền giảm

        public object TotalValOutAfterDesc { get; set; } // Tiền trả nhà cung cấp

        public object CreateDTimeUTC { get; set; } // Thời điểm tạo

        public object CreateBy { get; set; } // Người tạo

        public object LUDTimeUTC { get; set; } // Thời điểm cập nhật cuối cùng

        public object LUBy { get; set; } // Người cập nhật cuối cùng

        public object ApprDTimeUTC { get; set; } // Thời điểm duyệt

        public object ApprBy { get; set; } // Người duyệt

        public object ProfileStatus { get; set; } // 20210520. Trạng thái hồ sơ: 0-chưa nhận, 1-đã nhận
        public object IF_InvOutStatus { get; set; } // Trạng thái phiếu xuất

        public object Remark { get; set; } // Ghi chú

        public object LogLUDTimeUTC { get; set; } // Thời điểm cập nhật cuối cùng

        public object LogLUBy { get; set; } // Người cập nhật cuối cùng


        public object InvFCFOutCode01 { get; set; } //Code xuất khẩu

        public object InvFCFOutCode02 { get; set; } //Số Container

        public object InvFCFOutCode03 { get; set; } //Biển số xe

        public object InvFCFOutCode04 { get; set; }

        public object InvFCFOutCode05 { get; set; }

        public object InvFCFOutCode06 { get; set; }

        public object InvFCFOutCode07 { get; set; }

        public object InvFCFOutCode08 { get; set; }

        public object InvFCFOutCode09 { get; set; }

        public object InvFCFOutCode10 { get; set; }


        public object mct_CustomerCode { get; set; } // 
        public object mct_CustomerCodeSys { get; set; } // 

        public object mct_CustomerName { get; set; } //


        public object miot_InvOutType { get; set; } // Loại xuất kho

        public object miot_InvOutTypeName { get; set; } // Tên loại xuất kho


        public object mii_InvCode { get; set; } // Mã kho 

        public object mii_InvName { get; set; } // tên kho
        ////
        public object IF_InvOutNo_Root { get; set; } // tên kho
        public object FlagCallInBrand { get; set; } // Cờ call InBrand
        public object FlagNotify { get; set; } // 20210312. Cờ thông báo: 1-thông báo, 0-không thông báo

        public object ShippingCustomerCode { get; set; } // 20210707. Mã đơn vị vận tải

        public object ShippingAreaCode { get; set; } // 20210707. Mã vùng vận tải

        public object AreaCode { get; set; } // 20210707. Mã vùng
        public object su_UserCode_Create { get; set; } // 20230104. Mã người tạo phiếu xuất
        public object su_UserName_Create { get; set; } // 20230104. Tên người tạo phiếu xuất
    }
}
