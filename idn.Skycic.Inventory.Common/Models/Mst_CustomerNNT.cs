using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
    public class Mst_CustomerNNT
    {
        public object CustomerNNTCode { get; set; } // Mã Khác hàng

        public object MST { get; set; } // Mã số thuế

        public object NetworkID { get; set; } // Mã Khác hàng

        public object AccCenterCode { get; set; } // Mã Tài khoản

        public object CustomerNNTName { get; set; } // Tên khách hàng

        public object CustomerNNTType { get; set; } // Loại khách hàng

        public object CustomerNNTAddress { get; set; } // Địa chỉ 

        public object CustomerNNTEmail { get; set; } // Email

        public object CustomerNNTPhone { get; set; } // Số điện thoại

        public object CustomerNNTFax { get; set; } // Số Fax

        public object ContactName { get; set; } // Tên người liên hệ

        public object ContactPhone { get; set; } // Số điện thoại người liên hệ

        public object ContactEmail { get; set; } // Email người liên hệ

        public object CustomerNNTDOB { get; set; } // Ngày sinh

        public object CustomerMST { get; set; } // Mã số thuế

        public object ProvinceCode { get; set; } // Mã tỉnh

        public object DistrictCode { get; set; } // Mã huyện

        public object AccNo { get; set; } // Số tài khoản

        public object BankName { get; set; } // tên ngân hàng

        public object GovIDType { get; set; } // loại giấy tờ 

        public object GovID { get; set; } // Số giấy tờ

        public object Remark { get; set; }

        public object FlagActive { get; set; }

        public object LogLUDTimeUTC { get; set; }

        public object LogLUBy { get; set; }

        //// //
        ////
        public object mp_ProvinceCode { get; set; } // Mã tỉnh
        public object mp_ProvinceName { get; set; } // Tên tỉnh
        ////
        public object md_DistrictCode { get; set; } // Mã huyện
        public object md_DistrictName { get; set; } // Tên huyện


    }
}
