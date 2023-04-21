using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Validation
{
    public enum ValidationErrorType
    {
        NullOrEmpty,
        Format,
        Range,
        MaxLength,
        MinLength,
        CompareNotMatch,
        DuplicateNotAllowed,
        FileFormat,
        AccessRights
    }
}
