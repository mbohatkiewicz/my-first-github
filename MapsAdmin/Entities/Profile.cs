using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    using System.ComponentModel.DataAnnotations;

    public class Profile
    {
      
        public int ProfileId { get; set; }
        public DateTime StartDate { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }

        public virtual List<Address> ProfileAddresses { get; set; }
    }
}
