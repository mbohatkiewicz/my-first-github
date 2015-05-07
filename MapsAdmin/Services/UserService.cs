using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;
using Services.Interfaces;

namespace Services
{
    public class UserService : IUserService
    {
        private MapsDB _mapsDB;

        public UserService()
        {
            _mapsDB = new MapsDB();
        }

        public IEnumerable<Entities.User> GetUsers()
        {
            var users = from u in _mapsDB.Users
                        select u;

            return users;
        }

        public Entities.User GetUser(int userId)
        {
            throw new NotImplementedException();
        }

        public void AddUser(Entities.User user)
        {
            throw new NotImplementedException();
        }

        public void UpdateUser(int userId, Entities.User user)
        {
            throw new NotImplementedException();
        }

        public void DeleteUser(int userId)
        {
            throw new NotImplementedException();
        }
    }
}
