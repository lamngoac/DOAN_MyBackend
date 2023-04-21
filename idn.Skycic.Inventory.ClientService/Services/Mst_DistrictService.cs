using idn.Skycic.Inventory.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.ClientService.Services
{
    public class Mst_DistrictService : ClientServiceBase<Mst_District>
    {
        public static Mst_DistrictService Instance
        {
            get
            {
                return GetInstance<Mst_DistrictService>();
            }
        }

        public override string ApiControllerName
        {
            get
            {
                return "Mst_District";
            }
        }

        public RT_Mst_District WA_Mst_District_Get(RQ_Mst_District objRQ_Mst_District, string addressAPIs)
        {
            var result = PostData<RT_Mst_District, RQ_Mst_District>("MstDistrict", "WA_Mst_District_Get", new { }, objRQ_Mst_District, addressAPIs);
            return result;
        }

        public RT_Mst_District WA_Mst_District_Create(RQ_Mst_District objRQ_Mst_District, string addressAPIs)
        {
            var result = PostData<RT_Mst_District, RQ_Mst_District>("MstDistrict", "WA_Mst_District_Create", new { }, objRQ_Mst_District, addressAPIs);
            return result;
        }

        public RT_Mst_District WA_Mst_District_Update(RQ_Mst_District objRQ_Mst_District, string addressAPIs)
        {
            var result = PostData<RT_Mst_District, RQ_Mst_District>("MstDistrict", "WA_Mst_District_Update", new { }, objRQ_Mst_District, addressAPIs);
            return result;
        }

        public RT_Mst_District WA_Mst_District_Delete(RQ_Mst_District objRQ_Mst_District, string addressAPIs)
        {
            var result = PostData<RT_Mst_District, RQ_Mst_District>("MstDistrict", "WA_Mst_District_Delete", new { }, objRQ_Mst_District, addressAPIs);
            return result;
        }

        public RT_Mst_District WA_RptSv_Mst_District_Get(RQ_Mst_District objRQ_Mst_District, string addressAPIs)
        {
            var result = PostData<RT_Mst_District, RQ_Mst_District>("MstDistrict", "WA_RptSv_Mst_District_Get", new { }, objRQ_Mst_District, addressAPIs);
            return result;
        }

        public RT_Mst_District WA_RptSv_Mst_District_Create(RQ_Mst_District objRQ_Mst_District, string addressAPIs)
        {
            var result = PostData<RT_Mst_District, RQ_Mst_District>("MstDistrict", "WA_RptSv_Mst_District_Create", new { }, objRQ_Mst_District, addressAPIs);
            return result;
        }

        public RT_Mst_District WA_RptSv_Mst_District_Update(RQ_Mst_District objRQ_Mst_District, string addressAPIs)
        {
            var result = PostData<RT_Mst_District, RQ_Mst_District>("MstDistrict", "WA_RptSv_Mst_District_Update", new { }, objRQ_Mst_District, addressAPIs);
            return result;
        }

        public RT_Mst_District WA_RptSv_Mst_District_Delete(RQ_Mst_District objRQ_Mst_District, string addressAPIs)
        {
            var result = PostData<RT_Mst_District, RQ_Mst_District>("MstDistrict", "WA_RptSv_Mst_District_Delete", new { }, objRQ_Mst_District, addressAPIs);
            return result;
        }
    }
}
