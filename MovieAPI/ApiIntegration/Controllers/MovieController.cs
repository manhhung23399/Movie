using Microsoft.AspNetCore.Mvc;
using Movie.Core.Dtos;
using System.Threading.Tasks;
using Movie.Core.Interfaces;
using Movie.Core.Constants;
using Movie.Core.Entities;
using System.Collections.Generic;
using Movie.Core.Resources.Response;
using AutoMapper;
using System.Linq;
using Movie.Core.FakerData;
using Microsoft.Extensions.Caching.Memory;
using Movie.ApiIntegration.Cache;
using Movie.Core.Filters.FIlterExtensions;
using Movie.ApiIntegration.ServerContainer;
using Movie.Infrastructure.Extensions.Generics;

namespace Movie.ApiIntegration.Controllers
{
    [ApiController]
    [Route(ApiRoutes.Movie.DEFAULT)]
    public class MovieController : ControllerBase
    {
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;
        private ISetCacheMemory _memoryCache;
        private IMemoryCache _memoryCacheEntry;
        public MovieController(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            ISetCacheMemory memoryCache,
            IMemoryCache memoryCacheEntry)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _memoryCache = memoryCache;
            _memoryCacheEntry = memoryCacheEntry;
        }

        [HttpGet]
        [Route(ApiRoutes.FAKE)]
        public async Task<IActionResult> FakeMovieData(
            [FromQuery] string url, [FromQuery] string apiKey)
        {
            var faker = new MovieFakeData();
            await faker.FakeDataAsync(url, apiKey,
                async movie => await _unitOfWork.Movie.AddOrUpdateMovieAsync(movie));
            return Ok();
        }
        [HttpPost]
        public async Task<IActionResult> AddMovie([FromBody] MovieDto movieDtos)
        {
            await _unitOfWork.Movie.AddOrUpdateMovieAsync(movieDtos);
            return Ok(ResponseBase.Success(Notify.NOTIFY_SUCCESS));
        }
        [HttpPut(ApiRoutes.QUERY)]
        public async Task<IActionResult> UpdateMovie([FromBody] MovieDto movieDtos, [FromRoute] string id)
        {
            await _unitOfWork.Movie.AddOrUpdateMovieAsync(movieDtos, id);
            return Ok(ResponseBase.Success(Notify.NOTIFY_UPDATE));
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MovieResponse>>> GetMovie(
            [FromQuery]string filter,
            [FromQuery]string orderby,
            [FromQuery]int random)
        {
            List<MovieModel> result = new List<MovieModel>();
            result = (List<MovieModel>)await _memoryCache.SetCacheAsync(result, 0, _memoryCacheEntry);
            if (!string.IsNullOrEmpty(filter)) result = result.FilterData(filter);
            if (!string.IsNullOrEmpty(orderby)) result = result.OrderByExtension(orderby);
            if (random > 0)
            {
                var randomMovie = result.RandomElement(random).ToList();
                result = randomMovie;
            }

            var movies = _mapper.Map<IEnumerable<MovieResponse>>(result);
            return Ok(ResponseBase.Success(movies));
        }

        [HttpGet("{movieId}")]
        public async Task<IActionResult> GetCurrentMovie([FromRoute]string movieId)
        {
            MovieModel resultDetail = (MovieModel)await _unitOfWork.Movie.GetMovieAsync(movieId);
            MovieDetailResponnse resp = new MovieDetailBuilder()
                                                .AddWithMovieId(resultDetail.Id)
                                                .AddWithTitle(resultDetail.Title)
                                                .AddWithDescription(resultDetail.Description)
                                                .AddWithPoster(resultDetail.Poster)
                                                .AddWithStatus(resultDetail.IsRelease)
                                                .AddWithYearRelease(resultDetail.DateRelease.Year)
                                                .AddWithVote(resultDetail.VoteCount, resultDetail.Score)
                                                .Build();
            var source = resultDetail.Sources.GetLink();
            resp.Sources.Add(source);

            var taskCompany = Task.Run(async () =>
            {
                var companyComponent = new MovieComponentGenerics<Company>(_memoryCache, _memoryCacheEntry);
                var companies = await companyComponent.ConvertListAsync(resultDetail.Companies.Split(",").ToList());
                if (companies.Count > 0)
                    resp.Companies = companies.Select(x => new CompanyResponse(x)).ToList();
            });

            var taskGenres = Task.Run(async () =>
            {
                var genreComponent = new MovieComponentGenerics<Genre>(_memoryCache, _memoryCacheEntry);
                var genres = await genreComponent.ConvertListAsync(resultDetail.Genres.Split(",").ToList());
                if (genres.Count > 0)
                    resp.Genres = genres.Select(x => new GenresResponse(x)).ToList();
            });

            var taskCast = Task.Run(async () =>
            {
                var castComponent = new MovieComponentGenerics<Cast>(_memoryCache, _memoryCacheEntry);
                var casts = await castComponent.ConvertListAsync(resultDetail.Casts.Split(",").ToList());
                if (casts.Count > 0)
                    resp.Casts = casts.Select(
                        x => new CastResponse
                    {
                        Biography = x.Biography,
                        Avatar = x.Avatar,
                        Name = x.Name,
                        Id = x.Id,
                    }).ToList();
            });

            Task.WaitAll(taskCompany, taskGenres, taskCast);
            return Ok(ResponseBase.Success(resp));
        }
        [HttpDelete(ApiRoutes.QUERY)]
        public async Task<IActionResult> DeleteMovie([FromRoute]string id)
        {
            await _unitOfWork.Movie.DeleteMovieAsync(id);
            return Ok(ResponseBase.Success(Notify.NOTIFY_DELETE));
        }
    }
}
