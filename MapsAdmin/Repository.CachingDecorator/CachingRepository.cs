using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using Repository.Interface;

namespace Repository.CachingDecorator
{
    public class CachingRepository : IRepository
    {
        private TimeSpan _cacheDuration = TimeSpan.FromSeconds(1000);

        private DateTime _dataDateTime;

        private IRepository _repository;

        private IEnumerable<User> _cachedUsers;

        private IEnumerable<Company> _cachedCompanies;

        private bool IsCacheValid
        {
            get
            {
                var _cacheAge = DateTimeOffset.Now - _dataDateTime;
                return _cacheAge < _cacheDuration;
            }
        }

        private void ValidateUsersCache()
        {
            if (_cachedUsers == null || !IsCacheValid)
            {
                try
                {
                    _cachedUsers = _repository.GetUsers();
                    _dataDateTime = DateTime.Now;
                }
                catch
                {
                    _cachedUsers = new List<User>()
                    {
                        new User()
                        {
                            UserId = 0,
                            LastName = "No Data Available"
                        }
                    };
                }
            }
        }

        private void ValidateCompaniesCache()
        {
            if (_cachedCompanies == null || !IsCacheValid)
            {
                try
                {
                    _cachedCompanies = _repository.GetCompanies();
                    _dataDateTime = DateTime.Now;
                }
                catch
                {
                    _cachedCompanies = new List<Company>()
                    {
                        new Company()
                        {
                            CompanyId = 0,
                            CompanyName = "No Data Available"
                        }
                    };
                }
            }
        }
        public IEnumerable<User> GetUsers()
        {
            this.ValidateUsersCache();
            return _cachedUsers;
        }

        public IEnumerable<Company> GetCompanies()
        {
            this.ValidateCompaniesCache();
            return _cachedCompanies;
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
