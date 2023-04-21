using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
    public class Mst_NNT
    {
        public object MST { get; set; } // Mã số thuế

        public object OrgID { get; set; } // Org ID

        public object NNTFullName { get; set; } // Tên doanh nghiệp

        public object NetworkID { get; set; } // 

        public object MSTParent { get; set; } // Đơn vị trực thuộc

        public object MSTBUCode { get; set; } // 

        public object MSTBUPattern { get; set; } // 

        public object MSTLevel { get; set; } // 

        public object ProvinceCode { get; set; } // Mã tỉnh

        public object DistrictCode { get; set; } // Mã huyện

        //public object NNTType { get; set; } // Loại khách hàng

        public object DLCode { get; set; } // Mã đại lý

        public object NNTAddress { get; set; } // Địa chỉ người nộp thuế

        public object NNTMobile { get; set; } // ĐT Di động

        public object NNTPhone { get; set; } // ĐT Cố định

        public object NNTFax { get; set; } // Fax

        public object PresentBy { get; set; } // Người đại diện

        public object BusinessRegNo { get; set; } // Giấy phép KD

        public object NNTPosition { get; set; } // Chức vụ

        public object PresentIDNo { get; set; } // Số giấy tờ

        public object PresentIDType { get; set; } // Loại giấy tờ tùy thân

        public object GovTaxID { get; set; } // CQT quản lý

        public object ContactName { get; set; } // Tên người liên lạc

        public object ContactPhone { get; set; } // Điện thoại người liên hệ

        public object ContactEmail { get; set; } // Email người liên hệ

        public object Website { get; set; } // website

        public object CANumber { get; set; } // Chứng thư số

        public object CAOrg { get; set; } // Tổ chức cấp CTS

        public object CAEffDTimeUTCStart { get; set; } // Thời gian bắt đầu hiệu lực

        public object CAEffDTimeUTCEnd { get; set; } // Thời gian hết hiệu lực

        public object PackageCode { get; set; } // Mã số thuế

        public object CreatedDate { get; set; } // Ngày tạo

        public object CreateDTime { get; set; } //Thời gian tạo

        public object CreateBy { get; set; } // Người tạo

        public object AccNo { get; set; } // Số tài khoản

        public object AccHolder { get; set; } // Chủ tài khoản

        public object BankName { get; set; } // Ngân hàng

        public object BizType { get; set; } // Loại hình tổ chức

        public object BizFieldCode { get; set; } // Lĩnh vực hoạt động

        public object BizSizeCode { get; set; } // Quy mô tổ chức

        public object DealerType { get; set; } // NPP hay NCC

        public object RegisterStatus { get; set; } // Trạng thái ĐK

		public object TCTStatus { get; set; } // 

        public object FlagActive { get; set; } // 

		public object Remark { get; set; } 

		public object LogLUDTimeUTC { get; set; } // 

        public object LogLUBy { get; set; } // 

        //// //
        public object mgt_GovTaxID { get; set; } //

        public object mgt_GovTaxName { get; set; } // 
        ////

        //// //
        public object DepartmentCode { get; set; } // Mã bộ phận
        public object DepartmentName { get; set; } // Tên bộ phận

        //// //
        public object UserName { get; set; }
        public object UserPassword { get; set; }
        public object UserPasswordRepeat { get; set; }
        ////
        public object mp_ProvinceCode { get; set; } // Mã tỉnh
        public object mp_ProvinceName { get; set; } // Tên tỉnh
        ////
        public object md_DistrictCode { get; set; } // Mã huyện
        public object md_DistrictName { get; set; } // Tên huyện

		//// //
		public object QtyLicense { get; set; } // 

		public object CTSPath { get; set; } // 

		public object CTSPwd { get; set; } // 

		public object msio_OrderId { get; set; } // Mã đơn hàng

		public object ipmio_Status { get; set; } // OrderStatus
		
		public object OrgIDSln { get; set; } // OrgIDSln

        public object ma_AreaName { get; set; }

        public object AreaCode { get; set; }
        public object OrgID_MA { get; set; } // 20210923. OrgID của vùng thị trường 
    }
}
