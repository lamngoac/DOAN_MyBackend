using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TJson = Newtonsoft.Json;

namespace idn.Skycic.Inventory.Common.Models
{
	public class Mst_Customer
	{
		public object OrgID { get; set; }
		public object CustomerCodeSys { get; set; }
		public object NetworkID { get; set; }
		public object CustomerCode { get; set; }
		public object CustomerType { get; set; }
		public object CustomerGrpCode { get; set; }
		public object CustomerSourceCode { get; set; }
		public object CustomerName { get; set; }
		public object CustomerNameEN { get; set; }
		public object CustomerGender { get; set; }
		public object CustomerPhoneNo { get; set; }
		public object CustomerMobilePhone { get; set; }
		public object ProvinceCode { get; set; }
		public object DistrictCode { get; set; }
		public object WardCode { get; set; }
		public object AreaCode { get; set; }
		public object CustomerAvatarName { get; set; }
		public object CustomerAvatarSpec { get; set; }
		public object FlagCustomerAvatarPath { get; set; }
		public object CustomerAvatarPath { get; set; }
		public object CustomerAddress { get; set; }
		public object CustomerEmail { get; set; }
		public object CustomerDateOfBirth { get; set; }
		public object GovIDType { get; set; }
		public object GovIDCardNo { get; set; }
		public object GovIDCardDate { get; set; }
		public object GovIDCardPlace { get; set; }
		public object TaxCode { get; set; }
		public object BankCode { get; set; }
		public object BankName { get; set; }
		public object BankAccountNo { get; set; }
		public object RepresentName { get; set; }
		public object RepresentPosition { get; set; }
		public object UserCodeOwner { get; set; }
		public object ContactName { get; set; }
		public object ContactPhone { get; set; }
		public object ContactEmail { get; set; }
		public object Fax { get; set; }
		public object Facebook { get; set; }
		public object InvoiceCustomerName { get; set; }
		public object InvoiceCustomerAddress { get; set; }
		public object InvoiceOrgName { get; set; }
		public object InvoiceEmailSend { get; set; }
		public object MST { get; set; }
		public string ListOfCustDynamicFieldValue { get; set; }
		public object FlagDealer { get; set; }
        public object FlagShipper { get; set; }
        public object FlagSupplier { get; set; }
		public object FlagEndUser { get; set; }
		public object FlagBank { get; set; }
		public object FlagInsurrance { get; set; }
        public object DTimeUsed { get; set; }
        public object CreateDTimeUTC { get; set; }
		public object CreateBy { get; set; }
		public object LUDTimeUTC { get; set; }
		public object LUBy { get; set; }
		public object FlagActive { get; set; }
		public object Remark { get; set; }
		public object LogLUDTimeUTC { get; set; }
		public object LogLUBy { get; set; }
        public object Org_NNTFullName { get; set; }
        public object Network_NNTFullName { get; set; }
        public object mct_CustomerTypeName { get; set; }
        public object mcg_CustomerGrpName { get; set; }
        public object mcs_CustomerSourceName { get; set; }

        public object mp_ProvinceName { get; set; }

        public object md_DistrictName { get; set; }
        public object mw_WardName { get; set; }
        public object ma_AreaName { get; set; }
        public object mg_GovIDTypeName { get; set; }

        public Prd_DynamicField Setting { get; set; }

        private Dictionary<string, object> customDataDict;

        public Dictionary<string, object> CustomDataDict
        {
            get
            {
                if (customDataDict == null && !string.IsNullOrEmpty(this.ListOfCustDynamicFieldValue))
                {
                    customDataDict = TJson.JsonConvert.DeserializeObject<Dictionary<string, object>>(this.ListOfCustDynamicFieldValue);
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
                    this.ListOfCustDynamicFieldValue = TJson.JsonConvert.SerializeObject(customDataDict);
                else
                    this.ListOfCustDynamicFieldValue = null;
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
        public void UpdateListOfCustDynamicFieldValue()
        {
            this.ListOfCustDynamicFieldValue = TJson.JsonConvert.SerializeObject(this.CustomDataDict);
        }
    }
}
