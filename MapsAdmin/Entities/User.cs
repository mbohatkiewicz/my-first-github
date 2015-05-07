using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class User
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public AccessLevel AccessLevelId { get; set; }
        public bool IsOwner { get; set; }

        public virtual Company Company { get; set; }
        public virtual Profile Profile { get; set; }
        
    }
}
