using idn.Skycic.Inventory.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.ClientService.Services
{
    public class Mst_GovTaxIDService : ClientServiceBase<Mst_GovTaxID>
    {
        public static Mst_GovTaxIDService Instance
        {
            get
            {
                return GetInstance<Mst_GovTaxIDService>();
            }
        }

        public override string ApiControllerName
        {
            get
            {
                return "MstGovTaxID";
            }
        }

        public RT_Mst_GovTaxID WA_Mst_GovTaxID_Get(RQ_Mst_GovTaxID objRQ_Mst_GovTaxID, string addressAPIs)
        {
            var result = PostData<RT_Mst_GovTaxID, RQ_Mst_GovTaxID>("MstGovTaxID", "WA_Mst_GovTaxID_Get", new { }, objRQ_Mst_GovTaxID, addressAPIs);
            return result;
        }

        public RT_Mst_GovTaxID WA_Mst_GovTaxID_Create(RQ_Mst_GovTaxID objRQ_Mst_GovTaxID, string addressAPIs)
        {
            var result = PostData<RT_Mst_GovTaxID, RQ_Mst_GovTaxID>("MstGovTaxID", "WA_Mst_GovTaxID_Create", new { }, objRQ_Mst_GovTaxID, addressAPIs);
            return result;
        }

        public RT_Mst_GovTaxID WA_Mst_GovTaxID_Update(RQ_Mst_GovTaxID objRQ_Mst_GovTaxID, string addressAPIs)
        {
            var result = PostData<RT_Mst_GovTaxID, RQ_Mst_GovTaxID>("MstGovTaxID", "WA_Mst_GovTaxID_Update", new { }, objRQ_Mst_GovTaxID, addressAPIs);
            return result;
        }

        public RT_Mst_GovTaxID WA_Mst_GovTaxID_Delete(RQ_Mst_GovTaxID objRQ_Mst_GovTaxID, string addressAPIs)
        {
            var result = PostData<RT_Mst_GovTaxID, RQ_Mst_GovTaxID>("MstGovTaxID", "WA_Mst_GovTaxID_Delete", new { }, objRQ_Mst_GovTaxID, addressAPIs);
            return result;
        }

        public RT_Mst_GovTaxID WA_RptSv_Mst_GovTaxID_Get(RQ_Mst_GovTaxID objRQ_Mst_GovTaxID, string addressAPIs)
        {
            var result = PostData<RT_Mst_GovTaxID, RQ_Mst_GovTaxID>("MstGovTaxID", "WA_RptSv_Mst_GovTaxID_Get", new { }, objRQ_Mst_GovTaxID, addressAPIs);
            return result;
        }

        public RT_Mst_GovTaxID WA_RptSv_Mst_GovTaxID_Create(RQ_Mst_GovTaxID objRQ_Mst_GovTaxID, string addressAPIs)
        {
            var result = PostData<RT_Mst_GovTaxID, RQ_Mst_GovTaxID>("MstGovTaxID", "WA_RptSv_Mst_GovTaxID_Create", new { }, objRQ_Mst_GovTaxID, addressAPIs);
            return result;
        }

        public RT_Mst_GovTaxID WA_RptSv_Mst_GovTaxID_Update(RQ_Mst_GovTaxID objRQ_Mst_GovTaxID, string addressAPIs)
        {
            var result = PostData<RT_Mst_GovTaxID, RQ_Mst_GovTaxID>("MstGovTaxID", "WA_RptSv_Mst_GovTaxID_Update", new { }, objRQ_Mst_GovTaxID, addressAPIs);
            return result;
        }

        public RT_Mst_GovTaxID WA_RptSv_Mst_GovTaxID_Delete(RQ_Mst_GovTaxID objRQ_Mst_GovTaxID, string addressAPIs)
        {
            var result = PostData<RT_Mst_GovTaxID, RQ_Mst_GovTaxID>("MstGovTaxID", "WA_RptSv_Mst_GovTaxID_Delete", new { }, objRQ_Mst_GovTaxID, addressAPIs);
            return result;
        }
    }
}
