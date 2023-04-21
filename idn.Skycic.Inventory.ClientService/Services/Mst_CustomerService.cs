using idn.Skycic.Inventory.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.ClientService.Services
{
    public class Mst_CustomerService : ClientServiceBase<RT_Mst_Customer>
    {
        public static Mst_CustomerService Instance
        {
            get
            {
                return GetInstance<Mst_CustomerService>();
            }
        }
        public override string ApiControllerName
        {
            get
            {
                return "MstCustomer";
            }
        }

        public RT_Mst_Customer WA_Mst_Customer_Get(RQ_Mst_Customer objRQ_Mst_Customer, string addressAPIs)
        {
            var result = PostData<RT_Mst_Customer, RQ_Mst_Customer>(ApiControllerName, "WA_Mst_Customer_Get", new { }, objRQ_Mst_Customer, addressAPIs);
            return result;
        }

        public RT_Mst_Customer WA_Mst_Customer_Create(RQ_Mst_Customer objRQ_Mst_Customer, string addressAPIs)
        {
            var result = PostData<RT_Mst_Customer, RQ_Mst_Customer>(ApiControllerName, "WA_Mst_Customer_Create", new { }, objRQ_Mst_Customer, addressAPIs);
            return result;
        }

        public RT_Mst_Customer WA_Mst_Customer_Update(RQ_Mst_Customer objRQ_Mst_Customer, string addressAPIs)
        {
            var result = PostData<RT_Mst_Customer, RQ_Mst_Customer>(ApiControllerName, "WA_Mst_Customer_Update", new { }, objRQ_Mst_Customer, addressAPIs);
            return result;
        }

        public RT_Mst_Customer WA_Mst_Customer_Delete(RQ_Mst_Customer objRQ_Mst_Customer, string addressAPIs)
        {
            var result = PostData<RT_Mst_Customer, RQ_Mst_Customer>(ApiControllerName, "WA_Mst_Customer_Delete", new { }, objRQ_Mst_Customer, addressAPIs);
            return result;
        }

        // Hàm lưu dưới network
        public RT_Mst_Customer WA_Mst_Customer_Save(RQ_Mst_Customer objRQ_Mst_Customer, string addressAPIs)
        {
            var result = PostData<RT_Mst_Customer, RQ_Mst_Customer>(ApiControllerName, "WA_Mst_Customer_Save", new { }, objRQ_Mst_Customer, addressAPIs);
            return result;
        }
    }
}
