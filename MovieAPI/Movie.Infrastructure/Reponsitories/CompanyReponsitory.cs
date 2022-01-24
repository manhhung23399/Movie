using AutoMapper;
using Movie.Core.Constants;
using Movie.Core.Dtos;
using Movie.Core.Entities;
using Movie.Core.Interfaces;
using Movie.Infrastructure.Extensions;
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
        private readonly IMapper _mapper;
        private readonly IFileEvent _file;
        public CompanyReponsitory(IFirebaseManager manager, IMapper mapper, IFileEvent file)
        {
            _manager = manager;
            _mapper = mapper;
            _file = file;
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
                
                var company = data.ResultAs<Company>();
                if (!string.IsNullOrEmpty(company.FileName)) 
                    await _manager.Storage().Child(ArgumentEntities.Company).Child(company.FileName).DeleteAsync();
                
                await _manager.Database().DeleteAsync($"{ArgumentEntities.Company}/{companyId}");
            }
            catch (Exception ex) { throw ex; }
        }

        public async Task<Company> AddOrUpdateCompanyAsync(CompanyDto companyDto, string companyId = "")
        {
            try
            {
                string path = string.IsNullOrEmpty(companyId) ? $"{ArgumentEntities.Company}/{companyDto.Id}" : $"{ArgumentEntities.Company}/{companyId}";

                var checkData = await _manager.Database().GetAsync(ArgumentEntities.Company);

                if (checkData.Body != "null")
                {
                    var data = checkData.ResultAs<Dictionary<string, Company>>();

                    if (string.IsNullOrEmpty(companyId))
                    {
                        if (data.Values.ToList().FirstOrDefault(x => x.Name == companyDto.Name) != null)
                            throw new Exception(Notify.NOTIFY_ISVALID_COMPANY);
                    }
                }

                Company company = new Company();
                company = _mapper.Map<Company>(companyDto);

                if(!string.IsNullOrEmpty(companyId)) company.Id = companyId;
                if (companyDto.Logo != null) company.Logo = await _file.UploadStreamAsync(companyDto.Logo, ArgumentEntities.Company);

                await _manager.Database().SetAsync(path, company);
                return company;
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
