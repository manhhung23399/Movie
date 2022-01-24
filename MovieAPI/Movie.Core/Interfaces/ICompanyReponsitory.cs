
using Movie.Core.Dtos;
using Movie.Core.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Movie.Core.Interfaces
{
    public interface ICompanyReponsitory
    {
        Task<object> GetCompanyAsync(string companyId = "");
        Task DeleteCompanyAsync(string companyId);
        Task<Company> AddOrUpdateCompanyAsync(CompanyDto company, string companyId = "");
        Task AddOrUpdateCompanyAsync(Company company);
    }
}
