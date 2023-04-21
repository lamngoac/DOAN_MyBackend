using idn.Skycic.Inventory.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.ClientService.Services
{
    public class Mst_ProvinceService : ClientServiceBase<Mst_Province>
    {
        public static Mst_ProvinceService Instance
        {
            get
            {
                return GetInstance<Mst_ProvinceService>();
            }
        }

        public override string ApiControllerName
        {
            get
            {
                return "MstProvince";
            }
        }

        public RT_Mst_Province WA_Mst_Province_Get(RQ_Mst_Province objRQ_Mst_Province, string addressAPIs)
        {
            var result = PostData<RT_Mst_Province, RQ_Mst_Province>("MstProvince", "WA_Mst_Province_Get", new { }, objRQ_Mst_Province, addressAPIs);
            return result;
        }

        public RT_Mst_Province WA_Mst_Province_Create(RQ_Mst_Province objRQ_Mst_Province, string addressAPIs)
        {
            var result = PostData<RT_Mst_Province, RQ_Mst_Province>("MstProvince", "WA_Mst_Province_Create", new { }, objRQ_Mst_Province, addressAPIs);
            return result;
        }

        public RT_Mst_Province WA_Mst_Province_Update(RQ_Mst_Province objRQ_Mst_Province, string addressAPIs)
        {
            var result = PostData<RT_Mst_Province, RQ_Mst_Province>("MstProvince", "WA_Mst_Province_Update", new { }, objRQ_Mst_Province, addressAPIs);
            return result;
        }

        public RT_Mst_Province WA_Mst_Province_Delete(RQ_Mst_Province objRQ_Mst_Province, string addressAPIs)
        {
            var result = PostData<RT_Mst_Province, RQ_Mst_Province>("MstProvince", "WA_Mst_Province_Delete", new { }, objRQ_Mst_Province, addressAPIs);
            return result;
        }

        // ReportServer
        public RT_Mst_Province WA_RptSv_Mst_Province_Get(RQ_Mst_Province objRQ_Mst_Province, string addressAPIs)
        {
            var result = PostData<RT_Mst_Province, RQ_Mst_Province>("MstProvince", "WA_RptSv_Mst_Province_Get", new { }, objRQ_Mst_Province, addressAPIs);
            return result;
        }

        public RT_Mst_Province WA_RptSv_Mst_Province_Create(RQ_Mst_Province objRQ_Mst_Province, string addressAPIs)
        {
            var result = PostData<RT_Mst_Province, RQ_Mst_Province>("MstProvince", "WA_RptSv_Mst_Province_Create", new { }, objRQ_Mst_Province, addressAPIs);
            return result;
        }

        public RT_Mst_Province WA_RptSv_Mst_Province_Update(RQ_Mst_Province objRQ_Mst_Province, string addressAPIs)
        {
            var result = PostData<RT_Mst_Province, RQ_Mst_Province>("MstProvince", "WA_RptSv_Mst_Province_Update", new { }, objRQ_Mst_Province, addressAPIs);
            return result;
        }

        public RT_Mst_Province WA_RptSv_Mst_Province_Delete(RQ_Mst_Province objRQ_Mst_Province, string addressAPIs)
        {
            var result = PostData<RT_Mst_Province, RQ_Mst_Province>("MstProvince", "WA_RptSv_Mst_Province_Delete", new { }, objRQ_Mst_Province, addressAPIs);
            return result;
        }
    }
}
