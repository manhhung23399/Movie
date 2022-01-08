using Movie.Core.Constants;
using Movie.Core.Entities;
using Movie.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movie.Infrastructure.Reponsitories
{
    public class CompanyReponsitory : ICompanyReponsitory
    {
        private IFirebaseManager _manager;
        public CompanyReponsitory(IFirebaseManager manager)
        {
            _manager = manager;
        }
        public async Task<object> GetCompanyAsync(string companyId = "")
        {
            try
            {
                if (string.IsNullOrEmpty(companyId))
                {
                    var data = await _manager.Database().GetAsync(ArgumentEntities.Company);
                    if (data.Body == "null") return new List<Company>();
                    var convertDataToList = data.ResultAs<Dictionary<string, Company>>();
                    return convertDataToList.Values.ToList();
                }
                else
                {
                    var data = await _manager.Database().GetAsync($"{ArgumentEntities.Company}/{companyId}");
                    if (data.Body == "null") throw new Exception(Notify.NOTIFY_VALID_COMPANY);
                    var genre = data.ResultAs<Company>();
                    return genre;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task DeleteCompanyAsync(string companyId)
        {
            try
            {
                var data = await _manager.Database().GetAsync($"{ArgumentEntities.Company}/{companyId}");
                if (data.Body == "null") throw new Exception(Notify.NOTIFY_VALID_COMPANY);
                await _manager.Database().DeleteAsync($"{ArgumentEntities.Genre}/{companyId}");
            }
            catch (Exception ex) { throw ex; }
        }

        public async Task AddOrUpdateCompanyAsync(Company company, string companyId)
        {
            try
            {
                string path = string.IsNullOrEmpty(companyId) 
                    ? $"{ArgumentEntities.Company}/{company.Id}" 
                    : $"{ArgumentEntities.Company}/{companyId}";
                var checkData = await _manager.Database().GetAsync(ArgumentEntities.Company);
                if (checkData.Body == "null") throw new Exception(Notify.NOTIFY_ERROR);
                var data = checkData.ResultAs<Dictionary<string, Company>>();
                if (data.Values.ToList().FirstOrDefault(x => x.Name == company.Name) != null)
                    throw new Exception(Notify.NOTIFY_ISVALID_COMPANY);
                await _manager.Database().SetAsync(path, company);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task AddOrUpdateCompanyAsync(Company company)
        {
            try
            {
                string path = $"{ArgumentEntities.Company}/{company.Id}";
                await _manager.Database().SetAsync(path, company);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
