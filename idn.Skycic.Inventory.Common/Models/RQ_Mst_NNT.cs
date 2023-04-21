using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
    public class RQ_Mst_NNT : WARQBase
    {
        public string Rt_Cols_Mst_NNT { get; set; }

        public Mst_NNT Mst_NNT { get; set; }

		public List<MstSv_Inos_User> Lst_MstSv_Inos_User { get; set; }

		public List<MstSv_Inos_Org> Lst_MstSv_Inos_Org { get; set; }

		// InosCreateUser
		public OS_Inos_User OS_Inos_User { get; set; }

		// InosCreateOrg
		public OS_Inos_Org OS_Inos_Org { get; set; }
		public iNOS_Mst_BizType iNOS_Mst_BizType { get; set; }
		public iNOS_Mst_BizField iNOS_Mst_BizField { get; set; }

		// InosCreateOrder
		public Inos_LicOrder Inos_LicOrder { get; set; }



		//public List<MstSv_Inos_OrgUser> Lst_MstSv_Inos_OrgUser { get; set; }

		//public List<MstSv_Inos_OrgInvite> Lst_MstSv_Inos_OrgInvite { get; set; }

		public List<MstSv_Inos_Package> Lst_MstSv_Inos_Package { get; set; }

		public object FlagIsCreateOrder { get; set; }
	}
}
