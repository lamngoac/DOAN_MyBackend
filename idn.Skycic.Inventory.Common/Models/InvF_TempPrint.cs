using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace idn.Skycic.Inventory.Common.Models
{
    public class InvF_TempPrint
    {
        public object IF_TempPrintNo { get; set; }

        public object NetworkID { get; set; }

        public object OrgID { get; set; }

        public object TempPrintType { get; set; }

        public object IF_TempPrintName { get; set; }

        public object LogoFilePath { get; set; }

        public object BackgroundFilePath { get; set; }

        public object TempPrintBody { get; set; }

        public object NNTName { get; set; }

        public object NNTAddress { get; set; }

        public object NNTPhone { get; set; }

        public object NNTFax { get; set; }

        public object NNTEmail { get; set; }

        public object NNTWebsite { get; set; }

        public object NNTBankName { get; set; }

        public object NNTAccNo { get; set; }

        public object Remark { get; set; }

        public object FlagActive { get; set; }

        public object LogLUDTimeUTC { get; set; }

        public object LogLUBy { get; set; }

        #region // []:

        public object LogoFilePathBase64 { get; set; }

        public object FlagUpdloadLogoFilePathBase64 { get; set; }

        public object LogoFileName { get; set; }

        public object BackgroundFilePathBase64 { get; set; }

        public object FlagUpdloadBackgroundFilePathBase64 { get; set; }

        public object BackgroundFileName { get; set; }
        #endregion 
    }
}
