using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
    public class RQ_VerificationEmail : WARQBase
    {

        //string VerificationEmail

        public bool VerifyEmailId { get; set; }
        public string Rt_Cols_VerificationEmail { get; set; }
        public VerificationEmail VerificationEmail { get; set; }
    }
}
