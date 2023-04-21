using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace idn.Skycic.Inventory.Web.Models
{
    public class DATA
    {
        public HEADER HEADER { get; set; }
        public BODY BODY { get; set; }

        public SECURITY SECURITY { get; set; }
    }

    public class HEADER
    {
        public string VERSION { get; set; }
        public string SENDER_CODE { get; set; }
        public string SENDER_NAME { get; set; }
        public string RECEIVER_CODE { get; set; }
        public string RECEIVER_NAME { get; set; }
        public string TRAN_CODE { get; set; }
        public string MSG_ID { get; set; }
        public string MSG_REFID { get; set; }
        public string ID_LINK { get; set; }
        public string SEND_DATE { get; set; }
        public string ORIGINAL_CODE { get; set; }
        public string ORIGINAL_NAME { get; set; }
        public string ORIGINAL_DATE { get; set; }
        public string ERROR_CODE { get; set; }
        public string ERROR_DESC { get; set; }
        public string SPARE1 { get; set; }
        public string SPARE2 { get; set; }
        public string SPARE3 { get; set; }
    }

    public class BODY
    {
        public ROW ROW { get; set; }
    }

    public class ROW
    {
        public string TYPE { get; set; }
        public string NAME { get; set; }
        public TRUYVAN TRUYVAN { get; set; }
    }

    public class TRUYVAN
    {
        public string MST { get; set; }
    }

    public class SECURITY
    {

    }
}