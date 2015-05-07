using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;

namespace Services.Interfaces
{
    interface ICompanyService
    {
        IEnumerable<Company> GetCompanies();

        Company GetCompany(int companyId);

        void AddCompany(Company company);
        void UpdateCompany(int companyId, Company company);
        void DeleteCompany(int companyId);
    }
}
