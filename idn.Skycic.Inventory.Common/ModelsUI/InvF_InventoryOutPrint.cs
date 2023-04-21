using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.ModelsUI
{
    public class InvF_InventoryOutPrint
    {
        public object NNTFullName { get; set; }
        public object NNTAddress { get; set; }
        public object NNTPhone { get; set; }
        public object IF_InvOutNo { get; set; }
        public object DatePrint { get; set; }
        public object MonthPrint { get; set; }
        public object YearPrint { get; set; }
        public object CustomerName { get; set; }
        public object OrderNo { get; set; }
        public object InvOutTypeName { get; set; }
        public object Remark { get; set; }
        public object Idx { get; set; }
        public object TotalQty { get; set; }
        public object TotalValOut { get; set; }
        public object TotalValOutDesc { get; set; }
        public object TotalValOutAfterDesc { get; set; }
        public object CreateUserCode { get; set; }
        public object CreateUserName { get; set; }
        public object LogoFilePath { get; set; }        

        public List<InvF_InventoryOutDtlUI> Lst_InvF_InventoryOutDtlUI { get; set; }
    }
}
