using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;

namespace Services.Interfaces
{
    interface IUserService
    {
        IEnumerable<User> GetUsers();
        
        User GetUser(int userId);
        
        void AddUser(User user);
        void UpdateUser(int userId, User user);
        void DeleteUser(int userId);
    }
}
