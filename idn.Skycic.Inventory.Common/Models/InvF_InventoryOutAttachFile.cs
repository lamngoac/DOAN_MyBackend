using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
    public class InvF_InventoryOutAttachFile
    {
        public object Idx { get; set; }

        public object IF_InvOutNo { get; set; }
        public object NetworkID { get; set; }

        public object AttachFilePath { get; set; }

        public object AttachFileSpec { get; set; }

        public object FlagIsFilePath { get; set; }

        public object AttachFileName { get; set; }

        public object AttachFileDesc { get; set; }

        public object FlagActive { get; set; }

        public object LogLUDTimeUTC { get; set; }

        public object LogLUBy { get; set; }
    }
}
