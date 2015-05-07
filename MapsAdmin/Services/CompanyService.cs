using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;
using Services.Interfaces;

namespace Services
{
    using System.Security.Cryptography;

    public class CompanyService : ICompanyService
    {
        private MapsDB _mapsDB;

        public CompanyService()
        {
            _mapsDB = new MapsDB();
        }

        public IEnumerable<Entities.Company> GetCompanies()
        {
            //var mapsDB = new MapsDB();
            var companies = from c in _mapsDB.Companies
                            select c;

            return companies;
        }

        public Entities.Company GetCompany(int companyId)
        {
            var companies = from c in _mapsDB.Companies
                            select c;
            return companies.FirstOrDefault(c => c.CompanyId == companyId);
        }

        public void AddCompany(Entities.Company company)
        {
            throw new NotImplementedException();
        }

        public void UpdateCompany(int companyId, Entities.Company company)
        {
            throw new NotImplementedException();
        }

        public void DeleteCompany(int companyId)
        {
            throw new NotImplementedException();
        }
    }
}
