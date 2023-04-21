using idn.Skycic.Inventory.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.ClientService.Services
{
    public class Mst_NNTService : ClientServiceBase<Mst_NNT>
    {
        public static Mst_NNTService Instance
        {
            get
            {
                return GetInstance<Mst_NNTService>();
            }
        }

        public override string ApiControllerName
        {
            get
            {
                return "MstNNT";
            }
        }

        public RT_Mst_NNT WA_Mst_NNT_Get(RQ_Mst_NNT objRQ_Mst_NNT, string addressAPIs)
        {
            var result = PostData<RT_Mst_NNT, RQ_Mst_NNT>("MstNNT", "WA_Mst_NNT_Get", new { }, objRQ_Mst_NNT, addressAPIs);
            return result;
        }

        public RT_Mst_NNT WA_Mst_NNT_Create(RQ_Mst_NNT objRQ_Mst_NNT, string addressAPIs)
        {
            var result = PostData<RT_Mst_NNT, RQ_Mst_NNT>("MstNNT", "WA_Mst_NNT_Create", new { }, objRQ_Mst_NNT, addressAPIs);
            return result;
        }

        public RT_Mst_NNT WA_Mst_NNT_CreateForNetwork(RQ_Mst_NNT objRQ_Mst_NNT, string addressAPIs)
        {
            var result = PostData<RT_Mst_NNT, RQ_Mst_NNT>("MstNNT", "WA_Mst_NNT_CreateForNetwork", new { }, objRQ_Mst_NNT, addressAPIs);
            return result;
        }

        public RT_Mst_NNT WA_Mst_NNT_Update(RQ_Mst_NNT objRQ_Mst_NNT, string addressAPIs)
        {
            var result = PostData<RT_Mst_NNT, RQ_Mst_NNT>("MstNNT", "WA_Mst_NNT_Update", new { }, objRQ_Mst_NNT, addressAPIs);
            return result;
        }

        public RT_Mst_NNT WA_Mst_NNT_Delete(RQ_Mst_NNT objRQ_Mst_NNT, string addressAPIs)
        {
            var result = PostData<RT_Mst_NNT, RQ_Mst_NNT>("MstNNT", "WA_Mst_NNT_Delete", new { }, objRQ_Mst_NNT, addressAPIs);
            return result;
        }

        public RT_Mst_NNT WA_Mst_NNT_CreateNNTAndDepartment(RQ_Mst_NNT objRQ_Mst_NNT, string addressAPIs)
        {
            var result = PostData<RT_Mst_NNT, RQ_Mst_NNT>("MstNNT", "WA_Mst_NNT_CreateNNTAndDepartment", new { }, objRQ_Mst_NNT, addressAPIs);
            return result;
        }

        //public RT_Mst_NNT WA_Mst_NNT_Registry(RQ_Mst_NNT objRQ_Mst_NNT, string addressAPIs)
        //{
        //    var result = PostData<RT_Mst_NNT, RQ_Mst_NNT>("MstNNT", "WA_Mst_NNT_Registry", new { }, objRQ_Mst_NNT);
        //    return result;
        //}

        //Dùng link API Report Server 
        public RT_Mst_NNT WA_Mst_NNT_Registry(RQ_Mst_NNT objRQ_Mst_NNT, string addressAPIs)
        {
            var result = PostData<RT_Mst_NNT, RQ_Mst_NNT>("MstNNT", "WA_Mst_NNT_Registry", new { }, objRQ_Mst_NNT, addressAPIs);
            return result;
        }

        //public RT_Mst_NNT WA_Mst_NNT_UpdateRegisterStatus(RQ_Mst_NNT objRQ_Mst_NNT)
        //{
        //    var result = PostData<RT_Mst_NNT, RQ_Mst_NNT>("MstNNT", "WA_Mst_NNT_UpdateRegisterStatus", new { }, objRQ_Mst_NNT);
        //    return result;
        //}

        public RT_Mst_NNT WA_Mst_NNT_UpdateRegisterStatus(RQ_Mst_NNT objRQ_Mst_NNT, string addressAPIs)
        {
            var result = PostData<RT_Mst_NNT, RQ_Mst_NNT>("MstNNT", "WA_Mst_NNT_UpdateRegisterStatus", new { }, objRQ_Mst_NNT, addressAPIs);
            return result;
        }

        public RT_Mst_NNT WA_RptSv_Mst_NNT_Get(RQ_Mst_NNT objRQ_Mst_NNT, string addressAPIs)
        {
            var result = PostData<RT_Mst_NNT, RQ_Mst_NNT>("ReportServer", "WA_RptSv_Mst_NNT_Get", new { }, objRQ_Mst_NNT, addressAPIs);
            return result;
        }

        public RT_Mst_NNT WA_RptSv_Mst_NNT_UpdateRegisterStatus(RQ_Mst_NNT objRQ_Mst_NNT, string addressAPIs)
        {
            var result = PostData<RT_Mst_NNT, RQ_Mst_NNT>("MstNNT", "WA_RptSv_Mst_NNT_UpdateRegisterStatus", new { }, objRQ_Mst_NNT, addressAPIs);
            return result;
        }

        public RT_Mst_NNT WA_RptSv_Mst_NNT_Update(RQ_Mst_NNT objRQ_Mst_NNT, string addressAPIs)
        {
            var result = PostData<RT_Mst_NNT, RQ_Mst_NNT>("MstNNT", "WA_RptSv_Mst_NNT_Update", new { }, objRQ_Mst_NNT, addressAPIs);
            return result;
        }

        public RT_Mst_NNT WA_RptSv_Mst_NNT_Delete(RQ_Mst_NNT objRQ_Mst_NNT, string addressAPIs)
        {
            var result = PostData<RT_Mst_NNT, RQ_Mst_NNT>("MstNNT", "WA_RptSv_Mst_NNT_Delete", new { }, objRQ_Mst_NNT, addressAPIs);
            return result;
        }

        // 2019-09-13: nâng cấp hàm đăng ký sử dụng dịch vụ
        //1. Tính toán thử
        public RT_Mst_NNT WA_RptSv_Mst_NNT_Calc(RQ_Mst_NNT objRQ_Mst_NNT, string addressAPIs)
        {
            var result = PostData<RT_Mst_NNT, RQ_Mst_NNT>("MstNNT", "WA_RptSv_Mst_NNT_Calc", new { }, objRQ_Mst_NNT, addressAPIs);
            return result;
        }
        public RT_Mst_NNT WA_RptSv_Mst_NNT_CalcByUserExist(RQ_Mst_NNT objRQ_Mst_NNT, string addressAPIs)
        {
            var result = PostData<RT_Mst_NNT, RQ_Mst_NNT>("MstNNT", "WA_RptSv_Mst_NNT_CalcByUserExist", new { }, objRQ_Mst_NNT, addressAPIs);
            return result;
        }
        //2. Thành công => gọi hàm Add

        public RT_Mst_NNT WA_RptSv_Mst_NNT_Add(RQ_Mst_NNT objRQ_Mst_NNT, string addressAPIs)
        {
            var result = PostData<RT_Mst_NNT, RQ_Mst_NNT>("MstNNT", "WA_RptSv_Mst_NNT_Add", new { }, objRQ_Mst_NNT, addressAPIs);
            return result;
        }
        public RT_Mst_NNT WA_RptSv_Mst_NNT_AddByUserExist(RQ_Mst_NNT objRQ_Mst_NNT, string addressAPIs)
        {
            var result = PostData<RT_Mst_NNT, RQ_Mst_NNT>("MstNNT", "WA_RptSv_Mst_NNT_AddByUserExist", new { }, objRQ_Mst_NNT, addressAPIs);
            return result;
        }
    }
}
