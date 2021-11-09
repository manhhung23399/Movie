using Microsoft.AspNetCore.Mvc;
using Movie.Core.Dtos;
using System;
using System.Threading.Tasks;
using Movie.Infrastructure.GlobalExceptionResponse;
using Movie.Infrastructure.Reponsitories;
using Movie.Core.Interfaces;
using Movie.Core.Constants;
using System.Collections;
using Movie.Core.Entities;
using System.Collections.Generic;
using Bogus;
using Bogus.Hollywood;
using Movie.Core.Resources.Response;
using AutoMapper;
using System.Linq;
using Movie.Core.FakerData;
using Movie.Core.Utils;

namespace Movie.ApiIntegration.Controllers
{
    [ApiController]
    [Route(ApiRoutes.Movie.DEFAULT)]
    public class MovieController : ControllerBase
    {
        private readonly IErrorMessage _status;
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;

        public MovieController(IErrorMessage status, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _status = status;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        [Route(ApiRoutes.FAKE)]
        public async Task<IActionResult> FakeMovieData(
            [FromQuery]string url, [FromQuery]string apiKey)
        {
            try
            {
                var faker = new MovieFakeData();
                await faker.FakeDataAsync(url, apiKey,
                    async movie => await _unitOfWork.Movie.AddOrUpdateMovieAsync(movie));
                return Ok();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        public async Task<IActionResult> AddMovie([FromBody] MovieDto movieDtos)
        {
            try
            {
                await _unitOfWork.Movie.AddOrUpdateMovieAsync(movieDtos);
                return Ok(Notify.NOTIFY_SUCCESS);
            }
            catch (Exception ex)
            {
                return BadRequest(_status.Error(ex.Message));
            }
        }
        [HttpPut(ApiRoutes.QUERY)]
        public async Task<IActionResult> UpdateMovie([FromBody]MovieDto movieDtos, [FromRoute]string id)
        {
            try
            {
                await _unitOfWork.Movie.AddOrUpdateMovieAsync(movieDtos, id);
                return Ok(Notify.NOTIFY_UPDATE);
            }
            catch (Exception ex)
            {
                return BadRequest(_status.Error(ex.Message));
            }
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MovieResponse>>> GetListMovie()
        {
            try
            {
                IEnumerable<MovieModel> result = (IEnumerable<MovieModel>)await _unitOfWork.Movie.GetMovieAsync();
                return Ok(_mapper.Map<IEnumerable<MovieResponse>>(result));
            }
            catch (Exception ex)
            {
                return BadRequest(_status.Error(ex.Message));
            }
        }
        [HttpGet(ApiRoutes.QUERY)]
        public async Task<ActionResult<MovieDetailResponnse>> GetMovie([FromRoute]string id)
        {
            try
            {
                MovieModel result = (MovieModel)await _unitOfWork.Movie.GetMovieAsync(id);
                MovieDetailResponnse resp = new MovieDetailResponnse
                {
                    Title = result.Title,
                    Description = result.Description,
                    Poster = result.Poster
                };
                var task1 = GetListGenericAsync(result.Companies.Split(",").ToList(), resp.Companies);
                var task2 = GetListGenericAsync(result.Genres.Split(",").ToList(), resp.Genres);
                var task3 = GetListGenericAsync(result.Casts.Split(",").ToList(), resp.Casts);
                Task.WaitAll(task1, task2, task3);
                return Ok(resp);
            }
            catch (Exception ex)
            {
                return BadRequest(_status.Error(ex.Message));
            }
        }
        [HttpDelete(ApiRoutes.QUERY)]
        public async Task<IActionResult> DeleteMovie([FromRoute]string id)
        {
            try
            {
                await _unitOfWork.Movie.DeleteMovieAsync(id);
                return Ok(_status.Success(Notify.NOTIFY_DELETE));
            }
            catch(Exception ex)
            {
                return BadRequest(_status.Error(ex.Message));
            }
        }

        private async Task GetListGenericAsync(object input, object output)
        {
            if(output is List<Genre> genres)
            {
                if(input is List<string> genreIds)
                {
                    var genresData = (List<Genre>)await _unitOfWork.Genre.GetGenresAsync();
                    genreIds.ForEach(g =>
                    {
                        g = g.Replace(" ", "");
                        try
                        {
                            var item = genresData.FirstOrDefault(x => x.Id.Equals(g));
                            if(item != null) genres.Add(item);
                        }
                        catch { }
                    });
                }
            }
            if (output is List<Company> companies)
            {
                if (input is List<string> companyIds)
                {
                    var companiesData = (List<Company>)await _unitOfWork.Company.GetCompanyAsync();
                    companyIds.ForEach(c =>
                    {
                        try
                        {
                            c = c.Replace(" ", "");
                            var item = companiesData.FirstOrDefault(x => x.Id.Equals(c));
                            if(item != null) companies.Add(item);
                        }
                        catch { }
                    });
                }
            }
            if (output is List<Cast> casts)
            {
                if (input is List<string> castIds)
                {
                    var castsData = (List<Cast>)await _unitOfWork.Cast.GetCastAsync();
                    castIds.ForEach(ca =>
                    {
                        try
                        {
                            ca = ca.Replace(" ", "");
                            var item = castsData.FirstOrDefault(x => x.Id.Equals(ca));
                            if(item != null) casts.Add(item);
                        }
                        catch { }
                    });
                }
            }
        }
    }
}
