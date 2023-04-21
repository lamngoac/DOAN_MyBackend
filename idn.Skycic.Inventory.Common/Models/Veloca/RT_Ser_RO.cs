using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models.Veloca
{
    public class RT_Ser_RO : WARTBase
    {
        public List<Ser_RO> Lst_Ser_RO { get; set; }

        public List<Ser_ROReasonStop> Lst_Ser_ROReasonStop { get; set; }

        public List<Ser_ROAttachFile> Lst_Ser_ROAttachFile { get; set; }

        public List<Ser_ROProductService> Lst_Ser_ROProductService { get; set; }

        public List<Ser_ROProductPart> Lst_Ser_ROProductPart { get; set; }

        public List<Ser_ROProductEngineer> Lst_Ser_ROProductEngineer { get; set; }

        public List<Ser_ROInvoice> Lst_Ser_ROInvoice { get; set; }
    }
}
