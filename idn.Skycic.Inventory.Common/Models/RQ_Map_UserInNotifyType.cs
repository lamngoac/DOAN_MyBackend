using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
	public class RQ_Map_UserInNotifyType : WARQBase
	{
		public string Rt_Cols_Map_UserInNotifyType { get; set; }
        public string Rt_Cols_Rt_Cols_Mst_NotifyType { get; set; }
        public string Rt_Cols_Rt_Cols_Mst_ManageNotify { get; set; }

        public Map_UserInNotifyType Map_UserInNotifyType { get; set; }
        public List<Map_UserInNotifyType> Lst_Map_UserInNotifyType { get; set; }
    }
}
