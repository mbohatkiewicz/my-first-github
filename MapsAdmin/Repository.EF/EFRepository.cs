using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.Interface;
using Entities;

namespace Repository.EF
{
    public class EFRepository : IRepository
    {
        private MapsDB _mapsDB;

        public EFRepository()
        {
            _mapsDB = new MapsDB();
        }
        public IEnumerable<User> GetUsers()
        {
            var users = from u in _mapsDB.Users
                        select u;

            return users;
        }

        public IEnumerable<Company> GetCompanies()
        {
            //var mapsDB = new MapsDB();
            var companies = from c in _mapsDB.Companies
                            select c;

            return companies;
        }

        public User GetUser(int userId)
        {
            throw new NotImplementedException();
        }

        public Company GetCompany(int companyId)
        {
            throw new NotImplementedException();
        }

        public void AddUser(User user)
        {
            throw new NotImplementedException();
        }

        public void UpdateUser(int userId, User user)
        {
            throw new NotImplementedException();
        }

        public void DeleteUser(int userId)
        {
            throw new NotImplementedException();
        }

        public void AddCompany(Company company)
        {
            throw new NotImplementedException();
        }

        public void UpdateCompany(int companyId, Company company)
        {
            throw new NotImplementedException();
        }

        public void DeleteCompany(int companyId)
        {
            throw new NotImplementedException();
        }
    }
}
