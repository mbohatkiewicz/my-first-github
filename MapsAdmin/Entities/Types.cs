using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Entities
{
    public enum LicenseType
    {
        IC,
        Master,
        PC,
        Supplier
    }

    public enum AccessLevel
    {
        Full,
        Accounting,
        Employee
    }

    public enum AddressType
    {
        Billing,
        Shipping,
        Other
    }
}
