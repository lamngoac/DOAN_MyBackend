using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using idn.Skycic.Inventory.Common.Models;

namespace idn.Skycic.Inventory.ClientService.Services
{
    public class Mst_DepartmentService : ClientServiceBase<Mst_Department>
    {
        public static Mst_DepartmentService Instance
        {
            get
            {
                return GetInstance<Mst_DepartmentService>();
            }
        }

        public override string ApiControllerName
        {
            get
            {
                return "MstDepartment";
            }
        }

        public RT_Mst_Department WA_Mst_Department_Get(RQ_Mst_Department objRQ_Mst_Department, string addressAPIs)
        {
            
            var result = PostData<RT_Mst_Department, RQ_Mst_Department>("MstDepartment", "WA_Mst_Department_Get", new { }, objRQ_Mst_Department, addressAPIs);
            return result;
        }

        public RT_Mst_Department WA_Mst_Department_Create(RQ_Mst_Department objRQ_Mst_Department, string addressAPIs)
        {
            var result = PostData<RT_Mst_Department, RQ_Mst_Department>("MstDepartment", "WA_Mst_Department_Create", new { }, objRQ_Mst_Department, addressAPIs);
            return result;
        }

        public RT_Mst_Department WA_Mst_Department_Update(RQ_Mst_Department objRQ_Mst_Department, string addressAPIs)
        {
            var result = PostData<RT_Mst_Department, RQ_Mst_Department>("MstDepartment", "WA_Mst_Department_Update", new { }, objRQ_Mst_Department, addressAPIs);
            return result;
        }

        public RT_Mst_Department WA_Mst_Department_Delete(RQ_Mst_Department objRQ_Mst_Department, string addressAPIs)
        {
            var result = PostData<RT_Mst_Department, RQ_Mst_Department>("MstDepartment", "WA_Mst_Department_Delete", new { }, objRQ_Mst_Department, addressAPIs);
            return result;
        }
    }
}
