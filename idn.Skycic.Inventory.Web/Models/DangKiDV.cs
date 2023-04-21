using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace idn.Skycic.Inventory.Web.Models
{
    public class DangKiDV
    {
        public string maCQT { get; set; }
        public string tenCQT { get; set; }

        public string maDVu { get; set; }
        public string tenDVu { get; set; }
        public string soGPhepKDoanh { get; set; }

        public string maDKy { get; set; }
        public string mauDKy { get; set; }
        public string tenDKy { get; set; }
        public string pBanDKy { get; set; }
        public string ngayDKy { get; set; }
        public string tIN { get; set; }
        public string tenNNT { get; set; }

        public string diaDiemTB { get; set; }
        public string issuer { get; set; }
        public string subject { get; set; }
        public string serial { get; set; }
        public string email { get; set; }
        public string tel { get; set; }
        public string fromDateCA { get; set; }
        public string toDateCA { get; set; }
        public string dkyTDT { get; set; }
        public string kkTDT { get; set; }
        public string tenToChuc { get; set; }
        public string soGiayCNhan { get; set; }
    }
}