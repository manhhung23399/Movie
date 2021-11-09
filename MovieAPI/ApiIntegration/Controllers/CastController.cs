using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Movie.Core.Constants;
using Movie.Core.Dtos;
using Movie.Core.Entities;
using Movie.Core.Interfaces;
using Movie.Core.FakerData;
using Movie.Core.Resources.Response;
using Movie.Infrastructure.GlobalExceptionResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movie.ApiIntegration.Controllers
{
    [ApiController]
    [Route(ApiRoutes.Cast.DEFAULT)]
    public class CastController : Controller
    {
        private readonly IErrorMessage _status;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CastController(
            IErrorMessage status, 
            IUnitOfWork unitOfWork, 
            IMapper mapper)
        {
            _status = status;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        [Route(ApiRoutes.FAKE)]
        public async Task<IActionResult> FakeCastData([FromQuery] string url)
        {
            try
            {
                var castFake = new CastFakeData();
                await castFake.FakeAsync(url, async x => await _unitOfWork.Cast.AddOrUpdateCastAsync(x));
                return Ok();
            }
            catch (Exception ex) { throw ex; }
        }

        [HttpPost]
        public async Task<IActionResult> AddCast([FromBody]CastDto castDto)
        {
            try
            {
                await _unitOfWork.Cast.AddOrUpdateCastAsync(castDto);
                return Ok(_status.Success(Notify.NOTIFY_SUCCESS));
            }
            catch (Exception ex)
            {
                return BadRequest(_status.Error(ex.Message));
            }
        }
        [HttpPut(ApiRoutes.QUERY)]
        public async Task<IActionResult> UpdateCast([FromBody]CastDto castDto, [FromRoute] string id)
        {
            try
            {
                await _unitOfWork.Cast.AddOrUpdateCastAsync(castDto, id);
                return Ok(Notify.NOTIFY_UPDATE);
            }
            catch (Exception ex)
            {
                return BadRequest(_status.Error(ex.Message));
            }
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CastResponse>>> GetListCast()
        {
            try
            {
                List<Cast> data = (List<Cast>)await _unitOfWork.Cast.GetCastAsync();
                var casts = _mapper.Map<List<CastResponse>>(data);
                return Ok(casts);
            }
            catch (Exception ex)
            {
                return BadRequest(_status.Error(ex.Message));
            }
        }
        [HttpGet(ApiRoutes.QUERY)]
        public async Task<ActionResult<CastDetailResponse>> GetCast([FromRoute] string id)
        {
            try
            {
                Cast cast = (Cast)await _unitOfWork.Cast.GetCastAsync(id);
                CastDetailResponse result = _mapper.Map<CastDetailResponse>(cast);
                List<MovieModel> movies = (List<MovieModel>)await _unitOfWork.Movie.GetMovieAsync();
                List<MovieModel> movieByCast = movies.Where(mv => mv.Casts.Contains(cast.Id)).ToList();
                if (movieByCast.Count > 0)
                {
                    List<MovieResponse> movieMapper = _mapper.Map<List<MovieResponse>>(movieByCast);
                    result.Movies = movieMapper;
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(_status.Error(ex.Message));
            }
        }
        [HttpDelete(ApiRoutes.QUERY)]
        public async Task<IActionResult> DeleteCast([FromRoute] string id)
        {
            try
            {
                await _unitOfWork.Cast.DeleteCastAsync(id);
                return Ok(_status.Success(Notify.NOTIFY_DELETE));
            }
            catch (Exception ex)
            {
                return BadRequest(_status.Error(ex.Message));
            }
        }
    }
}
