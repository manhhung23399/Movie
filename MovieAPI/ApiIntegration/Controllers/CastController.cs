using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Movie.Core.Constants;
using Movie.Core.Dtos;
using Movie.Core.Entities;
using Movie.Core.Interfaces;
using Movie.Core.FakerData;
using Movie.Core.Resources.Response;
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
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CastController(
            IUnitOfWork unitOfWork, 
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        [Route(ApiRoutes.FAKE)]
        public async Task<IActionResult> FakeCastData([FromQuery] string url)
        {
            var castFake = new CastFakeData();
            await castFake.FakeAsync(url, async x => await _unitOfWork.Cast.AddOrUpdateCastAsync(x));
            return Ok(ResponseBase.Success("Fake data success"));
        }

        [HttpPost]
        public async Task<IActionResult> AddCast([FromBody]CastDto castDto)
        {
            await _unitOfWork.Cast.AddOrUpdateCastAsync(castDto);
            return Ok(ResponseBase.Success(Notify.NOTIFY_SUCCESS));
        }
        [HttpPut(ApiRoutes.QUERY)]
        public async Task<IActionResult> UpdateCast([FromBody]CastDto castDto, [FromRoute] string id)
        {
            await _unitOfWork.Cast.AddOrUpdateCastAsync(castDto, id);
            return Ok(ResponseBase.Success(Notify.NOTIFY_UPDATE));
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CastResponse>>> GetListCast()
        {
            var data = (List<Cast>)await _unitOfWork.Cast.GetCastAsync();
            var casts = _mapper.Map<IEnumerable<CastResponse>>(data);
            return Ok(ResponseBase.Success(casts));
        }
        [HttpGet(ApiRoutes.QUERY)]
        public async Task<ActionResult<CastDetailResponse>> GetCast([FromRoute] string id)
        {
            Cast cast = (Cast)await _unitOfWork.Cast.GetCastAsync(id);
            CastDetailResponse result = _mapper.Map<CastDetailResponse>(cast);
            IEnumerable<MovieModel> movies = (IEnumerable<MovieModel>)await _unitOfWork.Movie.GetMovieAsync();

            var movieByCast = new List<MovieModel>();
            foreach(var movie in movies)
            {
                if(movie.Casts != null)
                {
                    if (movie.Casts.Contains(","))
                    {
                        var castByMovie = movie.Casts.Split(",").ToList();
                        if (castByMovie.Any(x => x.Equals(cast.Id)))
                        {
                            movieByCast.Add(movie);
                        }
                    }
                    else movieByCast.Add(movie);
                }
            }
            if (movieByCast.Count > 0)
            {
                IEnumerable<MovieResponse> movieMapper = _mapper.Map<IEnumerable<MovieResponse>>(movieByCast);
                result.Movies = movieMapper.ToList();
            }
            return Ok(ResponseBase.Success(result));
        }
        [HttpDelete(ApiRoutes.QUERY)]
        public async Task<IActionResult> DeleteCast([FromRoute] string id)
        {
            await _unitOfWork.Cast.DeleteCastAsync(id);
            return Ok(ResponseBase.Success(Notify.NOTIFY_DELETE));
        }
    }
}
