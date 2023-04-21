using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TJson = Newtonsoft.Json;

namespace idn.Skycic.Inventory.Common.Models
{
    public class Prd_DynamicField
    {
        public object OrgID { get; set; }
        public object NetworkID { get; set; }

        //public object Detail { get; set; }
        public string Detail
        {
            get
            {
                return TJson.JsonConvert.SerializeObject(this.ParamTypes);
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    this.ParamTypes = TJson.JsonConvert.DeserializeObject<List<PrdDynamicFieldParamType>>(value);
                }
                else
                {
                    this.ParamTypes = new List<PrdDynamicFieldParamType>();
                }
            }
        }
        public object FlagActive { get; set; }
        public object LogLUDTimeUTC { get; set; }
        public object LogLUBy { get; set; }

        public object SolutionCode { get; set; }
        public object FunctionActionType { get; set; }
        public List<PrdDynamicFieldParamType> ParamTypes { get; set; }
    }

    public enum ControlTypes
    {
        Default = 0,
        TextBox = 1,
        Select = 3,
    }

    public enum PrdDynamicFieldDataTypes
    {
        String = 1,
        Int = 2,
        Double = 3,
        Bool = 4,
        DateTime = 5,
        Date = 6,
    }

    public class PrdDynamicFieldParamType
    {
        public string Code { get; set; }
        public string Title { get; set; }
        public PrdDynamicFieldDataTypes DataType { get; set; }
        public ControlTypes ControlType { get; set; }
        public string DefaultValue { get; set; }

    }
}
