using idn.Skycic.Inventory.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.ClientService.Services
{
    public class Mst_ProductService : ClientServiceBase<Mst_Product>
    {
        public static Mst_ProductService Instance
        {
            get
            {
                return GetInstance<Mst_ProductService>();
            }
        }
        public override string ApiControllerName
        {
            get
            {
                return "MstProduct";
            }
        }

        public RT_Mst_Product WA_Mst_Product_Get(RQ_Mst_Product objRQ_Mst_Product, string addressAPIs)
        {
            var result = PostData<RT_Mst_Product, RQ_Mst_Product>(ApiControllerName, "WA_Mst_Product_Get", new { }, objRQ_Mst_Product, addressAPIs);
            return result;
        }

        public RT_Mst_Product WA_Mst_Product_Create(RQ_Mst_Product objRQ_Mst_Product, string addressAPIs)
        {
            var result = PostData<RT_Mst_Product, RQ_Mst_Product>(ApiControllerName, "WA_Mst_Product_Create", new { }, objRQ_Mst_Product, addressAPIs);
            return result;
        }

        public RT_Mst_Product WA_Mst_Product_Update(RQ_Mst_Product objRQ_Mst_Product, string addressAPIs)
        {
            var result = PostData<RT_Mst_Product, RQ_Mst_Product>(ApiControllerName, "WA_Mst_Product_Update", new { }, objRQ_Mst_Product, addressAPIs);
            return result;
        }

        public RT_Mst_Product WA_Mst_Product_Delete(RQ_Mst_Product objRQ_Mst_Product, string addressAPIs)
        {
            var result = PostData<RT_Mst_Product, RQ_Mst_Product>(ApiControllerName, "WA_Mst_Product_Delete", new { }, objRQ_Mst_Product, addressAPIs);
            return result;
        }

        public RT_Mst_Product WA_Mst_Product_UpdateMaster(RQ_Mst_Product objRQ_Mst_Product, string addressAPIs)
        {
            var result = PostData<RT_Mst_Product, RQ_Mst_Product>(ApiControllerName, "WA_Mst_Product_UpdateMaster", new { }, objRQ_Mst_Product, addressAPIs);
            return result;
        }
        // Lưu tại net wetwork 
        public RT_Mst_Product WA_Mst_Product_Save(RQ_Mst_Product objRQ_Mst_Product, string addressAPIs)
        {
            var result = PostData<RT_Mst_Product, RQ_Mst_Product>(ApiControllerName, "WA_Mst_Product_Save", new { }, objRQ_Mst_Product, addressAPIs);
            return result;
        }
    }
}
