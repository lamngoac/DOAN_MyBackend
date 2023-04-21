using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models.Veloca
{
    public class  Ser_ROAttachFile
    {
        public object RONoSys { get; set; }

        public object IndexFile { get; set; }

        public object NetworkID { get; set; }

        public object ROFileType { get; set; }

        public object ROFilePath { get; set; }

        public object ROFileName { get; set; }

        public object Remark { get; set; }

        public object LogLUDTimeUTC { get; set; }

        public object LogLUBy { get; set; }


        public object ROFilePathBase64 { get; set; }

        public object FlagUpdloadROFilePathBase64 { get; set; }
    }
}
