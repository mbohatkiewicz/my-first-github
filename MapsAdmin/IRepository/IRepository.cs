using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
namespace IRepository
{
    public interface IRepository
    {
        IEnumerable<User> GetUsers();
        IEnumerable<Company> GetCompanies();

        User GetUser(int userId);
        Company GetCompany(int companyId);

        void AddUser(User user);
        void UpdateUser(int userId, User user);
        void DeleteUser(int userId);

        void AddCompany(Company company);
        void UpdateCompany(int companyId, Company company);
        void DeleteCompany(int companyId);

    }
}
