using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TJson = Newtonsoft.Json;

namespace idn.Skycic.Inventory.Common.Models
{
	public class Mst_Product
	{
		public object OrgID { get; set; }
		public object ProductCode { get; set; }
		public object NetworkID { get; set; }
		public object ProductLevelSys { get; set; }
		public object ProductCodeUser { get; set; }
        public object BrandCode { get; set; }       // Mã nhãn hiệu
        public object ProductType { get; set; }
        public object ProductGrpCode { get; set; }      // Mã nhóm hàng
        public object ProductName { get; set; }
        public object ProductNameEN { get; set; }       // Tên tiếng Anh
        public object ProductBarCode { get; set; }      // Mã vạch
        public object ProductCodeNetwork { get; set; }
		public object ProductCodeBase { get; set; }
        public object ProductCodeRoot { get; set; }     // Mã hàng gốc
        public object ProductImagePathList { get; set; }
		public object ProductFilePathList { get; set; }
        public object FlagSerial { get; set; }      // QL Serial
        public object FlagLot { get; set; }         // QL LOT
        public object ValConvert { get; set; }
        public object VATRateCode { get; set; }
        public object UnitCode { get; set; }        // Mã đơn vị
        public object FlagSell { get; set; }
		public object FlagBuy { get; set; }
		public object UPBuy { get; set; }
		public object UPSell { get; set; }
        public object QtyMaxSt { get; set; }        // Tồn lớn nhất
        public object QtyMinSt { get; set; }        // Tồn nhỏ nhất
        public object QtyEffSt { get; set; }        // Tồn tối ưu
        public string ListOfPrdDynamicFieldValue { get; set; }      // List các trường động
        public object ProductStd { get; set; }      // Tiêu chuẩn
        public object ProductExpiry { get; set; }       // Hạn sử dụng
        public object ProductQuyCach { get; set; }      // Quy cách
        public object ProductMnfUrl { get; set; }
		public object ProductIntro { get; set; }
		public object ProductUserGuide { get; set; }
		public object ProductDrawing { get; set; }
		public object ProductOrigin { get; set; }       // Xuất xứ
        public object FlagFG { get; set; }
        public object CreateDTimeUTC { get; set; }
		public object CreateBy { get; set; }
		public object LUDTimeUTC { get; set; }
		public object LUBy { get; set; }
		public object FlagActive { get; set; }      // Trạng thái
        public object Remark { get; set; }
		public object LogLUDTimeUTC { get; set; }
		public object LogLUBy { get; set; }
        public object ListAttribute { get; set; }       // List các Thuộc tính
        public object ListBOM { get; set; }
        public object ProductDelType { get; set; }
        public object mb_BrandName { get; set; }
        public object mpg_ProductGrpName { get; set; }

        public object mpt_ProductType { get; set; }
        public object mpt_ProductTypeName { get; set; }
        public object mvat_VATRate { get; set; }
        public object DTimeUsed { get; set; }
        public object CustomField1 { get; set; }
        public object CustomField2 { get; set; }
        public object CustomField3 { get; set; }
        public object CustomField4 { get; set; }
        public object CustomField5 { get; set; }

        public object SolutionCode { get; set; }

		public object FunctionActionType { get; set; }

		public Prd_DynamicField Setting { get; set; }

		private Dictionary<string, object> customDataDict;

		public Dictionary<string, object> CustomDataDict
		{
			get
			{
				if (customDataDict == null && !string.IsNullOrEmpty(this.ListOfPrdDynamicFieldValue))
				{
					customDataDict = TJson.JsonConvert.DeserializeObject<Dictionary<string, object>>(this.ListOfPrdDynamicFieldValue);
				}


				if (customDataDict == null)
				{
					customDataDict = new Dictionary<string, object>();
				}
				return customDataDict;
			}

			set
			{
				this.customDataDict = value;
				if (this.customDataDict != null)
					this.ListOfPrdDynamicFieldValue = TJson.JsonConvert.SerializeObject(customDataDict);
				else
					this.ListOfPrdDynamicFieldValue = null;
			}
		}

		public object GetCustomData(string code)
		{
			if (Setting != null)
			{
				var ptype = Setting.ParamTypes.FirstOrDefault(p => p.Code.Equals(code, StringComparison.InvariantCultureIgnoreCase));
				if (ptype != null)
				{
					if (CustomDataDict.ContainsKey(code))
					{
						var val = CustomDataDict[code];

						object rval = null;

						switch (ptype.DataType)
						{
							case PrdDynamicFieldDataTypes.String:
								rval = val.ToString(); break;
							case PrdDynamicFieldDataTypes.Int:
								rval = int.Parse(val.ToString()); break;
							case PrdDynamicFieldDataTypes.Double:
								rval = double.Parse(val.ToString()); break;
							case PrdDynamicFieldDataTypes.Bool:
								rval = val != null && val.ToString().Equals("true", StringComparison.InvariantCultureIgnoreCase) ? true : false;
								break;
							case PrdDynamicFieldDataTypes.Date:
								rval = DateTime.ParseExact(val.ToString(), "yyyy-MM-dd", null); break;
							case PrdDynamicFieldDataTypes.DateTime:
								rval = DateTime.ParseExact(val.ToString(), "yyyy-MM-dd HH:mm", null); break;


						}

						return rval;
					}
				}
			}

			return null;

		}

		public object GetCustomDataString(string code)
		{
			if (Setting != null)
			{
				var ptype = Setting.ParamTypes.FirstOrDefault(p => p.Code.Equals(code, StringComparison.InvariantCultureIgnoreCase));
				if (ptype != null)
				{
					if (CustomDataDict.ContainsKey(code))
					{
						var val = CustomDataDict[code];

						return val.ToString();
					}
				}
			}

			return null;

		}

		public object SetCustomData(string code, object val)
		{
			if (Setting != null && val != null)
			{
				string sval = val.ToString();
				if (!string.IsNullOrEmpty(sval))
				{
					var ptype = Setting.ParamTypes.FirstOrDefault(p => p.Code.Equals(code, StringComparison.InvariantCultureIgnoreCase));
					if (ptype != null)
					{


						object rval = null;


						switch (ptype.DataType)
						{
							case PrdDynamicFieldDataTypes.String:
								rval = sval; break;
							case PrdDynamicFieldDataTypes.Int:
								rval = int.Parse(sval); break;
							case PrdDynamicFieldDataTypes.Double:
								rval = double.Parse(sval); break;
							case PrdDynamicFieldDataTypes.Bool:
								rval = val != null && sval.Equals("true", StringComparison.InvariantCultureIgnoreCase) ? true : false;
								break;
							case PrdDynamicFieldDataTypes.Date:
								if (val is DateTime) rval = (DateTime)val;
								else
									rval = DateTime.ParseExact(sval, "yyyy-MM-dd", null); break;
							case PrdDynamicFieldDataTypes.DateTime:
								if (val is DateTime) rval = (DateTime)val;
								else
									rval = DateTime.ParseExact(sval, "yyyy-MM-dd HH:mm", null); break;


						}

						if (rval != null)
						{

							CustomDataDict[code] = val;

							return rval;

						}
					}
				}
			}

			return null;
		}

		public void UpdateListOfPrdDynamicFieldValue()
		{
			this.ListOfPrdDynamicFieldValue = TJson.JsonConvert.SerializeObject(this.CustomDataDict);
		}
	}
}
