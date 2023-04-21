using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
	public class Mst_ProductGroup
    {
        public object OrgID { get; set; }
        public object ProductGrpCode { get; set; }
		public object NetworkID { get; set; }
		public object ProductGrpCodeParent { get; set; }
		public object ProductGrpBUCode { get; set; }
		public object ProductGrpBUPattern { get; set; }
		public object ProductGrpName { get; set; }
        public object ProductGrpDesc { get; set; }
        public object BrandCode { get; set; }
        public object ProductGrpLevel { get; set; }
        public object FlagFG { get; set; }  // 20221123: NC thêm cờ FlagFG
        public object FlagActive { get; set; }
		public object LogLUDTimeUTC { get; set; }
		public object LogLUBy { get; set; }

        public object SolutionCode { get; set; }

        public object FunctionActionType { get; set; }
    }

    public class Mst_ProductGroupUI : Mst_ProductGroup
    {
        private Mst_ProductGroupUI _parent;
        public Mst_ProductGroupUI Parent
        {
            get
            {
                return _parent ?? (_parent = new Mst_ProductGroupUI()
                {
                    ProductGrpCode = ProductGrpCodeParent
                });
            }
            set
            {
                _parent = value;
            }
        }
        public List<Mst_ProductGroupUI> Children { get; set; }
        public int HLevel
        {
            get;
            set;
        }
        public object HLevelTitle
        {
            get
            {
                if (HLevel > 0)
                {
                    var l = "";
                    for (int i = 1; i <= HLevel; i++)
                    {
                        l += "&emsp;";
                    }
                    return string.Format("{0}{1}", l, ProductGrpName);
                }
                return ProductGrpName;
            }
        }
        public object HLevelCode
        {
            get
            {
                if (HLevel > 0)
                {
                    var l = "";
                    for (var i = 1; i <= HLevel; ++i)
                    {
                        l += "&emsp;";
                    }
                    return string.Format("{0}{1}", l, ProductGrpName);

                }
                return ProductGrpName;
            }
        }

        public object CreateDateFrom { get; set; }
        public object CreateDateTo { get; set; }
    }
}
