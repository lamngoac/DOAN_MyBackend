using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
	public class RT_Map_UserInNotifyType : WARTBase
	{
		public List<Map_UserInNotifyType> Lst_Map_UserInNotifyType { get; set; }
        public List<Mst_NotifyType> Lst_Mst_NotifyType { get; set; }
        public List<Mst_ManageNotify> Lst_Mst_ManageNotify { get; set; }
    }
}
