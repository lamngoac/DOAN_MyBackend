using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Validation
{
    public class ValidationException : Exception
    {
        public List<ValidationError> ValidationErrors
        {
            get;
            private set;
        }

        public ValidationException(List<ValidationError> errors)
        {
            this.ValidationErrors = errors;
        }

        public ValidationException(ValidationError err)
        {
            this.ValidationErrors = new List<ValidationError>()
            {
                {err}
            };
        }
    }
}
