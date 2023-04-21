using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models.Veloca
{
    public class RQ_Ser_RO : WARQBase
    {
        public string Rt_Cols_Ser_RO { get; set; }

        public string Rt_Cols_Ser_ROReasonStop { get; set; }

        public string Rt_Cols_Ser_ROAttachFile { get; set; }

        public string Rt_Cols_Ser_ROProductService { get; set; }

        public string Rt_Cols_Ser_ROProductPart { get; set; }

        public string Rt_Cols_Ser_ROProductEngineer { get; set; }

        public string Rt_Cols_Ser_ROInvoice { get; set; }

        public Ser_RO Ser_RO { get; set; }

        public List<Ser_ROReasonStop> Lst_Ser_ROReasonStop { get; set; }

        public List<Ser_ROAttachFile> Lst_Ser_ROAttachFile { get; set; }

        public List<Ser_ROProductService> Lst_Ser_ROProductService { get; set; }

        public List<Ser_ROProductPart> Lst_Ser_ROProductPart { get; set; }

        public List<Ser_ROProductEngineer> Lst_Ser_ROProductEngineer { get; set; }

        public List<Ser_ROInvoice> Lst_Ser_ROInvoice { get; set; }
    }
}
