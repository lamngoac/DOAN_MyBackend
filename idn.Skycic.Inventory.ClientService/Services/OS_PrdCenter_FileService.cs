using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductCentrer = idn.Skycic.Inventory.Common.Models.ProductCentrer;

namespace idn.Skycic.Inventory.ClientService.Services
{
    public class OS_PrdCenter_FileService : ClientServiceBase<ProductCentrer.RQ_File>
    {
        public static OS_PrdCenter_FileService Instance
        {
            get
            {
                return GetInstance<OS_PrdCenter_FileService>();
            }
        }

        public override string ApiControllerName
        {
            get
            {
                return "File";
            }
        }

        public ProductCentrer.RT_File WA_OS_UploadFile(ProductCentrer.RQ_File objRQ_File, string addressAPIs)
        {
            var result = PostData<ProductCentrer.RT_File, ProductCentrer.RQ_File>("File", "WA_UploadFile", new { }, objRQ_File, addressAPIs);
            return result;
        }
        public ProductCentrer.RT_File WA_OS_GetFileFromPath(ProductCentrer.RQ_File objRQ_File, string addressAPIs)
        {
            var result = PostData<ProductCentrer.RT_File, ProductCentrer.RQ_File>("File", "WA_OS_GetFileFromPath", new { }, objRQ_File, addressAPIs);
            return result;
        }

        public ProductCentrer.RT_File WA_OS_MoveFile(ProductCentrer.RQ_File objRQ_File, string addressAPIs)
        {
            var result = PostData<ProductCentrer.RT_File, ProductCentrer.RQ_File>("File", "WA_MoveFile", new { }, objRQ_File, addressAPIs);
            return result;
        }

        public ProductCentrer.RT_File WA_OS_DeleteFile(ProductCentrer.RQ_File objRQ_File, string addressAPIs)
        {
            var result = PostData<ProductCentrer.RT_File, ProductCentrer.RQ_File>("File", "WA_DeleteFile", new { }, objRQ_File, addressAPIs);
            return result;
        }
    }
}
