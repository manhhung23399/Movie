using Microsoft.AspNetCore.Mvc;
using Movie.Core.Constants;
using Movie.Core.Entities;
using Movie.Core.FakerData;
using Movie.Core.Interfaces;
using Movie.Infrastructure.GlobalExceptionResponse;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Movie.ApiIntegration.Controllers
{
    [ApiController]
    [Route(ApiRoutes.Company.DEFAULT)]
    public class CompanyController : ControllerBase
    {
        private readonly IErrorMessage _status;
        private readonly IUnitOfWork _unitOfWork;

        public CompanyController(IErrorMessage status, IUnitOfWork unitOfWork)
        {
            _status = status;
            _unitOfWork = unitOfWork;
        }
        [HttpGet]
        [Route(ApiRoutes.FAKE)]
        public async Task<IActionResult> FakeData([FromQuery]string url)
        {
            try
            {
                var companyFakeData = new CompanyFakeData(url);
                await companyFakeData.FakeDataAsync(async x 
                    => await _unitOfWork.Company.AddOrUpdateCompanyAsync(x));
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        public async Task<IActionResult> AddCompany([FromBody]Company company)
        {
            try
            {
                await _unitOfWork.Company.AddOrUpdateCompanyAsync(company);
                return Ok(Notify.NOTIFY_SUCCESS);
            }
            catch (Exception ex)
            {
                return BadRequest(_status.Error(ex.Message));
            }
        }
        [HttpPut(ApiRoutes.QUERY)]
        public async Task<IActionResult> UpdateCompany([FromBody]Company company, [FromRoute] string id)
        {
            try
            {
                await _unitOfWork.Company.AddOrUpdateCompanyAsync(company, id);
                return Ok(Notify.NOTIFY_UPDATE);
            }
            catch (Exception ex)
            {
                return BadRequest(_status.Error(ex.Message));
            }
        }
        [HttpGet]
        public async Task<ActionResult<List<Company>>> GetListCompany()
        {
            try
            {
                List<Company> result = (List<Company>)await _unitOfWork.Company.GetCompanyAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(_status.Error(ex.Message));
            }
        }
        [HttpGet(ApiRoutes.QUERY)]
        public async Task<ActionResult<Company>> GetCompany([FromRoute] string id)
        {
            try
            {
                var result = (Company)await _unitOfWork.Company.GetCompanyAsync(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(_status.Error(ex.Message));
            }
        }
        [HttpDelete(ApiRoutes.QUERY)]
        public async Task<IActionResult> DeleteCompany([FromRoute] string id)
        {
            try
            {
                await _unitOfWork.Company.DeleteCompanyAsync(id);
                return Ok(_status.Success(Notify.NOTIFY_DELETE));
            }
            catch (Exception ex)
            {
                return BadRequest(_status.Error(ex.Message));
            }
        }
    }
}
