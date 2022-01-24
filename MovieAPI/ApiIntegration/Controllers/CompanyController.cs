using Microsoft.AspNetCore.Mvc;
using Movie.Core.Constants;
using Movie.Core.Dtos;
using Movie.Core.Entities;
using Movie.Core.FakerData;
using Movie.Core.Interfaces;
using Movie.Core.Resources.Response;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Movie.ApiIntegration.Controllers
{
    [Route(ApiRoutes.Company.DEFAULT)]
    public class CompanyController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public CompanyController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        [Route(ApiRoutes.FAKE)]
        public async Task<IActionResult> FakeData([FromQuery]string url)
        {
            var companyFakeData = new CompanyFakeData(url);
            await companyFakeData.FakeDataAsync(async x
                => await _unitOfWork.Company.AddOrUpdateCompanyAsync(x));
            return Ok(ResponseBase.Success("Fake data success")); 
        }
        [HttpPost]
        public async Task<IActionResult> AddCompany(CompanyDto company)
        {
            var companyAdded = await _unitOfWork.Company.AddOrUpdateCompanyAsync(company);
            return Ok(ResponseBase.Success(Notify.NOTIFY_SUCCESS, (int)HttpStatusCode.Created, companyAdded));
        }

        [HttpPut(ApiRoutes.QUERY)]
        public async Task<IActionResult> UpdateCompany(CompanyDto company, [FromRoute] string id)
        {
            company.Id = id;
            var companyUpdated = await _unitOfWork.Company.AddOrUpdateCompanyAsync(company, id);
            return Ok(ResponseBase.Success(Notify.NOTIFY_UPDATE, (int)HttpStatusCode.Created, companyUpdated));
        }

        [HttpGet]
        public async Task<ActionResult<List<Company>>> GetListCompany()
        {
            List<Company> result = (List<Company>)await _unitOfWork.Company.GetCompanyAsync();
            return Ok(ResponseBase.Success(result));
        }

        [HttpGet(ApiRoutes.QUERY)]
        public async Task<ActionResult<Company>> GetCompany([FromRoute] string id)
        {
            var result = (Company)await _unitOfWork.Company.GetCompanyAsync(id);
            return Ok(ResponseBase.Success(result));
        }

        [HttpDelete(ApiRoutes.QUERY)]
        public async Task<IActionResult> DeleteCompany([FromRoute] string id)
        {
            await _unitOfWork.Company.DeleteCompanyAsync(id);
            return Ok(ResponseBase.Success(Notify.NOTIFY_DELETE));
        }
    }
}
