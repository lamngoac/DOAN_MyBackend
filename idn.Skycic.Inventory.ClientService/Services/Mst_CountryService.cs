using idn.Skycic.Inventory.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.ClientService.Services
{
    public class Mst_CountryService : ClientServiceBase<Mst_Country>
    {
        public static Mst_CountryService Instance
        {
            get
            {
                return GetInstance<Mst_CountryService>();
            }
        }

        public override string ApiControllerName
        {
            get
            {
                return "MstCountry";
            }
        }

        public RT_Mst_Country WA_Mst_Country_Get(RQ_Mst_Country objRQ_Mst_Country, string addressAPIs)
        {
            var result = PostData<RT_Mst_Country, RQ_Mst_Country>("MstCountry", "WA_Mst_Country_Get", new { }, objRQ_Mst_Country, addressAPIs);
            return result;
        }

        public RT_Mst_Country WA_Mst_Country_Create(RQ_Mst_Country objRQ_Mst_Country, string addressAPIs)
        {
            var result = PostData<RT_Mst_Country, RQ_Mst_Country>("MstCountry", "WA_Mst_Country_Create", new { }, objRQ_Mst_Country, addressAPIs);
            return result;
        }

        public RT_Mst_Country WA_Mst_Country_Update(RQ_Mst_Country objRQ_Mst_Country, string addressAPIs)
        {
            var result = PostData<RT_Mst_Country, RQ_Mst_Country>("MstCountry", "WA_Mst_Country_Update", new { }, objRQ_Mst_Country, addressAPIs);
            return result;
        }

        public RT_Mst_Country WA_Mst_Country_Delete(RQ_Mst_Country objRQ_Mst_Country, string addressAPIs)
        {
            var result = PostData<RT_Mst_Country, RQ_Mst_Country>("MstCountry", "WA_Mst_Country_Delete", new { }, objRQ_Mst_Country, addressAPIs);
            return result;
        }
    }
}
