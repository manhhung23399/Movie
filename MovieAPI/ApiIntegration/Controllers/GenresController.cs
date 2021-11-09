using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Movie.Core.Constants;
using Movie.Core.Entities;
using Movie.Core.FakerData;
using Movie.Core.Interfaces;
using Movie.Infrastructure.GlobalExceptionResponse;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Movie.ApiIntegration.Controllers
{
    [ApiController]
    [Route(ApiRoutes.Genre.DEFAULT)]
    public class GenresController : Controller
    {
        private readonly IErrorMessage _status;
        private IUnitOfWork _unitOfWork;

        public GenresController(IErrorMessage status, IUnitOfWork unitOfWork)
        {
            _status = status;
            _unitOfWork = unitOfWork;
        }
        [HttpGet]
        [Route(ApiRoutes.FAKE)]
        public async Task<IActionResult> FakeData()
        {
            try
            {
                var genreFakeData = new GenreFakeData();
                await genreFakeData.FakeAsync(async x => await _unitOfWork.Genre.AddOrUpdateGenresAsync(x));
                return Ok();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        public async Task<IActionResult> AddGenre([FromBody]Genre genre)
        {
            try
            {
                await _unitOfWork.Genre.AddOrUpdateGenresAsync(genre);
                return Ok(Notify.NOTIFY_SUCCESS);
            }
            catch (Exception ex)
            {
                return BadRequest(_status.Error(ex.Message));
            }
        }
        [HttpPut(ApiRoutes.QUERY)]
        public async Task<IActionResult> UpdateGenre([FromBody]Genre genre, [FromRoute] string id)
        {
            try
            {
                await _unitOfWork.Genre.AddOrUpdateGenresAsync(genre, id);
                return Ok(Notify.NOTIFY_UPDATE);
            }
            catch (Exception ex)
            {
                return BadRequest(_status.Error(ex.Message));
            }
        }
        [HttpGet]
        public async Task<ActionResult<List<Genre>>> GetListGenres()
        {
            try
            {
                List<Genre> result = (List<Genre>)await _unitOfWork.Genre.GetGenresAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(_status.Error(ex.Message));
            }
        }
        [HttpGet(ApiRoutes.QUERY)]
        public async Task<ActionResult<Genre>> GetGenre([FromRoute] string id)
        {
            try
            {
                var result = (Genre)await _unitOfWork.Genre.GetGenresAsync(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(_status.Error(ex.Message));
            }
        }
        [HttpDelete(ApiRoutes.QUERY)]
        public async Task<IActionResult> DeleteGenre([FromRoute] string id)
        {
            try
            {
                await _unitOfWork.Genre.DeleteGenresAsync(id);
                return Ok(_status.Success(Notify.NOTIFY_DELETE));
            }
            catch (Exception ex)
            {
                return BadRequest(_status.Error(ex.Message));
            }
        }
    }
}
