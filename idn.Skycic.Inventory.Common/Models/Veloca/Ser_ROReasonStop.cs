using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models.Veloca
{
    public class Ser_ROReasonStop
    {
        public object RONoSys { get; set; }

        public object IdxReasonStop { get; set; }

        public object NetworkID { get; set; }

        public object OrgID { get; set; }

        public object ReasonStopCode { get; set; }

        public object ReasonStopName { get; set; }

        public object StopStartDTimeUTC { get; set; }

        public object StopStartBy { get; set; }

        public object StopEndDTimeUTC { get; set; }

        public object StopEndBy { get; set; }

        public object Remark { get; set; }

        public object LogLUDTimeUTC { get; set; }

        public object LogLUBy { get; set; }
        /////

        public object RONo { get; set; }

        public object EngineerNo { get; set; }

        public object IdxPrdService { get; set; }
    }
}
