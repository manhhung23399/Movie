using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Movie.Core.Constants;
using Movie.Core.Entities;
using Movie.Core.FakerData;
using Movie.Core.Interfaces;
using Movie.Core.Resources.Response;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Movie.ApiIntegration.Controllers
{
    [ApiController]
    [Route(ApiRoutes.Genre.DEFAULT)]
    public class GenresController : Controller
    {
        private IUnitOfWork _unitOfWork;

        public GenresController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [HttpGet]
        [Route(ApiRoutes.FAKE)]
        public async Task<IActionResult> FakeData()
        {
            var genreFakeData = new GenreFakeData();
            await genreFakeData.FakeAsync(async x => await _unitOfWork.Genre.AddOrUpdateGenresAsync(x));
            return Ok();
        }
        [HttpPost]
        public async Task<IActionResult> AddGenre([FromBody]Genre genre)
        {
            genre.Id = Guid.NewGuid().ToString();
            await _unitOfWork.Genre.AddOrUpdateGenresAsync(genre, genre.Id);
            return Ok(ResponseBase.Success(Notify.NOTIFY_SUCCESS, (int)HttpStatusCode.Created, genre));
        }
        [HttpPut(ApiRoutes.QUERY)]
        public async Task<IActionResult> UpdateGenre([FromBody]Genre genre, [FromRoute] string id)
        {
            genre.Id = id;
            await _unitOfWork.Genre.AddOrUpdateGenresAsync(genre, id);
            return Ok(ResponseBase.Success(Notify.NOTIFY_UPDATE, (int)HttpStatusCode.OK, genre));
        }
        [HttpGet]
        public async Task<ActionResult<List<Genre>>> GetListGenres()
        {
            List<Genre> result = (List<Genre>)await _unitOfWork.Genre.GetGenresAsync();
            return Ok(ResponseBase.Success(result));
        }
        [HttpGet(ApiRoutes.QUERY)]
        public async Task<ActionResult<Genre>> GetGenre([FromRoute] string id)
        {
            var result = (Genre)await _unitOfWork.Genre.GetGenresAsync(id);
            return Ok(ResponseBase.Success(result));
        }
        [HttpDelete(ApiRoutes.QUERY)]
        public async Task<IActionResult> DeleteGenre([FromRoute] string id)
        {
            await _unitOfWork.Genre.DeleteGenresAsync(id);
            return Ok(ResponseBase.Success(Notify.NOTIFY_DELETE));
        }
    }
}
