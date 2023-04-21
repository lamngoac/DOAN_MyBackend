using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using idn.Skycic.Inventory.Common.Models;

namespace idn.Skycic.Inventory.ClientService.Services
{
    public class OS_InosService : ClientServiceBase<OS_Inos_Package>
    {
        public static OS_InosService Instance
        {
            get
            {
                return GetInstance<OS_InosService>();
            }
        }

        public override string ApiControllerName
        {
            get
            {
                return "OSInos";
            }
        }

        public RT_OS_Inos_Package WA_OS_Inos_Package_Get(RQ_OS_Inos_Package objRQ_OS_Inos_Package, string addressAPIs)
        {
            var result = PostData<RT_OS_Inos_Package, RQ_OS_Inos_Package>("OSInos", "WA_OS_Inos_Package_Get", new { }, objRQ_OS_Inos_Package, addressAPIs);
            return result;
        }

        public RT_OS_Inos_Package WA_RptSv_OS_Inos_Package_Get(RQ_OS_Inos_Package objRQ_OS_Inos_Package, string addressAPIs)
        {
            var result = PostData<RT_OS_Inos_Package, RQ_OS_Inos_Package>("OSInos", "WA_RptSv_OS_Inos_Package_Get", new { }, objRQ_OS_Inos_Package, addressAPIs);
            return result;
        }

        //public RT_OS_Inos_Org WA_OS_Inos_Org_GetMyOrgList(RQ_OS_Inos_Org objRQ_OS_Inos_Org, string addressAPIs)
        //{
        //    var result = PostData<RT_OS_Inos_Org, RQ_OS_Inos_Org>("OSInos", "WA_OS_Inos_Org_GetMyOrgList", new { }, objRQ_OS_Inos_Org, addressAPIs);
        //    return result;
        //}

        //public RT_OS_Inos_OrgLicense WA_OS_Inos_OrgLicense(RQ_OS_Inos_OrgLicense objRQ_OS_Inos_OrgLicense, string addressAPIs)
        //{
        //    var result = PostData<RT_OS_Inos_OrgLicense, RQ_OS_Inos_OrgLicense>("OSInos", "WA_OS_Inos_OrgLicense", new { }, objRQ_OS_Inos_OrgLicense, addressAPIs);
        //    return result;
        //}

        public RT_VerificationEmail WA_OS_Inos_SendEmailVerificationEmail(RQ_VerificationEmail objRQ_VerificationEmail, string addressAPIs)
        {
            var result = PostData<RT_VerificationEmail, RQ_VerificationEmail>("OSInos", "WA_OS_Inos_SendEmailVerificationEmail", new { }, objRQ_VerificationEmail, addressAPIs);
            return result;
        }

        public RT_VerificationEmail WA_OS_Inos_VerifyEmail(RQ_VerificationEmail objRQ_VerificationEmail, string addressAPIs)
        {
            var result = PostData<RT_VerificationEmail, RQ_VerificationEmail>("OSInos", "WA_OS_Inos_VerifyEmail", new { }, objRQ_VerificationEmail, addressAPIs);
            return result;
        }

        public RT_OS_Inos_User WA_OS_Inos_User_Create(RQ_OS_Inos_User objRQ_OS_Inos_User, string addressAPIs)
        {
            var result = PostData<RT_OS_Inos_User, RQ_OS_Inos_User>("OSInos", "WA_OS_Inos_User_Create", new { }, objRQ_OS_Inos_User, addressAPIs);
            return result;
        }

        public RT_OS_Inos_Org WA_OS_Inos_Org_Create(RQ_OS_Inos_Org objRQ_OS_Inos_Org, string addressAPIs)
        {
            var result = PostData<RT_OS_Inos_Org, RQ_OS_Inos_Org>("OSInos", "WA_OS_Inos_Org_Create", new { }, objRQ_OS_Inos_Org, addressAPIs);
            return result;
        }

        #region["Network"]
        // Get mã giảm giá
        public RT_OS_Inos_LicOrder WA_OS_Inos_OrderService_GetDiscountCode(RQ_OS_Inos_LicOrder objRQ_OS_Inos_LicOrder, string addressAPIs)
        {
            var result = PostData<RT_OS_Inos_LicOrder, RQ_OS_Inos_LicOrder>("OSInos", "WA_OS_Inos_OrderService_GetDiscountCode", new { }, objRQ_OS_Inos_LicOrder, addressAPIs);
            return result;
        }


        public RT_OS_Inos_LicOrder WA_OS_Inos_OrderService_CreateOrder(RQ_OS_Inos_LicOrder objRQ_OS_Inos_LicOrder, string addressAPIs)
        {
            var result = PostData<RT_OS_Inos_LicOrder, RQ_OS_Inos_LicOrder>("OSInos", "WA_OS_Inos_OrderService_CreateOrder", new { }, objRQ_OS_Inos_LicOrder, addressAPIs);
            return result;
        }

        public RT_OS_Inos_LicOrder WA_OS_Inos_OrderService_CheckOrderStatus(RQ_OS_Inos_LicOrder objRQ_OS_Inos_LicOrder, string addressAPIs)
        {
            var result = PostData<RT_OS_Inos_LicOrder, RQ_OS_Inos_LicOrder>("OSInos", "WA_OS_Inos_OrderService_CheckOrderStatus", new { }, objRQ_OS_Inos_LicOrder, addressAPIs);
            return result;
        }
        #endregion

        #region["ReportServer"]
        // Get mã giảm giá
        public RT_OS_Inos_LicOrder WA_RptSv_OS_Inos_OrderService_GetDiscountCode(RQ_OS_Inos_LicOrder objRQ_OS_Inos_LicOrder, string addressAPIs)
        {
            var result = PostData<RT_OS_Inos_LicOrder, RQ_OS_Inos_LicOrder>("OSInos", "WA_RptSv_OS_Inos_OrderService_GetDiscountCode", new { }, objRQ_OS_Inos_LicOrder, addressAPIs);
            return result;
        }



        public RT_OS_Inos_LicOrder WA_RptSv_OS_Inos_OrderService_CreateOrder(RQ_OS_Inos_LicOrder objRQ_OS_Inos_LicOrder, string addressAPIs)
        {
            var result = PostData<RT_OS_Inos_LicOrder, RQ_OS_Inos_LicOrder>("OSInos", "WA_RptSv_OS_Inos_OrderService_CreateOrder", new { }, objRQ_OS_Inos_LicOrder, addressAPIs);
            return result;
        }

        public RT_OS_Inos_LicOrder WA_RptSv_OS_Inos_OrderService_CheckOrderStatus(RQ_OS_Inos_LicOrder objRQ_OS_Inos_LicOrder, string addressAPIs)
        {
            var result = PostData<RT_OS_Inos_LicOrder, RQ_OS_Inos_LicOrder>("OSInos", "WA_RptSv_OS_Inos_OrderService_CheckOrderStatus", new { }, objRQ_OS_Inos_LicOrder, addressAPIs);
            return result;
        }
        #endregion

    }
}
